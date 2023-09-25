namespace AnalyticsService

#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open AnalyticsService.Database.Migrations
open FluentMigrator.Runner

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        Dapper.FSharp.PostgreSQL.OptionTypes.register()

        let builder = WebApplication.CreateBuilder(args)

        let connectionString =
            match builder.Environment.IsDevelopment() with
            | true -> builder.Configuration["DbConnection"]
            | false -> System.Environment.GetEnvironmentVariable("DB_CONN")

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen()
        builder.Environment.IsDevelopment()
        
        builder.Services
            .AddFluentMigratorCore()
            .ConfigureRunner(fun i ->
                i.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn((typeof<InitMigration>).Assembly).For.Migrations()
                |> ignore
            )

        let app = builder.Build()

        app.UseHttpsRedirection()
        app.UseSwagger().UseSwaggerUI()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
