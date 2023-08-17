
CREATE TABLE IF NOT EXISTS public.questions
(
    id uuid NOT NULL,
    question_text text  NOT NULL,
    category_id uuid NOT NULL,
    creator_user_id uuid NOT NULL,
    question_type_id uuid,
    CONSTRAINT primarykey_of_questions PRIMARY KEY (id),
    CONSTRAINT questions_category_id_fkey FOREIGN KEY (category_id)
        REFERENCES public.categories (id) MATCH SIMPLE
        ON DELETE CASCADE,
    CONSTRAINT questions_creator_user_id_fkey FOREIGN KEY (creator_user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT questions_question_type_id_fkey FOREIGN KEY (question_type_id)
        REFERENCES public.question_type (id) MATCH SIMPLE
        ON DELETE CASCADE
);