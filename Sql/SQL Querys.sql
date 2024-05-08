/* Only for Personale */
Select Personale.Id, CPR, Personale.Navn, Mail, Adgangskode, ArbejdsTlfNr, PrivatTlfNr, Adresse, Status From Personale;

/* Search */
Select Personale.Id, CPR, Personale.Navn, Mail, Adgangskode, ArbejdsTlfNr, PrivatTlfNr, Adresse, Status From Personale WHERE CPR = 20202222 OR Navn = '' OR Mail = '' OR ArbejdsTlfNr = '' or PrivatTlfNr = '' or Adresse = '';


/* Til at få deres location */
Select PersonalePaaLokation.Id, CPR, Personale.Navn, Mail, ArbejdsTlfNr, PrivatTlfNr, Status, Lokation.Navn As Location, PersonalePaaLokation.Created as Date From Personale Join PersonalePaaLokation on PersonalePaaLokation.Personal = Personale.CPR Join Lokation on PersonalePaaLokation.Lokation = Lokation.Id;


/* Delete for når du slette en person fra Personale table */
DELETE FROM Personale WHERE CPR = 101011111;

/* Delete for når du sletter fra  PersonalePaaLokation table */
DELETE FROM PersonalePaaLokation WHERE Personal = 101011111;


Select Id, Navn from Lokation;
Select Id, Navn from Bestilt;
Select Id, Navn from Prioritet;
Select Id, Navn from Proeve;
Select Id, Navn from SaerligeForhold;


Select Afdeling.Id, Afdeling.Navn, Omkring, Lokation.Navn  from Afdeling Join Lokation on Lokation.Id = Afdeling.Lokation;

update Booking Set TakedAf = 0202022222 Where Id = 1;

SELECT Booking.Id, Booking.CPR, Booking.Navn, Afdeling.Navn, StueEllerSengeplads, Isolationspatient, Inaktiv, Prioritet.Navn, BestiltTime, BestiltDato, Bestilt.Navn, 
    Kommentar, c.Navn as CreatedAf, t.Navn as TakenAf, Done  
FROM Booking 
JOIN Afdeling ON Booking.Afdeling = Afdeling.Id
JOIN Prioritet ON Booking.Prioritet = Prioritet.Id 
JOIN Bestilt ON Booking.Bestilt = Bestilt.Id 
JOIN Personale c ON c.CPR = Booking.CreatedAf
LEFT JOIN Personale t ON t.CPR = Booking.TakedAf
WHERE Afdeling.Lokation = 1 OR t.CPR = 101011111 AND t.CPR IS NULL 
ORDER BY Prioritet.Id DESC, BestiltTime ASC;

<!-- BestiltDato = '2024-05-08' --> 

SELECT Booking.Id, Booking.CPR, Booking.Navn, Done  
FROM Booking 
JOIN BookedForProeve ON BookedForProeve.Booked = Booking.Id 
JOIN Proeve ON BookedForProeve.Proeve = Proeve.Id;


SELECT Booking.Id, Booking.CPR, Booking.Navn,  SaerligeForhold.Navn, Done  
FROM Booking 
JOIN BookedForSaerligeForhold ON Booking.Id = BookedForSaerligeForhold.Booked
JOIN SaerligeForhold ON BookedForSaerligeForhold.SaerligeForhold = SaerligeForhold.Id;