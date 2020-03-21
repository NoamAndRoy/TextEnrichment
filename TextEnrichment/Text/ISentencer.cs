﻿using System.Collections.Generic;

namespace TextEnrichment.Text
{
    public interface ISentencer
    {
        IEnumerable<(string sentence, string? tag)> GetSentences(string text);
    }
}
