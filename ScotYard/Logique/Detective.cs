using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class Detective: Joueur{
        

        public Detective(string nom, int caseActuelle, Color color) : base(nom, caseActuelle, color) {
            NbrTaxi = 10;
            NbrBus = 3;
            NbrMetro = 3;
        }


        public void deplacerCase(int laCase) {
            CaseActuelle = laCase;
        }

    }
}
