# Authentication

Title: Authentication
ID: SEC-001
Category: Security
Source: ArchMind Internal Architecture Knowledge Base
Version: 1.0

## Overview

Authentication verifies the identity of a user, service, or system
before access is granted.

Common approaches include session-based authentication, OAuth 2.0,
OpenID Connect, and signed access tokens.

## When to use

Authentication should be used when an application needs to distinguish
between users or services and protect resources from anonymous access.

## Guidance

Use established authentication protocols and libraries rather than
implementing cryptographic authentication mechanisms from scratch.

Passwords should never be stored in plaintext.

Use strong password hashing algorithms when password authentication is
required.

For browser applications, consider secure session management and
appropriate cookie security settings.

For distributed systems, use established identity protocols such as
OAuth 2.0 and OpenID Connect where appropriate.

## Security considerations

Use HTTPS for authenticated communication.

Protect authentication credentials and tokens.

Configure appropriate token expiration.

Use secure, HttpOnly, and SameSite cookie settings when cookies are used.

Do not log passwords, access tokens, refresh tokens, or other
authentication secrets.

## Scalability considerations

Authentication infrastructure should support horizontal scaling where
required.

Stateless token-based approaches can simplify horizontal scaling, but
token revocation and lifecycle management must still be considered.

## Risks

- Credential theft.
- Session hijacking.
- Token leakage.
- Weak password storage.
- Incorrect token validation.
- Long-lived credentials.

## Related patterns

- Authorization
- API Security
- Secrets Management
