using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Klant
    {
        // Voortgang van de klant bij de kassa. Moet 0 zijn als niet voor in een rij
        private int Voortgang;

        // Het aantal producten (dus hoeveel er verwerkt moet worden)
        private readonly int NProducten;

        /// <summary>
        /// Constructor voor het Klant object
        /// </summary>
        /// <param name="aantalproducten">De snelheid van de klant</param>
        public Klant(int aantalproducten) { throw new NotImplementedException(); }

        /// <summary>
        /// Verhoog de huidige voortgang
        /// </summary>
        /// <param name="n">de hoeveelheid om mee op te hogen</param>
        /// <returns></returns>
        public int IncVoortgang(int n) { throw new NotImplementedException(); }
    }
}
