CREATE TABLE IF NOT EXISTS public.docs
(
    "Id" uuid NOT NULL,
    client_id uuid NOT NULL,
    user_id uuid NOT NULL,
    taken_date date,
    CONSTRAINT docs_pkey PRIMARY KEY ("Id")
)