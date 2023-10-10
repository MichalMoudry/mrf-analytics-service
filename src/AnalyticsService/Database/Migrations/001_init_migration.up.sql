BEGIN;

CREATE TABLE batch_stats (
    id UUID PRIMARY KEY,
    start_date TIMESTAMP NOT NULL,
    end_date TIMESTAMP NOT NULL,
    number_of_documents INTEGER NOT NULL,
    run_time INTERVAL NOT NULL,
    status SMALLINT NOT NULL
);

COMMIT;