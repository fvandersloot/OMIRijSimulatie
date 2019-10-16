using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Rij
    {
        // Klanten in de rij
        public List<Klant> klanten;

        // Verhoging van de voortgang van de voorste klant in de rij per tijdstap
        private readonly int snelheid;

        // Aantal klanten in de rij
        public virtual int Count { get { return klanten.Count; } }

        // De persoon die aan de beurt is
        public Klant Head { get { return klanten[0]; } }

        public string Naam { get; private set; }

        /// <summary>
        /// Constructor voor het Rij object
        /// </summary>
        /// <param name="snelheid">Verwerkingsnelheid van deze kassa</param>
        public Rij(int snelheid, string naam = "kassa")
        {
            klanten = new List<Klant>();
            Naam = naam;
            this.snelheid = snelheid;
        }

        /// <summary>
        /// Haal een persoon uit de rij
        /// </summary>
        /// <param name="index">index van de persoon in de rij</param>
        public void Pop(int index)
        {
            if (klanten.Count >= index)
                klanten.RemoveAt(index);
        }

        /// <summary>
        /// Haal een persoon uit de rij
        /// </summary>
        /// <param name="klant">de persoon die je uit de rij wilt halen</param>
        public void Pop(Klant klant)
        {
            if (klanten.Contains(klant))
                klanten.Remove(klant);
        }

        /// <summary>
        /// Voeg een Klant toe achteraan de rij
        /// </summary>
        /// <param name="klant">de Klant</param>
        public void Push(Klant klant)
        {
            klanten.Add(klant);
        }

        /// <summary>
        /// Voer een tijdstap stap uit
        /// </summary>
        public virtual void Step()
        {
            if (klanten.Count != 0) //Zodat head niet op een lege lijst word aangeroepen
                Head.IncVoortgang(snelheid);
        }

        /// <summary>
        /// Vind de positie van een klant
        /// </summary>
        /// <param name="klant"></param>
        /// <returns></returns>
        public int RijPositie(Klant klant)
        {
            return klanten.IndexOf(klant);
        }

        public bool Bevat(Klant klant)
        {
            return klanten.Contains(klant);
        }

        public void Show()
        {
            Console.Write("{0}: ", Naam);
            foreach (Klant k in klanten)
            {
                k.Show();
            }
        }  
    }
}
