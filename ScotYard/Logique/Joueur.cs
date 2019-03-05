using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScotYard.Graphe;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public abstract class Joueur {

        public String Nom { get; set; }

        public int Case { get; set; }

        public int NbrTaxi { get; set; }

        public int NbrMetro { get; set; }

        public int NbrBus { get; set; }
        
        public Color Color { get; set; }

        public Joueur(String nom, int caseActuelle, Color color) {
            this.Nom = nom;
            this.Case = caseActuelle;
            this.Color = color;
        }
        
        
        public void decrementeTrans(String transport) {
            switch (transport) {
                case "Taxi":
                    NbrTaxi--;
                    break;
                case "Metro":
                    NbrMetro--;
                    break;
                case "Bus":
                    NbrBus--;
                    break;
            }
        }
    }
}
