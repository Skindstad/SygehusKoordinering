/* Only for Personale */
Select Personale.Id, CPR, Personale.Navn, Mail, Adgangskode, ArbejdsTlfNr, PrivatTlfNr, Adresse, Status From Personale;

/* Search */
Select Personale.Id, CPR, Personale.Navn, Mail, Adgangskode, ArbejdsTlfNr, PrivatTlfNr, Adresse, Status From Personale WHERE CPR = 20202222 OR Navn = '' OR Mail = '' OR ArbejdsTlfNr = '' or PrivatTlfNr = '' or Adresse = '';


/* Til at få deres location */
Select Personale.Id, CPR, Personale.Navn, Mail, Adgangskode, ArbejdsTlfNr, PrivatTlfNr, Adresse, Status, Lokation.Navn As Location, PersonalePaaLokation.Created as Date From Personale Join PersonalePaaLokation on PersonalePaaLokation.Personal = Personale.CPR Join Lokation on PersonalePaaLokation.Lokation = Lokation.Id;


/* Delete for når du slette en person fra Personale table */
DELETE FROM Personale WHERE CPR = 101011111;

/* Delete for når du sletter fra  PersonalePaaLokation table */
DELETE FROM PersonalePaaLokation WHERE Personal = 101011111;

