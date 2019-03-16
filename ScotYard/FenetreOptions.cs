using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScotYard.Logique;

namespace ScotYard {
    public partial class FenetreOptions : Form {

        List<Detective> listeDetec = new List<Detective>();
        FenetreJeu fenetreJeu;

        int[] nameErrState = { 0, 0, 0 };

        public FenetreOptions(FenetreJeu fenetreJeu, List<Detective> listeDetec) {
            InitializeComponent();
            this.listeDetec = listeDetec;
            this.fenetreJeu = fenetreJeu;

            TxtBoxDetec1.Text = listeDetec[0].Nom;
            TxtBoxDetec2.Text = listeDetec[1].Nom;
            TxtBoxDetec3.Text = listeDetec[2].Nom;

            BtnDect1Color.BackColor = listeDetec[0].Color;
            BtnDect2Color.BackColor = listeDetec[1].Color;
            BtnDect3Color.BackColor = listeDetec[2].Color;
        }


        private void BtnDect1Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                BtnDect1Color.BackColor = clrDlg.Color;
                listeDetec[0].Color = clrDlg.Color;
            }
        }


        private void BtnDect2Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                BtnDect2Color.BackColor = clrDlg.Color;
                listeDetec[1].Color = clrDlg.Color;
            }
        }


        private void BtnDect3Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                BtnDect3Color.BackColor = clrDlg.Color;
                listeDetec[2].Color = clrDlg.Color;
            }
        }


        private void BtnSmttre_Click(object sender, EventArgs e) {
            VerifieErrNom(TxtBoxDetec1, 0);
            VerifieErrNom(TxtBoxDetec2, 1);
            VerifieErrNom(TxtBoxDetec3, 2);

            if (nameErrState.Contains(1)) {
                // something
            }
            else {
                listeDetec[0].Nom = TxtBoxDetec1.Text;
                listeDetec[1].Nom = TxtBoxDetec2.Text;
                listeDetec[2].Nom = TxtBoxDetec3.Text;

                fenetreJeu.UpdateDetecGrpBox();
                fenetreJeu.PaintDetecPos();
                this.Close();
            }
        }


        private void VerifieErrNom(TextBox txtBoxDetec, int index) {
            errProv.SetError(txtBoxDetec, "");

            if (txtBoxDetec.Text.Trim() == String.Empty) {
                errProv.SetError(txtBoxDetec, "Le nom ne doit pas être vide");
                nameErrState[index] = 1;
            }
            else if (txtBoxDetec.Text.Length > 32) {
                errProv.SetError(txtBoxDetec, "Le nom doit avoir au plus 32 charactères");
                nameErrState[index] = 1;
            }
            else {
                nameErrState[index] = 0;
            }
        }
    }
}
