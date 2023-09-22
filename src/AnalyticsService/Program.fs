namespace AnalyticsService

#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen()

        let app = builder.Build()

        app.UseHttpsRedirection()
        app.UseSwagger().UseSwaggerUI()

        app.UseAuthorization()
        app.MapControllers()

        Database.Management.init(null)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        app.Run()

        exitCode
