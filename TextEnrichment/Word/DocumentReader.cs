using Aspose.Words;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextEnrichment.Word
{
    public class DocumentReader : IDocumentReader
    {
        public IEnumerable<string> ReadParagraphs(string documentPath)
        {
            using var file = new FileStream(documentPath, FileMode.Open, FileAccess.Read);
            var doc = new Document(file);

            return doc.GetChildNodes(NodeType.Paragraph, true).Select(p => p.GetText());
        }
    }
}
