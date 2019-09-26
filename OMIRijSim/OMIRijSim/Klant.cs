using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIRijSim
{
    public class Klant
    {
        public enum KlantActie
        {
            Blijf,
            WisselNaarKortste
        }

        // Voortgang van de klant bij de kassa. Moet 0 zijn als niet voor in een rij
        public int Voortgang { get; private set; }

        // Het aantal producten (dus hoeveel er verwerkt moet worden)
        private readonly int NProducten;

        /// <summary>
        /// Constructor voor het Klant object
        /// </summary>
        /// <param name="aantalproducten">De snelheid van de klant</param>
        public Klant(int aantalproducten)
        {
            NProducten = aantalproducten;
        }

        /// <summary>
        /// Verhoog de huidige voortgang
        /// </summary>
        /// <param name="n">de hoeveelheid om mee op te hogen</param>
        /// <returns></returns>
        public void IncVoortgang(int n)
        {
            Voortgang += n;
        }

        /// <summary>
        /// Besluit op basis van de huidige rij en de kortste rij wat te doen
        /// </summary>
        /// <param name="huidig"></param>
        /// <param name="kortst"></param>
        /// <returns></returns>
        public KlantActie Besluit(Rij huidig, Rij kortst)
        {
            //TODO complexer maken
            if (huidig == null)
                return KlantActie.WisselNaarKortste;

            if (huidig.RijPositie(this) > kortst.Count)
                return KlantActie.WisselNaarKortste;

            return KlantActie.Blijf;
        }

        public string Show()
        {
            return "O";
        }
    }
}
