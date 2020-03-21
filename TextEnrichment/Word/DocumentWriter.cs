using Aspose.Words;
using Aspose.Words.Tables;
using System.Drawing;

namespace TextEnrichment.Word
{
    public class DocumentWriter : IDocumentWriter
    {
        private const string FONT_FAMILY = "Calibri";
        private const int FONT_SIZE = 12;
        private const int EMPTY_LINE_HEIGHT = 20;

        private readonly Document document;
        private readonly DocumentBuilder documentBuilder;

        public DocumentWriter()
        {
            document = new Document();
            documentBuilder = new DocumentBuilder(document);

            InitializeFont();
            documentBuilder.StartTable();
        }

        private void InitializeFont()
        {
            var font = documentBuilder.Font;
            font.Name = FONT_FAMILY;
            font.Size = FONT_SIZE;
        }

        public void InsertSentence(string text, string tag)
        {
            var tagCell = documentBuilder.InsertCell();
            tagCell.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            documentBuilder.Font.Italic = true;
            documentBuilder.Writeln(tag);
            documentBuilder.Font.Italic = false;

            documentBuilder.InsertCell();
            documentBuilder.Writeln(text);

            documentBuilder.EndRow();
        }

        public void InsertSentence(string text)
        {
            InsertSentence(text, string.Empty);
        }

        public void InsertEmptyLine()
        {
            documentBuilder.InsertCell();
            documentBuilder.InsertCell();

            var row = documentBuilder.EndRow();
            row.RowFormat.HeightRule = HeightRule.AtLeast;
            row.RowFormat.Height = EMPTY_LINE_HEIGHT;
        }

        public void Save(string documentName)
        {
            var table = documentBuilder.EndTable();
            table.ClearBorders();
            table.SetBorder(BorderType.Vertical, LineStyle.Single, 1, Color.Black, true);

            document.Save(documentName);
        }
    }
}
