using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class Detective : Joueur {

        bool estBloque;
        public bool EstBloque { get; set; }

        public Detective(string nom, int caseActuelle, Color color) : base(nom, caseActuelle, color) {
            // temp
            // NbrTaxi = 10;
            //NbrBus = 3;
            //NbrMetro = 3;

            NbrTaxi = 1;
            NbrBus = 1;
            NbrMetro = 1;

            estBloque = false;
            
        }


        public void deplacerCase(int laCase) {
            CaseActuelle = laCase;
        }

    }
}
