create table if not exists "FreelanceBurses" (
    "Id" int not null primary key,
    "Name" text unique not null,
    "Link" text unique not null
)