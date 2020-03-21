using System.Collections.Generic;

namespace TextEnrichment.Word
{
    public interface IDocumentReader
    {
        IEnumerable<string> ReadParagraphs(string documentPath);
    }
}
