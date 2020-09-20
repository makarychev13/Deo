create table if not exists "Orders" (
    "Id" int not null primary key,
    "Title" text not null,
    "Description" text not null,
    "Link" text unique not null,
    "PublicationDate" timestamp not null,
    "FreelanceBurseId" int not null references "FreelanceBurses"("Id"),
    "Status" "OrderStatus" not null,
    "LastModificationDate" timestamp not null
)