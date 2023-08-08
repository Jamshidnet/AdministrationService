
CREATE TABLE IF NOT EXISTS public.users
(
    id uuid NOT NULL,
    username character varying(20) COLLATE pg_catalog."default" NOT NULL,
    password character varying(100) COLLATE pg_catalog."default" NOT NULL,
    salt_id uuid NOT NULL,
    person_id uuid NOT NULL,
    refresh_token text COLLATE pg_catalog."default",
    expires_date timestamp with time zone,
    user_type_id uuid NOT NULL,
    CONSTRAINT user_id_pr PRIMARY KEY (id),
    CONSTRAINT users_username_key UNIQUE (username),
    CONSTRAINT user_person_id FOREIGN KEY (person_id)
        REFERENCES public.person (id) MATCH SIMPLE,
    CONSTRAINT user_type_fk FOREIGN KEY (user_type_id)
        REFERENCES public.user_type (id) MATCH SIMPLE
)