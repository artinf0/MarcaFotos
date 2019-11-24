using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarcaFotos
{
    public partial class Form1 : Form
    {

        string dirFolder = string.Empty;
        string dirFile = string.Empty;

        public Form1()
        {
            InitializeComponent();
            toolStripLabel1.Text = "Desenvolvido por Artinfo " + DateTime.Now.Year;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dirFile) && !string.IsNullOrEmpty(dirFolder))
                try {
                    AddWaterMark();

                } catch (Exception ex) {
                    MessageBox.Show(this, "Algo deu errado \n Detalhes: " + ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                MessageBox.Show(this, "Selecione primeiro a pasta das imagens e em seguida a marca d'agua.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddWaterMark()
        {
            DirectoryInfo d = new DirectoryInfo(dirFolder);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.JPG"); //Getting Text files

            foreach (FileInfo file in Files) {

                using (Image image = Image.FromFile(file.FullName))
                using (Image watermarkImage = Image.FromFile(dirFile))
                using (Graphics imageGraphics = Graphics.FromImage(image)) {

                    int x1 = watermarkImage.Width * 2;
                    int y1 = watermarkImage.Height * 2;

                    int x = ((image.Width - x1));
                    int y = ((image.Height - y1));

                    imageGraphics.DrawImage(watermarkImage, x, y, x1, y1);

                    string local = Path.Combine(dirFolder, "mod" + file.Name);
                    image.Save(local);
                }
            }
            MessageBox.Show(this, "Finalizado!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #region Eventos button
        private void btnFotos_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            this.txbFolder.Text = dirFolder = folderBrowserDialog1.SelectedPath.ToString();
        }

        private void btnWaterMark_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.png | *.png";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            this.txbFile.Text = dirFile = openFileDialog1.FileName;
        }

        #endregion
    }
}
