# casus-wandel-app, WandelApp
voor de casus "Wandelen met een app" 2018

#Een app voor wandelfanatieken. Krijg een kaart gevuld met routes en bezienswaardigheden voor het ultieme wandelplezier!

Platformen: Android (via Xamarin)

#Hoe gebruik ik de app als een developer?
 1. Clone deze repository en clone https://github.com/Ozzymops/casus-wandel-app-web naar jouw Visual Studio installatie.
    Geef ze tijdens het clonen een betere naam zoals WandelApp en WandelAppWeb.
    Voer ook de database script in DATABASE.sql in WandelAppWeb uit in SQL Server Studio in een database met de naam WandelAppDb.
 2. Klik bovenaan op Tools en ga naar Extensions and Updates.
 3. Klik op Online en zoek voor de extension 'Conveyor by Keyoti'. Installeer deze. 
 4. Sluit Visual Studio en laat de extension installeren.
 5. Open na de installatie beide projecten in aparte instanties van Visual Studio.
 6. Update van beide projecten alle NuGet packages naar de laatste versie.
 7. Voer het ASP.net project (WandelAppWeb) uit. Er verschijnt een venstertje van Conveyor.
    ? De webpagina MOET een tabje API laten zien. Als dit niet het geval is (of enige andere fouten), sluit dan de pagina,
      open het potloodje beneden en Revert / Delete ALLE changes. Daarna zou het moeten werken.
    ? Test de database connectie door in de adresbalk localhost:[port]/api/user/LogIn?username=Justintje&password=abc in te vullen.
      Als je niet 'null' terug krijgt, werkt het.
 8. BELANGRIJK: In het Conveyor venstertje zie je 1 tot 2 keer een Local URL en een Remote URL. Kopieer de bovenste Remote URL
    inclusief poort en sla deze ergens op.
 9. Ga in het Xamarin project naar Views/LogInPage.xaml. Open de .cs hiervan. Scroll naar beneden totdat je een url   
    ziet met een IP adres erin. SLUIT NIET HET ASP.net PROJECT AF! Houd deze lopend!
10. Verander de IP adres naar de IP en port combinatie van stap 8. HOUD WEL NOG ALLES VAN /api/... NA DE PORT!!!
11. Voer het Xamarin project uit (dit kan een tijdje nemen om op te starten), druk op de drie streepjes en ga naar Login.
12. Probeer in te loggen met gebruikersnaam Justintje en wachtwoord abc. Als de verbinding goed werkt krijg je een alert te zien met 
    'Succes!' erin.
13. Ga nu weer naar de drie streepjes en ga dan naar Account. Daar staan nu alle account gegevens.
14. Klaar! Happy developing.
