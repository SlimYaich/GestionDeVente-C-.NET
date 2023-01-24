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
    public partial class Categories : Form
    {
        Functions Con;
        public Categories()
        {
            InitializeComponent();
            Con = new Functions();
            ListerCategories();
        }
        private void ListerCategories()
        {
            String Req = "Select * from CategorieTbl";
            CategoriesListe.DataSource = Con.RecupererDonnees(Req);
        }

        /* Ajouter Catégorie */
        private void EnregistrerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomTb.Text == "" || RemTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text; 
                    string Rem = RemTb.Text;
                    string Req = "insert into CategorieTbl values('{0}','{1}')";
                    Req = string.Format(Req, Nom, Rem);
                    Con.EnvoyerDonnees(Req);
                    ListerCategories();
                    MessageBox.Show("Categorie Ajoutée.");
                    NomTb.Text = ""; 
                    RemTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        private void CategoriesListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


                 /* Lorsque on clique sur les lignes de datagrid , il s'affiche sur le formulaire */

        private void CategoriesListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = CategoriesListe.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            NomTb.Text = CategoriesListe.SelectedRows[0].Cells[1].Value.ToString();
            RemTb.Text = CategoriesListe.SelectedRows[0].Cells[2].Value.ToString();
            if (NomTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CategoriesListe.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        /* Modifier Catégorie */
        private void ModifierBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomTb.Text == "" || RemTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text; string Rem = RemTb.Text;
                    string Req = "Update CategorieTbl set CatNom = '{0}', CatRem = '{1}' where CatCode = {2} ";
                    Req = string.Format(Req, Nom, Rem , Key);
                    Con.EnvoyerDonnees(Req);
                    ListerCategories();
                    MessageBox.Show("Categorie Modifiée.");
                    NomTb.Text = ""; RemTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /* Supprimer Catégorie */
        private void SupprimerBtn_Click(object sender, EventArgs e)
        {
             try
            {
                if (NomTb.Text == "" || RemTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text; string Rem = RemTb.Text;
                    string Req = "Delete from  CategorieTbl  where CatCode = {0} ";
                    Req = string.Format(Req,Key);
                    Con.EnvoyerDonnees(Req);
                    ListerCategories();
                    MessageBox.Show("Categorie Supprimée.");
                    NomTb.Text = ""; RemTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        
        }

        private void CategoriesListe_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

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

        private void label12_Click(object sender, EventArgs e)
        {
            Connexion Obj = new Connexion();
            Obj.Show();
            this.Hide();
        }
    }
}
