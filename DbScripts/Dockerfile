FROM postgres:11-alpine

ENV POSTGRES_DB=kariyerNetCookieManager
ENV POSTGRES_USER=kariyernet
ENV POSTGRES_PASSWORD=kariyernet
ENV PGDATA=/data

COPY postgresBackup.sql /docker-entrypoint-initdb.d/