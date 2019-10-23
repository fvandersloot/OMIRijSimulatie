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
        public List<Rij> Rijen { get; private set; }

        // De klanten in de simulatie
        private List<Klant> Klanten;

        private int[] IntroductionTimes;
        private int Iteraties;

        private readonly IDisplayer Display;

        // The rij met de minste klanten
        public Rij Kortste
        {
            get
            {
                List<Rij> min = new List<Rij>();

                foreach (Rij r in Rijen.FindAll(x => x.IsOpen))
                {
                    if (min.Count == 0 || min[0].Count > r.Count)
                    {
                        min = new List<Rij> { r };
                    }
                    else if (r.IsOpen && min[0].Count == r.Count)
                    {
                        min.Add(r);
                    }
                }

                return min[R.Next(min.Count)];
            }
        }

        public List<Rij> Gesloten { get { return Rijen.FindAll(x => !x.IsOpen); } }

        public List<Rij> Sluitbaar { get { return Rijen.FindAll(x => x.IsOpen).FindAll(x => !x.BlijftOpen); } }

        /// <summary>
        /// Constructor voor de simulatie
        /// </summary>
        /// <param name="aantalklanten">Het aantal klanten dat per tijdseenheid aan het systeem wordt toegevoegd</param>
        /// <param name="rijen">Het aantal rijen voor deze simulatie</param>
        public Simulatie(int rijen, int geslotenrijen, int aantalklanten, int iterations, int seed = 1)
        {
            Display = new ConsoleDisplayer();

            R = new Random(seed);
            CurrentTime = 0;
            Klanten = new List<Klant>();
            Rijen = new List<Rij>();
            for (int i = 0; i < rijen; i++)
                Rijen.Add(new Rij(R.Next(1, 25), string.Format("Kassa {0}", i + 1)));
            for (int i = 0; i < geslotenrijen; i++)
                Rijen.Add(new Rij(R.Next(1, 25), string.Format("Kassa {0}", i + rijen + 1), false, false));

            Iteraties = iterations;
            IntroductionTimes = new int[Iteraties];

            for (int i = 0; i < Iteraties; i++)
                IntroductionTimes[i] = 0;

            for (int i = 0; i < aantalklanten; i++)
                IntroductionTimes[R.Next(Iteraties)] += 1;
        }

        /// <summary>
        /// Update de huidige staat met een tijdstap
        /// </summary>
        public int Step()
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
                            huidig.Pop(k);
                        break;
                }

            }

            // Rijen
            // Ik heb de voortgang nu gedaan voor het wisselen van de klanten, anders krijg je rare situaties waar er 3 mensen in 1 rij staan terwijl er ook een rij leeg is.

            int klantenklaar = 0;

            foreach (Rij r in Rijen)
            {
                r.Step();

                if (r.klanten.Count != 0) //Zodat head niet aangeroepen word op een lege lijst
                    if (r.Head.Voortgang >= 10) //Aangezien de voortang alleen omhoog gaat moeten we poppen op een standaard hoge value, ipv op 0
                    {
                        verwijder.Add(r.Head);
                        klantenklaar++;
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

            if (Kortste.Count < 3 && Sluitbaar.Count > 0)
                Sluitbaar[0].Sluit();

            if (Gesloten.Count > 0 && Kortste.Count > 4)
                Gesloten[0].Open();

            // Tijd
            CurrentTime += 1;
            return klantenklaar;
        }

        public List<State> Run()
        {
            List<State> states = new List<State>();

            for (int i = 0; i < Iteraties; i++)
            {
                if (Visualiseer)
                {
                    Display.Show(this);
                    Thread.Sleep(150);
                    //Console.ReadKey();
                }

                states.Add(new State
                {
                    AantalKlanten = Rijen.Sum(r => r.Count),
                    AVGRijlengte = Rijen.Average(r => Convert.ToDouble(r.Count)),
                    GeholpenNu = Step()
                });
            }

            return states;
        }
    }

    public struct State
    {
        public int AantalKlanten;
        public double AVGRijlengte;
        public int GeholpenNu;

        public override string ToString()
        {
            return string.Format("|            {0:000} |   {1:00.000} |              {2:000} |", AantalKlanten, AVGRijlengte, GeholpenNu);
        }
    }
}
