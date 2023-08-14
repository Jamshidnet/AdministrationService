create table public.users ( Id  uuid primary key, username varchar(20) unique, fullname varchar(50), phone varchar(20), password varchar(20) );
create table public.roles ( Id  uuid primary key, role_name varchar(20));
create table public.permissions ( Id  uuid primary key, permission_name varchar(20));
create table public.user_roles (  user_id uuid references users(Id), role_id  uuid references  roles(Id),  primary key (user_id, role_id));
create table public.role_permissions ( role_id  uuid references roles(Id), permission_id uuid references permissions(Id),   primary key (role_id, permission_id));

create table public.regions ( Id  uuid primary key, region_name varchar(20));
create table public.districts ( Id  uuid primary key, district_name varchar(20), region_id uuid references regions(Id));
create table public.quarters ( Id  uuid primary key, quarter_name varchar(20)  , district_id uuid references districts(Id));


create table public.categories  (Id uuid primary key, category_name varchar(50));
create table public.clients (Id uuid primary key, client_name varchar(50), quarter_id uuid references quarters(Id));
create table public.questions (Id uuid primary key, question_text text, category_id uuid references categories(Id), creator_user_id uuid references users(Id));
create table public.answers (Id uuid primary key, answer_text text, question_id uuid references questions(Id), client_id uuid references clients(Id), unique(question_id, client_id)); 

create table public.userrefreshtokens (Id uuid primary key, refresh_token text, username varchar(50), expires_date date);




drop table answers;
drop table questions;
drop table clients;






drop table regions;
drop table districts;
drop table quarter;

drop table users;
drop table roles;
drop table permissions;
drop table user_roles;
drop table role_permissions;



Scaffold-DBContext "Host=localhost;Username=postgres;Password=Jam2001!!!;Database=newdatabase" Npgsql.EntityFrameworkCore.PostgreSQL -DataAnnotations -OutputDir Entities -Namespace NewProject -StartupProject NewProject -Project Domein



