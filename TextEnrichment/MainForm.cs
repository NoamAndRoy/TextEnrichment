using System.IO;
using System.Windows.Forms;
using TextEnrichment.Text;
using TextEnrichment.Word;

namespace TextEnrichment
{
    public partial class MainForm : Form
    {
        private readonly ISentencer sentencer;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentWriter documentWriter;

        public MainForm(ISentencer sentencer, IDocumentReader documentReader, IDocumentWriter documentWriter)
        {
            this.sentencer = sentencer;
            this.documentReader = documentReader;
            this.documentWriter = documentWriter;

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

            foreach (var paragraph in documentReader.ReadParagraphs(filePathBeforeTextBox.Text))
            {
                var sentences = sentencer.GetSentencesFromParagraph(paragraph);

                foreach (var (sentence, tag) in sentences)
                {
                    documentWriter.InsertSentence(sentence, tag ?? string.Empty);
                }

                documentWriter.InsertEmptyLine();
            }

            documentWriter.Save(filePathAfterTextBox.Text);

            MessageBox.Show("File Enriched");
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