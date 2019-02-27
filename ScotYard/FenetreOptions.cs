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

        public FenetreOptions() {
            InitializeComponent();
        }

        private void btnDect1Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
            }

        }

        private void btnDect2Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
            }
        }

        private void btnDect3Color_Click(object sender, EventArgs e) {
            if (clrDlg.ShowDialog() == DialogResult.OK) {
                btnDect1Color.BackColor = clrDlg.Color;
            }
        }
    }
}
