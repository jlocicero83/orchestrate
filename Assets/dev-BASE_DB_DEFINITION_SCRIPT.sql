/* INITIAL DB POPULATION SCRIPT */

/* ****************** LOOKUP TABLES ******************* */
--create table instrument_families (
--	id serial primary key,
--	name varchar(30),
--	score_order integer
--);

--insert into instrument_families(name, score_order)
--	Values('Woodwinds', 1),
--		('Brass', 2),
--		('Percussion', 3),
--		('Keyboard', 4),
--		('Other', 5),
--		('Strings', 6);

--create table sections (
--	id serial primary key,
--	name varchar(30) not null,
--	family_id integer references instrument_families(id),
--	intra_family_order integer
--);

--insert into sections(name, family_id, intra_family_order)
--	values('Flute', 1, 1),
--		('Oboe', 1, 2),
--		('Clarinet', 1, 3),
--		('Bassoon', 1, 4),
--		('Horn', 2, 1),
--		('Trumpet', 2, 2),
--		('Trombone', 2, 3),
--		('Tuba', 2, 4),
--		('Timpani', 3, 1),
--		('Percussion', 3, 2),
--		('Keyboard', 4, 1),
--		('Harp', 5, 1),
--		('Violin 1', 6, 1),
--		('Violin 2', 6, 2),
--		('Viola', 6, 3),
--		('Cello', 6, 4),
--		('Bass', 6, 5)

--create table instruments (
--	id serial primary key,
--	abbrev varchar(10) not null,
--	name varchar(100) not null,
--	family_id integer references instrument_families(id)
--);

--insert into instruments(abbrev, name, family_id)
--	Values('Picc.', 'Piccolo', 1),
--		('Fl.', 'Flute', 1),
--		('Ob.', 'Oboe', 1),
--		('E. Hn.', 'English Horn', 1),
--		('Cl.', 'Clarinet', 1),
--		('B. Cl.', 'Bass Clarinet', 1),
--		('Bn.', 'Bassoon', 1),
--		('C. Bn.', 'Contra Bassoon', 1),
--		('Hn.', 'Horn', 2),
--		('Tpt.', 'Trumpet', 2),
--		('Trb.', 'Trombone', 2),
--		('B. Trb.', 'Bass Trombone', 2),
--		('Tuba', 'Tuba', 2),
--		('Timp.', 'Timpani', 3),
--		('Pno.', 'Piano', 4),
--		('Harp', 'Harp', 5),
--		('Vln.', 'Violin', 6),
--		('Vla.', 'Viola', 6),
--		('Vcl.', 'Cello', 6),
--		('Db.', 'Bass', 6),
--		/* TODO: other percussion instruments here? */

/* default roles with preset permissions. Can be overridden/supplemented by
user permission overrides table */
--create table roles (
--	role_id serial primary key,
--	role_name varchar(100) not null,
--	module_permissions JSONB not null,
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by varchar(50) not null,
--	updated_by varchar(50)
--);

/* TODO: INSERT DEFAULT ROLES */

--create table person_profile_types (
--	id serial primary key,
--	name varchar(50)
--);

--insert into person_profile_types(name)
--	Values('Musician'),
--		('Substitute Musician'),
--		('Staff'),
--		('Conductor'),
--		('Guest Conductor'),
--		('Soloist');

/* ************* ENTITY TABLES ****************** */c
--create table tenants (
--	tenant_id serial primary key,
--	parent_tenant_id int references tenants(tenant_id) on delete set null,
--	name varchar(150),
--	subdomain varchar(10) not null unique,
--	contact_name varchar(100) not null,
--	contact_email varchar(100) not null,
--	contact_phone varchar(50),
--	mailing_address JSONB,
--	is_active boolean not null default true,
--	paid_thru_date date,
--	agreed_price decimal(10, 2) not null check (agreed_price >= 0),
--	notes text,
--  created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--  updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--  created_by varchar(30) not null,
--  updated_by varchar(30)
--);

/* Index for subdomain lookup */
--create index idx_tenant_subdomain on tenants(subdomain);
--create index idx_active_tenant on tenants(is_active);

--insert into tenants(name, subdomain, contact_name, contact_email, paid_thru_date, agreed_price, created_by)
--	Values('Orchestrate Admin', 'admin', 'Joe LoCicero', 'N/A', DATE('2099-01-01'), 0.00, 'SYS-ADMIN-JVL');

/* People table for storing person information. A Person is not always a User of the system...
	but a User is always a Person */
--create table people (
--	person_id serial primary key,
--	tenant_id integer references tenants(tenant_id) not null,
--	first_name varchar(100) not null,
--	last_name varchar(100) not null,
--	preferred_name varchar(100),
--	email varchar(100),
--	phone varchar(50),
--	address JSONB,
--	is_active boolean not null default true /* active relationship with tenant, not active as a user */
--);

--create index idx_active_person_tenant on people(tenant_id, is_active);

--insert into people(tenant_id, first_name, last_name)
--	Values(1, 'Joe', 'LoCicero'),
--		(1, 'Alex', 'Wadner');

/* Represents an actual User of the Orchestrate system. Must have an associated
	 entry in the People table. */
--create table users (
--	user_id serial primary key,
--	person_id integer not null references people(person_id),
--	email varchar(100) unique not null,
--	is_active boolean not null default true, /* active user in the system. if false, login denied/ideally removed creds from clerk */
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by varchar(30) not null,
--);

--create index idx_user_email on users(email);


/* ****************** ENTITY EXTENDED DATA TABLES + JOINING TABLES ************************************************ */
/* UserTenants table allows for a user (with single login by email) to be active
	with more than one tenant. ie: substitutes working with multiple orchestras
	Note: The User's 'Role' is assigned here since so it can be tenant specific. */
--create table user_tenants (
--	id serial primary key,
--	user_id integer not null references users(user_id),
--	tenant_id integer not null references tenants(tenant_id),
--	role_id integer not null references roles(role_id), /* role for this user in this specific tenant */
--	is_active boolean default true,
--	unique(user_id, tenant_id)
--);
--
--create index idx_active_user_tenant on user_tenants(user_id, tenant_id, is_active);

/* Allows for more granular permission handling. This table can be used
	 to supplement the default permissions that come with a person's assigned role. */
--create table user_permission_overrides (
--	id serial primary key,
--	user_id integer references users(user_id),
--	tenant_id integer references tenants(tenant_id),
--	module_permissions JSONB not null,
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by varchar(50) not null,
--	updated_by varchar(50)
--);
--
--create index idx_user_tenant_overrides on user_permission_overrides(user_id, tenant_id);

/* maps people to their profiles based on profile type */
--create table person_profiles (
--	id serial primary key,
--	tenant_id integer references tenants(tenant_id),
--	person_id integer references people(person_id),
--	profile_type_id integer references person_profile_types(id),
--	is_active boolean default true, /* whether the specific profile is active for the person */
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by integer references people(person_id),
--	updated_by integer references people(person_id),
--);
--
--create index idx_active_person_profile_tenant on person_profiles(tenant_id, person_id, is_active);
--create index idx_person_profile_type on person_profiles(profile_type_id);

--create table musician_profiles (
--	id serial primary key,
--	tenant_id integer references tenants(tenant_id),
--	person_id integer references people(person_id),
--	relationship varchar(50), /* ie. Tenured FTE, Untenured FTE, Short-Term Contract, etc.) */
--	start_date date,
--	instrument_id integer references instruments(id),
--	section_id integer references sections(id),
--	title varchar(50), /* ie. Concertmaster, Principal, section, etc. */
--	base_roster_order integer,
--	can_rotate boolean default false, /* only applicable for section string players (i think) */
--	last_rotated_out date,
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by integer references people(person_id),
--	updated_by integer references people(person_id),
--);
--
--create index idx_musician_person_tenant on musician_profiles(tenant_id, person_id);

/* enables tracking a full history of when musicians have been rotated out.
from what, for what reason, etc. Note: this will be different from Attendance */
--create table rotate_out_history (
--	id serial primary key,
--	musician_profile_id integer references musician_profiles(id),
--	rotation_date date,
--	reason varchar(50), /* can change to text if necessary for more of a 'notes' field
--	however, i think just options like 'Orchestration', or 'Reduced Section' would suffice. */
--	created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--	created_by integer references people(person_id),
--	updated_by integer references people(person_id),
--/* TODO: finish wiring up with Events/Programs/Works as necessary once those entities are in place. */
--);

