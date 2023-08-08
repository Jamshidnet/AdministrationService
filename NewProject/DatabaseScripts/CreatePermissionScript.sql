

CREATE TABLE IF NOT EXISTS public.permissions
(
    id uuid NOT NULL,
    permission_name character varying(70) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT primarykey_of_permissions PRIMARY KEY (id)
)