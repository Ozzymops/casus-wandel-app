# casus-wandel-app, WandelApp
### voor de casus "Wandelen met een app" 2018-2019

## Beschrijving:
Een app voor wandelfanatieken. Krijg een kaart gevuld met routes en bezienswaardigheden voor het ultieme wandelplezier!

## Platformen:
Android (Xamarin).

## Installatie
1. Clone deze repository (**WandelApp**) en clone https://github.com/Ozzymops/casus-wandel-app-web (**WandelAppWeb**) naar jouw Visual Studio installatie.
2. Voer de ``DATABASE.sql`` script in WandelAppWeb uit in jouw SQL Server Studio installatie in een database met de naam **WandelAppDb**.
3. Open Visual Studio en klik bovenaan op *Tools* en vervolgens op *Extensions and Updates*.
4. Klik op *Online* en zoek voor de extension **Conveyor by Keyoti**. Installeer deze.
5. Open na de installatie beide projecten in aparte instanties van Visual Studio.
6. Update van beide projecten alle NuGet packages naar de laatste versie.
7. Voer het ASP.net project (**WandelAppWeb**) uit. Een verschijnt een browser en een venster van **Conveyor**.
   - Wanneer je een error krijgt in het ASP.net project dat lijkt op ``Could not find a part of the path … bin\roslyn\csc.exe`` heb je de NuGet packages niet goed geïnstalleerd.
   - Wanneer je een pagina krijgt ZONDER *API* knop bovenaan, moet je de browser sluiten, beneden op de potlood klikken en dan vervolgens alle changes reverten / deleten. Start het project opnieuw op en voer het opnieuw uit.
8. Test de site door bovenin het adresbalk ``localhost:[port]/api/user/LogIn?username=Justintje&password=abc`` in te voeren. Als je niet ``null`` terug krijgt, werkt het!
9. **BELANGRIJK**: in het **Conveyor** venstertje zie je een of twee Local en Remote URL's staan. Kopieer **de eerste Remote URL** en sla deze ergens op.
10. Ga in het Xamarin project (**WandelApp**) **ZONDER** dat je **WandelAppWeb** afsluit!
11. Ga naar ``Views/LogIn.xaml`` en open de **.cs** hiervan. Scroll naar beneden totdat je een URL ziet. **Vervang ALLEEN de IP en port in de URL naar de Remote URL die je net hebt gekopieerd! Laat de rest staan!**
12. Voer het Xamarin project uit in een Android emulator. Dit kan even nemen om op te starten.
13. Druk op de drie streepjes rechtsboven in wanneer de app is opgestart en selecteer *Log in*.
14. Vul als inloginformatie bij de gebruikersnaam ``Justintje`` in en bij de wachtwoord ``abc``. Druk op *Log in*. Als je een bericht krijgt met ``Succes! Welkom Justin!`` erin, dan werkt alles!
15. Ga nu weer naar de drie streepjes en selecteer *Account*. Hier zie je alle account informatie.
16. *Klaar!*

## Uitvoeren
1. Open beide projecten in aparte instanties van Visual Studio.
2. Voer **WandelAppWeb** eerst uit en dan **WandelApp**.
3. *Klaar!*
