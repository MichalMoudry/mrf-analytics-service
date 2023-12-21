namespace AnalyticsService.TaskService

open System
open System.Threading
open System.Threading.Tasks
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

type ArchiveService(logger: ILogger<ArchiveService>) =
    inherit BackgroundService()

    override _.ExecuteAsync(ct: CancellationToken) =
        task {
            while not ct.IsCancellationRequested do
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now
                )
                do! Task.Delay(TimeSpan.FromSeconds(5))
        }