CREATE TABLE IF NOT EXISTS public.languages
(
    id uuid NOT NULL,
    language_name character varying(50),
    CONSTRAINT translate_bindings_pkey PRIMARY KEY (id)
);
