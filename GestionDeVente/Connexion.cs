using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeVente
{
    public partial class Connexion : Form
    {
        Functions Con;
        public Connexion()
        {
            InitializeComponent();
            Con = new Functions();
        }

        public static int UserId;
        public static string UserName;
        private void Connexion_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void NomTb_TextChanged(object sender, EventArgs e)
        {

        }



        /* Se Connecter */
        private void ConBtn_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || MPasseTb.Text == "")
            {
                MessageBox.Show("Entrez les Informations Completes !");
            }else if(NomTb.Text == "Admin" && MPasseTb.Text == "Admin")
            {
                Articles Obj = new Articles();
                Obj.Show();
                this.Hide();
            }
            
            else
            {
                string Req = "select*from VendeursTbl where VendPseudo = '{0}' and VendPass = '{1}' ";
                Req = string.Format(Req, NomTb.Text, MPasseTb.Text);
                DataTable dt = Con.RecupererDonnees(Req);
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("Vendeur Inexistant !");
                } else
                {
                    /* Lorsque l'User se connecte -> son nom va apparaitre dans la page de Facturation */
                    UserName = dt.Rows[0][2].ToString();
                    UserId = Convert.ToInt32(dt.Rows[0][0].ToString());

                    Factures Obj = new Factures();
                    Obj.Show();
                    this.Hide();
                }
            }

        }
    }
}
