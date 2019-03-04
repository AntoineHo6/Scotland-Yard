using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public class Detective : Joueur {

        public bool EstBloque { get; set; }

        public bool IsFirstInTurn { get; set; }
        public bool IsLastInTurn { get; set; }

        public int IdNum { get; set; }

        public Detective(string nom, int caseActuelle, Color color, int id) : base(nom, caseActuelle, color) {
            NbrTaxi = 10;
            NbrBus = 8;
            NbrMetro = 4;
            // temp
            //NbrTaxi = 1;
            //NbrBus = 0;
            //NbrMetro = 0;

            EstBloque = false;

            IsFirstInTurn = false;
            IsLastInTurn = false;

            this.IdNum = id;
        }


        public void deplacerCase(int laCase) {
            CaseActuelle = laCase;
        }

    }
}
