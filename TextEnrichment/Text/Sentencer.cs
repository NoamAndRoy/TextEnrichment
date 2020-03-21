using System;
using LexicalAnalyzer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextEnrichment.Tags;

namespace TextEnrichment.Text
{
    public class Sentencer: ISentencer
    {
        private readonly ILexer<eTokenType> lexer;
        private readonly Lazy<Dictionary<string, string>> tagExpressions;

        public Sentencer(ILexer<eTokenType> lexer, ITagsLoader tagsLoader)
        {
            this.lexer = lexer;

            tagExpressions = new Lazy<Dictionary<string, string>>(tagsLoader.LoadTags);
        }

        public IEnumerable<(string sentence, string? tag)> GetSentences(string text)
        {
            var tokens = lexer.GetTokens(text).ToList();
            var sentences = GetSentences(tokens);

            foreach (var sentence in sentences)
            {
                yield return (SentenceToText(sentence), GetSentenceTag(sentence, tagExpressions.Value));
            }
        }

        private List<List<Token<eTokenType>>> GetSentences(List<Token<eTokenType>> tokens)
        {
            var sentences = new List<List<Token<eTokenType>>>();
            var indexOfSentenceEnd = 0;

            while (indexOfSentenceEnd != -1)
            {
                indexOfSentenceEnd = FindSentenceEnd(tokens);

                if (indexOfSentenceEnd == -1)
                {
                    sentences.Add(tokens);
                    break;
                }

                sentences.Add(tokens.GetRange(0, indexOfSentenceEnd + 1));

                tokens = tokens.Skip(indexOfSentenceEnd + 1).ToList();
            }

            return sentences;
        }

        private int FindSentenceEnd(List<Token<eTokenType>> tokens)
        {
            var sentenceEndOptions = new[] {".", "?", "!"};
            var index = tokens.FindIndex(token => token.TokenType == eTokenType.Punctuation && sentenceEndOptions.Contains(token.Value.ToString()));

            if (tokens.Count > index + 1
                && tokens[index + 1].TokenType != eTokenType.Punctuation
                && char.IsUpper(tokens[index + 1].Value.ToString()[0]))
            {
                return index;
            }

            return -1;
        }

        private string SentenceToText(List<Token<eTokenType>> words)
        {
            if (words.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder(FormatOrdinalNumbers(words[0].Value.ToString()));
            var lastToken = words[0];

            foreach (var token in words.Skip(1))
            {
                builder.Append(token.TokenType switch
                {
                    eTokenType.Punctuation when token.Value.ToString() == "(" || token.Value.ToString() == "-" => $" {token.Value}",
                    eTokenType.Punctuation => token.Value.ToString(),
                    _ when lastToken.Value.ToString() != "(" => $" {token.Value}",
                    _ => token.Value.ToString()
                });

                lastToken = token;
            }

            return builder.ToString();
        }

        private string FormatOrdinalNumbers(string word)
        {
            var numbering = word.ToLower() switch
            {
                "first" => "1. ",
                "second" => "2. ",
                "third" => "3. ",
                "fourth" => "4. ",
                "fifth" => "5. ",
                "sixth" => "6. ",
                "seventh" => "7. ",
                "eighth" => "8. ",
                "ninth" => "9. ",
                _ => ""
            };

            return numbering + word;
        }

        private string? GetSentenceTag(IEnumerable<Token<eTokenType>> sentence,
            Dictionary<string, string> tagsExpressions)
        {
            foreach (var word in sentence.Select(t => t.Value.ToString().ToLower()))
            {
                if (tagsExpressions.ContainsKey(word))
                {
                    return tagsExpressions[word];
                }
            }

            return null;
        }
    }
}
