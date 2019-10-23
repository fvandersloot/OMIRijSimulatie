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
            Simulatie sim = new Simulatie(3, 10, 300, 200);
            Console.ReadLine();
            List<State> states = sim.Run();
            Console.Clear();
            Console.WriteLine("+----------------+----------+--------------------+");
            Console.WriteLine("| aantal klanten | avg rijl | klanten weggelopen |");
            Console.WriteLine("+----------------+----------+--------------------+");
            foreach (var state in states)
                Console.WriteLine(state.ToString());
            Console.WriteLine("+----------------+----------+--------------------+");
            Console.ReadKey();
        }
    }
}
