
CREATE TABLE IF NOT EXISTS public.client_type
(
    id uuid NOT NULL,
    type_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "UserType_pkey" PRIMARY KEY (id)
)