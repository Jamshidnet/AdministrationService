

CREATE TABLE IF NOT EXISTS public.roles
(
    id uuid NOT NULL,
    role_name character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT primarykey_of_roles PRIMARY KEY (id)
)