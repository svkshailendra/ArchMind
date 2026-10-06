using ArchMind.Application.Architectures;
using ArchMind.Domain.Architectures;
using ArchMind.Web.Models; 
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ArchMind.UnitTests
{
    public sealed class ArchitectUnitTests
    {
        [Fact]
        public void Constructor_ShouldPreserveProvidedValues()
        {
            var request = new ArchitectureRequest
            {
                BusinessProblem = "Build an internal note-taking application.",
                ExpectedUsers = "100",
                Constraints = "Intranet only and high security.",
                PreferredTechnology = "any"
            };

            Assert.Equal(
            "Build an internal note-taking application.",
            request.BusinessProblem);

            Assert.Equal("100", request.ExpectedUsers);
            Assert.Equal("Intranet only and high security.", request.Constraints);
            Assert.Equal("any", request.PreferredTechnology);
        }

        public sealed class ArchitectureFormModelTests
        {
            [Fact]
            public void ToRequest_ShouldTrimAllInputValues()
            {
                var model = new ArchitectureFormModel
                {
                    BusinessProblem = " Build a note-taking application. ",
                    ExpectedUsers = " 100 users ",
                    Constraints = " Intranet only. ",
                    PreferredTechnology = " any "
                };

                var request = model.ToRequest();

                Assert.Equal(
                "Build a note-taking application.",
                request.BusinessProblem);

                Assert.Equal("100 users", request.ExpectedUsers);
                Assert.Equal("Intranet only.", request.Constraints);
                Assert.Equal("any", request.PreferredTechnology);
            }

            [Fact]
            public void Defaults_ShouldEnforceFreeTechnologyConstraint()
            {
                var model = new ArchitectureFormModel();

                Assert.Contains(
                "free",
                model.Constraints,
                StringComparison.OrdinalIgnoreCase);
            }
        }

        public sealed class ArchitectureFormValidationTests
        {
            [Fact]
            public void Validation_ShouldFail_WhenRequiredValuesAreMissing()
            {
                var model = new ArchitectureFormModel
                {
                    BusinessProblem = string.Empty,
                    ExpectedUsers = string.Empty,
                    Constraints = string.Empty
                };

                var results = Validate(model);

                Assert.NotEmpty(results);

                Assert.Contains(
                results,
                result => result.MemberNames.Contains(
                nameof(ArchitectureFormModel.BusinessProblem)));

                Assert.Contains(
                results,
                result => result.MemberNames.Contains(
                nameof(ArchitectureFormModel.ExpectedUsers)));

                Assert.Contains(
                results,
                result => result.MemberNames.Contains(
                nameof(ArchitectureFormModel.Constraints)));
            }

            [Fact]
            public void Validation_ShouldFail_WhenBusinessProblemIsTooShort()
            {
                var model = CreateValidModel();
                model.BusinessProblem = "Too short";

                var results = Validate(model);

                Assert.Contains(
                results,
                result => result.MemberNames.Contains(
                nameof(ArchitectureFormModel.BusinessProblem)));
            }

            [Fact]
            public void Validation_ShouldPass_ForValidInput()
            {
                var model = CreateValidModel();

                var results = Validate(model);

                Assert.Empty(results);
            }

            private static ArchitectureFormModel CreateValidModel()
            {
                return new ArchitectureFormModel
                {
                    BusinessProblem =
                "Build an internal note-taking application with history.",
                    ExpectedUsers = "100 users",
                    Constraints = "Intranet only with strong security.",
                    PreferredTechnology = "any"
                };
            }

            private static IReadOnlyList<ValidationResult> Validate(object model)
            {
                var results = new List<ValidationResult>();

                Validator.TryValidateObject(
                model,
                new ValidationContext(model),
                results,
                validateAllProperties: true);

                return results;
            }
        }

        public sealed class ArchitectureProposalSerializationTests
        {
            private static readonly JsonSerializerOptions JsonOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };

            [Fact]
            public void Deserialize_ShouldCreateStructuredProposal()
            {
                const string json = """
{
"summary": "Internal note-taking architecture",
"requirements": [
"Support multiple profiles",
"Run inside the organisation"
],
"components": [
{
"name": "Web Application",
"responsibility": "Provides the user interface",
"technology": "Blazor",
"rationale": "Supports a unified .NET implementation"
}
],
"securityConsiderations": [
"Use organisation identity"
],
"scalabilityConsiderations": [
"Scale application instances when required"
],
"risks": [
"Identity provider availability"
],
"relationships": [
{
"name": "User-Application",
"source": "User",
"target": "Application",
"relationshipType": "uses"
}
],
"assumptions": [
"Users have organisation accounts"
],
"mermaidDiagram": "graph TD\nA[User] --> B[Application]"
}
""";

                var proposal =
                JsonSerializer.Deserialize<ArchitectureProposal>(
                json,
                JsonOptions);

                Assert.NotNull(proposal);
                Assert.Equal("Internal note-taking architecture", proposal.Summary);
                Assert.Equal(2, proposal.Requirements.Count);
                Assert.Single(proposal.Components);
                Assert.Equal("Web Application", proposal.Components[0].Name);
                Assert.NotEmpty(proposal.SecurityConsiderations);
                Assert.NotEmpty(proposal.Risks);
                Assert.StartsWith("graph TD", proposal.MermaidDiagram);
            }

            [Fact]
            public void Deserialize_ShouldSupportCaseInsensitivePropertyNames()
            {
                const string json = """
{
"SUMMARY": "Test architecture",
"REQUIREMENTS": [],
"COMPONENTS": [],
"SECURITYCONSIDERATIONS": [],
"SCALABILITYCONSIDERATIONS": [],
"RISKS": [],
"RELATIONSHIPS": [],
"ASSUMPTIONS": [],
"MERMAIDDIAGRAM": "graph TD"
}
""";

                var proposal =
                JsonSerializer.Deserialize<ArchitectureProposal>(
                json,
                JsonOptions);

                Assert.NotNull(proposal);
                Assert.Equal("Test architecture", proposal.Summary);
            }

            [Fact]
            public void Deserialize_ShouldThrow_WhenJsonIsMalformed()
            {
                const string malformedJson = """
                {
                "summary": "Invalid",
                "components":
                }
                """;

                Assert.Throws<JsonException>(
                () => JsonSerializer.Deserialize<ArchitectureProposal>(
                malformedJson,
                JsonOptions));
            }
        }

        internal sealed class FakeSolutionArchitect : ISolutionArchitect
        {
            public ArchitectureRequest? ReceivedRequest { get; private set; }

            public Task<ArchitectureProposal> GenerateAsync(
            ArchitectureRequest request,
            CancellationToken cancellationToken = default)
            {
                ReceivedRequest = request;

                var proposal = new ArchitectureProposal
                {
                    Summary = "Generated by the fake architect.",
                    Requirements = ["Support the requested business capability"],
                    Components =
                [
                    new ArchitectureComponent
                    {
                        Name = "Application",
                        Responsibility = "Handles application behavior",
                        Technology = ".NET",
                        Rationale = "Test implementation"
                    }
                ],
                    Relationships = Array.Empty<ArchitectureRelationship>(),
                    SecurityConsiderations = ["Validate access"],
                    ScalabilityConsiderations = ["Scale when required"],
                    Risks = ["Test risk"],
                    Assumptions = ["Test assumption"],
                    MermaidDiagram = "graph TD\nA[User] --> B[Application]"
                };

                return Task.FromResult(proposal);
            }
        }

        public sealed class SolutionArchitectContractTests
        {
            [Fact]
            public async Task GenerateAsync_ShouldReturnStructuredProposal()
            {
                var architect = new FakeSolutionArchitect();

                var request = new ArchitectureRequest
                {
                    BusinessProblem =
                "Build an internal note-taking application.",
                    ExpectedUsers = "100",
                    Constraints = "Intranet only.",
                    PreferredTechnology = "any"
                };

                var result = await architect.GenerateAsync(request);

                Assert.Same(request, architect.ReceivedRequest);
                Assert.NotNull(result);
                Assert.NotEmpty(result.Summary);
                Assert.NotEmpty(result.Components);
                Assert.NotEmpty(result.MermaidDiagram);
            }
        }
    }
}
