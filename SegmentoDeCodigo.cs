using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEnsamblador
{
    public class SegmentoDeCodigo
    {
        public List<ElementoSegmentoDeCodigo> Elementos { get; set; }

        public SegmentoDeCodigo()
        {
            Elementos = new List<ElementoSegmentoDeCodigo>();
        }
    }
}
