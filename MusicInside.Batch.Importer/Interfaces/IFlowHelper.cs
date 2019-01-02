using System.Collections.Generic;

namespace MusicInside.Batch.Importer.Interfaces
{
    interface IFlowHelper
    {
        ICollection<string> GetValidSubFolders();

        ICollection<string> GetValidFileNameInFolder(string folder);

        TagLib.Tag GetTagFromFileNameInFolder(string folder, string file);
    }
}
