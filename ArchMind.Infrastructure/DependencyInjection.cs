using ArchMind.Application.Architectures;
using ArchMind.Infrastructure.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace ArchMind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {

            services.Configure<AIOptions>(
        configuration.GetSection(AIOptions.SectionName));

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

            services.Configure<GroqOptions>(
        options =>
        {
            configuration
                .GetSection(GroqOptions.SectionName)
                .Bind(options);

            options.ApiKey =
                configuration["Groq:ApiKey"]
                ?? configuration["GROQ_API_KEY"]
                ?? string.Empty;
        });

            services.AddScoped<ISolutionArchitect, AiSolutionArchitect>();

            return services;
        }
    }
}
