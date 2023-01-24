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
    public partial class Articles : Form
    {
        Functions Con;

        public Articles()
        {
            
            InitializeComponent();
            Con = new Functions();
            ListerArticles();
            RemplirCategories();
        }

        private void ListerArticles()
        {
            String Req = "Select * from ArticlesTbl";
            ArticlesListe.DataSource = Con.RecupererDonnees(Req);
        }

        private void Filtrage()
        {
            String Req = "Select * from ArticlesTbl where ArtCat = {0} ";
            int Cat = Convert.ToInt32(FiltrerCb.SelectedValue.ToString());
            Req = string.Format(Req, Cat );
            ArticlesListe.DataSource = Con.RecupererDonnees(Req);
        }


        private void RemplirCategories()
        {
            string Req = "select * from CategorieTbl";
            CatCb.DisplayMember = Con.RecupererDonnees(Req).Columns["CatNom"].ToString();
            CatCb.ValueMember = Con.RecupererDonnees(Req).Columns["CatCode"].ToString();
            CatCb.DataSource = Con.RecupererDonnees(Req);

            FiltrerCb.DisplayMember = Con.RecupererDonnees(Req).Columns["CatNom"].ToString();
            FiltrerCb.ValueMember = Con.RecupererDonnees(Req).Columns["CatCode"].ToString();
            FiltrerCb.DataSource = Con.RecupererDonnees(Req);


        }


        /* Ajouter Article */
        private void EnregistrerBtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (NomTb.Text == "" || PrixTb.Text == "" || CatCb.SelectedIndex == -1 || StockTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text; 
                    int Prix = Convert.ToInt32 (PrixTb.Text) ;
                    int Categorie = Convert.ToInt32(CatCb.SelectedValue.ToString());
                    int Stock = Convert.ToInt32(StockTb.Text);
                    String ExpDate = ExpTb.Value.Date.ToString();

                    string Req = "insert into ArticlesTbl values('{0}',{1} , {2} , {3} , '{4}' )";
                    Req = string.Format(Req, Nom, Prix , Categorie , Stock , ExpDate);
                    Con.EnvoyerDonnees(Req);
                    ListerArticles();
                    MessageBox.Show("Article Ajouté.");
                    NomTb.Text = ""; 
                    PrixTb.Text = "";
                    StockTb.Text = "";
                    CatCb.SelectedIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void ArticlesListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /* Lorsque on clique sur les lignes de datagrid , il s'affiche sur le formulaire */

        int Key = 0;
        private void ArticlesListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = ArticlesListe.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            NomTb.Text = ArticlesListe.SelectedRows[0].Cells[1].Value.ToString();
            PrixTb.Text = ArticlesListe.SelectedRows[0].Cells[2].Value.ToString();

            CatCb.Text = ArticlesListe.SelectedRows[0].Cells[3].Value.ToString();
            StockTb.Text = ArticlesListe.SelectedRows[0].Cells[4].Value.ToString();
            

            if (NomTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ArticlesListe.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        /* Supprimer Article */
        private void SupprimerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Key==0)
                {
                    MessageBox.Show("Selectionner un Article S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text;
                    int Prix = Convert.ToInt32(PrixTb.Text);
                    int Categorie = Convert.ToInt32(CatCb.SelectedValue.ToString());
                    int Stock = Convert.ToInt32(StockTb.Text);
                    String ExpDate = ExpTb.Value.Date.ToString();

                    string Req = "delete from ArticlesTbl where ArtCode = {0} " ;
                    Req = string.Format(Req, Key);
                    Con.EnvoyerDonnees(Req);
                    ListerArticles();
                    MessageBox.Show("Article Supprimé.");
                    NomTb.Text = "";
                    PrixTb.Text = "";
                    StockTb.Text = "";
                    CatCb.SelectedIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /* Modifier Article */
        private void ModifierBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomTb.Text == "" || PrixTb.Text == "" || CatCb.SelectedIndex == -1 || StockTb.Text == "")
                {
                    MessageBox.Show("Remplir le Formulaire S'il vous plaît.");
                }
                else
                {
                    string Nom = NomTb.Text;
                    int Prix = Convert.ToInt32(PrixTb.Text);
                    int Categorie = Convert.ToInt32(CatCb.SelectedValue.ToString());
                    int Stock = Convert.ToInt32(StockTb.Text);
                    String ExpDate = ExpTb.Value.Date.ToString();

                    string Req = "Update  ArticlesTbl set ArtNom =  '{0}', ArtPrix = {1} , ArtCat =  {2} , ArtStock = {3} , ArtExpDate =  '{4}' where ArtCode = {5} ";
                    Req = string.Format(Req, Nom, Prix, Categorie, Stock, ExpDate , Key);
                    Con.EnvoyerDonnees(Req);
                    ListerArticles();
                    MessageBox.Show("Article Modifié.");
                    NomTb.Text = "";
                    PrixTb.Text = "";
                    StockTb.Text = "";
                    CatCb.SelectedIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        /* Combobox FiltrerCb au dessous de Liste des Articles */

        private void FiltrerCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrage();
        }


        /* Bouton Actualiser */

        private void ActualiserBtn_Click(object sender, EventArgs e)
        {
            ListerArticles();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Connexion Obj = new Connexion();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Factures Obj = new Factures();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Vendeurs Obj = new Vendeurs();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Articles Obj = new Articles();
            Obj.Show();
            this.Hide();
        }
    }
}
