using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DirCompare
{
    public class FileRepository
    {
        public ConcurrentDictionary<string, FileInfo> Files { get; } = new ConcurrentDictionary<string, FileInfo>();
        
        public void Add(string hash, string path)
        {
            if (!Files.ContainsKey(hash))
            {
                Files[hash] = new FileInfo(hash, path);
            }
            else
            {
                Files[hash].Paths.Add(path);
            }
        }
    }
}