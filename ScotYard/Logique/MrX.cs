using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScotAI;

namespace ScotYard.Logique {

    public class MrX : Joueur{

        int nbrBlack;
        public int NbrBlack { get; set; }


        public MrX(int caseActuelle) : base("Mr. X", caseActuelle, Color.DimGray) {
            NbrTaxi = 4;
            NbrMetro = 3;
            NbrBus = 3;
            nbrBlack = 3;
        }


        public void deplacerCase(int posDetec1, int posDetec2, int PosDetec3, out Transports? transportVoleur, out bool? blackTicketBool) {
            int prochaineCase = ScotAI.Case.ProchaineCaseVoleur(false, CaseActuelle, NbrTaxi, NbrBus, NbrMetro, NbrBlack, out transportVoleur, out blackTicketBool, posDetec1, posDetec2, PosDetec3);
            CaseActuelle = prochaineCase;
        }


        public void incrementeTrans(String transport) {
            switch (transport) {
                case "Taxi":
                    NbrTaxi++;
                    break;
                case "Metro":
                    NbrMetro++;
                    break;
                case "Bus":
                    NbrBus++;
                    break;
            }
        }
        
    }
}
