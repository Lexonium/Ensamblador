using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class ElementoSegmentoDeDatos
    {
        private string _variableName;
        private int _variableType;
        private int _direccion;
        private int _peso;
        private int? _numElementos;
        private int _vectorString;

        public string VariableName 
        { 
            get { return _variableName; }
            set { _variableName = value; } 
        }

        //1:int 2:double 3:string 11:arrayInt 12:arrayDouble 13:arrayString
        public int VariableType 
        {
            get { return _variableType; }
            set { _variableType = value; }
        }
        public int VectorString
        {
            get { return _vectorString; }
            set { _vectorString = value; }
        }
        public int Direccion 
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public int? NumElementos 
        {
            get { return _numElementos; }
            set { _numElementos = value; }
        }
        public int Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }
        public ElementoSegmentoDeDatos()
        {

        }
        public ElementoSegmentoDeDatos(string variableName, int variableType, int direccion, int? numElementos, int peso) : base()
        {
            _variableName = variableName;
            _variableType = variableType;
            _direccion = direccion;
            _numElementos = numElementos;
            _peso = peso;
        }
    }
}
