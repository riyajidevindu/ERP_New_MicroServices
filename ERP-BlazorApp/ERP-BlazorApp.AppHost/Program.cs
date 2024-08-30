var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ERP__Training__Management>("erp-training-management");

builder.Build().Run();
