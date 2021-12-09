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
            try
            {
                if (args.Length == 1)
                {
                    Console.WriteLine($"Agregaste el parametro {args[0]}");
                    Ensamblador cheang = new Ensamblador("/" + args[0]);
                    cheang.Run();
                }
                else
                {
                    Console.WriteLine("Numero Incorrecto de parametros introducido");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Constructor: " + e.Message);
                throw;
            }
            
            
            
            // Suspend the screen.  
            //Console.ReadLine();
        }
    }
}


