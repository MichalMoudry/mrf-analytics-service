namespace AnalyticsService.Web.Api

#nowarn "20"
open System
open System.Data
open AnalyticsService.Database.Api
open AnalyticsService.Database.Api.Extensions
open AnalyticsService.Web.Api.Extensions
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        printfn "Hello from analytics service! ʕ•ᴥ•ʔ"
        let builder = WebApplication.CreateSlimBuilder(args)
        let connectionString =
            match builder.Environment.IsDevelopment() with
            | true -> builder.Configuration["DbConnection"]
            | false -> Environment.GetEnvironmentVariable("DB_CONN")

        builder.Services.AddSwaggerGen()
        builder.Services.AddHealthChecks()
        builder.Services.AddRepositories()
        builder.Services.AddTransient<IDbConnection>(
            fun i -> Connector.GetConnection(connectionString)
        )
        builder.Services
            .AddMediatR(fun cfg ->
                cfg.RegisterServicesFromAssemblyContaining<Transport.Controllers.DaprController>() |> ignore
            )
            .AddValidators()

        let app = builder.Build()

        app.UseSwagger()
        if app.Environment.IsDevelopment() then app.UseSwaggerUI() |> ignore
        app.MapSubscribeHandler()
        app.UseHealthChecks("/healthz")

        app.Run()

        exitCode
