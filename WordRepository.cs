using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public static class WordRepository
    {
        public static void EsComentario(ref bool esComentario, ref bool yaLeido, string word)
        {
            if (word == ";")  
            {
                esComentario = true;
                yaLeido = true;
            }
        }

        public static void EsEtiqueta(bool esComentario, ref bool yaLeido, string word, ref List<string> listaEtiquetas, ref Dictionary<string,int> etiquetas, ref int countLinea)
        {
            if (word.EndsWith(':') && esComentario == false)  //METODO PARA VER SI ES ETIQUETA
            {
                //listaEtiquetas.Add(word);
                yaLeido = true;
                etiquetas.Add(word, countLinea);
                //GUARDAR SU DIRECCION AQUI.
            }
        }
        public static void EsCadena(ref bool empiezaCadena, string word,ref bool terminaCadena,ref string mensaje ) {
            if (word != "")
            {
                if (word[0] == '"')
                {
                    empiezaCadena = true;
                    terminaCadena = false;
                }
                if (word[word.Length - 1] == '"')
                {
                    terminaCadena = true;
                    empiezaCadena = true;
                    mensaje += word;
                }
            }
        }

        public static void TieneComa(bool esComentario, ref bool yaLeido, string word, ref List<string> listaVariables, int numVariables, string linea)
        {
            if (yaLeido == false && numVariables > 0 && esComentario == false)
            {
                if (numVariables == 1)
                {
                    if (!linea.Contains(','))
                    {
                        if (!String.IsNullOrEmpty(word) && !String.IsNullOrWhiteSpace(word))
                            listaVariables.Add(word);
                        //METER DIRECCION DE VARIABLE Y AGREGAR SU VALOR COMO?
                        //NECESITAMOS APRENDER EL TIPO DE VARIABLE QUE ES, SERIA EN COMANDOS?
                    }
                    else
                    {
                        Console.WriteLine("ERROR AQUI SOLO ES UNA VARIABLE");
                    }
                }
                if (numVariables == 2)
                {
                    if (!linea.Contains(','))
                    {
                        Console.WriteLine("ERROR AQUI SON 2 VARIABLEs");

                    }
                    else
                    {
                        listaVariables.Add(linea.Split(",")[0]);
                        //METER DIRECCION DE VARIABLE Y AGREGAR SU VALOR COMO?, agarrar el segundo valor del split para poder
                        //ver de cuanto es el arreglo.
                    }
                }
                yaLeido = true;
            }
        }
    }
}
