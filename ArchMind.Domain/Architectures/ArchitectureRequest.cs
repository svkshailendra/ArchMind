namespace ArchMind.Domain.Architectures
{
    public sealed record ArchitectureRequest
    {
        public required string BusinessProblem { get; init; }

        public required string ExpectedUsers { get; init; }

        public required string Constraints { get; init; }

        public required string PreferredTechnology { get; init; }
    }   
}
