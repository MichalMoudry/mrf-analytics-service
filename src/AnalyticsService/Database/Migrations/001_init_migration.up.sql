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

COMMIT;