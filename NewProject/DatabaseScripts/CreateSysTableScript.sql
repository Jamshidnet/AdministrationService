CREATE TABLE IF NOT EXISTS public.sys_tables
(
    id uuid NOT NULL,
    table_id integer,
    table_name character varying(50) ,
    CONSTRAINT sys_tables_pkey PRIMARY KEY (id)
);
