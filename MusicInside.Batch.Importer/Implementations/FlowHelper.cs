using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicInside.Batch.Importer.Infrastructure;
using MusicInside.Batch.Importer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicInside.Batch.Importer.Implementations
{
    public class FlowHelper : IFlowHelper
    {
        private readonly ILogger<FlowHelper> _logger;
        private readonly MusicFilesOptions _options;
        private readonly string _fileMusicRoot;
        private readonly Regex _configuredFolderRegex;
        private readonly Regex _allowedExtensionRegex;

        public FlowHelper(ILogger<FlowHelper> log, IOptions<MusicFilesOptions> options)
        {
            _logger = log;
            _options = options.Value;
            _logger.LogDebug("FlowHelper | Constructor: Attempt to initialize members...");
            // Build folder regex from configuration
            _configuredFolderRegex = new Regex(_options.RegexSubFolder);
            // Build extensions regex retrieving allowed extensions
            StringBuilder buildRegexString = new StringBuilder(@"(\.");
            buildRegexString.Append(string.Join(@"$|\.", _options.AvailableExtensions)).Append("$)");
            _allowedExtensionRegex = new Regex(buildRegexString.ToString());
            _logger.LogDebug("FlowHelper | Constructor: Initialization completed.");
        }

        public ICollection<string> GetValidSubFolders()
        {
            try
            {
                // Read all directories in the music root folder
                List<string> subFolders = Directory.GetDirectories(_options.RootDirectory).Select(Path.GetFileName).ToList();
                // Keep only directory with given regex
                subFolders.RemoveAll(c => !_configuredFolderRegex.Match(c).Success);
                return subFolders;
            }
            catch (Exception ex)
            {
                _logger.LogError("FlowHelper | GetValidSubFolders: Can't read directories in /{0} due to exception [{1}]", _options.RootDirectory, ex.Message);
                return null;
            }
        }

        public ICollection<string> GetValidFileNameInFolder(string folder)
        {
            try
            {
                // Retrieve file names
                List<string> fileNameList = Directory.GetFiles(Path.Combine(_options.RootDirectory, folder)).Select(Path.GetFileName).ToList();
                // Keep only file name with valid extensions
                fileNameList.RemoveAll(f => !_allowedExtensionRegex.Match(f).Success);
                return fileNameList;
            }
            catch (Exception ex)
            {
                _logger.LogError("FlowHelper | GetValidFileNameInFolder: Can't read content of folder /{0} due to exception [{1}]", folder, ex.Message);
                return null;
            }
        }

        public TagLib.Tag GetTagFromFileNameInFolder(string folder, string file)
        {
            try
            {
                _logger.LogDebug("FlowHelper | GetTagFromFileNameInFolder: Attempt to read tags from file <{0}>", file);
                var fileTag = TagLib.File.Create(Path.Combine(_options.RootDirectory, folder, file));
                var tag = fileTag.GetTag(TagLib.TagTypes.Id3v2);
                _logger.LogDebug("FlowHelper | GetTagFromFileNameInFolder: Tags from <{0}> were correctly read", file);
                return tag;
            }
            catch (Exception ex)
            {
                _logger.LogError("FlowHelper | GetTagFromFileNameInFolder: Can't read tag of file /{0}/{1} due to exception [{2}]", folder, file, ex.Message);
                return null;
            }
        }
    }
}
