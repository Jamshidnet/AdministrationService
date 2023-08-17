
CREATE TABLE IF NOT EXISTS public.docs
(
    id uuid NOT NULL,
    client_id uuid NOT NULL,
    user_id uuid NOT NULL,
    taken_date date,
    longitude numeric(100,0),
    latitude numeric(100,0),
    device character varying(70),
    CONSTRAINT docs_pkey PRIMARY KEY (id),
    CONSTRAINT cleint_doc_fk FOREIGN KEY (client_id)
        REFERENCES public.clients (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT user_doc_fk FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON DELETE CASCADE
);