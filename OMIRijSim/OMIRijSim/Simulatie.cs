using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Simulatie
    {
        // Aantal nieuwe  Klanten per tijdstap?
        public readonly int KlantFreq;

        // De rijen van de simulatie
        private List<Rij> Rijen;

        // De klanten in de simulatie
        private List<Klant> Klanten;

        /// <summary>
        /// Constructor voor de simulatie
        /// </summary>
        /// <param name="klantfrequentie">Het aantal klanten dat per tijdseenheid aan het systeem wordt toegevoegd</param>
        /// <param name="rijen">Het aantal rijen voor deze simulatie</param>
        public Simulatie(int klantfrequentie, int rijen) { throw new NotImplementedException(); }

        /// <summary>
        /// Update de huidige staat met een tijdstap
        /// </summary>
        public void Step() { throw new NotImplementedException(); }
    }
}
