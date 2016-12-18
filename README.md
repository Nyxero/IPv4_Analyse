# Hallo zusammen,

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
Theoretisch mögliche IP Adressen: 4.294.967.296
Praktisch übrige IP Adressen (ca.): 2.50.000.000
Dauert eines Ping: 5 Sekungen

(Anzahl der IP Adressen)*(Pingdauer)
4.294.967.296 * 5 = 21.474.839.980 Sekunden.
21.474.839.980 / 3600 = 5.965.233 Stunden.
248.551 Tage.
8.172 Monate.
681 Jahre.

Wenn man diese Berechnungen nun auf mehrere Prozesse und Geräte erweitert,
kann man durch genug Rechenleistung auf pasable ergebnisse kommen.
