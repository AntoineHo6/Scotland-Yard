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
        public String Nom {
            get { return nom; }
            set { nom = value; }
        }

        int caseActuelle;
        public int CaseActuelle {
            get { return caseActuelle; }
            set { caseActuelle = value; }
        }

        int nbrTaxi;
        public int NbrTaxi {
            get { return nbrTaxi; }
            set { nbrTaxi = value; }
        }

        int nbrBus;
        public int NbrBus {
            get { return nbrBus; }
            set { nbrBus = value; }
        }

        int nbrMetro;
        public int NbrMetro {
            get { return nbrMetro; }
            set { nbrMetro = value; }
        }

        Color color;
        public Color Color {
            get { return color; }
            set { color = value; }
        }

        public Joueur(String nom, int caseActuelle, Color color) {
            this.nom = nom;
            this.caseActuelle = caseActuelle;
            this.Color = color;
        }
        
        
        public void decrementeTrans(String transport) {
            switch (transport) {
                case "taxi":
                    nbrTaxi--;
                    break;
                case "metro":
                    nbrMetro--;
                    break;
                case "bus":
                    nbrBus--;
                    break;
            }
        }
        

        public abstract void deplacerCase(int laCase);
    }
}
