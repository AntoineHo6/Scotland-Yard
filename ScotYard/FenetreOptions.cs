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

        public FenetreOptions(FenetreJeu fenetreJeu, List<Detective> listeDetec) {
            InitializeComponent();
            this.listeDetec = listeDetec;
            this.fenetreJeu = fenetreJeu;

            txtBoxDetec1.Text = listeDetec[0].Nom;
            txtBoxDetec2.Text = listeDetec[1].Nom;
            txtBoxDetec3.Text = listeDetec[2].Nom;
        }

        private void btnDect1Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
                listeDetec[0].Color = clrDlg.Color;
            }
        }

        private void btnDect2Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect2Color.BackColor = clrDlg.Color;
                listeDetec[1].Color = clrDlg.Color;
            }
        }

        private void btnDect3Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect3Color.BackColor = clrDlg.Color;
                listeDetec[2].Color = clrDlg.Color;
            }
        }

        private void btnSmttre_Click(object sender, EventArgs e) {
            listeDetec[0].Nom = txtBoxDetec1.Text;
            listeDetec[1].Nom = txtBoxDetec2.Text;
            listeDetec[2].Nom = txtBoxDetec3.Text;

            fenetreJeu.updateDetecGrpBox();
            fenetreJeu.paintDetecPos();
            this.Close();
        }
    }
}
