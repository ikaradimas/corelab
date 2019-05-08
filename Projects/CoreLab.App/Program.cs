using System;

namespace CoreLab.App {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine(PasswordHash.CreateHash(args[0]));
        }
    }
}