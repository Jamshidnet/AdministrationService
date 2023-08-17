
CREATE TABLE IF NOT EXISTS public.client_answers
(
    id uuid NOT NULL,
    answer_text text,
    default_answer_id uuid,
    doc_id uuid NOT NULL,
    question_id uuid NOT NULL,
    CONSTRAINT client_answer_pk PRIMARY KEY (id),
    CONSTRAINT default_answer_fk FOREIGN KEY (default_answer_id)
        REFERENCES public.default_answers (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT doc_fk FOREIGN KEY (doc_id)
        REFERENCES public.docs (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT question_fk FOREIGN KEY (question_id)
        REFERENCES public.questions (id) MATCH SIMPLE
        ON DELETE CASCADE
);
