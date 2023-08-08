

CREATE TABLE IF NOT EXISTS public.client_answers
(
    id uuid NOT NULL,
    answer_text text COLLATE pg_catalog."default",
    default_answer_id uuid,
    doc_id uuid NOT NULL,
    question_id uuid NOT NULL,
    CONSTRAINT client_answer_pk PRIMARY KEY (id),
    CONSTRAINT default_answer_fk FOREIGN KEY (default_answer_id)
        REFERENCES public.default_answers (id)  ,
    CONSTRAINT doc_fk FOREIGN KEY (doc_id)
        REFERENCES public.docs (id)  ,
    CONSTRAINT question_fk FOREIGN KEY (question_id)
        REFERENCES public.questions (id)  
)
