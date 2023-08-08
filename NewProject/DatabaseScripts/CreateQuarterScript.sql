
CREATE TABLE IF NOT EXISTS public.quarters
(
    id uuid NOT NULL,
    quarter_name character varying(20) COLLATE pg_catalog."default" NOT NULL,
    district_id uuid NOT NULL,
    CONSTRAINT primarykey_of_quarters PRIMARY KEY (id),
    CONSTRAINT quarters_district_id_fkey FOREIGN KEY (district_id)
)
