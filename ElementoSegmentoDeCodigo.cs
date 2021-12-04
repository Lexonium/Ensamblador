using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class ElementoSegmentoDeCodigo
    {
        private string _commandName; //Si es un DEFI,HALT etc
        private int _variableType; //Si es PUSHI, PUSHD, seguir con los tipos de 1,2,3,11,12,13
        private int _numdeCodigo; // Saber el codigo del comando se sabe que Halt es 48
        private int _direccionComando; // La direccion del comando.
        private int _pesoComando; //Cuanto pesa el comando los Pushes pesan3 etc
        private int? _direccionVariable; //La direccion de la variable si lo tiene
        private string? _valorConstante; //El valor de la constante si tiene


        public string CommandName 
        { 
            get { return _commandName; }
            set { _commandName = value; } 
        }

        //1:int 2:double 3:string 11:arrayInt 12:arrayDouble 13:arrayString
        public int VariableType 
        {
            get { return _variableType; }
            set { _variableType = value; }
        }
        public int NumeroDeCodigo
        {
            get { return _numdeCodigo; }
            set { _numdeCodigo = value; }
        }
        public int DireccionComando 
        {
            get { return _direccionComando; }
            set { _direccionComando = value; }
        }
        public int PesoComando
        {
            get { return _pesoComando; }
            set { _pesoComando = value; }
        }
        public int? DireccionVariable
        {
            get { return _direccionVariable; }
            set { _direccionVariable = value; }
        }
        public string? ValorConstante
        {
            get { return _valorConstante; }
            set { _valorConstante = value; }
        }
        public ElementoSegmentoDeCodigo()
        {

        }

        public ElementoSegmentoDeCodigo(string commandName, int variableType, int numdeCodigo, int direccionComando, int pesoComando, int? direccionVariable, string? valorConstante)
        {
            CommandName = commandName;
            VariableType = variableType;
            _numdeCodigo = numdeCodigo;
            DireccionComando = direccionComando;
            PesoComando = pesoComando;
            DireccionVariable = direccionVariable;
            ValorConstante = valorConstante;
           
        }
    }
}
