# IPv4 Analyse

## Das Projekt
In dem Projekt "IPv4_Analyse" geht es um eine Consolen Anwendung.
Diese Consolen Anwendung, verfolgen das Zeil alle relevanten IP4v Adressen nach verfügbarkeit zu testen.
Dadurch kann eine Liste von IPv4 Adressen erstellt werden, die öffentlich zugänglich sind.

Das gesamte Projekt wird in der Programmiersprache C# entwickelt.
Als Entwicklungsumgebung, nutze ich Visual Studio 2015.

![demo](https://cloud.githubusercontent.com/assets/24595596/23338413/6005252c-fc0a-11e6-8d59-3159dd8a2c22.png)

### Über mich
Ich bin relativ neu in der Programmierung und am anfang meiner Ausbildung als 
Fachinformatiker für Anwendungsentwicklung.

Jedoch beschäftige ich mich schon etwas länger (privat) mit der Programmierung.

### Ideen findung
Die Idee für das Projekt ist aus Zufall entstanden.
Als ich mir mal wieder einige Klassen der .Net Frameworks angeschaut habe,
bin ich auf die Klasse Ping gekommen.

Nachdem ich mit der Klasse ein wenig gespielt habe, kahm ich auf die Idee
einfach mal alle IPv4 Adressen anzupingen.

Relativ schnell wurde mir klar, das dieses Projekt viel rechen Kapazität benötigen würde, 
um diesen kompletten Scann innerhalb einer passablen Zeit zu schaffen. Dadruch wurde das
Projekt etwas größer als anfangs angenommen.

### Angewandte Klassen
Wie eben erwähnt nutze ich für den IP-Scann die Klasse Ping von dem .Net Framework.
Dadurch merkt die Anwendung ob die IP-Adresse öffentlich zugänglich ist oder nicht
(eine Antwort oder nicht).

- System.Net.NetworkInformation;
- System.Threading;
- Mysql.Data; (für eine MySqlConnection)

Natürlich habe ich einige mehrere Klassen und Methoden genutzt.
Jedoch wollte ich nur die Klasse erwähnen, die erwähnenswert sind.

### Problematik
Einer der großen Probleme der Anwendung ist der praktische nutzen.
Die Anwendung braucht für ein kompletten scann sehr viel Zeit.
Auch diesem Grunde habe ich das Ziel verfolgt, die Aufgabe auf mehrere
Threads und geräte aufzuteilen.

Das ist zumindest die momentane Lösung die ich verfolge um
Rechenzeit zu spaaren.

### In Zahlen
* Theoretisch mögliche IP Adressen: 4.294.967.296
* Praktisch übrige IP Adressen (ca.): 2.500.000.000
* Dauert eines Ping: 0.05 Sekunden.
* Dauert eines WebRequest: 0.1 Sekunden.
* Dauert der Scanns sammt Verarbeitungszeit (ungefähr) 0.3 Sekunden
* Insgesamt ca. 12.500.500 Sekunden (ohne Threads).

Diese Zeiten, werden erziehlt, wenn das gesamte Programm nur auf einem Gerät mit einem Prozess läuft.

## Zukunftsaussichten
Momentan verfolg ich das Ziel, einen Scann zu einer passablen Zeit abschließen zu können.
Dem Ziel bin ich durch multi Threading sogar ein stück näher gekommen.

Momentan gibt es die Anwendung nur als Stand alone Verion.
In der Zukunft sollen die Aufgaben in zwei Anwendungen unterteielt werden.

1. Eine Server Anwendung, die die Ergebnisse in die Datenbank schreibt und die nächste IP-Adresse hergibt.
2. Eine Client Anwendung, die für die Analyse zuständig ist.

Durch die Aufteilung dieser Aufgaben soll es in der Zukunf möglich werden diese Aufgabe unter mehreren 
Rechnern aufzuteilen.

Darüberhinaus könnte die Anwendung noch folgende Aufgaben zusätzlich erfüllen:

* Homepage Inhalte anhand von Keywords bestimmen.

## Dokumente

### IPv4Address.cs
Diese Datei ist eine eigen erstellt Klasse, 
um alle Informationen eines IP-Scanns in einem Objekt zusammen zu fassen.

### Program.cs
In diesem Dokument wird ein Algorithmus beschrieben, der die Hauptfunktion beschreibt.
Dementsprechend wir in dem Dokument die Analyse der Whitelist durchgeführt.
