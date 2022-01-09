using System;
using System.Linq;

namespace CBUS.Bifrost
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            var loader = new Loader("cbus-4.0-Rev-8d-Guide-6b-opcodes");
            loader.LoadResource();
            var builder = new Builder(loader);
            builder.Build();

            //Console.WriteLine(builder.Version);
            //Console.WriteLine(builder.FileCommentBlock.Text);
            //Console.WriteLine(builder.HistoryBlock.Text);
            //Console.WriteLine(builder.LicenceBlock.Text);

            //foreach(var item in builder.OpCodeBaseAbstractClassSuffixes)
            //    Console.WriteLine(item.ToString());

            //foreach(var item in builder.ResponseBlocks)
            //    Console.WriteLine(item.ToString());

            foreach (var group in builder.OpCodeBlocks.Select(n => n.Group).Distinct().OrderBy(n => n))
            {
                Console.WriteLine(group);
                foreach (var item in builder.OpCodeBlocks.Where(n => n.Group == group))
                    Console.WriteLine($"\t{item.Name}");
            }

            Console.ReadLine();
        }
    }
}
