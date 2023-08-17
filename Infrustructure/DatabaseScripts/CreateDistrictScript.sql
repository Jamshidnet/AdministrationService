
CREATE TABLE IF NOT EXISTS public.districts
(
    id uuid NOT NULL,
    district_name character varying(20) NOT NULL,
    region_id uuid NOT NULL,
    CONSTRAINT primarykey_of_districts PRIMARY KEY (id),
    CONSTRAINT districts_region_id_fkey FOREIGN KEY (region_id)
        REFERENCES public.regions (id) MATCH SIMPLE
        ON DELETE CASCADE
);
