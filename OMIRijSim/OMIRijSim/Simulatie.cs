using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Simulatie
    {
        // Random object
        private Random R;

        // Huidige tijdstap
        private int CurrentTime;

        // Tijdstappen tussen twee nieuwe klanten
        public readonly int KlantFreq;

        // De rijen van de simulatie
        private List<Rij> Rijen;

        // De klanten in de simulatie
        private List<Klant> Klanten;

        // The rij met de minste klanten
        public Rij Kortste
        {
            get
            {
                Rij min = Rijen[0];

                foreach (Rij r in Rijen)
                    if (min.Count > r.Count)
                        min = r;

                return min;
            }
        }

        /// <summary>
        /// Constructor voor de simulatie
        /// </summary>
        /// <param name="klantfrequentie">Het aantal klanten dat per tijdseenheid aan het systeem wordt toegevoegd</param>
        /// <param name="rijen">Het aantal rijen voor deze simulatie</param>
        public Simulatie(int klantfrequentie, int rijen, int seed = 1)
        {
            R = new Random(seed);
            CurrentTime = 0;
            KlantFreq = klantfrequentie;
            Klanten = new List<Klant>();
            Rijen = new List<Rij>();
            for (int i = 0; i < rijen; i++)
                Rijen.Add(new Rij(5));//TODO Hardcoded Value!!!
        }

        /// <summary>
        /// Update de huidige staat met een tijdstap
        /// </summary>
        public void Step()
        {
            // Klanten
            if (CurrentTime % KlantFreq == 0)
                Klanten.Add(new Klant(10)); //TODO Hardcoded Value!!!

            foreach (Klant k in Klanten)
            {
                //TODO Do Stuff
            }

            // Rijen
            foreach (Rij r in Rijen)
            {
                //TODO Do Stuff
            }

            // Tijd
            CurrentTime += 1;
        }
    }
}
