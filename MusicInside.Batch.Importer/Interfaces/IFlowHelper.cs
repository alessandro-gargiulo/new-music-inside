using System.Collections.Generic;

namespace MusicInside.Batch.Importer.Interfaces
{
    interface IFlowHelper
    {
        IEnumerable<string> GetValidSubFolders();

        IEnumerable<string> GetValidFileNameInFolder(string folder);

        TagLib.Tag GetTagFromFileNameInFolder(string folder, string file);
    }
}
