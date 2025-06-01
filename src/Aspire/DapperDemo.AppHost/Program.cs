var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var sqlScriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "init.sql");
var sqlScriptContent = await File.ReadAllTextAsync(sqlScriptPath);

var sqlDb = sql.AddDatabase("app-db")
    .WithCreationScript(sqlScriptContent);

builder.AddProject<Projects.DapperDemo_Api>("dapper-demo-api", launchProfileName: "https")
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

builder.Build().Run();