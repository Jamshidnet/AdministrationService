
CREATE TABLE IF NOT EXISTS public.client_category
(
    client_id uuid NOT NULL,
    category_id uuid NOT NULL,
    CONSTRAINT client_category_pkey PRIMARY KEY (client_id, category_id),
    CONSTRAINT category_fk FOREIGN KEY (category_id)
        REFERENCES public.categories (id) MATCH SIMPLE,
    CONSTRAINT client_fk FOREIGN KEY (client_id)
        REFERENCES public.clients (id) MATCH SIMPLE
)