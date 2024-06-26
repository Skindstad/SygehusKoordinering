/*F�rst maker du create database linje ogs� Execute*/
Create database Alfa_SygehusKoordinering;

/* Maker Use linje ogs� Execute*/
Use Alfa_SygehusKoordinering;

/* Maker indtil n�ste kommentar ogs� Execute */
create table Personale
(
Id int not null Identity(1,1) Primary key,
CPR bigint not null UNIQUE,
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
Personal bigint not null FOREIGN KEY REFERENCES Personale(CPR),
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
Id bigint not null Identity(1,1) Primary key,
CPR bigint not null,
Navn Varchar(255) not null,
Afdeling int not null FOREIGN KEY REFERENCES Afdeling(Id),
StueEllerSengeplads Varchar(100) not null,
Isolationspatient bit DEFAULT 0,
Inaktiv bit Default 0,
Prioritet int not null FOREIGN KEY REFERENCES Prioritet(Id),
BestiltTime Time not null,
BestiltDato Date not null,
Bestilt int not null FOREIGN KEY REFERENCES Bestilt(Id),
Kommentar Text,
CreatedAf bigint not null FOREIGN KEY REFERENCES Personale(CPR), 
TakedAf bigint FOREIGN KEY REFERENCES Personale(CPR),
Created DateTime DEFAULT CURRENT_TIMESTAMP,
Updated DateTime DEFAULT CURRENT_TIMESTAMP,
Begynd bit default 0,
Done bit default 0
)
create table BookedForProeve (
Id bigint not null Identity(1,1) Primary key,
Proeve int not null FOREIGN KEY REFERENCES Proeve(Id), 
Booked bigint not null FOREIGN KEY REFERENCES Booking(Id),
)

create table BookedForSaerligeForhold (
Id bigint not null Identity(1,1) Primary key,
SaerligeForhold int not null FOREIGN KEY REFERENCES SaerligeForhold(Id), 
Booked bigint not null FOREIGN KEY REFERENCES Booking(Id),
)

/* Maker alle delete ogs� Execute */
Delete From PersonalePaaLokation;
Delete From BookedForProeve;
Delete From BookedForSaerligeForhold;
Delete From SaerligeForhold;
Delete From Proeve;
Delete From Booking;
Delete From Prioritet;
Delete From Bestilt;
Delete From Afdeling;

/*Maker alt ned af ogs� Execute*/
insert into Bestilt(Navn) values ('Til Bestilt tid');
insert into Bestilt(Navn) values ('Inden for 1 time');
insert into Bestilt(Navn) values ('Inden for 2 time');
insert into Bestilt(Navn) values ('Inden for 3 time');

insert into Proeve(Navn) values ('Blodpr�ver');
insert into Proeve(Navn) values ('EKG');
insert into Proeve(Navn) values ('Glukose-Csv');
insert into Proeve(Navn) values ('POCT-PCR');

insert into SaerligeForhold(Navn) values ('S�rlige OBS');
insert into SaerligeForhold(Navn) values ('Medicinafh�ngig pr�vetagning');

insert into Prioritet(Navn) values ('Normal');
insert into Prioritet(Navn) values ('Haster');
insert into Prioritet(Navn) values ('Livstruende');

insert into Lokation(Navn) values ('H�jhuset');
insert into Lokation(Navn) values ('Skadestuen');
insert into Lokation(Navn) values ('Medicinerhuset');

insert into Afdeling(Navn, Omkring, Lokation) values ('A1', 'Mave-tarm-kirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('S1', 'Hjertemedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('S2', 'Hjertemedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('NHH', 'Neuro-, hoved-, halskirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('O1', 'Ortop�dkirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('O2', 'Ortop�dkirurgisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('Geriatrisk', '�ldremedicinsk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('A2', 'Mave-tarm-kirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('T', 'Thoraxkirugisk', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('Notia', 'Neuro- og traume-intensiv', 1);
insert into Afdeling(Navn, Omkring, Lokation) values ('ATC', 'Akut Traume center/skadestuen', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('Gr�nt spor', 'Patienter hvor det ikke haster, men de skal ses af l�ge', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('Bl�t Spor', 'Br�kkede knogler', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('AMA 1', 'Akutmodtageafsnit', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('AMA 2', 'Akutmodtageafsnit', 2);
insert into Afdeling(Navn, Omkring, Lokation) values ('9�', 'Gastromedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('6V', 'Lungemedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('7V', 'H�matologisk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('7�', 'Infektionsmedicinsk', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('6�', 'Apopleksi', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('2�', 'Intermedi�rt', 3);
insert into Afdeling(Navn, Omkring, Lokation) values ('R', 'Almen intensivt', 3);

insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Jimmy G', 'arbejde@mail.dk', 'secret', '4511111111', '0101011111', 'Nowhere 1', '4512121212');
insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Jessie D', 'arbejde2@mail.dk', 'secret', '4522222222', '0202022222', 'Nowhere 2', '4521212121');
insert into Personale(Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) values ('Joy P', 'arbejde3@mail.dk', 'secret', '4511221122', '0303033333', 'new 1', '4522112211');

insert into Booking(CPR, Navn, Afdeling, StueEllerSengeplads, Prioritet, BestiltTime, BestiltDato, Bestilt, CreatedAf) values ('0101011112', 'Lasse Q', 1, 'Stue', 1, '13:30:00', '2024-05-01', 1, '0101011111');
insert into Booking(CPR, Navn, Afdeling, StueEllerSengeplads, Prioritet, BestiltTime, BestiltDato, Bestilt, CreatedAf) values ('0101011113', 'Oliver L', 1, 'Stue', 1, '14:30:00', '2024-05-01', 1, '0101011111');
insert into Booking(CPR, Navn, Afdeling, StueEllerSengeplads, Prioritet, BestiltTime, BestiltDato, Bestilt, CreatedAf) values ('0101011114', 'Jimmy Z', 1, 'Stue', 2, '14:30:00', '2024-05-01', 1, '0101011111');
insert into Booking(CPR, Navn, Afdeling, StueEllerSengeplads, Prioritet, BestiltTime, BestiltDato, Bestilt, CreatedAf) values ('0101011114', 'Lion B', 1, 'Stue', 3, '14:30:00', '2024-05-01', 1, '0101011111');


insert into BookedForProeve(Proeve, Booked) values(1, 1);
insert into BookedForProeve(Proeve, Booked) values(2, 1);
insert into BookedForProeve(Proeve, Booked) values(1, 2);
insert into BookedForProeve(Proeve, Booked) values(1, 3);
insert into BookedForProeve(Proeve, Booked) values(3, 4);
insert into BookedForProeve(Proeve, Booked) values(4, 4);

insert into BookedForSaerligeForhold(SaerligeForhold, Booked) values (1, 1);
insert into BookedForSaerligeForhold(SaerligeForhold, Booked) values (1, 2);

insert into PersonalePaaLokation(Personal, Lokation) values ('0101011111', 1);
insert into PersonalePaaLokation(Personal, Lokation) values ('0202022222', 1);