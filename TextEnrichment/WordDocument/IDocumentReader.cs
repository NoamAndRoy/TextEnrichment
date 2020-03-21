using System.Collections.Generic;

namespace TextEnrichment.WordDocument
{
    public interface IDocumentReader
    {
        IEnumerable<string> ReadParagraphs(string documentPath);
    }
}
