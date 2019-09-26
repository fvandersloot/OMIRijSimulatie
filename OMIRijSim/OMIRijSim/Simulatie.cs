using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Simulatie
    {
        private bool Visualiseer = true;

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
                Rijen.Add(new Rij(R.Next(20)));
            
        }

        public void Show()
        {
            Console.Clear();
            for (int i = 0; i < Rijen.Count; i++)
            {
                Console.WriteLine("Kassa {0}: {1}", i + 1, Rijen[i].Show());

            }
        }

        /// <summary>
        /// Update de huidige staat met een tijdstap
        /// </summary>
        public void Step()
        {
            // Klanten
            if (CurrentTime % KlantFreq == 0)
                Klanten.Add(new Klant(-50)); //TODO Hardcoded Value!!!

            foreach (Klant k in Klanten)
            {
                Rij huidig = null;
                try
                {
                    huidig = Rijen.Find(r => r.Bevat(k));
                }
                catch (ArgumentNullException) {/* huidig blijft null */}
                    

                switch (k.Besluit(huidig, Kortste))
                {
                    case Klant.KlantActie.Blijf:
                        break;
                    case Klant.KlantActie.WisselNaarKortste:
                        if (huidig != null)
                        {
                            huidig.Pop(k);                            
                        }
                        Kortste.Push(k); //De push buiten de if statement gehaald, anders worden er nooit klanten van de Klanten lijst in de klanten lijst gezet
                        break;
                }
            }

            // Rijen
            foreach (Rij r in Rijen)
            {
                r.Step();

                if (r.klanten.Count != 0) //Zodat head niet aangeroepen word op een lege lijst
                    if (r.Head.Voortgang >= 200) //Aangezien de voortang alleen omhoog gaat moeten we poppen op een standaard hoge value, ipv op 0
                        r.Pop(r.Head);
            }

            // Tijd
            CurrentTime += 1;
        }

        public List<StateData> Run(int rondes)
        {
            List<StateData> states = new List<StateData>();

            for (int i = 0; i <= rondes; i++)
            {
                if (Visualiseer)
                    Show();

                states.Add(new StateData
                {
                    AantalKlanten = Rijen.Sum(r => r.Count),
                    AVGRijlengte = Rijen.Average(r => Convert.ToDouble(r.Count))
                });

                Step();
            }

            return states;
        }
    }

    public struct StateData
    {
        public int AantalKlanten;
        public double AVGRijlengte;
    }
}
