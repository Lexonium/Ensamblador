using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public static class CommandRepository
    {
        //METODO PARA VER CUANTAS VARIABLES SE NECESITAN
        public static bool VariableDefine(string palabra, string[] coman1var, int countLinea, ref SegmentoDeDatos segmentoDatos, ref int countDatos, ref int countVS)
        {
            ElementoSegmentoDeDatos elemento = new ElementoSegmentoDeDatos();
            switch (palabra)
            {
                case "DEFI":
                    //INT
                    elemento.VariableType = 1;
                    elemento.Peso = 4;
                    elemento.VectorString = countVS;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    else
                    {
                        elemento.Direccion = countDatos + 1;
                        //countDatos += elemento.Peso;
                    }
                    countDatos += elemento.Peso;
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                case "DEFD":
                    //DOUBLE
                    elemento.VariableType = 2;
                    elemento.Peso = 8;
                    elemento.VectorString = countVS;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    else
                    {
                        elemento.Direccion = countDatos + 1;
                        //countDatos += elemento.Peso;
                    }
                    countDatos += elemento.Peso;
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                case "DEFS":
                    //STRING
                    elemento.VariableType = 3;
                    elemento.Peso = 2;
                    elemento.VectorString = countVS;
                    countVS++;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    else
                    {
                        elemento.Direccion = countDatos + 1;
                        //countDatos += elemento.Peso;
                    }
                    countDatos += elemento.Peso;
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                case "DEFAI":
                    //Array Int
                    elemento.VariableType = 11;
                    elemento.Peso = 4;
                    elemento.VectorString = countVS;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    //El countDatos para los arrays se estara definiendo fuera de aqui donde checa esVariable
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                case "DEFAD":
                    //Array Double
                    elemento.VariableType = 12;
                    elemento.Peso = 8;
                    elemento.VectorString = countVS;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                case "DEFAS":
                    //Array String
                    elemento.VariableType = 13;
                    elemento.Peso = 2;
                    if (countLinea == 0)
                    {
                        elemento.Direccion = 0;
                    }
                    segmentoDatos.Elementos.Add(elemento);
                    break;
                default:
                    return false;
            }
            return true;
        }
    
        
    }
}
