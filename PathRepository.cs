using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public static class PathRepository
    {
        public static string CrearPath(string archivo)
        {
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += archivo;

            return path;
        }
    }
}
