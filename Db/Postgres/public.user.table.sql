create sequence user_seq;

create table public.user(
	usersqlid int default nextval ('user_seq') not null,
	email text null,
 constraint pk_user primary key 
(
	usersqlid)
)
