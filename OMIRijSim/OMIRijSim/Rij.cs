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

        // Aantal mensen in de rij
        public int Count { get { return klanten.Count; } }

        /// <summary>
        /// Constructor voor het Rij object
        /// </summary>
        /// <param name="snelheid">Verwerkingsnelheid van deze kassa</param>
        public Rij(int snelheid) { throw new NotImplementedException(); }

        //TODO: Misschien kan dit beter met een referentie naar het object niet met een index.
        /// <summary>
        /// Haal een persoon uit de rij
        /// </summary>
        /// <param name="index">index van de persoon in de rij</param>
        public void Pop(int index) { throw new NotImplementedException(); }

        /// <summary>
        /// Voeg een Klant toe achteraan de rij
        /// </summary>
        /// <param name="klant">de Klant</param>
        public void Push(Klant klant) { throw new NotImplementedException(); }


    }
}
