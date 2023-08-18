
CREATE OR REPLACE FUNCTION public.filter_docs_by_user(
	p_region_id uuid DEFAULT NULL::uuid,
	p_district_id uuid DEFAULT NULL::uuid,
	p_quarter_id uuid DEFAULT NULL::uuid,
	by_region boolean DEFAULT true,
	by_district boolean DEFAULT false,
	by_quarter boolean DEFAULT false)
    RETURNS TABLE(region_name character varying, district_name character varying, quarter_name character varying, user_type_name character varying, doc_count bigint, user_count bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    IF by_region THEN
        RETURN QUERY
            SELECT 
    r.region_name, null::character varying, null::character varying,
    ut.user_type,
    COUNT(DISTINCT d.client_id) AS doc_count,
    COUNT(DISTINCT u.id) AS user_count
FROM
    user_type AS ut
JOIN
    users AS u ON ut.id = u.user_type_id
JOIN
    docs AS d ON u.id = d.user_id
JOIN
    person AS p ON u.person_id = p.id
LEFT JOIN
    quarters AS q ON p.quarter_id = q.id
LEFT JOIN
    districts AS di ON q.district_id = di.id
LEFT JOIN
    regions AS r ON di.region_id = r.id
WHERE
    (p_region_id IS NULL OR r.id = p_region_id) AND
    (p_district_id IS NULL OR di.id = p_district_id) AND
    (p_quarter_id IS NULL OR q.id = p_quarter_id)
GROUP BY
    r.region_name, ut.user_type;

    ELSIF by_district THEN
        RETURN QUERY
            SELECT 
                r.region_name, di.district_name, null::text,
                ut.user_type,
                COUNT(DISTINCT d.client_id) AS doc_count,
                COUNT(DISTINCT u.id) AS user_count
            FROM
                user_type AS ut
            JOIN
                users AS u ON ut.id = u.user_type_id
            JOIN
                docs AS d ON u.id = d.user_id
            JOIN
                person AS p ON u.person_id = p.id
            JOIN
                quarters AS q ON p.quarter_id = q.id
            JOIN
                districts AS di ON q.district_id = di.id
            JOIN
                regions AS r ON di.region_id = r.id
            WHERE
                (p_region_id IS NULL OR r.id = p_region_id) AND
                (p_district_id IS NULL OR di.id = p_district_id) AND
                (p_quarter_id IS NULL OR q.id = p_quarter_id)
            GROUP BY
                r.region_name, di.district_name, ut.user_type;
    ELSE
        RETURN QUERY
            SELECT 
                max(r.region_name) :: character varying, max(di.district_name) :: character varying , q.quarter_name,
                ut.user_type,
                COUNT(DISTINCT d.client_id) AS doc_count,
                COUNT(DISTINCT u.id) AS user_count
            FROM
                user_type AS ut
           left JOIN
                users AS u ON ut.id = u.user_type_id
           left JOIN
                docs AS d ON u.id = d.user_id
           left JOIN
                person AS p ON u.person_id = p.id
           left JOIN
                quarters AS q ON p.quarter_id = q.id
           left JOIN
                districts AS di ON q.district_id = di.id
           right JOIN
                regions AS r ON di.region_id = r.id
            WHERE
                (p_region_id IS NULL OR r.id = p_region_id) AND
                (p_district_id IS NULL OR di.id = p_district_id) AND
                (p_quarter_id IS NULL OR q.id = p_quarter_id)
            GROUP BY
               q.quarter_name, ut.user_type;
    END IF;
END;
$BODY$;
