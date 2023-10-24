BEGIN;

CREATE TABLE analytics."BatchStats" (
    "Id" UUID PRIMARY KEY,
    "StartDate" TIMESTAMP NOT NULL,
    "EndDate" TIMESTAMP NOT NULL,
    "NumberOfDocuments" INTEGER NOT NULL,
    "RunTime" INTERVAL NOT NULL,
    "Status" SMALLINT NOT NULL,
    "AppId" UUID NOT NULL,
    "Created" TIMESTAMP NOT NULL
);

CREATE TABLE analytics."DLQ" (
    "Id" UUID PRIMARY KEY,
    "Endpoint" VARCHAR(255) NOT NULL,
    "RequestData" BYTEA NOT NULL,
    "Source" VARCHAR(128) NOT NULL,
    "DateAdded" TIMESTAMP NOT NULL
);

COMMIT;