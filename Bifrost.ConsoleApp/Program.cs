﻿using System;

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

            foreach(var item in builder.OpCodeBaseAbstractClassSuffixes)
                Console.WriteLine(item.ToString());

            foreach(var item in builder.ResponseBlocks)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
        }
    }
}
