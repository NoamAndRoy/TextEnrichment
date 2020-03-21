using System.Collections.Generic;

namespace TextEnrichment.Tags
{
    public interface ITagsLoader
    {
        Dictionary<string, string> LoadTags();
    }
}
