namespace TextEnrichment.Word
{
    public interface IDocumentWriter
    {
        void InsertSentence(string text, string tag);
        void InsertSentence(string text);
        void InsertEmptyLine();
        void Save(string documentName);
    }
}
