using ArchMind.Application.Architectures.Knowledge;
using ArchMind.Domain.Architectures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ArchMind.Infrastructure.Knowledge
{
    public sealed class LocalArchitectureKnowledgeRetriever
    : IArchitectureKnowledgeRetriever
    {
        private readonly List<KnowledgeDocument> _documents;
        private readonly ILogger<LocalArchitectureKnowledgeRetriever> _logger;

        public LocalArchitectureKnowledgeRetriever(
            ILogger<LocalArchitectureKnowledgeRetriever> logger)
        {
            _logger = logger;

            var knowledgePath = Path.Combine(
                AppContext.BaseDirectory,
                "Knowledge");

            _documents = LoadDocuments(knowledgePath);

            _logger.LogInformation(
                "Loaded {DocumentCount} architecture knowledge documents",
                _documents.Count);
        }

        public Task<IReadOnlyList<KnowledgeChunk>> RetrieveAsync(
            ArchitectureRequest request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = BuildQuery(request);

            var queryTerms = Tokenize(query);

            var results = _documents
                .Select(document => new
                {
                    Document = document,
                    Score = CalculateScore(
                        document,
                        queryTerms)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Take(5)
                .Select(x => new KnowledgeChunk
                {
                    Id = $"{x.Document.Id}-1",
                    DocumentId = x.Document.Id,
                    Title = x.Document.Title,
                    Category = x.Document.Category,
                    Source = x.Document.Source,
                    Content = x.Document.Content,
                    Score = x.Score
                })
                .ToList();

            _logger.LogInformation(
                "Retrieved {ResultCount} knowledge documents for architecture request",
                results.Count);

            return Task.FromResult<IReadOnlyList<KnowledgeChunk>>(
                results);
        }

        private static string BuildQuery(
            ArchitectureRequest request)
        {
            return string.Join(
                " ",
                request.BusinessProblem,
                request.ExpectedUsers,
                request.Constraints,
                request.PreferredTechnology);
        }

        private static HashSet<string> Tokenize(
            string text)
        {
            return Regex
                .Matches(
                    text.ToLowerInvariant(),
                    @"[a-z0-9]+")
                .Select(match => match.Value)
                .Where(word => word.Length >= 3)
                .ToHashSet();
        }

        private static double CalculateScore(
            KnowledgeDocument document,
            HashSet<string> queryTerms)
        {
            var documentText = string.Join(
                " ",
                document.Title,
                document.Category,
                document.Content);

            var documentTerms = Tokenize(documentText);

            if (documentTerms.Count == 0)
            {
                return 0;
            }

            var matches = queryTerms
                .Intersect(documentTerms)
                .Count();

            return matches;
        }

        private static List<KnowledgeDocument> LoadDocuments(
            string knowledgePath)
        {
            if (!Directory.Exists(knowledgePath))
            {
                throw new DirectoryNotFoundException(
                    $"Architecture knowledge directory was not found: " +
                    $"{knowledgePath}");
            }

            var files = Directory
                .GetFiles(
                    knowledgePath,
                    "*.md",
                    SearchOption.AllDirectories);

            return files
                .Select(ParseDocument)
                .ToList();
        }

        private static KnowledgeDocument ParseDocument(
            string filePath)
        {
            var content = File.ReadAllText(filePath);

            var title = ExtractMetadata(
                content,
                "Title");

            var id = ExtractMetadata(
                content,
                "ID");

            var category = ExtractMetadata(
                content,
                "Category");

            var source = ExtractMetadata(
                content,
                "Source");

            return new KnowledgeDocument
            {
                Id = id,
                Title = title,
                Category = category,
                Source = source,
                Content = content
            };
        }

        private static string ExtractMetadata(
            string content,
            string field)
        {
            var pattern =
                $@"^{Regex.Escape(field)}:\s*(.+)$";

            var match = Regex.Match(
                content,
                pattern,
                RegexOptions.Multiline |
                RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new InvalidOperationException(
                    $"Knowledge document is missing metadata: {field}");
            }

            return match.Groups[1].Value.Trim();
        }
    }
}
