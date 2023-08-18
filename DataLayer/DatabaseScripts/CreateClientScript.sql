
CREATE TABLE IF NOT EXISTS public.clients
(
    id uuid NOT NULL,
    client_type_id uuid NOT NULL,
    person_id uuid NOT NULL,
    CONSTRAINT client_pkey PRIMARY KEY (id),
    CONSTRAINT client_type_id_fkey FOREIGN KEY (client_type_id)
        REFERENCES public.client_type (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT person_client_fk FOREIGN KEY (person_id)
        REFERENCES public.person (id) MATCH SIMPLE
        ON DELETE CASCADE
);

