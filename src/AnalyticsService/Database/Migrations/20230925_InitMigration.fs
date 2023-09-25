namespace AnalyticsService.Database.Migrations

open FluentMigrator

/// An initial migration of the database.
type InitMigration () =
    inherit Migration()

    override this.Up() =
        this.Create.Table("batch_stats")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            |> ignore

    override this.Down() =
        this.Delete.Table("batch_stats")
        |> ignore
