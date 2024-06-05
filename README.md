# SygehusKoordinering

# Opsætning af Produktet

1. Udpak zip-filen
   - Download og udpak zip-filen til en passende mappe på din computer.

2. Åbn Microsoft SQL Server Management Studio
   - Start Microsoft SQL Server Management Studio (SSMS) på din computer.

3. Åbn SQL-mappen og derefter Alfa_SQLFil
   - Naviger til den udpakkede mappe.
   - Find mappen mærket "SQL" og åbn den.
   - Åbn filen med navnet "Alfa_SQLFil.sql".

4. Følg kodekommentarerne i SQL-filen
   - Læs og udfør instruktionerne i kommentarerne i "Alfa_SQLFil.sql" for at konfigurere din database.

 5. Åbn SygehusKoordinering.sln
    - Naviger tilbage til hovedmappen og åbn løsningen "SygehusKoordinering.sln" i din udviklingsmiljø (f.eks. Visual Studio).

 6. Opret en app.config-fil (hvis den ikke eksisterer)
     - Hvis der ikke er en app.config-fil i dit projekt, skal du oprette en ny.
       - Højreklik på dit projekt i Solution Explorer.
       - Vælg "Add" > "New Item".
       - Vælg "Application Configuration File" og navngiv den "app.config".

7. Tilføj følgende til din app.config-fil
   ```
   <configuration>
      <connectionStrings>
         <add name="post" connectionString="Data Source=[DinSQLExpress];Initial Catalog=Alfa_SygehusKoordinering;Integrated Security=True; Trust Server Certificate=True" providerName="Microsoft.Data.SqlClient"/>
      </connectionStrings>
   </configuration>
   ```

8. Opdater connectionString
    - Erstat [DinSQLExpress] med navnet på din SQL Server.
    - Dette navn kan du finde i Microsoft SQL Server Management Studio under "Server Name".
9. Byg løsningen
    - I Visual Studio, gå til "Build" menuen.
    - Vælg "Build Solution" for at bygge dit projekt. Sørg for, at der ikke er nogen fejl.
   
10. Klar til at køre
    - Dit produkt skulle nu være klar til brug. Kør applikationen for at sikre, at alt fungerer korrekt.

# Login
1.	Bruger
   - Email: arbejde@mail.dk
   - Adgangskode: secret
   - Arbejds Tlf. nr.: 4511111111
2.	Bruger
  - Email: arbejde2@mail.dk
  - Adgangskode: secret
  - Arbejds Tlf. nr.: 4522222222
3.	Bruger
  - Email: arbejde3@mail.dk
  - Adgangskode: secret
  - Arbejds Tlf. nr.: 4511221122
