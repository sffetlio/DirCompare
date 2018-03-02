using System.Collections.Generic;

namespace DirCompare
{
    public class FileInfo
    {
        public FileInfo(string hash, string file)
        {
            Hash = hash;
            Paths.Add(file);
        }

        public string Hash { get; set; }
        public List<string> Paths { get; set; } = new List<string>();
    }
}