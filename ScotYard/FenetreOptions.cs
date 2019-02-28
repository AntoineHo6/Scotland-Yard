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

        Detective detec1;
        Detective detec2;
        Detective detec3;

        public FenetreOptions() {
            InitializeComponent();
        }

        public FenetreOptions(Detective detec1, Detective detec2, Detective detec3) {
            InitializeComponent();
            this.detec1 = detec1;
            this.detec2 = detec2;
            this.detec3 = detec3;
        }

        private void btnDect1Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
                detec1.Color = clrDlg.Color;
            }
        }

        private void btnDect2Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
                detec2.Color = clrDlg.Color;
            }
        }

        private void btnDect3Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
                detec3.Color = clrDlg.Color;
            }
        }

        private void btnSmttre_Click(object sender, EventArgs e) {
            
            this.Close();
        }
    }
}
