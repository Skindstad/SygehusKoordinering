/*Først maker du create database linje også Execute*/
Create database Alfa_SygehusKoordinering;

/* Maker Use linje også Execute*/
Use Alfa_SygehusKoordinering;

/* Maker indtil næste kommentar også Execute */
create table Personale
(
Id int not null Identity(1,1) Primary key,
CPR int not null UNIQUE,
Navn Varchar(255) not null,
Mail Varchar(255) not null UNIQUE,
Adgangskode Varchar(255) not null,
ArbejdsTlfNr Varchar(10) not null UNIQUE,
Status Bit DEFAULT 0,
Adresse Varchar(255) not null,
PrivatTlfNr Varchar(10) not null UNIQUE,
Created DateTime not null DEFAULT CURRENT_TIMESTAMP,
Updated DateTime not null DEFAULT CURRENT_TIMESTAMP,
)

create table Lokation (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
)

create table PersonalePaaLokation (
Id int not null Identity(1,1) Primary key,
Personal int not null FOREIGN KEY REFERENCES Personale(CPR),
Lokation int not null FOREIGN KEY REFERENCES Lokation(Id),
Created DateTime not null DEFAULT CURRENT_TIMESTAMP,
)

create table Afdeling (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
Omkring Varchar(255) not null,
Lokation int not null FOREIGN KEY REFERENCES Lokation(Id),
)

create table Prioritet (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
)

create table Bestilt (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
)

create table Proeve (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
)

create table SaerligeForhold (
Id int not null Identity(1,1) Primary key,
Navn Varchar(255) not null,
)

create table Booking (
Id int not null Identity(1,1) Primary key,
CPR int not null,
Navn Varchar(255) not null,
Afdeling int not null FOREIGN KEY REFERENCES Afdeling(Id),
StueEllerSengeplads Varchar(10) not null,
Isolationspatient bit DEFAULT 0,
Inaktiv bit Default 0,
BestiltTime Time not null,
BestiltDato Date not null,
Bestilt int not null FOREIGN KEY REFERENCES Bestilt(Id),
Kommentar Text,
CreatedAf int not null FOREIGN KEY REFERENCES Personale(CPR), 
TakedAf int not null FOREIGN KEY REFERENCES Personale(CPR),
Created DateTime not null DEFAULT CURRENT_TIMESTAMP,
Updated DateTime not null DEFAULT CURRENT_TIMESTAMP,
Done bit default 0
)
create table BookedForProeve (
Id int not null Identity(1,1) Primary key,
Proeve int not null FOREIGN KEY REFERENCES Proeve(Id), 
Booked int not null FOREIGN KEY REFERENCES Booking(id),
)

create table BookedForSaerligeForhold (
Id int not null Identity(1,1) Primary key,
SaerligeForhold int not null FOREIGN KEY REFERENCES SaerligeForhold(Id), 
Booked int not null FOREIGN KEY REFERENCES Booking(id),
)

/* Maker alle delete også Execute */
Delete From PersonalePaaLokation;
Delete From BookedForProeve;
Delete From BookedForSaerligeForhold;
Delete From SaerligeForhold;
Delete From Proeve;
Delete From Booking;
Delete From Prioritet;
Delete From Bestilt;
Delete From Afdeling;

/*Maker alt ned af også Execute*/
insert into Bestilt(Navn) values ('Til Bestilt tid');
insert into Bestilt(Navn) values ('Inden for 1 time');
insert into Bestilt(Navn) values ('Inden for 2 time');
insert into Bestilt(Navn) values ('Inden for 3 time');

insert into Proeve(Navn) values ('Blodprøver');
insert into Proeve(Navn) values ('EKG');
insert into Proeve(Navn) values ('Glukose-Csv');
insert into Proeve(Navn) values ('POCT-PCR');

insert into SaerligeForhold(Navn) values ('Særlige OBS');
insert into SaerligeForhold(Navn) values ('Medicinafhængig prøvetagning');

insert into Prioritet(Navn) values ('Normal');
insert into Prioritet(Navn) values ('Haster');
insert into Prioritet(Navn) values ('Livstruende');

insert into Lokation(Navn) values ('Højhuset');
insert into Lokation(Navn) values ('Skadestuen');
insert into Lokation(Navn) values ('Medicinerhuset');

insert into Afdeling(Navn, Omkring, Lokation) values ('A1', 'Mave-tarm-kirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('S1', 'Hjertemedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('S2', 'Hjertemedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('NHH', 'Neuro-, hoved-, halskirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('O1', 'Ortopædkirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('O2', 'Ortopædkirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('Geriatrisk', 'Ældremedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('A2', 'Mave-tarm-kirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('T', 'Thoraxkirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('Notia', 'Neuro- og traume-intensiv', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('ATC', 'Akut Traume center/skadestuen', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('Grønt spor', 'Patienter hvor det ikke haster, men de skal ses af læge', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('Blåt Spor', 'Brækkede knogler', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('AMA 1', 'Akutmodtageafsnit', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('AMA 2', 'Akutmodtageafsnit', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('9Ø', 'Gastromedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('6V', 'Lungemedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('7V', 'Hæmatologisk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('7Ø', 'Infektionsmedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('6Ø', 'Apopleksi', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('2Ø', 'Intermediært', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('R', 'Almen intensivt', 3);

insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Jimmy G', 'arbejde@mail.dk', 'secret', '4511111111', '0101011111', 'Nowhere 1', '4512121212');
insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Jessie D', 'arbejde2@mail.dk', 'secret', '4522222222', '0202022222', 'Nowhere 2', '4521212121');
insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Joy P', 'arbejde3@mail.dk', 'secret', '4511221122', '0303033333', 'new 1', '4522112211');

