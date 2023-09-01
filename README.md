# Koerselslog
Programmet er lavet som opgave på P1 i skoleoplæringscenteret. 
Programmet indeholder alle krav og specifikationer, som opgaven kræver.


## Funktioner
### Oprettelse, redigering og sletning af bruger
Det er nemt og simpelt og oprette en bruger og gøres ude i venstre side i boksen "opret bruger". Her skal man blot udfylde navn og nummerplade.
Hvis man ønsker at redigere en bruger, kan det gøres ude i brugerlisten ude til højre, ved blot at dobbeltklikke på det felt man ønsker redigeret.
Ønsker man at slette en bruger gøres det ved at højreklikke på brugeren ude i brugerlisten, og trykke 'Slet bruger' og herefter bekræfte. Selvom brugeren bliver slettet, vil brugerens kørelogs stadig være tilgængelige i køreloglisten. 

### Oprettelse, redigering og sletning af kørelog.
For at oprette en kørelog, skal man blot vælge en bruger fra dropdown menu, og herefter indlæses nummerpladen automatisk. Man kan ændre datoen for opgaven ved at bruge datovælgeren. For give information om opgaven kan det skrives i opgave, og evt. km tal kan også registeres.
Hvis man ønsker at redigere en kørelog, fungere det ligesom ved redigering af bruger. Man dobbeltklikker på det man ønsker at redigere.
Sletning fungere på samme måde med bruger sletning, og kan gøres ved at højreklikke på en kørelog ude i køreloglisten og bekræfte sletning.

### Søge funktion
Med søge funktionen kan du søge efter det du leder efter både i brugerlisten og køreloglisten. Der kan søge efter f.eks. parameter som: **id, navn, nrplade, opgave og dato.**


## Tilslutning af database
For at tislutte din MS Sql server med programmet, kan det gøres i **App.config**
Programmet sørger selv for at oprette de fornødne databaser og tabeller.

![image](https://github.com/Hjaller/Koerselslog/assets/38131968/8325f65c-f965-42e3-89e3-a014be4a7e48)

