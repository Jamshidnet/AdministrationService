
CREATE TABLE IF NOT EXISTS public.translate_question
(
    id uuid NOT NULL,
    owner_id uuid,
    language_id uuid,
    column_name character varying(100) ,
    translate_text character varying(100) ,
    CONSTRAINT translate_question_pkey PRIMARY KEY (id),
    CONSTRAINT unique_questions UNIQUE (owner_id, language_id, column_name),
    CONSTRAINT language_fk FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.questions (id) MATCH SIMPLE
        ON DELETE CASCADE
);

