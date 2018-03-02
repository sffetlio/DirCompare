using System;
using System.Threading.Tasks;

namespace DirCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO read from args
            var r1 = new Reader("../test1");
            var r2 = new Reader("../test2");
            // Console.WriteLine("waiting");
            Task.WaitAll(new Task[]{ r1.Start(), r2.Start() });
            // Console.WriteLine("end wait");
            (new Comparer()).Compare(r1, r2);
            (new Comparer()).Compare(r2, r1);
        }
    }
}
