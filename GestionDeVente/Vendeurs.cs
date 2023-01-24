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
    public partial class Vendeurs : Form
    {
        Functions Con;
        public Vendeurs()
        {
            InitializeComponent();
            Con = new Functions();
            ListerVendeurs();
        }


        private void ListerVendeurs()
        {
            String Req = "Select * from VendeursTbl";
            VendeursListe.DataSource = Con.RecupererDonnees(Req);
        }

        /* Ajouter Vendeur */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomTb.Text == "" || PseudoTb.Text == "" || MotDePasseTb.Text == "" || AddressTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text;
                    string Pseudo = PseudoTb.Text;

                    string MPasse = MotDePasseTb.Text;
                    string Phone = PhoneTb.Text;
                    string Add = AddressTb.Text;



                    

                    string Req = "insert into VendeursTbl values('{0}','{1}' , '{2}' , {3} , '{4}' )";
                    Req = string.Format(Req, Nom, Pseudo, MPasse, Phone, Add);
                    Con.EnvoyerDonnees(Req);
                    ListerVendeurs();
                    MessageBox.Show("Vendeur Ajouté.");
                    NomTb.Text = "";
                    PseudoTb.Text = "";
                    MotDePasseTb.Text = "";
                    AddressTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /* Lorsque on clique sur les lignes de datagrid , il s'affiche sur le formulaire */

        int Key = 0;
        private void VendeursListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = VendeursListe.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            NomTb.Text = VendeursListe.SelectedRows[0].Cells[1].Value.ToString();
            PseudoTb.Text = VendeursListe.SelectedRows[0].Cells[2].Value.ToString();

            MotDePasseTb.Text = VendeursListe.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = VendeursListe.SelectedRows[0].Cells[4].Value.ToString();
            AddressTb.Text = VendeursListe.SelectedRows[0].Cells[5].Value.ToString();


            if (NomTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(VendeursListe.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        /* Supprimer Un Vendeur */

        private void SupprimerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Key == 0)
                {
                    MessageBox.Show("Selectionnez une Vendeur S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text;
                    string Pseudo = PseudoTb.Text;

                    string MPasse = MotDePasseTb.Text;
                    string Phone = PhoneTb.Text;
                    string Add = AddressTb.Text;





                    string Req = "delete from  VendeursTbl where VendCode = {0} ";
                    Req = string.Format(Req, Key);
                    Con.EnvoyerDonnees(Req);
                    ListerVendeurs();
                    MessageBox.Show("Vendeur Supprimé.");
                    NomTb.Text = "";
                    PseudoTb.Text = "";
                    MotDePasseTb.Text = "";
                    AddressTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /* Modifier Un Vendeur */
        private void ModifierBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomTb.Text == "" || PseudoTb.Text == "" || MotDePasseTb.Text == "" || AddressTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text;
                    string Pseudo = PseudoTb.Text;

                    string MPasse = MotDePasseTb.Text;
                    string Phone = PhoneTb.Text;
                    string Add = AddressTb.Text;





                    string Req = "update  VendeursTbl set VendNom = '{0}', VendPseudo = '{1}' , VendPass =  '{2}' , VendPhone = '{3}' , VendAdd = '{4}' where VendCode = {5}  ";
                    Req = string.Format(Req, Nom, Pseudo, MPasse, Phone, Add, Key);
                    Con.EnvoyerDonnees(Req);
                    ListerVendeurs();
                    MessageBox.Show("Vendeur Modifié.");
                    NomTb.Text = "";
                    PseudoTb.Text = "";
                    MotDePasseTb.Text = "";
                    AddressTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /* Deconnexion : Lorsque on clique sur Decoonecion , on fait l'appel : Connexion.cs  */ 
        private void DeconLbl_Click(object sender, EventArgs e)
        {
            Connexion Obj = new Connexion();
            Obj.Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Articles Obj = new Articles();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Vendeurs Obj = new Vendeurs();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Factures Obj = new Factures();
            Obj.Show();
            this.Hide();
        }
    }
    }

