using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class Detective : Joueur {

        public bool EstBloque { get; set; }

        public bool estPremier { get; set; }
        public bool estDernier { get; set; }

        public int IdNum { get; set; }

        public Detective(string nom, int Case, Color color, int id) : base(nom, Case, color) {
            NbrTaxi = 10;
            NbrBus = 8;
            NbrMetro = 4;
            // temp
            //NbrTaxi = 1;
            //NbrBus = 5;
            //NbrMetro = 5;

            EstBloque = false;

            estPremier = false;
            estDernier = false;

            this.IdNum = id;
        }


        public void deplacerCase(int laCase) {
            Case = laCase;
        }

    }
}
