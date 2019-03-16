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
        List<PictureBox> _listePicBox = new List<PictureBox>();      // Permet l'acces aux pictureBox.
        List<int> listeTourRevele = new List<int>();

        List<Detective> listeDetec = new List<Detective>();
        MrX mrX;

        int detecTurn = 1;      // Le tour du detective. À 3, on revient a 1 si c'est le cas.
        int gameTurn = 1;       // Tour du jeu global

        // Le thread qui indique les chemins possibles
        Thread blinkRouteThread;
        List<int> lstPosRouteBtn = new List<int>();

        Thread blinkMrXThread;

        String transChoisi;     // Le transport choisi par le detective en jeu


        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public FenetreJeu() {
            InitializeComponent();
            InitialiserBoutons();
            InitialiserPictureBoxListe();
            InitialiseTourReveleListe();

            ScotYard.Graphe.Case.CreerCases();

            Setup();
            MrXDeplace();
            PaintDetecPos();
            UpdateDetecGrpBox();
        }


        /// <summary>
        ///     Initialise les objets joueurs et set Disabled tous les boutons.
        /// </summary>
        private void Setup() {
            // Par defaut, tout les boutons sont disabled
            for (int i = 0; i < _listeBoutons.Count; i++) {
                _listeBoutons[i].Enabled = false;
                _listeBoutons[i].BackColor = Color.Transparent;
            }
            
            List<int> exclude = new List<int>();    // Contient les cases deja occupe par les autres joueurs
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
            listeDetec[0].estPremier = true;

            listeDetec.Add(new Detective("Detective 2", caseInitiales[1], Color.Green, 2));

            listeDetec.Add(new Detective("Detective 3", caseInitiales[2], Color.SteelBlue, 3));
            listeDetec[2].estDernier = true;

            mrX = new MrX(caseInitiales[3]);
        }


        /// <summary>
        /// Insertion des boutons dans une liste.
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
                _listeBoutons[i].Click += (s, e) => { DetecDeplace(s, e); };
            }
        }


        /// <summary>
        ///     Insertion des pictureBoxs dans une liste.
        /// </summary>
        private void InitialiserPictureBoxListe() {
            // Pour que l'index commence a 1
            _listePicBox.Add(null);
            _listePicBox.Add(picBoxTurn1);
            _listePicBox.Add(picBoxTurn2);
            _listePicBox.Add(picBoxTurn3);
            _listePicBox.Add(picBoxTurn4);
            _listePicBox.Add(picBoxTurn5);
            _listePicBox.Add(picBoxTurn6);
            _listePicBox.Add(picBoxTurn7);
            _listePicBox.Add(picBoxTurn8);
            _listePicBox.Add(picBoxTurn9);
            _listePicBox.Add(picBoxTurn10);
            _listePicBox.Add(picBoxTurn11);
            _listePicBox.Add(picBoxTurn12);
            _listePicBox.Add(picBoxTurn13);
            _listePicBox.Add(picBoxTurn14);
            _listePicBox.Add(picBoxTurn15);
            _listePicBox.Add(picBoxTurn16);
            _listePicBox.Add(picBoxTurn17);
            _listePicBox.Add(picBoxTurn18);
            _listePicBox.Add(picBoxTurn19);
            _listePicBox.Add(picBoxTurn20);
            _listePicBox.Add(picBoxTurn21);
            _listePicBox.Add(picBoxTurn22);
        }


        /// <summary>
        ///     Contient les tours ou est-ce que Mr.X se revele.
        /// </summary>
        private void InitialiseTourReveleListe() {
            listeTourRevele.Add(3);
            listeTourRevele.Add(8);
            listeTourRevele.Add(13);
            listeTourRevele.Add(18);
            listeTourRevele.Add(22);
        }


        /// <summary>
        ///     Mr. X se deplace et son carnet ajoute le transport qu'il vient d'utiliser au tour global du jeu courant.
        /// </summary>
        private void MrXDeplace() {
            Transports? transport;
            bool? blackTicketBool;

            mrX.deplacerCase(listeDetec[0].Case, listeDetec[1].Case, listeDetec[2].Case, out transport, out blackTicketBool);
            mrX.decrementeTrans(transport.ToString());

            bool newBlackTicketBool = blackTicketBool ?? false;

            if (newBlackTicketBool) {
                mrX.NbrBlack--;
            }

            UpdateMrCarnet(transport.ToString(), newBlackTicketBool);
        }

        /// <summary>
        ///     Clignote la position de Mr.X
        /// </summary>
        private void ReveleMrX() {
            _listeBoutons[mrX.Case].Width = 36;
            _listeBoutons[mrX.Case].Height = 28;

            blinkMrXThread = new Thread(delegate () {
                while (true) {
                    _listeBoutons[mrX.Case].BackColor = Color.Black;
                    System.Threading.Thread.Sleep(300);
                    _listeBoutons[mrX.Case].BackColor = Color.Transparent;
                    System.Threading.Thread.Sleep(300);
                }
            });

            blinkMrXThread.Start();
        }


        /// <summary>
        ///     Arrete le clignotement de Mr.X
        /// </summary>
        private void CacheMrX() {
            if (blinkMrXThread != null) {
                blinkMrXThread.Abort();
            }
            _listeBoutons[mrX.Case].Width = 28;
            _listeBoutons[mrX.Case].Height = 20;
            _listeBoutons[mrX.Case].BackColor = Color.Transparent;
        }


        /// <summary>
        ///     Ajoute la carte joué par Mr.X sur le carnet des tours.
        /// </summary>
        /// <param name="transport"> Le transport utilise par Mr. X </param>
        /// <param name="usedBlackTicket"> Boolean si Mr. X a utilise un black ticket ou pas </param>
        private void UpdateMrCarnet(String transport, bool usedBlackTicket) {
            if (!usedBlackTicket) {
                switch (transport) {
                    case "Taxi":
                        _listePicBox[gameTurn].BackgroundImage = Properties.Resources.taxi_card;
                        break;
                    case "Metro":
                        _listePicBox[gameTurn].BackgroundImage = Properties.Resources.metro_card;
                        break;
                    case "Bus":
                        _listePicBox[gameTurn].BackgroundImage = Properties.Resources.bus_card;
                        break;
                }
            }
            else {
                _listePicBox[gameTurn].BackgroundImage = Properties.Resources.black_ticket;
            }

        }
        

        /// <summary>
        ///     Coloris les boutons où les détectives sont présents.
        /// </summary>
        public void PaintDetecPos() {
            for (int i = 0; i < listeDetec.Count; i++) {
                int caseDetec = listeDetec[i].Case;
                _listeBoutons[caseDetec].BackColor = listeDetec[i].Color;

                _listeBoutons[caseDetec].Enabled = true;
                _listeBoutons[caseDetec].Width = 36;
                _listeBoutons[caseDetec].Height = 28;

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
        public void UpdateDetecGrpBox() {
            Detective detec = listeDetec[detecTurn - 1];

            grpBoxDetec.Text = detec.Nom;
            grpBoxDetec.BackColor = detec.Color;

            lblNbrTaxi.Text = "x " + detec.NbrTaxi.ToString();
            lblNbrMetro.Text = "x " + detec.NbrMetro.ToString();
            lblNbrBus.Text = "x " + detec.NbrBus.ToString();

            lblCaseAct.Text = "Case : " + detec.Case.ToString();

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
            DisableBtnTransIndisponible(Graphe.Case.ListeCases[detec.Case].ListeTaxis, btnTaxi, Properties.Resources.taxi_card_disabled, "Taxi");
            DisableBtnTransIndisponible(Graphe.Case.ListeCases[detec.Case].ListeMetros, btnMetro, Properties.Resources.metro_card_disabled, "Metro");
            DisableBtnTransIndisponible(Graphe.Case.ListeCases[detec.Case].ListeBus, btnBus, Properties.Resources.bus_card_disabled, "Bus");

            if (!btnTaxi.Enabled && !btnMetro.Enabled && !btnBus.Enabled) {
                if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                    EcranDefaite();
                    return;
                }

                // Compte le nombre de detectives bloques.
                int nbrDetecBloque = 0;
                for (int i = 0; i < listeDetec.Count; i++) {
                    if (listeDetec[i].EstBloque) {
                        nbrDetecBloque++;
                    }
                }

                // Change le tour du detective et mrX se deplace
                if ((detec.estDernier || detecTurn == 3) && nbrDetecBloque > 1) {
                    DernierTourRotationLogique();
                }
                else {
                    detecTurn++;
                }

                // refactor
                //if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                //    detec.IsLastInTurn = true;
                //    ecranDefaite();
                //}
                UpdateDetecGrpBox();
            }
        }


        /// <summary>
        ///     Disable les boutons de transports des detectives s'il sont pas disponibles
        /// </summary>
        /// <param name="lstCheminTrans"> La liste des chemin possibles avec le transport choisi </param>
        /// <param name="btn"> Le bouton de taxi, bus ou metro </param>
        /// <param name="disabledImage"> Un copie de l'image du bouton de transport en gris </param>
        /// <param name="transport"> Le nom du transport a verifier s'il est presentement indisponible </param>
        private void DisableBtnTransIndisponible(List<Graphe.Case> lstCheminTrans, Button btn, Bitmap disabledImage, string transport) {
            bool indisponible = false;
            int nbrCheminIndisponible = 0;

            for (int i = 0; i < lstCheminTrans.Count; i++) {
                // Vérifie s'il n'y a aucun chemin possible avec le transport a partir de la case courante.
                if (lstCheminTrans.Count == 0) {
                    indisponible = true;
                    break;
                }
                // Vérifie si toutes les cases possibles sont déjà occupées.
                else if (lstCheminTrans[i].Numero == listeDetec[0].Case || lstCheminTrans[i].Numero == listeDetec[1].Case || lstCheminTrans[i].Numero == listeDetec[2].Case) {
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

            if (nbrCheminIndisponible == lstCheminTrans.Count) {
                indisponible = true;
            }

            if (indisponible) {
                btn.Enabled = false;
                btn.BackgroundImage = disabledImage;
            }
        }
        

        /// <summary>
        ///     Arrete le thread qui fait clignoter les chemins possibles et vide la liste des chemins possibles.
        /// </summary>
        private void StopBlinkPaths() {
            // arrete le thread
            if (blinkRouteThread != null) {
                blinkRouteThread.Abort();

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
        private void OptsMnuItem_Click(object sender, EventArgs e) {
            FenetreOptions fntOpts = new FenetreOptions(this, listeDetec);
            fntOpts.Show();
        }


        /// <summary>
        ///     Fait clignoter les chemins disponibles avec le mode de transport choisi
        /// </summary>
        /// <param name="lstCheminTrans"> La liste des chemin possibles avec le transport choisi </param>
        /// <param name="color"> La couleur associe au transport </param>
        private void BlinkRoutes(List<Graphe.Case> lstCheminTrans, Color color) {
            StopBlinkPaths();

            for (int i = 0; i < lstCheminTrans.Count; i++) {
                int cheminPossible = lstCheminTrans[i].Numero;

                // Si le chemin possible n'est pas une case occupé par un autre detective.
                if (!(cheminPossible == listeDetec[0].Case) && !(cheminPossible == listeDetec[1].Case) && !(cheminPossible == listeDetec[2].Case)) {
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
        private void BtnTaxi_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].Case;
            BlinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeTaxis, Color.Yellow);
            transChoisi = "Taxi";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en metro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMetro_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].Case;
            BlinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeMetros, Color.LightCoral);
            transChoisi = "Metro";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Indique à quelles cases le joueur peut se déplacer en bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBus_Click(object sender, EventArgs e) {
            int caseActuelle = listeDetec[detecTurn - 1].Case;
            BlinkRoutes(Graphe.Case.ListeCases[caseActuelle].ListeBus, Color.ForestGreen);
            transChoisi = "Bus";
            lblStep.Text = "2. Choisissez une route sur la planche";
        }


        /// <summary>
        ///     Deplace le joueur et change le tour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetecDeplace(object sender, EventArgs e) {
            StopBlinkPaths();

            int caseChoisi = int.Parse((sender as Button).Text);

            if (caseChoisi == listeDetec[0].Case || caseChoisi == listeDetec[1].Case || caseChoisi == listeDetec[2].Case) {
                return;
            }
            else {
                Detective detec = listeDetec[detecTurn - 1];

                // Decoloris le bouton ou le detective se retrouve
                _listeBoutons[detec.Case].BackColor = Color.Transparent;
                _listeBoutons[detec.Case].Enabled = false;
                _listeBoutons[detec.Case].ForeColor = Color.Black;
                _listeBoutons[detec.Case].Width = 28;
                _listeBoutons[detec.Case].Height = 20;

                detec.deplacerCase(caseChoisi);

                if (detec.Case == mrX.Case) {
                    EcranVictoire();
                    return;
                }

                detec.decrementeTrans(transChoisi);
                // Donne a Mr.X le transport utilisé par le detective
                mrX.incrementeTrans(transChoisi);

                lblStep.Text = "1. Choisissez une carte de transport";

                VerifieDetecBloque(detec);

                // Change le tour du detective et mrX se deplace
                if (detec.estDernier || detecTurn == 3) {
                    DernierTourRotationLogique();
                }
                else {
                    detecTurn++;
                }

                PaintDetecPos();
                UpdateDetecGrpBox();

                if (gameTurn == 22) {
                    EcranDefaite();
                }
            }
        }


        /// <summary>
        ///     Change le tour au premier detective dans la rotation des tours entres les joueurs et Mr. X se deplace.
        /// </summary>
        private void DernierTourRotationLogique() {
            // Le tour commence par celui qui est premier
            for (int i = 0; i < listeDetec.Count; i++) {
                if (listeDetec[i].estPremier) {
                    detecTurn = listeDetec[i].IdNum;
                }
            }

            gameTurn++;
            lblTour.Text = "Tour: " + gameTurn;

            // Declignote Mr.X pendant les tours normaux
            if (gameTurn < listeTourRevele[0]) {
                CacheMrX();
            }

            MrXDeplace();

            if (gameTurn == listeTourRevele[0]) {
                ReveleMrX();
                listeTourRevele.Remove(gameTurn);
            }
        }


        /// <summary>
        ///     Verifie si le detective en question est bloque en permanence
        /// </summary>
        /// <param name="detec"> Le detective a verifier s'il est bloque en permanence </param>
        private void VerifieDetecBloque(Detective detec) {
            bool taxiBloque = false;
            bool metroBloque = false;
            bool busBloque = false;
            // Verifie si le transport est inutilisable.
            if (detec.NbrTaxi == 0 || Graphe.Case.ListeCases[detec.Case].ListeTaxis.Count == 0) {
                taxiBloque = true;
            }
            if (detec.NbrMetro == 0 || Graphe.Case.ListeCases[detec.Case].ListeMetros.Count == 0) {
                metroBloque = true;
            }
            if (detec.NbrBus == 0 || Graphe.Case.ListeCases[detec.Case].ListeBus.Count == 0) {
                busBloque = true;
            }

            // Verifie si tout les transports sont bloques.
            if (taxiBloque && metroBloque && busBloque) {
                detec.EstBloque = true;
            }

            // Si le detective devenu bloque en permanence etait le dernier dans la rotation 
            // des tours entre les detectives, on donne la qualite d'etre dernier a un autre detective.
            if (detec.estDernier && detec.EstBloque) {
                detec.estDernier = false;

                switch (detec.IdNum) {
                    case 1:
                        listeDetec[1].estDernier = true;
                        break;
                    case 2:
                        listeDetec[0].estDernier = true;
                        break;
                    case 3:
                        if (listeDetec[0].EstBloque && listeDetec[1].EstBloque && listeDetec[2].EstBloque) {
                            detec.estDernier = true;
                            EcranDefaite();
                        }
                        else if (!listeDetec[1].EstBloque) {
                            listeDetec[1].estDernier = true;
                        }
                        else {
                            listeDetec[0].estDernier = true;
                        }
                        break;
                }
            }
            // Si le detective devenu bloque en permanence etait le premier dans la rotation 
            // des tours entre les detectives, on donne la qualite d'etre premier a un autre detective.
            else if (detec.estPremier && detec.EstBloque) {
                detec.estPremier = false;

                switch (detec.IdNum) {
                    case 1:
                        if (!listeDetec[1].EstBloque) {
                            listeDetec[1].estPremier = true;
                        }
                        else {
                            listeDetec[2].estPremier = true;
                        }
                        break;
                    case 2:
                        listeDetec[2].estPremier = true;
                        break;
                    case 3:
                        listeDetec[1].estPremier = true;
                        break;
                }
            }
        }


        /// <summary>
        ///     Affiche la victoire au joueur.
        /// </summary>
        private void EcranVictoire() {
            lblVictoire.Visible = true;
            // add celebration flairs ?
            DisabledAllEnabledBtns();
        }


        /// <summary>
        ///     Affiche la defaite au joueur.
        /// </summary>
        private void EcranDefaite() {
            lblDefaite.Visible = true;
            DisabledAllEnabledBtns();
        }


        /// <summary>
        ///     Disable TOUS les boutons qui sont potentiellement enabled.
        /// </summary>
        private void DisabledAllEnabledBtns() {
            btnTaxi.Enabled = false;
            btnBus.Enabled = false;
            btnMetro.Enabled = false;

            // Disable les cases ou les detectives se retrouvent.
            _listeBoutons[listeDetec[0].Case].Enabled = false;
            _listeBoutons[listeDetec[1].Case].Enabled = false;
            _listeBoutons[listeDetec[2].Case].Enabled = false;

            StopBlinkPaths();
        }


        private void NouvPartMnuItem_Click(object sender, EventArgs e) {
            lblDefaite.Visible = false;
            lblVictoire.Visible = false;

            CacheMrX();

            // Reinitialise les boutons des detectives sur la planche de jeu.
            for (int i = 0; i < listeDetec.Count; i++) {
                int caseDetec = listeDetec[i].Case;
                _listeBoutons[caseDetec].BackColor = Color.Transparent;
                _listeBoutons[caseDetec].ForeColor = Color.Black;
                _listeBoutons[caseDetec].Width = 28;
                _listeBoutons[caseDetec].Height = 20;
            }

            InitialiseTourReveleListe();
            // remove all pictures in picture boxes
            for (int i = 1; i < _listePicBox.Count; i++) {
                if (listeTourRevele.Contains(i)) {
                    _listePicBox[i].BackgroundImage = Properties.Resources.empty_reveal_slot;
                }
                else {
                    _listePicBox[i].BackgroundImage = Properties.Resources.empty_normal_slot;
                }
            }

            listeDetec.Clear();
            Setup();

            detecTurn = 1;
            gameTurn = 1;

            MrXDeplace();

            PaintDetecPos();
            UpdateDetecGrpBox();
        }
    }
}


/// TODO MAJEUR: 
/// TODO: make error providers in options. Add char limit.
/// TODO: make menu functionalities
/// TODO: Make mr.X dimgray