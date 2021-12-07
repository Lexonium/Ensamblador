using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class Logger : ILogger
    {
        string magicnumber = "ICCTSN";
        public List<byte> codigoCompleto = new List<byte>();
        public List<byte> tsnvEnBytes = new List<byte>();
        public void IsRepeatedWord(string word, ref HashSet<string> nombresVariables)
        {
            if (!nombresVariables.Add(word))
            {
                //throw new CustomException("You cannot have 2 variables with the same name.");
            }
        }

        public void LogToConsole(string message)
        {
            //Console.WriteLine(message);
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
            byte[] letrasmagic = new byte[6];
            letrasmagic = Encoding.ASCII.GetBytes(magicnumber);
            int i = 0;
            for(i=0;i<letrasmagic.Length;i++)
            {
                codigoCompleto.Add(letrasmagic[i]);
            }
        }
        public void PrintCodigodeTSN(List<ElementoSegmentoDeCodigo> listaSegmentos,ref int contadorTSN)
        {
            int i = 0,xint;
            string losbytes;
            byte[] byteinstruccion = new byte[1];
            bool tienevar = false, tieneconst = false;
            foreach (var x in listaSegmentos)
            {
                byteinstruccion = BitConverter.GetBytes(x.NumeroDeCodigo);
                codigoCompleto.Add(byteinstruccion[0]);
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
                            dosbytes = BitConverter.GetBytes(xint);
                            for (i = 0; i < 2; i++)
                            {
                                codigoCompleto.Add(dosbytes[i]);
                                //twobytes[i] = Convert.ToString(dosbytes[i]);
                            }
                            losbytes = string.Join("-", twobytes);
                            Console.Write(losbytes);
                            contadorTSN += 2;
                            break;
                        case 4:
                            string[] fourbytes = new string[4];
                            byte[] cuatrobytes = new byte[4];
                            xint = Convert.ToInt32(x.ValorConstante);
                            cuatrobytes = BitConverter.GetBytes(xint);
                            for (i = 0; i < cuatrobytes.Length; i++)
                            {
                                codigoCompleto.Add(cuatrobytes[i]);
                                //fourbytes[i] = Convert.ToString(cuatrobytes[i]);
                            }
                           // losbytes = string.Join("-", fourbytes);
                            //Console.Write(losbytes);
                            contadorTSN += 4;
                            break;
                        case 8:
                            string[] eightbytes = new string[8];
                            byte[] ochobytes = new byte[8];
                            xint = Convert.ToInt32(x.ValorConstante);
                            ochobytes = BitConverter.GetBytes(xint);
                            for (i = 0; i < ochobytes.Length; i++)
                            {
                                codigoCompleto.Add(ochobytes[i]);
                                //eightbytes[i] = Convert.ToString(ochobytes[i]);
                            }
                            //losbytes = string.Join("-", eightbytes);
                            //Console.Write(losbytes);
                            contadorTSN += 8;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //if (x.DireccionVariable != null)
                    //{
                    //    tienevar = true;
                    //    tieneconst = false;
                    //}
                    //if (x.ValorConstante != null)
                    //{
                    //    tieneconst = true;
                    //    tienevar = false;
                    //}
                    //for (i = 1; i < x.PesoComando; i++)
                    //{
                    //    Console.Write((i == x.PesoComando - 1 ? (tienevar == true ? x.DireccionVariable : (tieneconst == true) ? (double.TryParse(x.ValorConstante, out double num)) ? x.ValorConstante : x.ValorConstante.Length + x.ValorConstante : "") : ""));
                    //    contadorTSN++;
                    //}
                    //tieneconst = false;
                    //tienevar = false;
                    byte[] mensaje= Encoding.ASCII.GetBytes(x.ValorConstante);
                    for (i = 0; i < mensaje.Length; i++)
                    {
                        codigoCompleto.Add(mensaje[i]);
                        contadorTSN++;
                    }
                }
            }
        }
        public void cambiarA2bytes(int numero)//, ref int contadorTSN) {
        { 
            int i;
            string losbytes;
            string[] twobytes = new string[2];
            byte[] dosbytes = new byte[2];
            dosbytes = BitConverter.GetBytes(numero);
            for (i = 0; i < 2; i++)
            {
                codigoCompleto.Add(dosbytes[i]);
                //twobytes[i] = Convert.ToString(dosbytes[i]);
            }
            //losbytes = string.Join("-", twobytes);
            //Console.Write(losbytes);
            //contadorTSN += 2;
        }
        public void PrintTSNV(List<ElementoSegmentoDeDatos> listaSegmentos)
        {
            int i = 0, temp;
            string[] thirtybytes = new string[30], twobytes = new string[2];
            byte[] unbyte = new byte[1];
            byte[] treintabytes = new byte[30];
            byte[] dosbytes = new byte[2];
            string nombre, direccion, tipo, numelem, vs;
            //Console.Write(listaSegmentos.Count);
            unbyte = BitConverter.GetBytes(listaSegmentos.Count);
            tsnvEnBytes.Add(unbyte[0]);
            foreach (var x in listaSegmentos)
            {
                i = 0;
                if (x.VariableName.Length < 30) {
                    while (x.VariableName.Length < 30) {
                        x.VariableName += '0';
                    }
                }
                 treintabytes = Encoding.ASCII.GetBytes(x.VariableName);
                for (i=0;i<treintabytes.Length;i++) {
                    tsnvEnBytes.Add(treintabytes[i]);
                }
                dosbytes = BitConverter.GetBytes(x.Direccion);
                for (i = 0; i < dosbytes.Length; i++)
                {
                    tsnvEnBytes.Add(dosbytes[i]);
                }
                unbyte = BitConverter.GetBytes(x.VariableType);
                tsnvEnBytes.Add(unbyte[0]);
                dosbytes = BitConverter.GetBytes(Convert.ToInt32(x.NumElementos));
                for (i = 0; i < dosbytes.Length; i++) {
                    tsnvEnBytes.Add(dosbytes[i]);
                }
                dosbytes = BitConverter.GetBytes(Convert.ToInt32(x.VectorString));
                for (i = 0; i < dosbytes.Length; i++)
                {
                    tsnvEnBytes.Add(dosbytes[i]);
                }
                //direccion = string.Join("-",Convert.ToString(BitConverter.GetBytes(x.Direccion)[0]));
                //tipo = string.Join("-", Convert.ToString(BitConverter.GetBytes(x.VariableType)[0]));
                //Console.Write(nombre + "");
                //Console.Write(direccion + "");
                //Console.Write(tipo + "");
                //temp = Convert.ToInt32(x.NumElementos);
                ////cambiarA2bytes(temp,ref temp);
                //temp = Convert.ToInt32(x.VectorString);
                ////cambiarA2bytes(temp, ref temp);
                //dosbytes = BitConverter.GetBytes(temp);  
            }
        }

    }
}
