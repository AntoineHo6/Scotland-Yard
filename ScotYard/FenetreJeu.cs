using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScotYard.Graphe;
using ScotYard.Logique;
using ScotAI;
using System.Threading;

namespace ScotYard {


    public partial class FenetreJeu : Form {
        // Liste des boutons du jeu
        List<Button> _listeBoutons = new List<Button>();

        // Les joueurs
        List<Detective> listeDetec = new List<Detective>();
        MrX mrX;

        int detecTurn = 1;

        List<Thread> listeBlinkBtn = new List<Thread>(); 

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public FenetreJeu() {
            InitializeComponent();
            InitialiserBoutons();
            ScotYard.Graphe.Case.CreerCases();
            
            setup();
            paintDetecPos();
            updateDetecGrpBox();

            

            // TEMP
            //Detective detective1 = new Detective("Detective 1", 1);
            //Console.WriteLine(detective1.CaseActuelle);
            //detective1.test();

            //MrX mrX = new MrX(1);
            //Transports? transportVoleur;
            //bool? blackTicketBool;
            //int a = ScotAI.Case.ProchaineCaseVoleur(false, 1, 3, 3, 3, 3, out transportVoleur, out blackTicketBool);
            //Console.WriteLine("Voleur a bouger de 1 a " + a + " avec " + transportVoleur.Value);
        }

        /// <summary>
        ///     Initialise les objets joueurs
        /// </summary>
        private void setup() {
            List<int> exclude = new List<int>();
            int[] caseInitiales = new int[4];

            Random rnd = new Random();

            // Génère des ints aléatoires entre 1 et 199 inclusif sans dupliques
            int randNumCase;
            for (int i = 0; i < caseInitiales.Length; i++) {
                while (true) {
                    randNumCase = rnd.Next(1, 199);

                    if (exclude.Contains(randNumCase)) {
                        continue;
                    }
                    else {
                        exclude.Add(randNumCase);
                        break;
                    }
                }
                caseInitiales[i] = randNumCase;
            }

            // Création joueurs
            listeDetec.Add(new Detective("Detective 1", caseInitiales[0], Color.Maroon));
            listeDetec.Add(new Detective("Detective 2", caseInitiales[1], Color.Green));
            listeDetec.Add(new Detective("Detective 3", caseInitiales[2], Color.Turquoise));
            
            mrX = new MrX(caseInitiales[3]);
        }


        public void paintDetecPos() {
            for (int i = 0; i < listeDetec.Count; i++) {
                int caseActuelleDetec = listeDetec[i].CaseActuelle;
                _listeBoutons[caseActuelleDetec].BackColor = listeDetec[i].Color;

                int rgbSum = listeDetec[i].Color.R + listeDetec[i].Color.G + listeDetec[i].Color.B;
                // La couleur du detective est trop Clair
                if (rgbSum > 382) {
                    _listeBoutons[caseActuelleDetec].ForeColor = Color.Black;
                }
                // La couleur du detective est trop Sombre
                else {
                    _listeBoutons[caseActuelleDetec].ForeColor = Color.White;
                }
            }
        }


        public void updateDetecGrpBox() {
            Detective detectiveCourant = listeDetec[detecTurn - 1];

            grpBoxDetec.Text = detectiveCourant.Nom;
            grpBoxDetec.BackColor = detectiveCourant.Color;

            lblNbrTaxi.Text = "x " + detectiveCourant.NbrTaxi.ToString();
            lblNbrMetro.Text = "x " + detectiveCourant.NbrMetro.ToString();
            lblNbrBus.Text = "x " + detectiveCourant.NbrBus.ToString();

            lblCaseAct.Text = "Case Actuelle: " + detectiveCourant.CaseActuelle.ToString();


            int rgbSum = detectiveCourant.Color.R + detectiveCourant.Color.G + detectiveCourant.Color.B;
            // La couleur du detective est trop Clair
            if (rgbSum > 382) {
                grpBoxDetec.ForeColor = Color.Black;
            }
            // La couleur du detective est trop Sombre
            else {
                grpBoxDetec.ForeColor = Color.White;
            }
        }


        /// <summary>
        /// Insertion des boutons dans une liste
        /// </summary>
        private void InitialiserBoutons() {
            _listeBoutons.Add(new Button()); // Bouton bidon
            _listeBoutons.Add(btn1);
            _listeBoutons.Add(btn2);
            _listeBoutons.Add(btn3);
            _listeBoutons.Add(btn4);
            _listeBoutons.Add(btn5);
            _listeBoutons.Add(btn6);
            _listeBoutons.Add(btn7);
            _listeBoutons.Add(btn8);
            _listeBoutons.Add(btn9);
            _listeBoutons.Add(btn10);
            _listeBoutons.Add(btn11);
            _listeBoutons.Add(btn12);
            _listeBoutons.Add(btn13);
            _listeBoutons.Add(btn14);
            _listeBoutons.Add(btn15);
            _listeBoutons.Add(btn16);
            _listeBoutons.Add(btn17);
            _listeBoutons.Add(btn18);
            _listeBoutons.Add(btn19);
            _listeBoutons.Add(btn20);
            _listeBoutons.Add(btn21);
            _listeBoutons.Add(btn22);
            _listeBoutons.Add(btn23);
            _listeBoutons.Add(btn24);
            _listeBoutons.Add(btn25);
            _listeBoutons.Add(btn26);
            _listeBoutons.Add(btn27);
            _listeBoutons.Add(btn28);
            _listeBoutons.Add(btn29);
            _listeBoutons.Add(btn30);
            _listeBoutons.Add(btn31);
            _listeBoutons.Add(btn32);
            _listeBoutons.Add(btn33);
            _listeBoutons.Add(btn34);
            _listeBoutons.Add(btn35);
            _listeBoutons.Add(btn36);
            _listeBoutons.Add(btn37);
            _listeBoutons.Add(btn38);
            _listeBoutons.Add(btn39);
            _listeBoutons.Add(btn40);
            _listeBoutons.Add(btn41);
            _listeBoutons.Add(btn42);
            _listeBoutons.Add(btn43);
            _listeBoutons.Add(btn44);
            _listeBoutons.Add(btn45);
            _listeBoutons.Add(btn46);
            _listeBoutons.Add(btn47);
            _listeBoutons.Add(btn48);
            _listeBoutons.Add(btn49);
            _listeBoutons.Add(btn50);
            _listeBoutons.Add(btn51);
            _listeBoutons.Add(btn52);
            _listeBoutons.Add(btn53);
            _listeBoutons.Add(btn54);
            _listeBoutons.Add(btn55);
            _listeBoutons.Add(btn56);
            _listeBoutons.Add(btn57);
            _listeBoutons.Add(btn58);
            _listeBoutons.Add(btn59);
            _listeBoutons.Add(btn60);
            _listeBoutons.Add(btn61);
            _listeBoutons.Add(btn62);
            _listeBoutons.Add(btn63);
            _listeBoutons.Add(btn64);
            _listeBoutons.Add(btn65);
            _listeBoutons.Add(btn66);
            _listeBoutons.Add(btn67);
            _listeBoutons.Add(btn68);
            _listeBoutons.Add(btn69);
            _listeBoutons.Add(btn70);
            _listeBoutons.Add(btn71);
            _listeBoutons.Add(btn72);
            _listeBoutons.Add(btn73);
            _listeBoutons.Add(btn74);
            _listeBoutons.Add(btn75);
            _listeBoutons.Add(btn76);
            _listeBoutons.Add(btn77);
            _listeBoutons.Add(btn78);
            _listeBoutons.Add(btn79);
            _listeBoutons.Add(btn80);
            _listeBoutons.Add(btn81);
            _listeBoutons.Add(btn82);
            _listeBoutons.Add(btn83);
            _listeBoutons.Add(btn84);
            _listeBoutons.Add(btn85);
            _listeBoutons.Add(btn86);
            _listeBoutons.Add(btn87);
            _listeBoutons.Add(btn88);
            _listeBoutons.Add(btn89);
            _listeBoutons.Add(btn90);
            _listeBoutons.Add(btn91);
            _listeBoutons.Add(btn92);
            _listeBoutons.Add(btn93);
            _listeBoutons.Add(btn94);
            _listeBoutons.Add(btn95);
            _listeBoutons.Add(btn96);
            _listeBoutons.Add(btn97);
            _listeBoutons.Add(btn98);
            _listeBoutons.Add(btn99);
            _listeBoutons.Add(btn100);
            _listeBoutons.Add(btn101);
            _listeBoutons.Add(btn102);
            _listeBoutons.Add(btn103);
            _listeBoutons.Add(btn104);
            _listeBoutons.Add(btn105);
            _listeBoutons.Add(btn106);
            _listeBoutons.Add(btn107);
            _listeBoutons.Add(btn108);
            _listeBoutons.Add(btn109);
            _listeBoutons.Add(btn110);
            _listeBoutons.Add(btn111);
            _listeBoutons.Add(btn112);
            _listeBoutons.Add(btn113);
            _listeBoutons.Add(btn114);
            _listeBoutons.Add(btn115);
            _listeBoutons.Add(btn116);
            _listeBoutons.Add(btn117);
            _listeBoutons.Add(btn118);
            _listeBoutons.Add(btn119);
            _listeBoutons.Add(btn120);
            _listeBoutons.Add(btn121);
            _listeBoutons.Add(btn122);
            _listeBoutons.Add(btn123);
            _listeBoutons.Add(btn124);
            _listeBoutons.Add(btn125);
            _listeBoutons.Add(btn126);
            _listeBoutons.Add(btn127);
            _listeBoutons.Add(btn128);
            _listeBoutons.Add(btn129);
            _listeBoutons.Add(btn130);
            _listeBoutons.Add(btn131);
            _listeBoutons.Add(btn132);
            _listeBoutons.Add(btn133);
            _listeBoutons.Add(btn134);
            _listeBoutons.Add(btn135);
            _listeBoutons.Add(btn136);
            _listeBoutons.Add(btn137);
            _listeBoutons.Add(btn138);
            _listeBoutons.Add(btn139);
            _listeBoutons.Add(btn140);
            _listeBoutons.Add(btn141);
            _listeBoutons.Add(btn142);
            _listeBoutons.Add(btn143);
            _listeBoutons.Add(btn144);
            _listeBoutons.Add(btn145);
            _listeBoutons.Add(btn146);
            _listeBoutons.Add(btn147);
            _listeBoutons.Add(btn148);
            _listeBoutons.Add(btn149);
            _listeBoutons.Add(btn150);
            _listeBoutons.Add(btn151);
            _listeBoutons.Add(btn152);
            _listeBoutons.Add(btn153);
            _listeBoutons.Add(btn154);
            _listeBoutons.Add(btn155);
            _listeBoutons.Add(btn156);
            _listeBoutons.Add(btn157);
            _listeBoutons.Add(btn158);
            _listeBoutons.Add(btn159);
            _listeBoutons.Add(btn160);
            _listeBoutons.Add(btn161);
            _listeBoutons.Add(btn162);
            _listeBoutons.Add(btn163);
            _listeBoutons.Add(btn164);
            _listeBoutons.Add(btn165);
            _listeBoutons.Add(btn166);
            _listeBoutons.Add(btn167);
            _listeBoutons.Add(btn168);
            _listeBoutons.Add(btn169);
            _listeBoutons.Add(btn170);
            _listeBoutons.Add(btn171);
            _listeBoutons.Add(btn172);
            _listeBoutons.Add(btn173);
            _listeBoutons.Add(btn174);
            _listeBoutons.Add(btn175);
            _listeBoutons.Add(btn176);
            _listeBoutons.Add(btn177);
            _listeBoutons.Add(btn178);
            _listeBoutons.Add(btn179);
            _listeBoutons.Add(btn180);
            _listeBoutons.Add(btn181);
            _listeBoutons.Add(btn182);
            _listeBoutons.Add(btn183);
            _listeBoutons.Add(btn184);
            _listeBoutons.Add(btn185);
            _listeBoutons.Add(btn186);
            _listeBoutons.Add(btn187);
            _listeBoutons.Add(btn188);
            _listeBoutons.Add(btn189);
            _listeBoutons.Add(btn190);
            _listeBoutons.Add(btn191);
            _listeBoutons.Add(btn192);
            _listeBoutons.Add(btn193);
            _listeBoutons.Add(btn194);
            _listeBoutons.Add(btn195);
            _listeBoutons.Add(btn196);
            _listeBoutons.Add(btn197);
            _listeBoutons.Add(btn198);
            _listeBoutons.Add(btn199);
        }

        private void optsMnuItem_Click(object sender, EventArgs e) {
            FenetreOptions fntOpts = new FenetreOptions(this, listeDetec);
            fntOpts.Show();
        }

        private void btnTaxi_Click(object sender, EventArgs e) {
            Detective detecCourant = listeDetec[detecTurn - 1];
            int caseActuelle = detecCourant.CaseActuelle;

            // Clear current running blinking threads in list
            for (int i = 0; i < listeBlinkBtn.Count; i++) {
                listeBlinkBtn[i].Abort();
            }
            listeBlinkBtn.Clear();

            // Loop through possible routes for taxi
            for (int i = 0; i < Graphe.Case.ListeCases[caseActuelle].ListeTaxis.Count; i++) {
                int casePossibleTaxi = Graphe.Case.ListeCases[caseActuelle].ListeTaxis[i].Numero;
                
                // Creates list of threads for blinking buttons
                Thread blinkThread = new Thread(delegate () {
                    while (true) {
                        _listeBoutons[casePossibleTaxi].BackColor = Color.Yellow;
                        System.Threading.Thread.Sleep(500);
                        _listeBoutons[casePossibleTaxi].BackColor = Color.Transparent;
                        System.Threading.Thread.Sleep(500);
                    }
                });

                listeBlinkBtn.Add(blinkThread);
            }

            // Execute all the threads
            for (int i = 0; i < listeBlinkBtn.Count; i++) {
                listeBlinkBtn[i].Start();
            }
        }
    }
}


// TODO: NE PAS TAXIER SUR LA CASE OU QUELQU UN DAUTRE SE RETROUVE
// TODO: 