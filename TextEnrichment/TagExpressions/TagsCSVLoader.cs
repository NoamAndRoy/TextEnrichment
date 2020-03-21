using System;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using TextEnrichment.Configs;

namespace TextEnrichment.Tags
{
    public class TagsCSVLoader : ITagsLoader
    {
        private readonly string tagExpressionsFilePath;
        public TagsCSVLoader(IOptions<DataFilesConfig> config)
        {
            _ = config ?? throw new ArgumentNullException(nameof(config));

            tagExpressionsFilePath = config.Value.TagExpressions;
        }

        public Dictionary<string, string> LoadTags()
        {
            using var reader = new CsvReader(new StreamReader(new FileStream(tagExpressionsFilePath, FileMode.Open, FileAccess.Read))
                , new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

            return reader.GetRecords<TagExpression>().ToDictionary(t => t.Expression.ToLower(), t => t.Tag.ToLower());
        }
    }
}
