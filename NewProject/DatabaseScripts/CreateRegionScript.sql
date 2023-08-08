
CREATE TABLE IF NOT EXISTS public.regions
(
    id uuid NOT NULL,
    region_name character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT primarykey_of_regions PRIMARY KEY (id)
)