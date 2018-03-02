using System;
using System.Linq;

namespace DirCompare
{
    public class Comparer
    {
        public void Compare(Reader reader1, Reader reader2)
        {
            // Console.WriteLine("Comparing");
            var result = reader1.Repository.Files.Where(r1 => !reader2.Repository.Files.ContainsKey(r1.Key));
            Console.WriteLine($"Results: {result.Count()}");
            foreach (var fileInfo in result)
            {
                foreach (var path in fileInfo.Value.Paths)
                {
                    Console.WriteLine($"{path} {fileInfo.Key}");
                }
            }       
        }
    }
}