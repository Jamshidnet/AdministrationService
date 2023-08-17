
CREATE TABLE IF NOT EXISTS public.translate_question_type
(
    id uuid NOT NULL,
    owner_id uuid,
    language_id uuid,
    translate_text character varying(100) ,
    column_name character varying(100) ,
    CONSTRAINT en_question_type_pkey PRIMARY KEY (id),
    CONSTRAINT unique_question_type UNIQUE (owner_id, language_id, column_name),
    CONSTRAINT langauge_fk FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.question_type (id) MATCH SIMPLE
        ON DELETE CASCADE
);
