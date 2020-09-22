create table if not exists "FreelanceBurses" (
    "Id" serial not null primary key,
    "Name" text unique not null,
    "Link" text unique not null
)