using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class SegmentoDeDatos
    {
        public List<ElementoSegmentoDeDatos> Elementos { get; set; }

        public SegmentoDeDatos()
        {
            Elementos = new List<ElementoSegmentoDeDatos>();
        }
    }
}
