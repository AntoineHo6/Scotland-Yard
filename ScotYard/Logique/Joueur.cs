using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScotYard.Graphe;
using System.Drawing;
using System.Windows.Forms;

namespace ScotYard.Logique {

    public abstract class Joueur {

        String nom;
        public String Nom { get; set; }

        int caseActuelle;
        public int CaseActuelle { get; set; }

        int nbrTaxi;
        public int NbrTaxi { get; set; }

        int nbrMetro;
        public int NbrMetro { get; set; }

        int nbrBus;
        public int NbrBus { get; set; }
        
        Color color;
        public Color Color { get; set; }

        public Joueur(String nom, int caseActuelle, Color color) {
            this.Nom = nom;
            this.CaseActuelle = caseActuelle;
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
