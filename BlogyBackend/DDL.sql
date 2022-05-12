drop database if exists blogy;
create database if not exists blogy;
use blogy;

create table if not exists users(
    username varchar(30) ,
    name varchar(50) not null,
    email varchar(70) unique,
    phone varchar(13) unique,
    password varchar(256) not null,
    profilePicture longblob ,
    primary key (username)
);

create table if not exists plans(
    id int auto_increment,
    type varchar(7) not null check(type in ('Basic', 'Premium')),
    username varchar(30) not null,
    primary key (id),
    foreign key (username) references users(username)
);

create table if not exists posts(
    id int auto_increment,
    title varchar(50) not null,
    content varchar(3000) not null,
    dateTime varchar(26) not null,
    username varchar(30) not null,
    image longblob,
    primary key (id)
);

create table if not exists pinPosts(
    username varchar(30) ,
    postId int ,
    primary key (username,postId),
    foreign key(username) references users(username),
    foreign key (postId) references posts(id)
);

create table if not exists comments(
    id int auto_increment,
    content varchar(1000) not null,
    username varchar(30) not null,
    postId int not null,
    primary key (id),
    foreign key (username) references users(username),
    foreign key (postId) references posts(id)
);

create table if not exists tempUsers(
    username varchar(30) ,
    name varchar(50) not null,
    email varchar(70) unique,
    phone varchar(13) unique,
    password varchar(256) not null,
    profilePicture longblob ,
    verificationCode varchar(6) not null,
    primary key (username)
);
