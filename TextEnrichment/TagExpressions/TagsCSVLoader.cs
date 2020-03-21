using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TextEnrichment.Configs;

namespace TextEnrichment.Tags
{
    public class TagsCSVLoader : ITagsLoader
    {
        private readonly string tagExpressionsFilePath;

        public TagsCSVLoader(IOptions<DataFilesConfig> config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            tagExpressionsFilePath = config.Value.TagExpressions;
        }

        public Dictionary<string, string> LoadTags()
        {
            using var stream = new StreamReader(new FileStream(tagExpressionsFilePath, FileMode.Open, FileAccess.Read));
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };
            using var reader = new CsvReader(stream, configuration);

            return reader.GetRecords<TagExpression>().ToDictionary(t => t.Expression.ToLower(), t => t.Tag.ToLower());
        }
    }
}
