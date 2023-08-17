
CREATE TABLE IF NOT EXISTS public.person
(
    id uuid NOT NULL,
    first_name character varying(50)  NOT NULL,
    last_name character varying(50)  NOT NULL,
    birthdate date NOT NULL,
    phone_number character varying(20) ,
    quarter_id uuid,
    CONSTRAINT person_pkey PRIMARY KEY (id),
    CONSTRAINT quarter_fk FOREIGN KEY (quarter_id)
        REFERENCES public.quarters (id) MATCH SIMPLE
        ON DELETE CASCADE
);
