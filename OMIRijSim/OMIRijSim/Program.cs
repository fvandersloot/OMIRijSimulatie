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
            Simulatie sim = new Simulatie(300, 5, 200);
            sim.Run();
            Console.ReadKey();
        }
    }
}
