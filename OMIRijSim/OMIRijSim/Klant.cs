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

        public readonly Guid ID;

        // Voortgang van de klant bij de kassa. Moet 0 zijn als niet voor in een rij
        public int Voortgang { get; private set; }

        //De hoeveelheid tijd dat een klant wilt wachten voordat hij van rij veranderd
        public int Geduld;

        // Het aantal producten (dus hoeveel er verwerkt moet worden)
        private readonly int NProducten;

        public readonly int color;

        public int Opgeven;

        /// <summary>
        /// Constructor voor het Klant object
        /// </summary>
        /// <param name="aantalproducten">De snelheid van de klant</param>
        public Klant(int aantalproducten, int geduld, int opgeven)
        {
            NProducten = aantalproducten;
            IncVoortgang(-NProducten); //Verlaagt de start voortgang aan de hand van de hoeveelheid producten
            Geduld = geduld;
            Opgeven = opgeven;

            ID = Guid.NewGuid();

            color = 1 + (NProducten % 14 <= 8 ? NProducten % 14 : (NProducten % 14) + 1);
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
        static public KlantActie Besluit(Klant k, Rij huidig, Rij kortst)
        {
            //TODO complexer maken
            if (huidig == null)
                return KlantActie.WisselNaarKortste;

            if (huidig.RijPositie(k) > (kortst.Count - (k.Geduld)))
                return KlantActie.WisselNaarKortste;

            return KlantActie.Blijf;
        }

        public string Show()
        {

            return "O";
        }
    }
}
