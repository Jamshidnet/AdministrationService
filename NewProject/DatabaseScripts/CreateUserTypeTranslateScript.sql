
CREATE TABLE IF NOT EXISTS public.translate_user_type
(
    id uuid NOT NULL,
    owner_id uuid,
    language_id uuid,
    column_name character varying(100) ,
    translate_text character varying(100) ,
    CONSTRAINT translate_user_type_pkey PRIMARY KEY (id),
    CONSTRAINT unique_user_type UNIQUE (owner_id, column_name, language_id),
    CONSTRAINT language_id FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.user_type (id) MATCH SIMPLE
        ON DELETE CASCADE
);
