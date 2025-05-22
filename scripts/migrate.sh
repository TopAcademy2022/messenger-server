#!/bin/bash

# Default MIGRATION_NAME = InitialCreate
# Usage: ./migrate.sh InitialCreate

MIGRATION_NAME=$1

if [ -z "$MIGRATION_NAME" ]; then
  echo "❌ Error: Migration name is required."
  exit 1
fi

dotnet ef database update "$MIGRATION_NAME" \
  --configuration Release \
  --project ../MessengerServer.Infrastructure \
  --startup-project ../MessengerServer.WebAPI \
  --context AppSqlServerDbContext
