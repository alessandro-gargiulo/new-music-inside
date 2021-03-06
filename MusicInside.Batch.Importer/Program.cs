﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using MusicInside.Batch.Importer.Implementations;
using MusicInside.Batch.Importer.Infrastructure;
using MusicInside.Batch.Importer.Interfaces;
using System;
using System.Collections.Generic;
using TagLib;

namespace MusicInside.Batch.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preliminary Configurations
            var servicesProvider = BatchDependencyInjection.BuildContainer();
            ILogger<Program> logger = (Logger<Program>)servicesProvider.GetService(typeof(ILogger<Program>));
            #endregion

            #region Program Info
            logger.LogInformation("-------------------------------------------------");
            logger.LogInformation("--------Welcome to the Music Update Batch--------");
            logger.LogInformation("--------This batch was written only for  --------");
            logger.LogInformation("--------MusicInside WebApp database      --------");
            logger.LogInformation("--------Author: Alessandro Gargiulo      --------");
            logger.LogInformation("-------------------------------------------------");
            logger.LogInformation("----------Execution Date:{0} -------------", DateTime.Now.ToShortDateString());
            logger.LogInformation("-------------------------------------------------");
            #endregion

            try
            {
                #region Object Initialization
                IDbHelper dbHelper = (DbHelper)servicesProvider.GetService(typeof(IDbHelper));
                IFlowHelper flowHelper = (FlowHelper)servicesProvider.GetService(typeof(IFlowHelper));
                int counterFolders = 0;
                int counterFiles = 0;
                #endregion

                #region Update Process
                ICollection<string> subFolders = flowHelper.GetValidSubFolders();

                logger.LogInformation("Attempt to iterate over {0} folders...", subFolders.Count);
                foreach (var folder in subFolders)
                {
                    ICollection<string> fileNameList = flowHelper.GetValidFileNameInFolder(folder);
                    logger.LogInformation("In folder {0} found {1} files", folder, fileNameList.Count);
                    var tempCounterFiles = 0;
                    foreach(var file in fileNameList)
                    {
                        logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        logger.LogInformation("++ Attempt to process file {0} ++", file);
                        logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                        // Begin transaction
                        IDbContextTransaction transaction = dbHelper.BeginTransaction();
                        try
                        {
                            // Retrieve Tag
                            Tag fileTag = flowHelper.GetTagFromFileNameInFolder(folder, file);

                            // Attempt to search for an album with the same name and linked to the same artist
                            int albumId = dbHelper.ExistAlbum(fileTag.Album, fileTag.FirstAlbumArtist);
                            if(albumId == -1)
                            {
                                // If the linked album doesn't exist, create a new one
                                albumId = dbHelper.CreateAlbum(fileTag);
                            }
                            // Create an empty statistic entry
                            int statisticId = dbHelper.CreateEmptyStatistic();
                            // Create the media file entry
                            int mediaFileId = dbHelper.CreateMediaFile(folder, file);

                            // Create the song entry
                            int songId = dbHelper.CreateSong(fileTag, statisticId, albumId, mediaFileId);

                            // Attempt to search for an artist who composed the album of the song
                            int artistId = dbHelper.ExistArtist(fileTag.FirstAlbumArtist);
                            if(artistId == -1)
                            {
                                // If the linked artist does not exist, create a new one
                                artistId = dbHelper.CreateArtist(fileTag.FirstAlbumArtist);
                            }

                            // Link the newly created artist as the principal artist of the song
                            dbHelper.LinkArtist(artistId, true, songId);

                            var feats = flowHelper.ExtractFeaturings(file);
                            // For the other artist (featured)
                            foreach(var featArtist in feats)
                            {
                                int featArtistId = dbHelper.ExistArtist(featArtist);
                                if(featArtistId == -1)
                                {
                                    featArtistId = dbHelper.CreateArtist(featArtist);
                                }
                                dbHelper.LinkArtist(featArtistId, false, songId);
                            }

                            foreach (var genre in fileTag.Genres)
                            {
                                int genreId = dbHelper.CreateGenre(genre);
                                dbHelper.LinkGenre(genreId, songId);
                            }
                            // Everything is ok. Commit transaction
                            transaction.Commit();
                            counterFiles++;
                            tempCounterFiles++;
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("Unable to process file [{0}] due to exception {1}", file, ex.Message);
                            transaction.Rollback();
                        }
                    }
                    logger.LogInformation("--End Folder {0}: Processed {1} files--", folder, tempCounterFiles);
                    tempCounterFiles = 0;
                    counterFolders++;
                }
                #endregion
                logger.LogInformation("END ROUTINE - Processed {0} folders and {1} files", counterFolders, counterFiles);
            }
            catch (Exception ex)
            {
                logger.LogCritical("Unable to terminate process due to exception: {0} [StackTrace: {1}]", ex.Message, ex.ToString());
            }
        }
    }
}
