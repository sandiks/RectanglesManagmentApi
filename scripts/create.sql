create database "rectangledb";

create user rectuser with password 'test123';

GRANT ALL PRIVILEGES ON DATABASE "rectangledb" to rectuser;

GRANT ALL ON ALL TABLES IN SCHEMA "public" TO rectuser;

GRANT
SELECT
    ON ALL TABLES IN SCHEMA public TO rectuser;

CREATE TABLE simple_rectangle (
    id SERIAL PRIMARY KEY,
    x int,
    y int,
    xx int,
    yy int,
    created TIMESTAMP
);

CREATE TABLE rectangle (
    id SERIAL PRIMARY KEY,
    x double precision,
    y double precision,
    width double precision,
    hight double precision,
    alpha double precision,
    created TIMESTAMP
);
