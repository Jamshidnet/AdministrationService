
CREATE TABLE IF NOT EXISTS public.quarters
(
    id uuid NOT NULL,
    quarter_name character varying(20)  NOT NULL,
    district_id uuid NOT NULL,
    CONSTRAINT primarykey_of_quarters PRIMARY KEY (id),
    CONSTRAINT quarters_district_id_fkey FOREIGN KEY (district_id)
        REFERENCES public.districts (id) MATCH SIMPLE
        ON DELETE CASCADE
);