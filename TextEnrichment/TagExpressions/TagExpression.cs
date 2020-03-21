using CsvHelper.Configuration.Attributes;

namespace TextEnrichment.Tags
{
    public class TagExpression
    {
        [Index(0)]
        public string Expression { get; set; }

        [Index(1)]
        public string Tag { get; set; }
    }
}
