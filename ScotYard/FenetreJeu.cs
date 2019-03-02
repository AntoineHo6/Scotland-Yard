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

        // Le tour du detective
        int detecTurn = 1;

        // Le thread qui indique les chemins possibles
        Thread blinkThread;
        List<int> lstPosRouteBtn = new List<int>();

        String transChoisi;

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public FenetreJeu() {
            InitializeComponent();
            InitialiserBoutons();

            // Ajoute fonction a tous les boutons.
            for (int i = 0; i < _listeBoutons.Count; i++) {
                _listeBoutons[i].Click += (s, e) => { btnCaseClick(s, e); };
            }

            ScotYard.Graphe.Case.CreerCases();

            setup();

            // MR X BOUGE

            paintDetecPos();
            updateDetecGrpBox();

            // TEMP

            MrX mrX = new MrX(1);
            //Transports? transportVoleur;
            //bool? blackTicketBool;
            //int a = ScotAI.Case.ProchaineCaseVoleur(false, 1, 3, 3, 3, 3, out transportVoleur, out blackTicketBool);
            //Console.WriteLine("Voleur a bouger de 1 a " + a + " avec " + transportVoleur.Value);
        }

        /// <summary>
        ///     Initialise les objets joueurs et set Disabled tous les boutons.
        /// </summary>
        private void setup() {
            // Par defaut, tout les boutons sont disabled
            for (int i = 0; i < _listeBoutons.Count; i++) {
                _listeBoutons[i].Enabled = false;
                _listeBoutons[i].BackColor = Color.Transparent;
            }
            
            List<int> exclude = new List<int>();
            int[] caseInitiales = new int[4];       // Contient les positions initiales des detectives et de Mr.X.

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
            // temp
            //listeDetec.Add(new Detective("Detective 1", caseInitiales[0], Color.Maroon));
            listeDetec.Add(new Detective("Detective 1", 1, Color.Maroon));

            // listeDetec.Add(new Detective("Detective 2", caseInitiales[1], Color.Green));
            listeDetec.Add(new Detective("Detective 2", 58, Color.Green));

            // listeDetec.Add(new Detective("Detective 3", caseInitiales[2], Color.Turquoise));
            listeDetec.Add(new Detective("Detective 3", 47, Color.Turquoise));

            // updateDetecPos();

            mrX = new MrX(caseInitiales[3]);
        }


        /// <summary>
        ///     Coloris les boutons où les détectives sont présents.
        /// </summary>
        public void paintDetecPos() {
            for (int i = 0; i < listeDetec.Count; i++) {
                int caseActuelleDetec = listeDetec[i].CaseActuelle;
                _listeBoutons[caseActuelleDetec].BackColor = listeDetec[i].Color;

                
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

            // Avant de verifier si des transports sont indisponibles, on les fixe a true.
            btnTaxi.Enabled = true;
            btnTaxi.BackgroundImage = Properties.Resources.taxi_card;
            btnMetro.Enabled = true;
            btnMetro.BackgroundImage = Properties.Resources.metro_card;
            btnBus.Enabled = true;
            btnBus.BackgroundImage = Properties.Resources.bus_card;

            // Disable les boutons de transports inutilisables.
            disableBtnTransIndisponible(Graphe.Case.ListeCases[listeDetec[detecTurn - 1].CaseActuelle].ListeTaxis, btnTaxi, Properties.Resources.taxi_card_disabled, "taxi");
            disableBtnTransIndisponible(Graphe.Case.ListeCases[listeDetec[detecTurn - 1].CaseActuelle].ListeMetros, btnMetro, Properties.Resources.metro_card_disabled, "metro");
            disableBtnTransIndisponible(Graphe.Case.ListeCases[listeDetec[detecTurn - 1].CaseActuelle].ListeBus, btnBus, Properties.Resources.bus_card_disabled, "bus");
        }


        private void disableBtnTransIndisponible(List<Graphe.Case> listeTrans, Button btn, Bitmap disabledImage, string transport) {
            bool indisponible = false;
            int nbrCheminIndisponible = 0;

            for (int i = 0; i < listeTrans.Count; i++) {
                // Vérifie s'il n'y a aucun chemin possible avec le transport a partir de la case courante.
                if (listeTrans.Count == 0) {
                    indisponible = true;
                    break;
                }
                // Vérifie si toutes les cases possibles sont déjà occupées.
                else if (listeTrans[i].Numero == listeDetec[0].CaseActuelle || listeTrans[i].Numero == listeDetec[1].CaseActuelle || listeTrans[i].Numero == listeDetec[2].CaseActuelle) {
                    nbrCheminIndisponible++;
                }
                // Si le joueur n'a aucune carte d'un type du transport, on set disabled cette carte.
                switch (transport) {
                    case "taxi":
                        if (listeDetec[detecTurn - 1].NbrTaxi == 0) {
                            indisponible = true;
                        }
                        break;
                    case "metro":
                        if (listeDetec[detecTurn - 1].NbrMetro == 0) {
                            indisponible = true;
                        }
                        break;
                    case "bus":
                        if (listeDetec[detecTurn - 1].NbrBus == 0) {
                            indisponible = true;
                        }
                        break;
                }
            }

            if (nbrCheminIndisponible == listeTrans.Count) {
                indisponible = true;
            }

            if (indisponible) {
                btn.Enabled = false;
                btn.BackgroundImage = disabledImage;
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

        private void stopBlinkPaths() {
            // arrete le thread
            if (blinkThread != null) {
                blinkThread.Abort();
            }

            // Set disabled les boutons qui clignotaient
            for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                _listeBoutons[lstPosRouteBtn[i]].Enabled = false;
                _listeBoutons[lstPosRouteBtn[i]].BackColor = Color.Transparent;
                _listeBoutons[lstPosRouteBtn[i]].Width = 28;
                _listeBoutons[lstPosRouteBtn[i]].Height = 20;
            }
            
            lstPosRouteBtn.Clear();

        }


        /// <summary>
        ///     Affiche la fenêtre d'options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optsMnuItem_Click(object sender, EventArgs e) {
            FenetreOptions fntOpts = new FenetreOptions(this, listeDetec);
            fntOpts.Show();
        }

        
        /// <summary>
        ///     Fait clignoter les chemins disponibles avec le mode de transport choisi
        /// </summary>
        /// <param name="listeTrans"></param>
        /// <param name="color"></param>
        private void blinkRoutes(List<Graphe.Case> listeTrans, Color color) {
            stopBlinkPaths();

            for (int i = 0; i < listeTrans.Count; i++) {
                int cheminPossible = listeTrans[i].Numero;

                // Si le chemin possible n'est pas une case occupé par un autre detective.
                if (!(cheminPossible == listeDetec[0].CaseActuelle) && !(cheminPossible == listeDetec[1].CaseActuelle) && !(cheminPossible == listeDetec[2].CaseActuelle)) {
                    lstPosRouteBtn.Add(cheminPossible);
                }
            }

            // Set Enabled toutes les cases accessibles par taxi
            for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                _listeBoutons[lstPosRouteBtn[i]].Enabled = true;
                _listeBoutons[lstPosRouteBtn[i]].Width = 36;
                _listeBoutons[lstPosRouteBtn[i]].Height = 28;
            }

            // Clignote les cases accessible par taxi
            blinkThread = new Thread(delegate () {
                while (true) {
                    // Coloris en jaune toutes les cases accessible par taxi
                    for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                        _listeBoutons[lstPosRouteBtn[i]].BackColor = color;
                    }
                    System.Threading.Thread.Sleep(300);

                    // Enleve le jaune de toutes les cases accessible par taxi
                    for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                        _listeBoutons[lstPosRouteBtn[i]].BackColor = Color.Transparent;
                    }
                    System.Threading.Thread.Sleep(300);
                }
            });

            blinkThread.Start();
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en taxi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaxi_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].CaseActuelle;
            blinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeTaxis, Color.Yellow);
            transChoisi = "taxi";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en metro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMetro_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].CaseActuelle;
            blinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeMetros, Color.LightCoral);
            transChoisi = "metro";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBus_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].CaseActuelle;
            blinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeBus, Color.ForestGreen);
            transChoisi = "bus";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Deplace le joueur et change le tour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaseClick(object sender, EventArgs e) {
            stopBlinkPaths();
            int caseChoisi = int.Parse((sender as Button).Text);

            _listeBoutons[listeDetec[detecTurn - 1].CaseActuelle].BackColor = Color.Transparent;

            listeDetec[detecTurn - 1].deplacerCase(caseChoisi);

            listeDetec[detecTurn - 1].decrementeTrans(transChoisi);

            lblStep.Text = "1. Choisissez une carte de transport";

            // Change le tour du detective
            if (detecTurn == 3) {
                detecTurn = 1;
                // mr.x bouge
            }
            else {
                detecTurn++;
            }

            paintDetecPos();
            updateDetecGrpBox();
        }
    }
}


// TODO: Mettre en fonction qui change la couleur du texte en noir ou blanc 
// TODO: 


///
/// Change la couleur du text pour qu'il soit visible.
///
//int rgbSum = listeDetec[i].Color.R + listeDetec[i].Color.G + listeDetec[i].Color.B;

//// La couleur du detective est trop Clair
//if (rgbSum > 382) {
//    _listeBoutons[caseActuelleDetec].ForeColor = Color.Black;
//}
//// La couleur du detective est trop Sombre
//else {
//    _listeBoutons[caseActuelleDetec].ForeColor = Color.White;

//}
