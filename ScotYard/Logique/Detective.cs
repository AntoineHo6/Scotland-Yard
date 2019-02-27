using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScotYard.Graphe;

namespace ScotYard.Logique {

    class Detective: Joueur{

        public Detective(string nom, int caseActuelle) : base(nom, caseActuelle) {
            this.NbrTaxi = 10;
            this.NbrBus = 3;
            this.NbrMetro = 3;
        }

        public void prochaineCase() {

        }


        public void test() {
            Console.WriteLine(this.NbrTaxi);
            Console.WriteLine(this.NbrBus);
            Console.WriteLine(this.NbrMetro);
        }

    }
}
