using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace AutomataEnsamblador
{
    class Program
    {
        static void Main(string[] args)
        {
            Ensamblador cheang = new();
            cheang.Run();
            
            // Suspend the screen.  
            Console.ReadLine();
        }
    }
}


