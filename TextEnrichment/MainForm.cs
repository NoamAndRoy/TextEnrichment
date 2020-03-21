using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TextEnrichment.Enrichment;

namespace TextEnrichment
{
    public partial class MainForm : Form
    {
        private readonly IEnricher enricher;

        public MainForm(IEnricher enricher)
        {
            this.enricher = enricher;

            InitializeComponent();
        }

        private void enrichBtn_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePathBeforeTextBox.Text) ||
                string.IsNullOrWhiteSpace(filePathAfterTextBox.Text))
            {
                MessageBox.Show("File paths should not be empty!");
                return;
            }

            enricher.Enrich(filePathBeforeTextBox.Text, filePathAfterTextBox.Text);

            if (MessageBox.Show("File Enriched Successfully, Would you like to open it now?", "File Enriched",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var pi = new ProcessStartInfo(filePathAfterTextBox.Text)
                {
                    UseShellExecute = true
                };

                Process.Start(pi);
            }
        }

        private void selectFileBtn_Click(object sender, System.EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Multiselect = false,
                RestoreDirectory = true,
                Filter = "Word files(*.Docx)|*.Docx;*.Doc",
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePathBeforeTextBox.Text = dialog.FileName;
                filePathAfterTextBox.Text = GetEnrichedFileName(dialog.FileName);
            }
        }

        private string GetEnrichedFileName(string filePath) =>
            $"{Path.GetDirectoryName(filePath)}\\{Path.GetFileNameWithoutExtension(filePath)} - Enriched.docx";
    }
}