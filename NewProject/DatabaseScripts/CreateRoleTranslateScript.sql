
CREATE TABLE IF NOT EXISTS public.translate_role
(
    id uuid NOT NULL,
    owner_id uuid,
    language_id uuid,
    column_name character varying(100) ,
    translate_text character varying(100) ,
    CONSTRAINT translate_role_pkey PRIMARY KEY (id),
    CONSTRAINT unique_role UNIQUE (owner_id, column_name, language_id),
    CONSTRAINT language_fk FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.roles (id) MATCH SIMPLE
        ON DELETE CASCADE
);
