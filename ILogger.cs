using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public interface ILogger
    {
        void LogToConsole(string message);
        void PrintList(string message, List<string> list);
        void PrintSegmentoDeDatos(List<ElementoSegmentoDeDatos> listaSegmentos);
        void IsRepeatedWord(string word, ref HashSet<string> nombresVariables);
    }
}
