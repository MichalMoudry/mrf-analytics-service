SELECT
    b."Id",
    b."StartDate",
    b."EndDate",
    b."NumberOfDocuments",
    b."RunTime",
    b."Status"
FROM
    "BatchStats" as b
WHERE
    b."WorkflowId" = @WorkflowId