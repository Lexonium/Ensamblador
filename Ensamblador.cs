using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class Ensamblador
    {
        Logger log;
        int numDeCaracterActual, //contador para saber en que caracter de la palabra estan
            direccion, numVariables, countLinea, countDatos, segCodigoSize, contadorTSN,countVS;
        bool esComentario, yaLeido, sigconst, comienzaCadena;
        string line;
        string word, comentario, mensaje;
        readonly string punCom, dosPuntos;

        string[] comandos1Var;
        Dictionary<string, int> reserved;
        Dictionary<string, int> reservedwithCode;
        Dictionary<string, int> etiquetas;

        List<string> listaComandos; // Posiblemente cambiar a diccionario?
        List<string> listaEtiquetas;
        List<string> listaComentarios;
        List<string> listaVariables;
        SegmentoDeDatos segmentoDeDatos;
        SegmentoDeCodigo segmentoDeCodigo;
        HashSet<string> comandosEnsamblador;
        HashSet<string> nombresVariables;
        HashSet<string> comandosSinVariable;
        HashSet<string> comandosDefine;
        HashSet<string> comandosGlobales;
        HashSet<string> comandosVarConst;

        // Read the file and display it line by line.
        StreamReader file;


        public Ensamblador()
        {
            log = new Logger();
            countVS = 0;
            numDeCaracterActual = 0;
            direccion = 0;
            numVariables = 0;
            countLinea = 0;
            countDatos = 0;
            segCodigoSize = 0;
            contadorTSN = 6;
            esComentario = false;
            yaLeido = false;
            sigconst = false;
            word = "";
            punCom = ";";
            dosPuntos = ":";
            comentario = " ";
            mensaje = " ";

            #region hashset&list
            listaComandos = new List<string>(); // Posiblemente cambiar a diccionario?
            listaEtiquetas = new List<string>();
            listaComentarios = new List<string>();
            listaVariables = new List<string>();

            //Comandos default globales
            comandosGlobales = new HashSet<string>();
            //Comandos default del lenguaje para definir una variable.
            comandosDefine = new HashSet<string>();
            //Comandos default del lenguaje que necesitan una variable para su udo.
            comandosEnsamblador = new HashSet<string>();
            //Comandos default del lenguaje que no dependen de una variable para su uso.
            comandosSinVariable = new HashSet<string>();
            //Variables que han entrado al programa
            nombresVariables = new HashSet<string>();
            //Variables que han entrado al programa
            comandosVarConst = new HashSet<string>();
            #endregion


            segmentoDeDatos = new SegmentoDeDatos();
            segmentoDeCodigo = new SegmentoDeCodigo();

            #region paths

                string[] nombreArgs = { "ComandosDefine", "ComandosSinVariable", "Comandos", "ComandosGlobales", "CMDConVarConst" };
                List<HashSet<string>> hashList = new List<HashSet<string>>();
                hashList.Add(comandosDefine);
                hashList.Add(comandosSinVariable);
                hashList.Add(comandosEnsamblador);
                hashList.Add(comandosGlobales);
                hashList.Add(comandosVarConst);

                foreach (var x in nombreArgs)
                {
                    for (int i = 0; i < nombreArgs.Length; i++)
                    {
                        var comandos = Environment.GetEnvironmentVariable(nombreArgs[i]);
                        string[] valores = comandos.Split(",");
                        foreach (var y in valores)
                        {
                            hashList[i].Add(y);
                        }
                    }
                }

                hashList.Clear();

                //Archivo de programa
                file = new StreamReader(PathRepository.CrearPath(@"\Files\PrgramaASCII.txt"));

            #endregion

            reserved = new Dictionary<string, int>() { { "NOP", 1 }, { "ADD", 1 }, { "SUB", 1 }, { "MULT", 1 }, { "DIV", 1 }, { "MOD", 1 }, { "INC", 3 }, { "DEC", 3 }, { "CMPEQ", 1 },
                { "CMPNE", 1 }, { "CMPLT", 1 }, { "CMPLE", 1 }, { "CMPGT", 1 }, { "CMPGE", 1 }, { "JMP", 3 }, { "JMPT", 3 }, { "JMPF", 3 }, { "SETIDX", 3 },
                { "SETIDXK", 5 }, { "PUSHI", 3 }, { "PUSHD", 3 }, { "PUSHS" , 3 }, { "PUSHAI" , 3 }, { "PUSHAD" , 3 }, { "PUSHAS" , 3 }, { "PUSHKI" , 5 },
                { "PUSHKD" , 9 }, { "PUSHKS" , 99+2 }, { "POPI" , 3 }, { "POPD" , 3 }, { "POPS" , 3 }, { "POPAI" , 3 }, { "POPAD" , 3 }, { "POPAS" , 3 },
                { "POPIDX" , 1 }, { "READI" , 3 }, { "READD" , 3 }, { "READS" , 3 }, { "READAI" , 3 }, { "READAD" , 3 }, { "READAS" , 3 }, { "PRTM" , 99+2 },
                { "PRTI" , 3 }, { "PRTD" , 3 }, { "PRTS" , 3 }, { "PRTAI" , 3 }, { "PRTAD" , 3 }, { "PRTAS" , 3 }, { "HALT" , 1 } 
            };
            reservedwithCode = new Dictionary<string, int>() { { "NOP", 0 }, { "ADD", 1 }, { "SUB", 2 }, { "MULT", 3 }, { "DIV", 4 }, { "MOD", 5 }, { "INC", 6 }, { "DEC", 7 }, { "CMPEQ", 8 },
                { "CMPNE", 9 }, { "CMPLT", 10 }, { "CMPLE", 11 }, { "CMPGT", 12 }, { "CMPGE", 13 }, { "JMP", 14 }, { "JMPT", 15 }, { "JMPF", 16 }, { "SETIDX", 17 },
                { "SETIDXK", 18 }, { "PUSHI", 19 }, { "PUSHD", 20 }, { "PUSHS" , 21 }, { "PUSHAI" , 22 }, { "PUSHAD" , 23 }, { "PUSHAS" , 24 }, { "PUSHKI" , 25 },
                { "PUSHKD" , 26 }, { "PUSHKS" , 27 }, { "POPI" , 28 }, { "POPD" , 29 }, { "POPS" , 30 }, { "POPAI" , 31 }, { "POPAD" , 32 }, { "POPAS" , 33 },
                { "POPIDX" , 34 }, { "READI" , 35 }, { "READD" , 36 }, { "READS" , 37 }, { "READAI" , 38 }, { "READAD" , 39 }, { "READAS" , 40 }, { "PRTM" , 41 },
                { "PRTI" , 42 }, { "PRTD" , 43 }, { "PRTS" , 44 }, { "PRTAI" , 45 }, { "PRTAD" , 46 }, { "PRTAS" , 47 }, { "HALT" , 48 }
            };
            etiquetas = new Dictionary<string, int>();
        }

        public void Run()
        {
            bool esVariable = false;
            bool skipStatement = false;
            string stackCommand = "";

            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                foreach (string linea in line.Split()) //Foreach que divide el documento en lineas de strings
                {
                    for (int i = 0; i < linea.Length; i++, numDeCaracterActual++) //For para dividir las palabras en letras
                    {
                        string c = linea[numDeCaracterActual].ToString(); //transforma cada caracter a string
                        word += c; //va acumulando la palabra que se forma                       
                    }

                    WordRepository.EsComentario(ref esComentario, ref yaLeido, word);
                    WordRepository.EsEtiqueta(esComentario, ref yaLeido, word, ref listaEtiquetas, ref etiquetas, ref countLinea);


                    //Se agrega la nueva variable al hashset de variable.
                    //No queremos que se entre aqui si no es un define.
                    if (esVariable && esComentario == false && skipStatement == false)
                    {
                        var elemento = segmentoDeDatos.Elementos.Last();

                        //arrayInt, arrayDouble, arrayString
                        if (elemento.VariableType < 10)
                        { //Si no es array
                            //Kills app if repeated word
                            log.IsRepeatedWord(word, ref nombresVariables);
                            if (elemento.VariableName == null)
                            {
                                elemento.VariableName = word;
                            }
                        }
                        else
                        {
                            var arrays = word.Split(',');
                            log.IsRepeatedWord(arrays[0], ref nombresVariables);

                            elemento.VariableName = arrays[0];
                            int elem = int.Parse(arrays[1].ToString());
                            elemento.NumElementos = elem;
                            elemento.Peso *= elem;
                            if (elemento.VariableType == 13) {
                                elemento.VectorString = countVS;
                                countVS += elem;
                            }

                            if (countLinea == 0)
                            {
                                elemento.Direccion = 0;
                                countDatos = elemento.Peso;
                            }
                            else
                            {
                                elemento.Direccion = countDatos + 1;
                                countDatos += elemento.Peso;
                            }
                        }
                        esVariable = false;
                        countLinea++;
                    }

                    //If a command is entered and it needs an operand (variable/constant) go in here
                    if (skipStatement && yaLeido==false)
                    {
                        if (word.Contains("\""))
                        {
                            var elementoCodigo = segmentoDeCodigo.Elementos.Last();
                            if (elementoCodigo.CommandName == "PRTM" || elementoCodigo.CommandName=="PUSHKS")
                            {
                                foreach (string pal in line.Split()) {
                                    if (pal != "") {
                                        if (comienzaCadena) {
                                            mensaje += " " + pal;
                                        }
                                        if (pal[0] == '"')
                                        {
                                            comienzaCadena = true;
                                            mensaje += pal.TrimStart('\"');
                                        }
                                        if (pal[pal.Length - 1] == '"') {
                                            comienzaCadena = false;
                                        }
                                    }
                                }
                                elementoCodigo.ValorConstante = mensaje.TrimStart('\"').TrimEnd('\"');
                                elementoCodigo.PesoComando = mensaje.Length;
                                mensaje = "";
                            };
                        }
                        else
                        {
                            //Si no existe la variable
                            if (!nombresVariables.Contains(word) && !int.TryParse(word, out _) && !etiquetas.ContainsKey(word + ":"))
                            {
                                throw new CustomException("The instance variable " + word + " was not defined.");
                            }
                            //if (etiquetas!=null)
                            //{
                            if (etiquetas.ContainsKey(word + ":"))
                            {
                                var elementoCodigo = segmentoDeCodigo.Elementos.Last();
                                elementoCodigo.DireccionVariable = etiquetas[word + ":"];
                            }
                            // }
                            else
                            {
                                if (esVariable)
                                { //Entra aqui cuando usa una variable y existe la variable
                                    var elementoCodigo = segmentoDeCodigo.Elementos.Last();
                                    var elementoDato = segmentoDeDatos.Elementos.Last();
                                    for (int i = 0; i < segmentoDeDatos.Elementos.Count; i++)
                                    {
                                        if (segmentoDeDatos.Elementos[i].VariableName == word)
                                        {
                                            elementoDato = segmentoDeDatos.Elementos[i];
                                        }
                                    }
                                    if (sigconst == false)
                                    {
                                        elementoCodigo.DireccionVariable = elementoDato.Direccion;
                                    }

                                }
                                else
                                {
                                    var elementoCodigo = segmentoDeCodigo.Elementos.Last();
                                    elementoCodigo.ValorConstante = word;
                                }
                                //Es una constante, se esta usando ej: pushki
                                if (sigconst)
                                {
                                    var elementoCodigo = segmentoDeCodigo.Elementos.Last();
                                    elementoCodigo.ValorConstante = word;
                                    sigconst = false;
                                }
                            }
                        }

                        skipStatement = false;
                    }

                    //Entra aqui si es un comando lo que se leyo. 
                    if (comandosGlobales.Contains(word) && yaLeido == false && esComentario == false && skipStatement == false)
                    {
                        //POSIBLEMENTE PASAR ESTAS 2 Lineas al else, porque creo que los defines no cuentan para el segmento de codigo.
                        listaComandos.Add(word);
                        segCodigoSize += reserved.Where(x => x.Key == word).Select(y => y.Value).FirstOrDefault();

                        //Si es un comando de define
                        if (word.Contains("DEF"))
                        {
                            //cambiar esto a futuro, que sea una tupla
                            esVariable = CommandRepository.VariableDefine(word, comandos1Var, countLinea, ref segmentoDeDatos, ref countDatos, ref countVS);
                            skipStatement = false;
                        }
                        else
                        {
                            //Implementacion de segmento de datos, aqui seria poner la direccion, peso, nombre y codigo(para el codigo, hacemos otra lista con los valores de cada comando ?)
                            ElementoSegmentoDeCodigo elemCodigo = new ElementoSegmentoDeCodigo();
                            elemCodigo.CommandName = word;
                            elemCodigo.DireccionComando = segCodigoSize;
                            elemCodigo.PesoComando = reserved.Where(x => x.Key == word).Select(y => y.Value).FirstOrDefault();
                            elemCodigo.NumeroDeCodigo= reservedwithCode.Where(x => x.Key == word).Select(y => y.Value).FirstOrDefault();
                            segmentoDeCodigo.Elementos.Add(elemCodigo);
                            if (word.Contains("PUSHK"))
                            {
                                sigconst = true;
                            }
                            if (comandosEnsamblador.Contains(word))
                            {
                                esVariable = true;
                                skipStatement = true;
                            } else if(comandosSinVariable.Contains(word))
                            {
                                esVariable = false;
                                skipStatement = true;
                            } else if (comandosVarConst.Contains(word))
                            {
                                stackCommand = word;
                            }
                            countLinea++;
                        }
                        yaLeido = true;
                        //GUARDAR DIRECCION DE COMANDO AQUI
                        //AGREGAR METODO SI NUM DE VARIABLES ES MAYOR QUE 0, PARA VER QUE SE ESPERA
                        // 0 = CONSTANTE, 1 = INT, 2 = DOUBLE, 3 = STRING ETC
                    }
                    else if (comandosSinVariable.Contains(word) && yaLeido == false && esComentario == false)
                    {//Si es un comando normal que no define variables. 

                        yaLeido = true;
                    }

                    //WordRepository.TieneComa(esComentario, ref yaLeido, word, ref listaVariables, numVariables, linea);

                    if (esComentario)
                    {
                        comentario += " " + word;
                    }

                    log.LogToConsole(word);  //Escribe la palabra y pasa el renglon
                    yaLeido = false;
                    word = "";//resetea el string donde se acumulan los caracteres 
                    numDeCaracterActual = 0;//resetea contador de caracteres 
                }

                if (!String.IsNullOrEmpty(comentario) && !String.IsNullOrWhiteSpace(word))
                    listaComentarios.Add(comentario);

                comentario = "";
                esComentario = false;
            }

            log.PrintList("Se encontro la etiqueta: ", listaEtiquetas);
            log.PrintList("Se encontro el comando: ", listaComandos);
            log.PrintList("Se encontro la variable: ", listaVariables);
            log.PrintList("Se encontro el comentario: ", listaComentarios);

            log.PrintSegmentoDeDatos(segmentoDeDatos.Elementos);
            log.PrintSegmentoDeCodigo(segmentoDeCodigo.Elementos);
            Console.WriteLine("============================================================");
            log.PrintMagicNumber(); //IMPRIME EL MAGIC NUMBER
            Console.Write(segCodigoSize); //IMPRIME EL TAMANO DEL SEG DE CODIGO
            Console.Write(""); //IMPRIME EL SEGUNDO ESPACIO (SEG DE CODIGO SE SUPONE QUE SON 2 BYTES)
            Console.Write(countDatos); //IMPREME CANTIDAD DE DATOS
            Console.Write(""); //SEGUNDA LINEA CANTIDAD DE DATOS
            Console.Write(countVS); // SE SUPONE QUE IMPRIME TAM DE VECTOR STRING(VECTOR STRING AUN NO IMPLEMENTADO)
            Console.Write(""); //AQUI EMPEZAR A IMPRIMIR LO DEL CODIGO (VOY A HACERLO UN LOGGER METHOD)
            log.PrintCodigodeTSN(segmentoDeCodigo.Elementos, contadorTSN);
            log.PrintTSNV(segmentoDeDatos.Elementos);



            file.Close(); //cierra el archivo a leer
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./output.tsn", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            } 
            Console.SetOut(writer);
            log.PrintMagicNumber(); //IMPRIME EL MAGIC NUMBER
            Console.Write(segCodigoSize); //IMPRIME EL TAMANO DEL SEG DE CODIGO
            Console.Write(""); //IMPRIME EL SEGUNDO ESPACIO (SEG DE CODIGO SE SUPONE QUE SON 2 BYTES)
            Console.Write(countDatos); //IMPREME CANTIDAD DE DATOS
            Console.Write(""); //SEGUNDA LINEA CANTIDAD DE DATOS
            Console.Write(0); // SE SUPONE QUE IMPRIME TAM DE VECTOR STRING(VECTOR STRING AUN NO IMPLEMENTADO)
            Console.Write(""); //AQUI EMPEZAR A IMPRIMIR LO DEL CODIGO (VOY A HACERLO UN LOGGER METHOD)
            log.PrintCodigodeTSN(segmentoDeCodigo.Elementos, contadorTSN);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
            try
            {
                ostrm = new FileStream("./output.tsnv", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            log.PrintTSNV(segmentoDeDatos.Elementos);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }
    }
}
