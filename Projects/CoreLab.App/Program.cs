using System;
using System.Linq;

namespace CoreLab.App
{
    class Program
    {
        static void Main (string[] args)
        {
            switch (args[0].ToLowerInvariant()) {
                case "hash":
                    Console.WriteLine(PasswordHash.CreateHash (args[0]));
                    return;
                case "sort":
                    Console.WriteLine(Sort.apply(Enumerable.Range(0, 10).ToArray()));
                    return;
                default:
                    return;
            }
            
        }
    }
}