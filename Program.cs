using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using MySql.Data.MySqlClient;

namespace threading
{
    class Program
    {
        public static MySqlConnection[] myConnections;
        public static string CurrentPosition { get; set; }      // Beinhaltet die zuletzt zugewiesene IP-Adresse an ein IP-Objekt.
        public static IPv4Address[] IPList;                     // Beinhaltet eine Liste aller IP-Objekte.
        public static Ping[] PingArray;                         // Beinhaltet eine Liste aller Ping-Objekte (notwendig um Ressourcen zu spaaren, ein IP-Objekt hat ein Ping-Objekt).
        public static List<Thread> ThrList;
        public static string Pfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.txt";
        static void Main(string[] args)
        {
            if (!File.Exists(Pfad))
            {
                File.Create(Pfad);
            }
            else
            {
                File.Delete(Pfad);
                File.Create(Pfad);
            }

            int i = 0;
            // Erwartet eine Ganzzahl als Eingabe. Wird solange wiederholt, bis die Eingabe Korrekt ist. 
            // Danach wird der Scann gestartet.
            do
            {
                Console.Clear();
                Console.WriteLine("Wie viele Threads möchten Sie verwenden?");
                try
                {
                    i = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    PingArray = new Ping[i];                    // Muss initialisiert werden um gefüllt zu werden.
                    IPList = new IPv4Address[i];                // Muss initialisiert werden um gefüllt zu werden.
                    myConnections = new MySqlConnection[i];     // Muss initialisiert werden um gefüllt zu werden.
                    FillObj();                                  // Füllt alle Listen aus.
                    CreatThreads();                             // Erstellt und Startet die Threads.
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Der eingegebene Wert ist nicht gültig. \r\nBitte achten Sie darauf das die Eingabe einer Ganzzahl entspricht.");
                    Console.ReadKey();
                }
            }
            while (i == 0);
            Console.ReadKey();
        }


        /// <summary>
        /// Thread Methode die aufgerufen wird und die Scanns durchführt.
        /// </summary>
        public async static void DoSomeWork()
        {
            // Gibt die aktuelle IP-Adresse als int array.
            string[] strCurrentPosition = CurrentPosition.Split('.');
            int[] iCurrentPosition = new int[strCurrentPosition.Length];

            for (int i = 0; i <= strCurrentPosition.Length - 1; i++)
            {
                iCurrentPosition[i] = Convert.ToInt32(strCurrentPosition[i]);
            }

            // Bestimmt bis welche Stelle gescannt wird.
            int thrName = Convert.ToInt32(Thread.CurrentThread.Name);
            if (iCurrentPosition[0] != 255 && iCurrentPosition[1] != 255 && iCurrentPosition[2] != 255 && iCurrentPosition[3] != 255)
            {
                // Gibt mir den Aktuellen Thread Index.               
                string IP = IPList[thrName].IpAddress;

                // Scannt das aktuelle IP-Objekt.
                bool b1 = await IPList[thrName].CheckPing(PingArray[Convert.ToInt32(Thread.CurrentThread.Name)], 50, 1);
                bool b2 = await IPList[thrName].CheckHttp(300, 1);

                // Ausgabe wenn die Ping true zurück gegeben hat.
                if (b1 == true && b2 == true)
                {
                    myConnections[thrName].Open();
                    MySqlCommand command = new MySqlCommand("insert into ipscann(Ping,WebRequest,Domain) value('true','true','" + IPList[thrName].DomainName + "')", myConnections[thrName]);
                    MySqlDataReader reader = command.ExecuteReader();
                    myConnections[thrName].Close();

                    Console.WriteLine("Ping: " + IPList[thrName].IpAddress + " [successfully]");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Domain: " + IPList[thrName].IpAddress + " : " + IPList[thrName].DomainName);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (b1 == true)
                {
                    Console.WriteLine("Ping: " + IPList[thrName].IpAddress + " [successfully]");
                }
                // Ausgabe wenn WebRequest true zurück gegeben hat.
                if(b2 == true && b1 == false)
                {
                    myConnections[thrName].Open();
                    MySqlCommand command = new MySqlCommand("insert into ipscann(Ping,WebRequest,Domain) value('false','true','" + IPList[thrName].DomainName + "')", myConnections[thrName]);
                    MySqlDataReader reader = command.ExecuteReader();
                    myConnections[thrName].Close();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Domain: " + IPList[thrName].IpAddress + " : " + IPList[thrName].DomainName);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // Löscht die IP-Adresse von dem aktuellen IP-Objekt.
                IPList[thrName].IpAddress = null;
                FillObj();
                DoSomeWork();
            }
            else if(iCurrentPosition[0] == 255 && iCurrentPosition[1] == 255 && iCurrentPosition[2] == 255 && iCurrentPosition[3] == 255)
            {
                ThrList[thrName] = null;
                if (!ScannThreadActivity())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("We are done here!");
                }
            }
        }

        /// <summary>
        /// Füllt alle Listen und IP-Adressen eines IP-Objektes.
        /// </summary>
        public static void FillObj()
        {
            string strIP;       // Hilfsvariable um CurrentPosition zu bestimmen.

            // Wird nur ausgeführt, wenn die Methode das erste mal aufgerufen wird, um alle Listen zu füllen.
            if (CurrentPosition == null)
            {
                //int[] iStartPosition = { 85, 169, 128, 0 };       // Bestimmt die die IP-Adresse bei dem der Scann beginnt.
                int[]  iStartPosition = { 85, 214, 22, 200 };

                int i = 0;      // Hilfsvariable um die schon gefüllten IPList-Positionen zu bestimmen.
                while (i < IPList.Length)
                {
                    // Wird benötigt um mehr als 256 Threads erstellen zu können.
                    for (int a = 0; a <= 256; a++)
                    {
                        if (i >= IPList.Length)
                        {
                            break;
                        }
                        // Erstellt IP-Objekt und zählt "iStartPosition" hoch um die nächste IP-Adresse zu bestimmen.
                        IPList[i] = new IPv4Address(iStartPosition[0] + "." + iStartPosition[1] + "." + iStartPosition[2] + "." + iStartPosition[3]);
                        iStartPosition[3]++;
                        i++;

                        // Sobald 255 Threads erstellt wurden (setzt die letzte Dezimalblock der IP-Adresse auf 0 und erhöht den dritten Block zum 1).
                        if (iStartPosition[3] == 255)
                        {
                            iStartPosition[3] = 0;
                            iStartPosition[2]++;
                        }
                    }
                    CurrentPosition = iStartPosition[0] + "." + iStartPosition[1] + "." + iStartPosition[2] + "." + iStartPosition[3];
                }

                // Erstellt Ping Objekte.
                for (int a = 0; a < PingArray.Length; a++)
                {
                    PingArray[a] = new Ping();
                }

                for(int a = 0; a < myConnections.Length; a++)
                {
                    myConnections[a] = new MySqlConnection("server=localhost;user id=XXXXXX;database=ipscann;password=XXXXXX;Connect Timeout=50;");
                }
            }
            // Wird ausgeführt, wenn die Methode weitere mahle ausgerufen wird (um eine Neue IP-Adresse im IP-Objekt zu bestimmten).
            else
            {
                // Gibt die aktuelle IP-Position als int Array aus.
                string[] strCurrentPosition = CurrentPosition.Split('.');
                int[] iCurrentPosition = new int[strCurrentPosition.Length];

                for (int i = 0; i <= strCurrentPosition.Length - 1; i++)
                {
                    iCurrentPosition[i] = Convert.ToInt32(strCurrentPosition[i]);
                }

                // Kontrolliert ob ein Block der momentanen IP-Adresse 255 entspricht, wenn dies der Fall ist, wird der vordere Block um 1 erhöht.
                if (iCurrentPosition[3] == 255)
                {
                    iCurrentPosition[3] = 0;
                    iCurrentPosition[2]++;
                }

                if (iCurrentPosition[2] == 255)
                {
                    iCurrentPosition[2] = 0;
                    iCurrentPosition[1]++;
                }

                if (iCurrentPosition[1] == 255)
                {
                    iCurrentPosition[1] = 0;
                    iCurrentPosition[0]++;
                }

                // Prüft welches Objekt keine IP-Adresse mehr hat und füllt dieses Objekt mit einer neuen IP-Adresse.
                for (int i = 0; i < IPList.Length; i++)
                {
                    if (IPList[i].IpAddress == null)
                    {
                        strIP = iCurrentPosition[0] + "." + iCurrentPosition[1] + "." + iCurrentPosition[2] + "." + (iCurrentPosition[3] + 1);
                        CurrentPosition = strIP;
                        IPList[Convert.ToInt32(Thread.CurrentThread.Name)].IpAddress = strIP;
                    }
                }
            }
        }

        /// <summary>
        /// Erstellt eine Thread Liste anhand der angegebenen Threads.
        /// </summary>
        public static void CreatThreads()
        {
            ThrList = new List<Thread>();
            for (int a = 0; a < IPList.Count(); a++)
            {
                Thread thr = new Thread(new ThreadStart(DoSomeWork));
                ThrList.Add(thr);
                thr.Name = Convert.ToString(a);
            }
            StartThreads();
        }

        /// <summary>
        /// Startet alle Threads (wird benötigt um eine korrekte Ausgabe zu tätigen "We are done here!").
        /// </summary>
        public static void StartThreads()
        {
            for(int i = 0; i < ThrList.Count(); i++)
            {
                ThrList[i].Start();
            }
        }

        /// <summary>
        /// Scannt ob noch ein Thread am arbeiten ist. 
        /// </summary>
        /// <returns>Gibt flase zurück, wenn kein Thread mehr arbeitet.</returns>
        public static bool ScannThreadActivity()
        {
            bool bo = false;
            for(int i = 0; i < ThrList.Count(); i++)
            {
                if(ThrList[i] != null)
                {
                    bo = true;
                }
            }
            return bo;
        }
    }
}
