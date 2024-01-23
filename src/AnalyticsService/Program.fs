namespace AnalyticsService

#nowarn "20"
open AnalyticsService.Extensions
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

[<Sealed>]
module Program =
    open AnalyticsService.Service.Queries
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        //builder.Services.AddSwaggerGen()
        builder.Services.AddAuthorization()
        builder.Services.AddHealthChecks()
        builder.Services.AddMediatR(fun cfg ->
            cfg.RegisterServicesFromAssemblyContaining<GetGenericStatsQuery>() |> ignore
        )
        builder.Services.RegisterValidators()

        let app = builder.Build()

        //app.UseHttpsRedirection()
        (*
        if app.Environment.IsDevelopment() then
            app.UseSwagger().UseSwaggerUI() |> ignore
        *)
        app.MapSubscribeHandler()
        app.UseAuthorization()
        app.UseHealthChecks("/health")

        app.MapGet("/", )

        app.Run()

        exitCode
