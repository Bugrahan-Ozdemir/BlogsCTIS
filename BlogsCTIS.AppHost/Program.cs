var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.API_BLOGS>("api-blogs");

builder.AddProject<Projects.API_USERS>("api-users");

builder.Build().Run();
