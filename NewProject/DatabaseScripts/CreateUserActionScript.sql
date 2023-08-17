
CREATE TABLE IF NOT EXISTS public.user_actions
(
    id uuid NOT NULL,
    user_id uuid,
    action_name character varying(50) ,
    table_id uuid,
    action_time timestamp without time zone,
    CONSTRAINT user_actions_pkey PRIMARY KEY (id),
    CONSTRAINT table_action FOREIGN KEY (table_id)
        REFERENCES public.sys_tables (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT user_fk FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON DELETE NO ACTION
);
