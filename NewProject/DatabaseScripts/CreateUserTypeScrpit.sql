
CREATE TABLE IF NOT EXISTS public.user_type
(
    id uuid NOT NULL,
    user_type character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT user_type_pkey PRIMARY KEY (id)
)
