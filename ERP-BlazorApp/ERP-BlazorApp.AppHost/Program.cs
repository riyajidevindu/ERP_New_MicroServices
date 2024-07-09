var builder = DistributedApplication.CreateBuilder(args);



builder.AddProject<Projects.ERP_Training_Managements>("erp-training-managements");

builder.Build().Run();
