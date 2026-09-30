using ArchMind.Application.Architectures;
using ArchMind.Infrastructure.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; 

namespace ArchMind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        { 

            services
            .AddOptions<OllamaOptions>()
            .Bind(configuration.GetSection(OllamaOptions.SectionName))
            .Validate(
            options => Uri.TryCreate(
            options.Endpoint,
            UriKind.Absolute,
            out _),
            "Ollama endpoint must be a valid absolute URI.")
            .Validate(
            options => !string.IsNullOrWhiteSpace(options.Model),
            "Ollama model must be configured.")
            .Validate(
            options => options.TimeoutSeconds is >= 10 and <= 600,
            "Ollama timeout must be between 10 and 600 seconds.")
            .ValidateOnStart();

            services.AddScoped<ISolutionArchitect, OllamaSolutionArchitect>();

            return services;
        }
    }
}
