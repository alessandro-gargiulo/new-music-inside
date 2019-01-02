using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using MusicInside.Batch.Importer.Implementations;
using MusicInside.Batch.Importer.Infrastructure;
using MusicInside.Batch.Importer.Interfaces;
using System;
using System.Collections.Generic;

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
            logger.LogInformation("--------Execution Time: {0} -----------", DateTime.Now.ToShortDateString());
            logger.LogInformation("-------------------------------------------------");
            #endregion

            try
            {
                #region Object Initialization
                IDbHelper dbHelper = (DbHelper)servicesProvider.GetService(typeof(IDbHelper));
                IFlowHelper flowHelper = (FlowHelper)servicesProvider.GetService(typeof(IFlowHelper));
                #endregion

                #region Update Process
                ICollection<string> subFolders = flowHelper.GetValidSubFolders();

                logger.LogInformation("Attempt to iterate over {0} folders...", subFolders.Count);
                foreach (var folder in subFolders)
                {
                    ICollection<string> fileNameList = flowHelper.GetValidFileNameInFolder(folder);
                    logger.LogInformation("In folder {0} found {1} files", folder, fileNameList.Count);
                    foreach(var file in fileNameList)
                    {
                        logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++");
                        logger.LogInformation("++ Attempt to process file {0} ++", file);
                        logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++");
                        var fileTag = flowHelper.GetTagFromFileNameInFolder(folder, file);

                        // Begin transaction
                        IDbContextTransaction transaction = dbHelper.BeginTransaction();
                        try
                        {

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("Unable to process file [{0}] due to exception {1}", file, ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.LogCritical("Unable to terminate process due to exception: {0} [StackTrace: {1}]", ex.Message, ex.ToString());
                throw;
            }
        }
    }
}
