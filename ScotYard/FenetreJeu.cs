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
        private static Random rnd;
        List<Button> _listeBoutons = new List<Button>();
        List<PictureBox> listePicBox = new List<PictureBox>();      // Permet l'acces aux pictureBox.
        List<int> listeTourRevele = new List<int>();

        List<Detective> listeDetec = new List<Detective>();
        MrX mrX;

        int detecTurn = 1;      // Le tour du detective
        int gameTurn = 1;       // Tour du jeu global

        // Le thread qui indique les chemins possibles
        Thread blinkRouteThread;
        List<int> lstPosRouteBtn = new List<int>();

        Thread blinkMrXThread;

        String transChoisi;     // Le transport choisi par le detective


        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public FenetreJeu() {
            InitializeComponent();
            InitialiserBoutons();
            InitialiserPictureBoxListe();
            InitialiseTourReveleListe();

            ScotYard.Graphe.Case.CreerCases();

            setup();

            mrXDeplace();

            paintDetecPos();
            updateDetecGrpBox();

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

            rnd = new Random();

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
            listeDetec.Add(new Detective("Detective 1", caseInitiales[0], Color.Maroon, 1));
            listeDetec[0].IsFirstInTurn = true;
            //listeDetec.Add(new Detective("Detective 1", 20, Color.Maroon));

            listeDetec.Add(new Detective("Detective 2", caseInitiales[1], Color.Green, 2));
            //listeDetec.Add(new Detective("Detective 2", 19, Color.Green));


            // listeDetec.Add(new Detective("Detective 3", caseInitiales[2], Color.Turquoise, 3));
            listeDetec.Add(new Detective("Detective 3", 20, Color.Turquoise, 3));
            listeDetec[2].IsLastInTurn = true;

            //listeDetec[2].NbrTaxi = 1;
            //listeDetec[2].NbrMetro = 2;
            //listeDetec[2].NbrBus = 2;

            // temp
            //listeDetec[1].NbrTaxi = 100;
            //listeDetec[1].NbrMetro = 100;
            //listeDetec[1].NbrBus = 100;

            mrX = new MrX(caseInitiales[3]);
            //mrX = new MrX(1);
        }


        private void InitialiserPictureBoxListe() {
            // Pour que l'index commence a 1
            listePicBox.Add(null);
            listePicBox.Add(picBoxTurn1);
            listePicBox.Add(picBoxTurn2);
            listePicBox.Add(picBoxTurn3);
            listePicBox.Add(picBoxTurn4);
            listePicBox.Add(picBoxTurn5);
            listePicBox.Add(picBoxTurn6);
            listePicBox.Add(picBoxTurn7);
            listePicBox.Add(picBoxTurn8);
            listePicBox.Add(picBoxTurn9);
            listePicBox.Add(picBoxTurn10);
            listePicBox.Add(picBoxTurn11);
            listePicBox.Add(picBoxTurn12);
            listePicBox.Add(picBoxTurn13);
            listePicBox.Add(picBoxTurn14);
            listePicBox.Add(picBoxTurn15);
            listePicBox.Add(picBoxTurn16);
            listePicBox.Add(picBoxTurn17);
            listePicBox.Add(picBoxTurn18);
            listePicBox.Add(picBoxTurn19);
            listePicBox.Add(picBoxTurn20);
            listePicBox.Add(picBoxTurn21);
            listePicBox.Add(picBoxTurn22);
        }


        private void mrXDeplace() {
            Transports? transport;
            bool? blackTicketBool;

            mrX.deplacerCase(listeDetec[0].CaseActuelle, listeDetec[1].CaseActuelle, listeDetec[2].CaseActuelle, out transport, out blackTicketBool);
            mrX.decrementeTrans(transport.ToString());

            bool newBlackTicketBool = blackTicketBool ?? false;

            if (newBlackTicketBool) {
                mrX.NbrBlack--;
            }

            updateMrXBoard(transport.ToString(), newBlackTicketBool);
        }

        /// <summary>
        ///     Clignote la position de Mr.X
        /// </summary>
        private void ReveleMrX() {
            _listeBoutons[mrX.CaseActuelle].Width = 36;
            _listeBoutons[mrX.CaseActuelle].Height = 28;

            blinkMrXThread = new Thread(delegate () {
                while (true) {
                    _listeBoutons[mrX.CaseActuelle].BackColor = Color.Black;
                    System.Threading.Thread.Sleep(300);
                    _listeBoutons[mrX.CaseActuelle].BackColor = Color.Transparent;
                    System.Threading.Thread.Sleep(300);
                }
            });

            blinkMrXThread.Start();
        }


        /// <summary>
        ///     Arrete le clignotement de Mr.X
        /// </summary>
        private void cacheMrX() {
            if (blinkMrXThread != null) {
                blinkMrXThread.Abort();
            }
            _listeBoutons[mrX.CaseActuelle].Width = 28;
            _listeBoutons[mrX.CaseActuelle].Height = 20;
            _listeBoutons[mrX.CaseActuelle].BackColor = Color.Transparent;
        }


        private void updateMrXBoard(String transport, bool newBlackTicketBool) {
            if (!newBlackTicketBool) {
                switch (transport) {
                    case "Taxi":
                        listePicBox[gameTurn].BackgroundImage = Properties.Resources.taxi_card;
                        break;
                    case "Metro":
                        listePicBox[gameTurn].BackgroundImage = Properties.Resources.metro_card;
                        break;
                    case "Bus":
                        listePicBox[gameTurn].BackgroundImage = Properties.Resources.bus_card;
                        break;
                }
            }
            else {
                listePicBox[gameTurn].BackgroundImage = Properties.Resources.black_ticket;
            }

        }


        private void InitialiseTourReveleListe() {
            listeTourRevele.Add(3);
            listeTourRevele.Add(8);
            listeTourRevele.Add(13);
            listeTourRevele.Add(18);
            listeTourRevele.Add(22);
        }


        /// <summary>
        ///     Coloris les boutons où les détectives sont présents.
        /// </summary>
        public void paintDetecPos() {
            for (int i = 0; i < listeDetec.Count; i++) {
                int caseDetec = listeDetec[i].CaseActuelle;
                _listeBoutons[caseDetec].BackColor = listeDetec[i].Color;

                _listeBoutons[caseDetec].Enabled = true;

                int rgbSum = listeDetec[i].Color.R + listeDetec[i].Color.G + listeDetec[i].Color.B;
                // La couleur du detective est trop Clair
                if (rgbSum > 382) {
                    _listeBoutons[caseDetec].ForeColor = Color.Black;
                }
                // La couleur du detective est trop Sombre
                else {
                    _listeBoutons[caseDetec].ForeColor = Color.White;
                }
            }
        }


        /// <summary>
        ///     Update le groupBox des detectives pour correspondre aux informations du detective en jeu.
        /// </summary>
        public void updateDetecGrpBox() {
            // temp
            Console.WriteLine(detecTurn);
            Detective detec = listeDetec[detecTurn - 1];

            grpBoxDetec.Text = detec.Nom;
            grpBoxDetec.BackColor = detec.Color;

            lblNbrTaxi.Text = "x " + detec.NbrTaxi.ToString();
            lblNbrMetro.Text = "x " + detec.NbrMetro.ToString();
            lblNbrBus.Text = "x " + detec.NbrBus.ToString();

            lblCaseAct.Text = "Case : " + detec.CaseActuelle.ToString();

            int rgbSum = detec.Color.R + detec.Color.G + detec.Color.B;
            // La couleur du detective est trop Clair
            if (rgbSum > 382) {
                grpBoxDetec.ForeColor = Color.Black;
            }
            // La couleur du detective est trop Sombre
            else {
                grpBoxDetec.ForeColor = Color.White;
            }

            // Avant de verifier si des transports sont indisponibles, on les fixe a true.
            btnTaxi.Enabled = true;
            btnTaxi.BackgroundImage = Properties.Resources.taxi_card;

            btnMetro.Enabled = true;
            btnMetro.BackgroundImage = Properties.Resources.metro_card;

            btnBus.Enabled = true;
            btnBus.BackgroundImage = Properties.Resources.bus_card;

            // Disable les boutons de transports inutilisables.
            disableBtnTransIndisponible(Graphe.Case.ListeCases[detec.CaseActuelle].ListeTaxis, btnTaxi, Properties.Resources.taxi_card_disabled, "Taxi");
            disableBtnTransIndisponible(Graphe.Case.ListeCases[detec.CaseActuelle].ListeMetros, btnMetro, Properties.Resources.metro_card_disabled, "Metro");
            disableBtnTransIndisponible(Graphe.Case.ListeCases[detec.CaseActuelle].ListeBus, btnBus, Properties.Resources.bus_card_disabled, "Bus");

            if (!btnTaxi.Enabled && !btnMetro.Enabled && !btnBus.Enabled) {
                // temp 
                Console.WriteLine("AAAAAAAHHHHHHHHHHHH");
                Console.WriteLine("Detective " + detec.IdNum + " n'est plus capable de bouger");
                if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                    ecranDefaite();
                    return;
                }

                // PEUT POTENTIELLEMENT REMPLACER LAUTRE CHECK QUE TT LE MONDE EST BLOQUE
                // if 2 in 3 detectives are blocked
                int nbrDetecBloque = 0;
                for (int i = 0; i < listeDetec.Count; i++) {
                    if (listeDetec[i].EstBloque) {
                        nbrDetecBloque++;
                    }
                }

                // Change le tour du detective et mrX se deplace
                if ((detec.IsLastInTurn || detecTurn == 3) && nbrDetecBloque > 1) {

                    // Le tour commence par celui qui est premier
                    for (int i = 0; i < listeDetec.Count; i++) {
                        if (listeDetec[i].IsFirstInTurn) {
                            detecTurn = listeDetec[i].IdNum;
                        }
                    }

                    gameTurn++;
                    lblTour.Text = "Tour: " + gameTurn;

                    // Declignote Mr.X pendant les tours normaux
                    if (gameTurn < listeTourRevele[0]) {
                        cacheMrX();
                    }

                    Console.WriteLine("Mr. X se deplace");
                    mrXDeplace();

                    if (gameTurn == listeTourRevele[0]) {
                        ReveleMrX();
                        listeTourRevele.Remove(gameTurn);
                    }
                }
                else {
                    detecTurn++;
                }

                // refactor
                if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                    detec.IsLastInTurn = true;
                    ecranDefaite();
                }

                updateDetecGrpBox();
            }
        }


        /// <summary>
        ///     Disable les boutons de transports des detectives s'il sont pas disponibles
        /// </summary>
        /// <param name="listeTrans"></param>
        /// <param name="btn"></param>
        /// <param name="disabledImage"></param>
        /// <param name="transport"></param>
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
                    case "Taxi":
                        if (listeDetec[detecTurn - 1].NbrTaxi == 0) {
                            indisponible = true;
                        }
                        break;
                    case "Metro":
                        if (listeDetec[detecTurn - 1].NbrMetro == 0) {
                            indisponible = true;
                        }
                        break;
                    case "Bus":
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

            // Ajoute fonction a tous les boutons.
            for (int i = 0; i < _listeBoutons.Count; i++) {
                _listeBoutons[i].Click += (s, e) => { detecDeplace(s, e); };
            }
        }


        /// <summary>
        ///     Arrete le thread qui fait clignoter les chemins possibles et vide la liste des chemins possibles.
        /// </summary>
        private void stopBlinkPaths() {
            // arrete le thread
            if (blinkRouteThread != null) {
                try {
                    blinkRouteThread.Abort();
                }
                catch (System.Threading.ThreadAbortException) {

                }

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

            // Set Enabled toutes les cases accessibles
            for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                _listeBoutons[lstPosRouteBtn[i]].Enabled = true;
                _listeBoutons[lstPosRouteBtn[i]].Width = 36;
                _listeBoutons[lstPosRouteBtn[i]].Height = 28;
            }

            // Clignote les cases accessibles
            blinkRouteThread = new Thread(delegate () {
                while (true) {
                    // Coloris en jaune toutes les cases accessibles
                    for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                        _listeBoutons[lstPosRouteBtn[i]].BackColor = color;
                    }
                    System.Threading.Thread.Sleep(300);

                    // Enleve le jaune de toutes les cases accessibles
                    for (int i = 0; i < lstPosRouteBtn.Count; i++) {
                        _listeBoutons[lstPosRouteBtn[i]].BackColor = Color.Transparent;
                    }
                    System.Threading.Thread.Sleep(300);
                }
            });

            blinkRouteThread.Start();
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en taxi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaxi_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].CaseActuelle;
            blinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeTaxis, Color.Yellow);
            transChoisi = "Taxi";
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
            transChoisi = "Metro";
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
            transChoisi = "Bus";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Deplace le joueur et change le tour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detecDeplace(object sender, EventArgs e) {
            stopBlinkPaths();

            int caseChoisi = int.Parse((sender as Button).Text);

            if (caseChoisi == listeDetec[0].CaseActuelle || caseChoisi == listeDetec[1].CaseActuelle || caseChoisi == listeDetec[2].CaseActuelle) {
                return;
            }
            else {
                Detective detec = listeDetec[detecTurn - 1];

                // Decoloris le bouton ou le detective se retrouve
                _listeBoutons[detec.CaseActuelle].BackColor = Color.Transparent;
                _listeBoutons[detec.CaseActuelle].Enabled = false;
                _listeBoutons[detec.CaseActuelle].ForeColor = Color.Black;

                detec.deplacerCase(caseChoisi);

                if (detec.CaseActuelle == mrX.CaseActuelle) {
                    ecranVictoire();
                    return;
                }

                detec.decrementeTrans(transChoisi);
                // Donne a Mr.X le transport utilisé par le detective
                mrX.incrementeTrans(transChoisi);

                lblStep.Text = "1. Choisissez une carte de transport";

                makeDetecStuck(detec);

                Console.WriteLine("Detective " + detec.IdNum + " IsFirstTurn?: " + detec.IsFirstInTurn);
                Console.WriteLine("Detective " + detec.IdNum + " IsLastInTurn?: " + detec.IsLastInTurn);

                // Change le tour du detective et mrX se deplace
                if (detec.IsLastInTurn || detecTurn == 3) {
                    // Le tour commence par celui qui est premier
                    for (int i = 0; i < listeDetec.Count; i++) {
                        if (listeDetec[i].IsFirstInTurn) {
                            detecTurn = listeDetec[i].IdNum;
                        }
                    }

                    gameTurn++;
                    lblTour.Text = "Tour: " + gameTurn;

                    // Declignote Mr.X pendant les tours normaux
                    if (gameTurn < listeTourRevele[0]) {
                        cacheMrX();
                    }

                    mrXDeplace();

                    if (gameTurn == listeTourRevele[0]) {
                        ReveleMrX();
                        listeTourRevele.Remove(gameTurn);
                    }
                }
                else {
                    detecTurn++;
                }

                paintDetecPos();
                updateDetecGrpBox();

                if (gameTurn == 22) {
                    ecranDefaite();
                }
            }
        }


        private void makeDetecStuck(Detective detec) {
            bool taxiBloque = false;
            bool metroBloque = false;
            bool busBloque = false;

            if (detec.NbrTaxi == 0 || Graphe.Case.ListeCases[detec.CaseActuelle].ListeTaxis.Count == 0) {
                taxiBloque = true;
            }
            if (detec.NbrMetro == 0 || Graphe.Case.ListeCases[detec.CaseActuelle].ListeMetros.Count == 0) {
                metroBloque = true;
            }
            if (detec.NbrBus == 0 || Graphe.Case.ListeCases[detec.CaseActuelle].ListeBus.Count == 0) {
                busBloque = true;
            }

            if (taxiBloque && metroBloque && busBloque) {
                detec.EstBloque = true;
            }


            if (detec.IsLastInTurn && detec.EstBloque) {
                detec.IsLastInTurn = false;

                // A RETRAVIALLER
                switch (detec.IdNum) {
                    case 1:
                        listeDetec[1].IsLastInTurn = true;
                        break;
                    case 2:
                        listeDetec[0].IsLastInTurn = true;
                        break;
                    case 3:
                        if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                            detec.IsLastInTurn = true;
                            ecranDefaite();
                        }
                        else if (!listeDetec[1].EstBloque) {
                            listeDetec[1].IsLastInTurn = true;
                        }
                        else {
                            listeDetec[0].IsLastInTurn = true;
                        }
                        break;
                }
            }
            else if (detec.IsFirstInTurn && detec.EstBloque) {
                detec.IsFirstInTurn = false;

                switch (detec.IdNum) {
                    case 1:
                        if (!listeDetec[1].EstBloque) {
                            listeDetec[1].IsFirstInTurn = true;
                        }
                        else {
                            listeDetec[2].IsFirstInTurn = true;
                        }
                        break;
                    case 2:
                        listeDetec[2].IsFirstInTurn = true;
                        break;
                    case 3:
                        listeDetec[1].IsFirstInTurn = true;
                        break;

                }
            }

        }


        private void ecranVictoire() {
            lblVictoire.Visible = true;
            // add celebration flairs
        }


        private void ecranDefaite() {
            lblDefaite.Visible = true;
        }

    }
}



/// TODO MAJEUR: 
/// TODO: quoi faire quand mr.X se deplace par bateau?
/// TODO: ne doit pas recommener le tour par detective 1, mais par detective avec le bool
/// TODO: refactor code
/// TODO: make error providers in options
/// TODO: remplacer les for loop i avec des foreachs


/// TODO MINEUR: 
/// TODO: paint buttons to correspond to the available transportation modes.
/// TODO: function parameters comments
/// TODO: Mettre dans une fonction le code qui change la couleur du texte en noir ou blanc 
/// TODO: Rendre plus visible les cases ou les detectives se retrouvent.
/// TODO: Dans l'ecran de victoire ou de defaite, ne pas permettre l'interaction avec les controles.
/// TODO: Ajouter des "next detective peek"