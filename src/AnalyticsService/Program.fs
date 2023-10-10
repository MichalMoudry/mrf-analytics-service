namespace AnalyticsService

#nowarn "20"
open System.Data
open AnalyticsService.Database.Context
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Transport.Validation
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open FluentValidation

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        let connectionString =
            match builder.Environment.IsDevelopment() with
            | true -> builder.Configuration["DbConnection"]
            | false -> System.Environment.GetEnvironmentVariable("DB_CONN")

        printfn $"Connecting to a database on '{connectionString}'"
        builder.Services.AddTransient<IDbConnection>(fun i -> DbInit connectionString)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen()
        builder.Services.AddHealthChecks()
        builder.Services.AddMediatR(fun cfg ->
            cfg.RegisterServicesFromAssemblyContaining<GetGenericStatsQuery>()
            |> ignore
        )
        builder.Services
            .AddValidatorsFromAssemblyContaining<BatchStatRequestValidator>()


        let app = builder.Build()

        //app.UseHttpsRedirection()
        app.UseSwagger().UseSwaggerUI()
        app.MapSubscribeHandler()
        app.UseAuthorization()
        app.MapControllers()
        app.UseHealthChecks("/health")

        app.Run()
        exitCode
