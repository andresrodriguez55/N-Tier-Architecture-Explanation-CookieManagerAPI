--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4 (Debian 14.4-1.pgdg110+1)
-- Dumped by pg_dump version 14.4 (Debian 14.4-1.pgdg110+1)

\connect kariyerNetCookieManager

-- SET statement_timeout = 0;
-- SET lock_timeout = 0;
-- SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
-- SET standard_conforming_strings = on;
-- SELECT pg_catalog.set_config('search_path', '', false);
-- SET check_function_bodies = false;
-- SET xmloption = content;
-- SET client_min_messages = warning;
-- SET row_security = off;

-- SET default_tablespace = '';

-- SET default_table_access_method = heap;

--
-- Name: Cookies; Type: TABLE; Schema: public; Owner: kariyernet
--

CREATE TABLE public."Cookies" (
    "Id" integer NOT NULL,
    "SessionId" character varying(64) NOT NULL,
    "Status" boolean NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "WebSiteCookieTypeDefinitionId" integer NOT NULL
);


ALTER TABLE public."Cookies" OWNER TO kariyernet;

--
-- Name: Cookies_Id_seq; Type: SEQUENCE; Schema: public; Owner: kariyernet
--

ALTER TABLE public."Cookies" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Cookies_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: WebSiteCookieTypeDefinitions; Type: TABLE; Schema: public; Owner: kariyernet
--

CREATE TABLE public."WebSiteCookieTypeDefinitions" (
    "Id" integer NOT NULL,
    "CookieType" character varying(50) NOT NULL,
    "Title" character varying(40) NOT NULL,
    "Description" character varying(150) NOT NULL,
    "IsRequired" boolean NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "IsActive" boolean NOT NULL,
    "WebSiteId" integer NOT NULL
);


ALTER TABLE public."WebSiteCookieTypeDefinitions" OWNER TO kariyernet;

--
-- Name: WebSiteCookieTypeDefinitions_Id_seq; Type: SEQUENCE; Schema: public; Owner: kariyernet
--

ALTER TABLE public."WebSiteCookieTypeDefinitions" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."WebSiteCookieTypeDefinitions_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: WebSites; Type: TABLE; Schema: public; Owner: kariyernet
--

CREATE TABLE public."WebSites" (
    "Id" integer NOT NULL,
    "Name" character varying(100) NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL
);


ALTER TABLE public."WebSites" OWNER TO kariyernet;

--
-- Name: WebSites_Id_seq; Type: SEQUENCE; Schema: public; Owner: kariyernet
--

ALTER TABLE public."WebSites" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."WebSites_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: kariyernet
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO kariyernet;

--
-- Data for Name: Cookies; Type: TABLE DATA; Schema: public; Owner: kariyernet
--

COPY public."Cookies" ("Id", "SessionId", "Status", "CreatedDate", "WebSiteCookieTypeDefinitionId") FROM stdin;
1	1344223ee2e2	t	2022-08-18 10:45:16.957961+00	1
\.


--
-- Data for Name: WebSiteCookieTypeDefinitions; Type: TABLE DATA; Schema: public; Owner: kariyernet
--

COPY public."WebSiteCookieTypeDefinitions" ("Id", "CookieType", "Title", "Description", "IsRequired", "CreatedDate", "IsActive", "WebSiteId") FROM stdin;
1	performance	performans	site performans?? art??r??r	t	2022-08-18 08:31:42.591362+00	t	1
2	performance	performans	site performans?? art??r??r	f	2022-08-18 08:31:51.395999+00	t	2
3	performance	performans	site performans?? art??r??r	f	2022-08-18 08:32:00.073999+00	f	3
\.


--
-- Data for Name: WebSites; Type: TABLE DATA; Schema: public; Owner: kariyernet
--

COPY public."WebSites" ("Id", "Name", "CreatedDate") FROM stdin;
1	kariyer	2022-08-18 08:27:11.215614+00
2	iskolig	2022-08-18 08:30:32.969187+00
3	coensio	2022-08-18 08:30:45.875391+00
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: kariyernet
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20220818072915_initial	6.0.8
20220820123835_initial1	6.0.8
\.


--
-- Name: Cookies_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: kariyernet
--

SELECT pg_catalog.setval('public."Cookies_Id_seq"', 1, true);


--
-- Name: WebSiteCookieTypeDefinitions_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: kariyernet
--

SELECT pg_catalog.setval('public."WebSiteCookieTypeDefinitions_Id_seq"', 3, true);


--
-- Name: WebSites_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: kariyernet
--

SELECT pg_catalog.setval('public."WebSites_Id_seq"', 8, true);


--
-- Name: Cookies PK_Cookies; Type: CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."Cookies"
    ADD CONSTRAINT "PK_Cookies" PRIMARY KEY ("Id");


--
-- Name: WebSiteCookieTypeDefinitions PK_WebSiteCookieTypeDefinitions; Type: CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."WebSiteCookieTypeDefinitions"
    ADD CONSTRAINT "PK_WebSiteCookieTypeDefinitions" PRIMARY KEY ("Id");


--
-- Name: WebSites PK_WebSites; Type: CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."WebSites"
    ADD CONSTRAINT "PK_WebSites" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Cookies_WebSiteCookieTypeDefinitionId; Type: INDEX; Schema: public; Owner: kariyernet
--

CREATE INDEX "IX_Cookies_WebSiteCookieTypeDefinitionId" ON public."Cookies" USING btree ("WebSiteCookieTypeDefinitionId");


--
-- Name: IX_WebSiteCookieTypeDefinitions_WebSiteId; Type: INDEX; Schema: public; Owner: kariyernet
--

CREATE INDEX "IX_WebSiteCookieTypeDefinitions_WebSiteId" ON public."WebSiteCookieTypeDefinitions" USING btree ("WebSiteId");


--
-- Name: Cookies FK_Cookies_WebSiteCookieTypeDefinitions_WebSiteCookieTypeDefin~; Type: FK CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."Cookies"
    ADD CONSTRAINT "FK_Cookies_WebSiteCookieTypeDefinitions_WebSiteCookieTypeDefin~" FOREIGN KEY ("WebSiteCookieTypeDefinitionId") REFERENCES public."WebSiteCookieTypeDefinitions"("Id") ON DELETE CASCADE;


--
-- Name: WebSiteCookieTypeDefinitions FK_WebSiteCookieTypeDefinitions_WebSites_WebSiteId; Type: FK CONSTRAINT; Schema: public; Owner: kariyernet
--

ALTER TABLE ONLY public."WebSiteCookieTypeDefinitions"
    ADD CONSTRAINT "FK_WebSiteCookieTypeDefinitions_WebSites_WebSiteId" FOREIGN KEY ("WebSiteId") REFERENCES public."WebSites"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

