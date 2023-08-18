--Index for quarterId column in person table since it had to be filtered by this column most in docs in both client and user
-- which inherts from person

CREATE INDEX IF NOT EXISTS by_quarter
    ON public.person USING btree
    (quarter_id );


