
CREATE TABLE IF NOT EXISTS public.translate_default_answers
(
    id uuid NOT NULL,
    owner_id uuid,
    language_id uuid,
    column_name character varying(100) ,
    translate_text character varying(100) ,
    CONSTRAINT en_default_answers_pkey PRIMARY KEY (id),
    CONSTRAINT unique_cons_s UNIQUE (owner_id, language_id, column_name),
    CONSTRAINT language_fk FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.default_answers (id) MATCH SIMPLE
        ON DELETE CASCADE
);