module AnalyticsService.Program

#nowarn "20"
open System.Data
open AnalyticsService.Database.Context
open AnalyticsService.Transport.Validation
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open FluentValidation
open Microsoft.IdentityModel.Tokens

let exitCode = 0
type internal Marker = interface end

[<EntryPoint>]
let main args =
    printfn "Hello from analytics service! ʕ•ᴥ•ʔ"
    let builder = WebApplication.CreateBuilder(args)
    
    let connectionString =
        match builder.Environment.IsDevelopment() with
        | true -> builder.Configuration["DbConnection"]
        | false -> System.Environment.GetEnvironmentVariable("DB_CONN")
    printfn $"Connecting to a database on '{connectionString}'"
    builder.Services.AddTransient<IDbConnection>(fun i -> DbInit connectionString)

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(fun options -> (
            options.Authority <- "https://securetoken.google.com/ocr-microservice-project"
            options.TokenValidationParameters <- TokenValidationParameters (
                ValidateIssuer = true,
                ValidIssuer = "https://securetoken.google.com/ocr-microservice-project",
                ValidateAudience = true,
                ValidAudience = "ocr-microservice-project",
                ValidateLifetime = not(builder.Environment.IsDevelopment())
            )
        ))

    builder.Services.AddControllers()
    builder.Services.AddSwaggerGen()
    builder.Services.AddHealthChecks()
    builder.Services.AddMediatR(fun cfg ->
        cfg.RegisterServicesFromAssembly(typeof<Marker>.Assembly) |> ignore
    )
    builder.Services.AddValidatorsFromAssemblyContaining<BatchStatRequestValidator>()

    let app = builder.Build()
    if app.Environment.IsDevelopment() then
        app.UseSwagger().UseSwaggerUI() |> ignore
    app.MapSubscribeHandler()
    app.UseAuthorization()
    app.MapControllers()
    app.UseHealthChecks("/health")
    app.Run()

    exitCode
