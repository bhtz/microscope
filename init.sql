CREATE SCHEMA IF NOT EXISTS mcsp_identity;
CREATE SCHEMA IF NOT EXISTS mcsp_common;

create role anon nologin;
grant ALL on schema public to anon;