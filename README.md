# ArchiMind

ArchiMind is a local-first AI solution architecture assistant built with .NET and Blazor. It converts business requirements, expected scale, constraints, and optional technology preferences into a structured and explainable architecture proposal.

The application uses a single AI agent backed by Ollama, so the core experience can run locally without paid AI APIs.

## Current status

**Functional MVP**

ArchiMind currently supports:

- Business requirements and constraint capture
- Neutral technology selection when the preference is `any`
- Structured architecture summaries
- Component responsibilities, technologies, and rationale
- Explicit component relationships
- Security and scalability considerations
- Risks and assumptions
- Mermaid-based architecture diagram generation and rendering
- Input validation, timeout handling, and controlled AI failure handling
- Local AI inference with no API key

## Screenshots

Add the current screenshots to `docs/images` using these names:

```text
docs/images/archimind-overview.png
docs/images/archimind-diagram.png
```

Then uncomment the image references below:

<!-- ![ArchiMind overview](docs/images/archimind-overview.png) -->
<!-- ![Generated architecture diagram](docs/images/archimind-diagram.png) -->

## How it works

```text
User requirements
       |
       v
Blazor Web App
       |
       v
ISolutionArchitect
       |
       v
SolutionArchitectAgent
       |
       v
Local Ollama model
       |
       v
Validated ArchitectureProposal
       |
       v
Structured UI and Mermaid diagram
```

The application uses one focused agent. Deterministic application behavior remains in standard C# services, while the language model handles architecture reasoning and proposal generation.

## Technology stack

- .NET 10
- ASP.NET Core
- Blazor Web App with Interactive Server rendering
- Microsoft Agent Framework
- Microsoft.Extensions.AI
- Ollama
- Mermaid
- xUnit

## Solution structure

```text
ArchiMind/
├── src/
│   ├── ArchiMind.Web/
│   ├── ArchiMind.Application/
│   ├── ArchiMind.Domain/
│   └── ArchiMind.Infrastructure/
├── tests/
│   └── ArchiMind.UnitTests/
├── knowledge/
├── docs/
└── ArchiMind.sln
```

### Project responsibilities

- `ArchiMind.Web`: Blazor UI, form models, presentation, and dependency composition
- `ArchiMind.Application`: Use-case contracts and application exceptions
- `ArchiMind.Domain`: Architecture request, proposal, component, and relationship models
- `ArchiMind.Infrastructure`: Agent Framework and Ollama integration
- `ArchiMind.UnitTests`: Unit tests for validation and deterministic application logic

## Prerequisites

Install:

- .NET 10 SDK
- Ollama
- Git
- A supported Ollama model such as `llama3.2`

Verify the installations:

```bash
dotnet --version
ollama --version
git --version
```

## Run locally

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd ArchiMind
```

### 2. Download the model

```bash
ollama pull llama3.2
```

### 3. Start Ollama

```bash
ollama serve
```

### 4. Configure the application

Update `src/ArchiMind.Web/appsettings.json` if required:

```json
{
  "Ollama": {
    "Endpoint": "http://localhost:11434",
    "Model": "llama3.2",
    "TimeoutSeconds": 180
  }
}
```

### 5. Build and test

```bash
dotnet restore
dotnet build
dotnet test
```

### 6. Run the web application

```bash
dotnet watch --project src/ArchiMind.Web
```

Open the URL shown in the terminal and navigate to `/architect`.

## Example input

```text
Business problem:
Build an internal note-taking application with complete history and multiple profiles per user.

Expected users or scale:
100 users

Constraints:
Intranet-only application, high security, and work-email authentication.

Preferred technology:
any
```

When `Preferred technology` is `any`, the agent is expected to select an appropriate stack based on the supplied requirements and explain the rationale. It does not automatically prefer .NET.

## Design principles

- Start with one agent and scale only when justified
- Keep model-provider details behind application interfaces
- Prefer strongly typed outputs over free-form text
- Validate AI responses before displaying them
- Treat user input as untrusted data
- Make assumptions, risks, and trade-offs visible
- Avoid premature multi-agent orchestration
- Keep the local development path free of paid services

## Known limitations

- Architecture quality depends on the selected local model
- Generated recommendations still require human review
- Clarification questions are not implemented yet
- Architecture knowledge is currently model-based and is not grounded through RAG
- Conversation history and saved proposals are not implemented
- Public cloud deployment requires a reachable model provider or a separately hosted model runtime

## Roadmap

### Milestone 1: Functional MVP

- [x] Clean .NET solution structure
- [x] Blazor requirements form
- [x] Single local AI agent
- [x] Structured architecture proposal
- [x] Architecture relationships
- [x] Security, scalability, risks, and assumptions
- [x] Rendered Mermaid diagram

### Milestone 2: Repository hardening

- [ ] Add unit tests for request and response validation
- [ ] Add integration tests with a replaceable fake AI implementation
- [ ] Add GitHub Actions build and test workflow
- [ ] Add Docker support for repeatable local setup
- [ ] Add screenshots and an animated demo
- [ ] Add sample architecture requests
- [ ] Add structured logging and health checks

### Milestone 3: Grounded architecture knowledge

- [ ] Add a curated local architecture knowledge base
- [ ] Add document chunking and embeddings
- [ ] Add local semantic retrieval
- [ ] Include sources used for recommendations
- [ ] Add architecture-pattern and security-checklist grounding

### Milestone 4: Product capabilities

- [ ] Save and reopen proposals
- [ ] Export proposals to Markdown or PDF
- [ ] Compare alternative architectures
- [ ] Add clarification questions
- [ ] Add architecture evaluation and quality scoring

### Milestone 5: Recruiter demo

- [ ] Add a safe public demo mode
- [ ] Deploy the web application
- [ ] Configure a deployment-compatible model provider
- [ ] Add CI/CD
- [ ] Add an architecture document and portfolio case study

## Testing strategy

The project should separate deterministic tests from model-dependent tests.

### Unit tests

Test without Ollama:

- Request validation
- Response validation
- JSON extraction and deserialization
- Constraint checks
- Duplicate component or relationship detection
- Empty and malformed model responses

### Integration tests

Run separately when Ollama is available:

- Agent connectivity
- Structured response generation
- Cancellation and timeout behavior
- End-to-end request-to-proposal flow

Model-dependent tests should verify structure and invariants rather than exact wording.

## Security notes

- Do not commit secrets or environment-specific credentials
- Validate and limit all user-controlled text
- Apply request timeouts and cancellation
- Do not execute generated Mermaid or model output as arbitrary code
- Review generated technology and security recommendations before implementation
- Use server-side execution for model access

## Contributing

This repository is currently a personal portfolio project. Issues and constructive suggestions are welcome once the repository is made public.

## License

Add a license before publishing the repository. The MIT License is suitable if the intention is to allow reuse with attribution.

## Acknowledgements

- Microsoft Agent Framework
- Ollama
- Mermaid
- .NET and Blazor
