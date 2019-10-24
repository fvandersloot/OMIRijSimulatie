using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    interface IDisplayer
    {
        void Show(Simulatie s);
        void Show(Rij r);
        void Show(Klant k);
    }

    public class ConsoleDisplayer : IDisplayer
    {
        public void Show(Simulatie s)
        {
            Console.Clear();
            Console.WriteLine("Iteration: {0}", s.CurrentTime);
            foreach (Rij r in s.Rijen)
                Show(r);
        }

        public void Show(Rij r)
        {
            Console.ForegroundColor = r.IsOpen ? ConsoleColor.Green : r.Count > 0 ? ConsoleColor.DarkGreen : ConsoleColor.DarkGray;
            Console.Write(" {0}: ", r.Naam);
            foreach (Klant k in r.klanten)
                Show(k);
            Console.Write(Environment.NewLine);
            Console.ResetColor();
        }

        public void Show(Klant k)
        {
            Console.ForegroundColor = (ConsoleColor)k.color;
            Console.Write("O");
            Console.ResetColor();
        }
    }
}
