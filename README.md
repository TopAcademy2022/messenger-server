# messenger
The simple messenger.

## Dependencies
1. dotnet tool install --global dotnet-ef

## Migration moment
For script first create use:
1. ./ef-add-migration.sh InitialCreate Sqlite

1. export ASPNETCORE_ENVIRONMENT=Production
2. export ConnectionStrings__Default=...SERVER=LOCALHOST!!!
3. ./ef-add-migration.sh InitialCreate SqlServer Production Release
4. For Ms Sql use 1 and 2, after ./migrate.sh InitCreate