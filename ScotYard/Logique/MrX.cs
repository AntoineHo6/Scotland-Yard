using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class MrX : Joueur{

        int nbrBlack;
        public int NbrBlack {
            get { return nbrBlack; }
            set { nbrBlack = value; }
        }

        public MrX(int caseActuelle) : base("Mr. X", caseActuelle, Color.Gray) {
            this.NbrTaxi = 4;
            this.NbrMetro = 3;
            this.NbrBus = 3;
            nbrBlack = 3;
        }

        public override void deplacerCase(int laCase) {
            throw new NotImplementedException();
        }
    }
}
