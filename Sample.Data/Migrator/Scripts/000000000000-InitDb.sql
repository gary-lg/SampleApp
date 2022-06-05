-- Run first to create DB & roles, this should be run as the PG Admin, 
-- OBVIOUSLY we don't want to use these passwords outside of dev so do an equivalent of this script 
-- manually in other environments
CREATE USER sampleapp_ro WITH ENCRYPTED PASSWORD 'skincare-evasion-grove-tavern-recapture-overlaid'; 
CREATE USER sampleapp WITH ENCRYPTED PASSWORD 'ecosystem-endless-vowel-stadium-attic-cufflink';
GRANT sampleapp_ro TO sampleapp;

CREATE DATABASE sampleappdb WITH ENCODING 'UTF8' OWNER sampleapp;
ALTER DATABASE sampleappdb SET TIMEZONE='UTC';

GRANT CONNECT ON DATABASE sampleappdb TO sampleapp_ro; -- RW will inherit
GRANT TEMP ON DATABASE sampleappdb TO sampleapp_ro; -- Allow RO queries to use temp tables

GRANT ALL PRIVILEGES ON DATABASE sampleappdb TO sampleapp WITH GRANT OPTION;