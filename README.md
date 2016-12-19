# IPv4 Analyse

## Das Projekt
In dem Projekt "IPv4_Analyse" geht es um eine (bzw. zwei) Consolen Anwendungen.
Diese Consolen Anwendungen verfolgen das Zeil alle relevanten IP4v Adressen nach verfügbarkeit zu testen.
Dadurch kann eine Liste von IPv4 Adressen erstellt werden, die öffentlich zugänglich sind.

Das gesamte Projekt wird in der Programmiersprache C# entwickelt.
Also Entwicklungsumgebung nutze ich Visual Studio 2015/2013.

### Über mich
Ich bin im relativ neu in der Programmierung bin am anfang der Ausbildung als 
Fachinformatiker für Anwendungsentwicklung.

Jedoch beschäftige ich mich schon etwas länger (privat) mit der Programmierung.

### Ideen findung
Die Idee für das Projekt ist aus zufall entstanden.
Als ich mir mal wieder einige Klassen der .Net Frameworks angeschaut habe,
bin ich auf die Klasse Ping gekommen.

Nachdem ich mit der Klasse ein wenig gespielt habe kahm ich auf die Idee
einfach mal alle IPv4 Adressen anzupingen.

Relativ schnell wurde mir klar, das dieses Projekt viel rechen kapazität benötigen würde, 
um diesen kompletten scann innerhalb einer passablen Zeit zu schaffen. Dadruch wurde das
Projekt etwaS größer als anfangs angenommen.

### Angewandte Methoden/Klassen
Wie eben erwähnt nutze ich für den IP-Scann die Klasse Ping von dem .Net Framework.
Dadurch merkt die Anwendung ob die IP-Adresse öffentlich zugänglich ist oder nicht
(eine Antwort oder nicht).

Natürlich habe ich einige mehrere Klassen und Methoden genutzt.
Jedoch wollte ich nur die Klasse erwähnen auf dem das Projekt basiert.

### Problematik
Einer der großen Probleme der Anwendung ist der praktische nutzen.
Die Anwendung braucht für ein kompletten scann unheimlich lange Zeiten,
wodurch diese Anwendung und das Ziel auf mehrere Geräte und Threads verteielt werden muss.

Das ist zumindest die momentane Lösung die ich verfolge um
Rechenzeit zu spaaren.

### In Zahlen
Theoretisch mögliche IP Adressen: 4.294.967.296
Praktisch übrige IP Adressen (ca.): 2.50.000.000
Dauert eines Ping: 5 Sekungen

Scann dauer in Sekunden: 21474839980
Scann dauer in Stunden: 5965233

Diese Zeiten werden erziehlt, wenn das gesamte Programm nur auf ein Gerät mit ein Prozess läuft.

Hallo zusammen,

heute möchte ich euch eine lustige aber auch sinnlose Anwendung vorstellen.

Ich habe heute auf der Arbeit mit ein paar .Net Klassen rumgespielt. Dabei bin ich auf die "Ping" Klasse gestoßen. 
Mit der Klasse kann ich geziehlt IP-Adressen im Internet anpingen. Das ermöglcht mir also festzustellen, ob eine IP-Adresse
vergeben wurde und ob diese öffentlich zugänglich ist.

Daraufhin dachte ich mir:
Warum kein Algorithmus entwickeln der alle IPv4 Adressen nach einander abscannt?
Dieses Ziel habe ich einfach mal ein wenig verfolgt und entstanden ist eine kleine Consolen Anwendung.

Den SourceCode zu dieser Anwendunf findet ihr im Dokument mit dem Namen: IPv4_Analyse_0.5

Die ganze Anwendung ist natürlich "etwas" unausgereift. Es werden beispielsweise Reservierte IP-Adressen
nicht ausgeschlossen und mit gescannt. Ich bin gerade dabei für dieses Problem eine gute lösung zu finden,
gewissen Ansätze habe ich schon sind jedoch noch in der entwicklung und vorbereitung.

Der Lösungsansatz:
Ich habe vor, zuerst eine Whitelists zu erstellen alles relevanten IPv4 Adressen. Dafür habe ich mir
zuerst eine Liste aller Reservierten IP-Adressen herrausgesucht. Dabei stellt ich fest, das dies schon
eine große Menge sind. Diese Reservierungen werden oft mit dem ersten Block, also Block "A" bestimmt.

Insgesammt gibt es um die 131 reservierte Blocks. Um es einfach zu halten, habe ich Blocke die erst 
ab eine bestimmten Ziffer im zweiten Block reserviert sind trotzdem auf dem ersten Block beschränkt.

Also habe ich nicht alle IP-Adressen in "192.168" Blockiert sondern alle in "192"
Danach wird angegeben auf wie viele Threads und wie viele Geräte der Scann laufen soll.
Daraufhin wird zu diesen Angaben dementsprechende Listen erstellt zb.:

Geraet1_Thread1.txt
Geraet1_Thread2.txt
Geraet1_Thread3.txt
Geraet1_Thread4.txt
Geraet2_Thread1.txt
Geraet2_Thread2.txt
Geraet2_Thread3.txt
Geraet2_Thread4.txt

Dadurch habe ich die möglichkeit den kompletten Scann auf mehrere Geräte und Prozesse zu verteilen.
Diese überlegung ist daraus erstanden, das ein Kompletter scann unheimlich lange dauern würde.
Wenn nur ein Gerät mit ein Prozess diese Analyse ausführen würde.

Hier ein Rechen beispiel:




Wenn man diese Berechnungen nun auf mehrere Prozesse und Geräte erweitert,
kann man durch genug Rechenleistung auf pasable ergebnisse kommen.
