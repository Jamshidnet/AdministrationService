
CREATE TABLE IF NOT EXISTS public.users
(
    id uuid NOT NULL,
    username character varying(20)  NOT NULL,
    password character varying(100)  NOT NULL,
    salt_id uuid NOT NULL,
    person_id uuid NOT NULL,
    refresh_token text ,
    expires_date timestamp with time zone,
    user_type_id uuid,
    language_id uuid,
    CONSTRAINT user_id_pr PRIMARY KEY (id),
    CONSTRAINT users_username_key UNIQUE (username),
    CONSTRAINT language_id FOREIGN KEY (language_id)
        REFERENCES public.languages (id) MATCH SIMPLE
        ON DELETE NO ACTION,
    CONSTRAINT user_person_id FOREIGN KEY (person_id)
        REFERENCES public.person (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT user_type_fk FOREIGN KEY (user_type_id)
        REFERENCES public.user_type (id) MATCH SIMPLE
        ON DELETE CASCADE
);
