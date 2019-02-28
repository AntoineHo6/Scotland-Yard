using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScotYard.Graphe;

namespace ScotYard.Logique {

    public abstract class Joueur {

        String nom;

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

        public Joueur(String nom, int caseActuelle) {
            this.nom = nom;
            this.caseActuelle = caseActuelle;
        }
    }
}
