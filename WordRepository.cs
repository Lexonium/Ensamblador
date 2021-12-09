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
                yaLeido = true;
                etiquetas.Add(word, countLinea);
                //GUARDAR SU DIRECCION AQUI.
            }
        }

    }
}
