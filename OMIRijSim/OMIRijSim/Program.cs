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
            Simulatie sim = new Simulatie(5, 150, 200);
            List<StateData> states = sim.Run();
            Console.Clear();
            foreach (var state in states)
                Console.WriteLine(state.ToString());
            Console.ReadKey();
        }
    }
}
