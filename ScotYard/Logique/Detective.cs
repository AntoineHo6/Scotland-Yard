using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class Detective: Joueur{
        

        public Detective(string nom, int caseActuelle, Color color) : base(nom, caseActuelle, color) {
            this.NbrTaxi = 10;
            this.NbrBus = 3;
            this.NbrMetro = 3;
        }


        public override void deplacerCase(int laCase) {
            CaseActuelle = laCase;
            Console.WriteLine(CaseActuelle);
        }

    }
}
