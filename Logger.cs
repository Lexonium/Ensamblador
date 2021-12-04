using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class Logger : ILogger
    {
        string[] magicnumber = {"I", "C", "C", "T", "S", "N"};
        public void IsRepeatedWord(string word, ref HashSet<string> nombresVariables)
        {
            if (!nombresVariables.Add(word))
            {
                //throw new CustomException("You cannot have 2 variables with the same name.");
            }
        }

        public void LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintList(string message, List<string> list)
        {
            foreach (var x in list)
            {
                Console.WriteLine(message + x);
            }
            
        }

        public void PrintSegmentoDeDatos(List<ElementoSegmentoDeDatos> listaSegmentos)
        {
            foreach(var x in listaSegmentos)
            {
                Console.Write("Nombre: " + x.VariableName + ", Direccion:" + x.Direccion + ", Tipo:" + x.VariableType + ", Elementos:" + (x.NumElementos <= 0 || x.NumElementos == null ? "N/A" : x.NumElementos));
            }
        }
        public void PrintSegmentoDeCodigo(List<ElementoSegmentoDeCodigo> listaSegmentos)
        {
            foreach (var x in listaSegmentos)
            {
                Console.Write("Nombre: " + x.CommandName + ", Size: " + x.PesoComando + ", Codigo: " + x.NumeroDeCodigo + ", DireccionVariable: " + x.DireccionVariable + ", Valor Constante: " + x.ValorConstante);
            }
        }
        public void PrintMagicNumber()
        {
            int i = 0;
            foreach (var x in magicnumber)
            {
                Console.Write(magicnumber[i]);
                i++;
            }
        }
        public void PrintCodigodeTSN(List<ElementoSegmentoDeCodigo> listaSegmentos, int contadorTSN)
        {
            int i = 0,xint;
            string losbytes;
            bool tienevar = false, tieneconst = false;
            foreach (var x in listaSegmentos)
            {
                Console.Write(x.NumeroDeCodigo);
                contadorTSN++;
                if (x.NumeroDeCodigo != 27 && x.NumeroDeCodigo != 41)
                {
                    x.PesoComando--;
                    switch (x.PesoComando)
                    {
                        case 0:
                            break;
                        case 2:
                            string[] twobytes = new string[2];
                            byte[] dosbytes = new byte[2];
                            xint = Convert.ToInt32(x.DireccionVariable);
                            for (i = 0; i < 2; i++)
                            {
                                twobytes[i] = Convert.ToString(dosbytes[i]);
                            }
                            losbytes = string.Join("", dosbytes);
                            Console.Write(losbytes);
                            contadorTSN += 2;
                            break;
                        case 4:
                            string[] fourbytes = new string[4];
                            byte[] cuatrobytes = new byte[4];
                            xint = Convert.ToInt32(x.ValorConstante);
                            for (i = 0; i < cuatrobytes.Length; i++)
                            {
                                fourbytes[i] = Convert.ToString(cuatrobytes[i]);
                            }
                            losbytes = string.Join("", fourbytes);
                            Console.Write(losbytes);
                            contadorTSN += 4;
                            break;
                        case 8:
                            string[] eightbytes = new string[8];
                            byte[] ochobytes = new byte[8];
                            xint = Convert.ToInt32(x.ValorConstante);
                            for (i = 0; i < ochobytes.Length; i++)
                            {
                                eightbytes[i] = Convert.ToString(ochobytes[i]);
                            }
                            losbytes = string.Join(" ", eightbytes);
                            Console.Write(losbytes);
                            contadorTSN += 8;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (x.DireccionVariable != null)
                    {
                        tienevar = true;
                        tieneconst = false;
                    }
                    if (x.ValorConstante != null)
                    {
                        tieneconst = true;
                        tienevar = false;
                    }
                    for (i = 1; i < x.PesoComando; i++)
                    {
                        Console.Write((i == x.PesoComando - 1 ? (tienevar == true ? x.DireccionVariable : (tieneconst == true) ? (double.TryParse(x.ValorConstante, out double num)) ? x.ValorConstante : x.ValorConstante.Length + x.ValorConstante : "") : ""));
                        contadorTSN++;
                    }
                    tieneconst = false;
                    tienevar = false;
                }
            }
        }
        public void PrintTSNV(List<ElementoSegmentoDeDatos> listaSegmentos)
        {
            int i = 0, temp;
            string[] thirtybytes = new string[30], twobytes = new string[2];
            byte[] treintabytes = new byte[30];
            byte[] dosbytes = new byte[2];
            string nombre, direccion, tipo, numelem, vs;
            Console.Write(listaSegmentos.Count);
            foreach (var x in listaSegmentos)
            {
                i = 0;
                treintabytes = Encoding.ASCII.GetBytes(x.VariableName);
                nombre = Encoding.ASCII.GetString(treintabytes);

                direccion = string.Join("-",Convert.ToString(BitConverter.GetBytes(x.Direccion)[0]));
                tipo = string.Join("-", Convert.ToString(BitConverter.GetBytes(x.VariableType)[0]));
                temp = Convert.ToInt32(x.NumElementos);
                dosbytes = BitConverter.GetBytes(temp);
                for (i = 0; i < twobytes.Length; i++) {
                    twobytes[i] = Convert.ToString(dosbytes[i]);
                }
                numelem = string.Join("-", twobytes);
                temp = Convert.ToInt32(x.VectorString);
                dosbytes = BitConverter.GetBytes(temp);
                for (i = 0; i < twobytes.Length; i++)
                {
                    twobytes[i] = Convert.ToString(dosbytes[i]);
                }
                vs = string.Join("-", twobytes);
                Console.Write(nombre + "");
                Console.Write(direccion+"");
                Console.Write(tipo + "");
                Console.Write(numelem + "");
                Console.Write(vs + "");
            }
        }

    }
}
