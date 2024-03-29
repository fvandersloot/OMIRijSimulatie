﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

        private int[] IntroductionTimes;
        private int Iteraties;

        // The rij met de minste klanten
        public Rij Kortste
        {
            get
            {
                List<Rij> min = new List<Rij>();

                foreach (Rij r in Rijen)
                {
                    if ((min.Count == 0 || min[0].Count > r.Count) && r.open)
                    {
                        min = new List<Rij>();
                        min.Add(r);
                    }
                    else if (min[0].Count == r.Count)
                    {
                        min.Add(r);
                    }
                }

                return min[R.Next(min.Count)];
            }
        }

        /// <summary>
        /// Constructor voor de simulatie
        /// </summary>
        /// <param name="aantalklanten">Het aantal klanten dat per tijdseenheid aan het systeem wordt toegevoegd</param>
        /// <param name="rijen">Het aantal rijen voor deze simulatie</param>
        public Simulatie(int rijen, int rijengesloten, int aantalklanten, int iterations, int seed = 1)
        {
            R = new Random(seed);
            CurrentTime = 0;
            Klanten = new List<Klant>();
            Rijen = new List<Rij>();
            for (int i = 0; i < rijen; i++)
                Rijen.Add(new Rij(R.Next(1, 25), true, string.Format("Kassa {0}", i+1)));
            for (int i = 0; i < rijengesloten; i++)
                Rijen.Add(new Rij(R.Next(1, 25), false, string.Format("Kassa {0}", i+1)));
            Iteraties = iterations;
            IntroductionTimes = new int[Iteraties];

            for (int i = 0; i < Iteraties; i++)
                IntroductionTimes[i] = 0;

            for (int i = 0; i < aantalklanten; i++)
                IntroductionTimes[R.Next(Iteraties)] += 1;
        }

        public void Show()
        {
            Console.Clear();
            for (int i = 0; i < Rijen.Count; i++)
            {
                Rijen[i].Show();
                Console.ResetColor();
                Console.Write(Environment.NewLine);
            }
        }

        /// <summary>
        /// Update de huidige staat met een tijdstap
        /// </summary>
        public void Step()
        {
            List<Klant> verwijder = new List<Klant>();
            // Klanten
            for (int i = 0; i < IntroductionTimes[CurrentTime]; i++)
                Klanten.Add(new Klant(R.Next(0, 150), R.Next(0, 1), 40)); //TODO Hardcoded Value!!!

            foreach (Klant k in Klanten)
            {
                k.Opgeven -= 1;
                if (k.Opgeven == 0)
                {
                    verwijder.Add(k);
                }
                Rij huidig = null;
                try
                {
                    huidig = Rijen.Find(r => r.Bevat(k));
                }
                catch (ArgumentNullException) {/* huidig blijft null */}


                switch (Klant.Besluit(k, huidig, Kortste))
                {
                    case Klant.KlantActie.Blijf:
                        break;
                    case Klant.KlantActie.WisselNaarKortste:
                        
                        Kortste.Push(k);
                        if (huidig != null)
                        {
                            huidig.Pop(k);
                        }

                        break;
                }
            }

            // Rijen
            // Ik heb de voortgang nu gedaan voor het wisselen van de klanten, anders krijg je rare situaties waar er 3 mensen in 1 rij staan terwijl er ook een rij leeg is.

            foreach (Rij r in Rijen)
            {
                r.Step();
                if (r.klanten.Count < 3)
                {
                    
                }
                if (r.klanten.Count != 0) //Zodat head niet aangeroepen word op een lege lijst
                    if (r.Head.Voortgang >= 10) //Aangezien de voortang alleen omhoog gaat moeten we poppen op een standaard hoge value, ipv op 0
                    {
                        verwijder.Add(r.Head);
                    }

               foreach (Klant kl in verwijder) // Ga langs alle klanten in de verwijder lijst en haal ze uit de rijen, een probleempje ik weet niet of self-checkout kassas in de rijen lijst mogen
                {
                    if (r.Bevat(kl))
                    {
                        r.Pop(kl);
                        Klanten.Remove(kl);
                    }
                }

            }

            // Tijd
            CurrentTime += 1;
        }

        public List<StateData> Run()
        {
            List<StateData> states = new List<StateData>();

            for (int i = 0; i < Iteraties; i++)
            {
                if (Visualiseer)
                {
                    Show();
                    Thread.Sleep(150);
                    //Console.ReadKey();
                }

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

        public override string ToString()
        {
            return String.Format("Klanten: {0:000}, Gemiddelde Rij lengte: {1:00.000}", AantalKlanten, AVGRijlengte);
        }
    }
}
