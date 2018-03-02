using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DirCompare
{
    public class Reader
    {
        private readonly string directoryPath;

        public FileRepository Repository { get; } = new FileRepository();

        public Reader(string directoryPath){
            this.directoryPath = directoryPath;
        }

        public Task Start()
        {
            // Console.WriteLine($"start {directoryPath}");
            return Task.Run(() => ReadDirectory(directoryPath));
        }

        private void ReadDirectory(string directoryPath)
        {
            // Console.WriteLine($"start reading {directoryPath}");
            // read files in directory
            CheckFiles(directoryPath);

            Parallel.ForEach(Directory.EnumerateDirectories(directoryPath), new ParallelOptions { MaxDegreeOfParallelism = 2 }, (directory) =>
            {
                // Log($"checking {directory}");

                // recursive read sub directories
                ReadDirectory(directory);
            });

            // Console.WriteLine($"end reading {directoryPath}");
        }

        private void CheckFiles(string directoryPath)
        {
            // Console.WriteLine($"start CheckFiles {directoryPath}");
            Parallel.ForEach(Directory.EnumerateFiles(directoryPath), new ParallelOptions { MaxDegreeOfParallelism = 2 }, (file) =>
            {
                // Log(directoryPath);
                var hash = Hash(file);
                var hashStr = BitConverter.ToString(hash).Replace("-","");
                Repository.Add(hashStr, file);
                // Log($"hashing {file}: {hashStr}");
            });
            // Console.WriteLine($"end CheckFiles {directoryPath}");
        }

        private byte[] Hash(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                using(var md5 = MD5.Create())
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
        
    }
}