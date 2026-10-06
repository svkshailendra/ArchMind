# Modular Monolith

Title: Modular Monolith
ID: PAT-001
Category: Architecture Pattern
Source: ArchMind Internal Architecture Knowledge Base
Version: 1.0

## Overview

A modular monolith is a single deployable application organized into
well-defined internal modules. Each module owns a specific business
capability and communicates with other modules through explicit
interfaces or application contracts.

## When to use

Use a modular monolith when:

- The system has several related business capabilities.
- A single deployment is operationally preferable.
- The team wants strong module boundaries without distributed-system
  complexity.
- The expected scale does not require independently deployed services.
- The domain is still evolving and service boundaries are uncertain.

## Benefits

- Simple deployment and operations.
- Lower network complexity than microservices.
- Clear internal boundaries.
- Easier local development and debugging.
- Modules can potentially be extracted into services later.

## Risks

- Poor module boundaries can result in a tightly coupled monolith.
- A single deployment can become a scaling bottleneck.
- Teams may bypass module boundaries over time.
- Database ownership can become unclear.

## Guidance

Keep business capabilities separated into modules.

Prefer explicit interfaces and contracts between modules.

Avoid allowing one module to directly manipulate another module's
internal data structures.

Do not introduce microservices solely because the application contains
multiple business domains.

## Security considerations

Apply authorization at module and application boundaries where
appropriate. Sensitive data should remain owned by the module responsible
for it.

## Scalability considerations

A modular monolith can scale horizontally by running multiple instances.
If one capability eventually requires independent scaling or deployment,
it may be extracted into a separate service.

## Related patterns

- Layered Architecture
- API Gateway
- Asynchronous Processing
