using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OMIRijSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            ViewSimulation(200);
            //CollectData(750, 1);

            Console.ReadLine();
        }

        public static void ViewSimulation(int nklant, bool showtable = true)
        {
            Simulatie sim = new Simulatie(3, 11, nklant, 51);
            List<State> states = sim.Run();

            Console.ReadKey();
            Console.Clear();

            if (showtable)
            {
                Console.WriteLine("+----------------+----------+--------------------+");
                Console.WriteLine("| aantal klanten | avg rijl | klanten weggelopen |");
                Console.WriteLine("+----------------+----------+--------------------+");
                foreach (var state in states)
                    Console.WriteLine(state.ToString());
                Console.WriteLine("+----------------+----------+--------------------+");
                Console.WriteLine("|                |          |                {0:000} |", states.Sum(x => x.Weggelopen));
                Console.WriteLine("+----------------+----------+--------------------+");
            }
            int nettoklanten = nklant - states.Last().AantalKlanten;
            Console.WriteLine("Efficiency: {0}", (nettoklanten - states.Sum(x => x.Weggelopen)) / (float)nettoklanten);
        }

        public static void CollectData(int iteraties, int klantdichtheid)
        {
            int nklant = (iteraties/40) * klantdichtheid;

            using (StreamWriter sw = new StreamWriter("results.csv", false, Encoding.UTF8)) { }

            List<List<State>> results = new List<List<State>>();

            Console.Clear();

            for (int i = 0; i < 25; i++)
                Console.Write("-");

            Console.Write(Environment.NewLine);

            Parallel.For(0, 100, i =>
            {
                for (int j = 0; j < nklant; j++)
                {
                    Simulatie sim = new Simulatie(
                        rijen: 1,
                        geslotenrijen: 15,
                        aantalklanten: nklant * i + j + 100,
                        iteraties: iteraties,
                        seed: j,
                        visualiseer: false
                        );

                    List<State> result = sim.Run();
                    lock (results)
                    {
                        results.Add(result);
                    }
                }

                if (i % 4 == 1)
                    Console.Write("#");
            });

            Console.Write(Environment.NewLine);

            List<List<State>> efficient = results.Where(x => x.Sum(y => y.Weggelopen) == 0).ToList();
            
            using (StreamWriter sw = new StreamWriter("results.csv", false, Encoding.UTF8))
            {
                sw.WriteLine("klanten,kassas");

                foreach (var state in efficient)
                {
                    int aantalklanten = state.Sum(s => s.AantalKlanten);
                    int aantalkassas = state.Max(s => s.AantalKassas);

                    sw.WriteLine("{0},{1}", aantalklanten, aantalkassas);
                }

            }
            Console.WriteLine("File closed");
        }
    }
}
