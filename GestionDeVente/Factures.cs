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
    public partial class Factures : Form
    {
        Functions Con;
        public Factures()
        {

            InitializeComponent();
            Con = new Functions();
            ListerArticles();
            /* Lorsque l'User se connecte -> son nom va apparaitre dans la page de Facturation */
            VendeurLbl.Text = Connexion.UserName;

           
        }
        
        private void ListerArticles()
        {
            try
            {
                String Req = "Select ArtCode as Code, ArtNom as Article, ArtPrix as Prix, CatNom as Categories, ArtStock as Stock from ArticlesTbl join CategorieTbl on ArticlesTbl.ArtCat = CategorieTbl.CatCode";
                ArticlesListe.DataSource = Con.RecupererDonnees(Req);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        /* Deconnexion */
        private void DeconLbl_Click(object sender, EventArgs e)
        {
            Connexion Obj = new Connexion();
            Obj.Show();
            this.Hide();


        }

        private void VendeursListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int Key = 0;
        private void ArticlesListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = ArticlesListe.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            NomTb.Text = ArticlesListe.SelectedRows[0].Cells[1].Value.ToString();
            PrixTb.Text = ArticlesListe.SelectedRows[0].Cells[2].Value.ToString();

            //CatCb.Text = ArticlesListe.SelectedRows[0].Cells[3].Value.ToString();
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

        /*Reinisialisation */

        private void ReinitialiserBtn_Click(object sender, EventArgs e)
        {
            NomTb.Text = "";
            PrixTb.Text = "";
            StockTb.Text = "";
            QuantiteTb.Text = "";

        }

        private void ModifierStock()
        {
      
            int Stock = Convert.ToInt32(StockTb.Text);
            int NewStock = Stock - Convert.ToInt32(QuantiteTb.Text);
            

            string Req = "Update  ArticlesTbl set  ArtStock = {0}  where ArtCode = {1} ";
            Req = string.Format(Req, NewStock,Key);
            Con.EnvoyerDonnees(Req);
            ListerArticles();
           
        }


        /*Ajouter Une Facture */
        int n = 0;
        int GrdTotal = 0;
        private void ModifierBtn_Click(object sender, EventArgs e)
        {
            if (PrixTb.Text == "" || QuantiteTb.Text == "" || StockTb.Text == "" || NomTb.Text == "")
            {
                MessageBox.Show("Information Manquante !");
            } else 

            {
                if (Convert.ToInt32(QuantiteTb.Text) > Convert.ToInt32(StockTb.Text))
                {
                    MessageBox.Show("Stock Non Disponible !");

                }
                else
                {
                    int total = Convert.ToInt32(QuantiteTb.Text) * Convert.ToInt32(PrixTb.Text);
                    DataGridViewRow Ligne = new DataGridViewRow();
                    Ligne.CreateCells(FactureListe);
                    Ligne.Cells[0].Value = n + 1;
                    Ligne.Cells[1].Value = NomTb.Text;
                    Ligne.Cells[2].Value = QuantiteTb.Text;
                    Ligne.Cells[3].Value = PrixTb.Text;
                    Ligne.Cells[4].Value = total;
                    FactureListe.Rows.Add(Ligne);
                    GrdTotal = GrdTotal + total;
                    PrixtTotalLbl.Text = GrdTotal + "Dinars";
                    ModifierStock();
                    NomTb.Text = "";
                    PrixTb.Text = "";
                    StockTb.Text = "";
                    QuantiteTb.Text = "";
                    n++;

                }

            }
        }



        /*Inserer Facture */
        private void InsererFacture()
        {
            try
            {
               int Vendeur = Connexion.UserId;
                MessageBox.Show("" +Vendeur);
                string Req = "insert into FacturesTbl values('{0}',{1} , {2}  )";
                Req = string.Format(Req, DateTime.Today.Date.ToString(), Vendeur, GrdTotal);
                Con.EnvoyerDonnees(Req);
                ListerArticles();
                MessageBox.Show("Facture Ajoutée.");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            
        
           
        }

        /*Imprimer Facture */
        private void ImprimerBtn_Click(object sender, EventArgs e)
        {
            InsererFacture();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

        }

        int ACode, APrix, AQty, ATotal;

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Factures Obj = new Factures();
            Obj.Show();
            this.Hide();
        }

        private void VendeurLbl_Click(object sender, EventArgs e)
        {

        }

        String ANom;
        int pos = 60;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MagaZine est ", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in FactureListe.Rows)
            {
                ACode = Convert.ToInt32(row.Cells["Column1"].Value);
                ANom = ""+row.Cells["Column2"].Value;
                APrix = Convert.ToInt32(row.Cells["Column3"].Value);
                AQty = Convert.ToInt32(row.Cells["Column4"].Value);
                ATotal = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + ACode, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + ANom, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + APrix, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + AQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + ATotal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;

            }
            e.Graphics.DrawString("Grand Total" + GrdTotal + "Dinars", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("********MagaZine********", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            FactureListe.Rows.Clear();
            FactureListe.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
            PrixtTotalLbl.Text = "";


        }
    }
}
