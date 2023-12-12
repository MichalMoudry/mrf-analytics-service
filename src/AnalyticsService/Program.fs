namespace AnalyticsService

#nowarn "20"
open AnalyticsService.Extensions
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

[<Sealed>]
module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.RegisterValidators()
        builder.Services.AddSwaggerGen()
        builder.Services.AddHealthChecks()
        builder.Services.AddControllers()

        let app = builder.Build()

        //app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()
        do
            if app.Environment.IsDevelopment() then
                app.UseSwagger().UseSwaggerUI() |> ignore
        app.UseHealthChecks("/health")

        app.Run()

        exitCode
