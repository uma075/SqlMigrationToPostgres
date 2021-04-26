create sequence customevent_seq;

create table customevent(
	customeventsqlid int default nextval ('customevent_seq') not null,
	usersqlid int not null,
	customeventdate timestamp(3) not null,
	fullname varchar(255) null,
	source text null,
 constraint pk_customevent primary key 
(
	customeventsqlid
) 
)
