use ucn;

drop table if exists bambooSessionUser;
drop table if exists bambooSession;
drop table if exists swipedRecipe;
drop table if exists ingredient;
drop table if exists instruction;

drop table if exists recipe;
drop table if exists [user];

create table [user] (
    userId varchar(255) not null unique,
    email varchar(255) not null unique,
    firstName varchar(255) not null,
    lastName varchar(255) not null,
    password varchar(255) not null,
    address varchar(255),
    role varchar(10) not null check (role in('user', 'verifiedUser', 'admin')),
    primary key (userId, email)
);

create table recipe (
    recipeId varchar(255) not null unique,
    name varchar(255) not null,
    description varchar(255) not null,
    authorId varchar(255) not null,
    foreign key (authorId) references [user](userId)
    on delete cascade,
    pictureURL varchar(255) not null,
    time int not null,
    primary key (recipeId),
    portionNum int not null 
);

create table ingredient (
    recipeId varchar(255) not null,
    foreign key (recipeId) references recipe(recipeId)
    on delete cascade,
    name varchar(255) not null,
    amount int not null,
    unit varchar(255) not null,
    primary key (recipeId, name)
);

create table instruction (
    recipeId varchar(255) not null,
    foreign key (recipeId) references recipe(recipeId)
    on delete cascade,
    step int not null,
    description varchar(255) not null,
    primary key (recipeId, step)
);

create table swipedRecipe (
    userId varchar(255) not null,
    foreign key (userId) references "user"(userId),
    recipeId varchar(255) not null,
    foreign key (recipeId) references recipe(recipeId),
    isLiked bit not null
);

create table bambooSession (
    sessionId varchar(255) not null unique,
    hostId varchar(255) not null,
    foreign key (hostId) references "user"(userId),
    address varchar(255) not null,
    recipeId VARCHAR(255) not null,
    foreign key (recipeId) references recipe(recipeId),
    description varchar(255) not null,
    dateTime datetime not null,
    slotsNumber int not null,
    primary key (sessionId)
);

create table bambooSessionUser (
    sessionId varchar(255) not null,
    foreign key (sessionId) references bambooSession(sessionId)
    on delete cascade,
    userId varchar(255),
    seat varchar(255) not null,
    foreign key (userId) references "user"(userId)
    on delete cascade,
    primary key (sessionId, userId)
);
