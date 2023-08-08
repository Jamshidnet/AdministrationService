
CREATE TABLE IF NOT EXISTS public.person
(
    id uuid NOT NULL,
    first_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    last_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    birthdate date NOT NULL,
    phone_number character varying(20) COLLATE pg_catalog."default",
    quarter_id uuid,
    CONSTRAINT person_pkey PRIMARY KEY (id),
    CONSTRAINT quarter_fk FOREIGN KEY (quarter_id)
        REFERENCES public.quarters (id) MATCH SIMPLE
)
