
CREATE TABLE IF NOT EXISTS public.translate_categories
(
    id uuid NOT NULL,
    owner_id uuid,
    translate_text character varying(100) ,
    column_name character varying(100) ,
    language_id uuid,
    CONSTRAINT tr_categories_pkey PRIMARY KEY (id),
    CONSTRAINT value_unique UNIQUE (owner_id, column_name, language_id),
    CONSTRAINT language_fk FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT owner_fk FOREIGN KEY (owner_id)
        REFERENCES public.categories (id) MATCH SIMPLE
        ON DELETE CASCADE
)