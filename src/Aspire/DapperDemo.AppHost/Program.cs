var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var postgres = builder
    .AddPostgres("postgres", postgresUsername, postgresPassword)
    .WithDataVolume()
    .WithPgAdmin();

var postgresDb = postgres.AddDatabase("dapper-demo-db");

builder.AddProject<Projects.DapperDemo_Api>("dapper-demo-api", launchProfileName: "https")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

builder.Build().Run();