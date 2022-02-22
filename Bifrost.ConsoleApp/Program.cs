using System;
using System.Linq;

namespace CBUS.Bifrost
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Hello World!");
                var loader = new Loader("cbus-4.0-Rev-8j-Guide-6c-opcodes");

                foreach (var item in Loader.VersionNames)
                    Console.WriteLine(item);

                try
                {
                    loader.LoadResource();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }

                var builder = new Builder(loader);
                try
                {
                    builder.Build();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }

                //Console.WriteLine(builder.Version);
                //Console.WriteLine(builder.FileCommentBlock.Text);
                //Console.WriteLine(builder.HistoryBlock.Text);
                //Console.WriteLine(builder.LicenceBlock.Text);

                //foreach(var item in builder.OpCodeBaseAbstractClassSuffixes)
                //    Console.WriteLine(item.ToString());

                //foreach(var item in builder.ResponseBlocks)
                //    Console.WriteLine(item.ToString());

                //foreach (var group in builder.OpCodeBlocks.Select(n => n.Group).Distinct().OrderBy(n => n))
                //{
                //    Console.WriteLine(group);
                //    foreach (var item in builder.OpCodeBlocks.Where(n => n.Group == group))
                //        Console.WriteLine($"\t{item.Name}");
                //}

                foreach (var item in builder.NodeNumbersIndividualBlocks)
                    Console.WriteLine(item);

                foreach (var item in builder.NodeNumbersRangeBlocks)
                    Console.WriteLine(item);
            }
            finally
            {
                Console.WriteLine("Press Enter to Exit");
                Console.ReadLine();
            }
        }
    }
}
