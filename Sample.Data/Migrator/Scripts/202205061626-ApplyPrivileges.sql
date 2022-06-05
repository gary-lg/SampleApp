-- Allow access to all things we care about for the read-only user and ensure they get auto-applied in future
GRANT USAGE ON SCHEMA public TO sampleapp_ro;
ALTER DEFAULT PRIVILEGES FOR USER sampleapp IN SCHEMA public GRANT SELECT,REFERENCES	ON TABLES		TO sampleapp_ro;
ALTER DEFAULT PRIVILEGES FOR USER sampleapp IN SCHEMA public GRANT ALL					ON SEQUENCES	TO sampleapp_ro;
ALTER DEFAULT PRIVILEGES FOR USER sampleapp IN SCHEMA public GRANT EXECUTE				ON FUNCTIONS	TO sampleapp_ro;
ALTER DEFAULT PRIVILEGES FOR USER sampleapp IN SCHEMA public GRANT USAGE				ON TYPES		TO sampleapp_ro;

CREATE TABLE LookupResult (
     id UUID PRIMARY KEY,
     IpAddress TEXT NOT NULL,
     CountryCode TEXT NOT NULL,
     City TEXT NOT NULL,
     Zip TEXT NOT NULL,
     createdutc TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC')
);