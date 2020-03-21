namespace TextEnrichment.Enrichment
{
    public interface IEnricher
    {
        public void Enrich(string filePath, string enrichedFilePath);
    }
}
