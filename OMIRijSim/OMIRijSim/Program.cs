using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulatie sim = new Simulatie(2, 5);
            sim.Run(100);

        }
    }
}
