
CREATE TABLE IF NOT EXISTS public.default_answers
(
    id uuid NOT NULL,
    question_id uuid NOT NULL,
    answer_text text  NOT NULL,
    CONSTRAINT default_answers_pkey PRIMARY KEY (id),
    CONSTRAINT question_fk FOREIGN KEY (question_id)
        REFERENCES public.questions (id) MATCH SIMPLE
        ON DELETE CASCADE
);