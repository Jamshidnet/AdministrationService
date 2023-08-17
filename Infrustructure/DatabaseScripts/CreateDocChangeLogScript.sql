CREATE TABLE IF NOT EXISTS public.doc_change_log
(
    id uuid NOT NULL,
    user_id uuid,
    table_id uuid,
    doc_id uuid,
    date_at date,
    action_name character varying(50),
    CONSTRAINT doc_change_log_pkey PRIMARY KEY (id),
    CONSTRAINT doc_fk FOREIGN KEY (doc_id)
        REFERENCES public.docs (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT table_id FOREIGN KEY (table_id)
        REFERENCES public.sys_tables (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT user_fk FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON DELETE CASCADE
);
