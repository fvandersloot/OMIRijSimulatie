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
        private List<Klant> klanten;

        // Verhoging van de voortgang van de voorste klant in de rij per tijdstap
        private readonly int snelheid;

        // Aantal klanten in de rij
        public int Count { get { return klanten.Count; } }

        // De persoon die aan de beurt is
        public Klant Head { get { return klanten[0]; } }

        /// <summary>
        /// Constructor voor het Rij object
        /// </summary>
        /// <param name="snelheid">Verwerkingsnelheid van deze kassa</param>
        public Rij(int snelheid)
        {
            klanten = new List<Klant>();
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


    }
}
