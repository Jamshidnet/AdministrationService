CREATE TABLE IF NOT EXISTS public.question_type
(
    id uuid NOT NULL,
    question_type_name character varying(50)  NOT NULL,
    CONSTRAINT question_type_pkey PRIMARY KEY (id)
);
