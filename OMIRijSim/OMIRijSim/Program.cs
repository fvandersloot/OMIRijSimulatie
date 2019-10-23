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
            int nklant = 300;
            Simulatie sim = new Simulatie(3, 10, nklant, 200);
            Console.ReadLine();
            List<State> states = sim.Run();
            Console.Clear();
            Console.WriteLine("+----------------+----------+--------------------+");
            Console.WriteLine("| aantal klanten | avg rijl | klanten weggelopen |");
            Console.WriteLine("+----------------+----------+--------------------+");
            foreach (var state in states)
                Console.WriteLine(state.ToString());
            Console.WriteLine("+----------------+----------+--------------------+");
            Console.WriteLine("|                |          |                 {0:000}|", states.Sum(x => x.Weggelopen));
            Console.WriteLine("+----------------+----------+--------------------+");
            int nettoklanten = nklant - states.Last().AantalKlanten;
            Console.WriteLine("Efficiency: {0}", ((float)nettoklanten - (float)states.Sum(x => x.Weggelopen))/(float)nettoklanten);
            Console.ReadLine();
        }
    }
}
