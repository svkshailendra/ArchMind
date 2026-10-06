# Authorization

Title: Authorization
ID: SEC-002
Category: Security
Source: ArchMind Internal Architecture Knowledge Base
Version: 1.0

## Overview

Authorization determines what an authenticated identity is allowed to
access or perform.

Authentication answers:

"Who are you?"

Authorization answers:

"What are you allowed to do?"

## Common approaches

- Role-based access control.
- Policy-based authorization.
- Permission-based access control.
- Resource-based authorization.

## When to use

Authorization should be implemented whenever different users, roles,
services, or tenants have different access rights.

## Guidance

Enforce authorization on the server.

Do not rely on client-side controls to protect sensitive operations.

Use least privilege.

Centralize reusable authorization policies where practical.

For resource-based operations, verify that the authenticated identity
has access to the specific resource.

## Security considerations

Do not assume authentication implies authorization.

Avoid trusting role or permission information supplied directly by the
client.

Review privileged operations carefully.

Log security-relevant authorization failures without exposing sensitive
information.

## Scalability considerations

Authorization policies should remain understandable as the application
grows.

For large systems, policy evaluation may need centralized management or
consistent enforcement across services.

## Risks

- Privilege escalation.
- Broken access control.
- Horizontal access violations.
- Incorrect role configuration.
- Missing authorization checks.

## Related patterns

- Authentication
- API Security
- API Gateway
