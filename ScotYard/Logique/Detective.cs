using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScotYard.Logique {

    public class Detective: Joueur{

        Color color;
        public Color Color {
            get { return color; }
            set { color = value; }
        }


        public Detective(string nom, int caseActuelle, Color color) : base(nom, caseActuelle) {
            this.NbrTaxi = 10;
            this.NbrBus = 3;
            this.NbrMetro = 3;
            this.color = color;
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
