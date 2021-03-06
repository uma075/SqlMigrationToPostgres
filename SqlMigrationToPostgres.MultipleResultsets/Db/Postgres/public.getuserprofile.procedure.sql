create or replace function public.getuserprofile(	
	par_userid integer,	
	par_profileid uuid)
	returns setof refcursor 
    language 'plpgsql'
as $body$
declare
refcur_1  refcursor ;
refcur_2  refcursor ;	
refcur_3  refcursor ;
begin
 OPEN refcur_1 FOR select *from public.user u where u.usersqlid=par_userid;
	return next refcur_1 ;
	
OPEN refcur_2 FOR select *from public.customevent u where u.usersqlid=par_userid;
	return next refcur_2;
    
OPEN refcur_3 FOR select *from public.profile u where u.profileid=par_profileid;
	return next refcur_3;
end;
$body$;
