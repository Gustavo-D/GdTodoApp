--Primeiro execute o comando de criação do banco de dados
CREATE DATABASE "GdTodoApp";

--Depois, conecte no banco de dados criado e execute o script:
CREATE TABLE public."Tarefas" (
    "Id" bigint NOT NULL,
    "Title" character varying(384) NOT NULL,
    "Description" character varying(2048),
    "IsCompleted" integer NOT NULL,
    "Category" character varying(384),
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    "UpdatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    "UserId" bigint
);

ALTER TABLE public."Tarefas" OWNER TO postgres;

ALTER TABLE public."Tarefas" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Tarefas_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

CREATE TABLE public."Usuario" (
    "Id" bigint NOT NULL,
    "Username" character varying(256) NOT NULL,
    "PasswordHash" character(84) NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL
);

ALTER TABLE public."Usuario" OWNER TO postgres;

ALTER TABLE public."Usuario" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Usuario_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

ALTER TABLE ONLY public."Tarefas"
    ADD CONSTRAINT "Tarefas_pkey" PRIMARY KEY ("Id");

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT "Usuario_pkey" PRIMARY KEY ("Id");

ALTER TABLE ONLY public."Tarefas"
    ADD CONSTRAINT "FK_Tarefas_Usuario" FOREIGN KEY ("UserId") REFERENCES public."Usuario"("Id") MATCH FULL;