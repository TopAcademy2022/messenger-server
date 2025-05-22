#!/bin/bash

# Usage: ./scripts/ef-add-migration.sh MigrationName [Environment] [Configuration]
# [Environment] = Development | Production
# [Configuration] = Debug | Release

set -e

# ==== Params ====
MIGRATION_NAME=$1
DB_PROVIDER=$2
ENVIRONMENT=${3:-Development}
CONFIGURATION=${4:-Debug}

# ==== Check param required ====
if [ -z "$MIGRATION_NAME" ] || [ -z "$DB_PROVIDER" ]; then
  echo "❌ Error: Migration name is required."
  echo "Usage: ./scripts/ef-add-migration.sh MigrationName [Environment] [Configuration]"
  exit 1
fi

# ==== Set environment ====
export ASPNETCORE_ENVIRONMENT=$ENVIRONMENT

# ==== Determine DbContext ====
if [ "$ENVIRONMENT" == "Development" ]; then
  CONTEXT="AppSqliteDbContext" # Or AppSqlServerDbContext, depending on your dev setup
elif [ "$ENVIRONMENT" == "Production" ]; then
  CONTEXT="AppSqlServerDbContext" # Production likely uses SQL Server
else
  echo "❌ Error: Invalid environment. Supported environments are Development and Production."
  exit 1
fi

# ==== Run migration ====
dotnet ef migrations add "$MIGRATION_NAME" \
  --configuration "$CONFIGURATION" \
  --project ../MessengerServer.Infrastructure \
  --startup-project ../MessengerServer.WebAPI \
  --output-dir Persistence/Migrations/$DB_PROVIDER \
  --context "$CONTEXT" \
  -- --provider $DB_PROVIDER
