# Cache-Aside Pattern

Title: Cache-Aside Pattern
ID: PAT-005
Category: Architecture Pattern
Source: ArchMind Internal Architecture Knowledge Base
Version: 1.0

## Overview

Cache-aside is a caching pattern where the application checks the cache
before accessing the primary data store.

If the requested data is not in the cache, the application retrieves it
from the data store and places the result into the cache.

## Typical flow

1. Application receives a read request.
2. Application checks the cache.
3. If data exists, return the cached value.
4. If data does not exist, query the primary data store.
5. Store the result in the cache.
6. Return the result.

## When to use

Use cache-aside when:

- Data is read frequently.
- Data changes less frequently than it is read.
- Lower read latency is important.
- Database read load needs to be reduced.

## Benefits

- Reduces database read traffic.
- Can improve response latency.
- Cache can scale independently.
- Application retains control over what is cached.

## Risks

- Cache invalidation complexity.
- Stale data.
- Cache misses can create additional database load.
- Additional infrastructure.

## Guidance

Define appropriate expiration policies.

Consider whether stale data is acceptable.

Do not cache sensitive information without considering isolation,
encryption, expiration, and access-control requirements.

Avoid caching everything by default.

## Security considerations

Sensitive cached data must be protected appropriately.

Cache keys should not expose secrets or unnecessary personal information.

Consider tenant or user isolation when caching user-specific data.

## Scalability considerations

Caching can significantly reduce load on a primary database for
read-heavy workloads.

Monitor hit rate, miss rate, memory usage, eviction rate, and latency.

## Related patterns

- Caching
- Horizontal Scaling
- Database Scaling
