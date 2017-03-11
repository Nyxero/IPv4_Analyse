using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace threading
{
    public class IPv4Address
    {
        /// <summary>
        /// IPv4Address Konstruktor, erstellt das Objekt (benötigt eine IP-Adresse).
        /// </summary>
        /// <param name="IpAddress">IP-Adresse</param>
        public IPv4Address(short block1, short block2, short block3, short block4)
        {
            this.IpAddress = block1 + "." + block2 + "." + block3 + "." + block4;
            CurrentIP = block1 + "." + block2 + "." + block3 + "." + block4;
            DomainName = "";
        }

        public IPv4Address()
        {
            this.IpAddress = "0.0.0.0";
            CurrentIP = "0.0.0.0";
            DomainName = "";
        }

        public string IpAddress { get; set; }        // Hinterlegte IP-Adresse
        public string DomainName { get; set; }       // Beinhaltet den Domainnamen einer IP-Adresse (wird in CheckHttp bestimmt, Standart = "")
        public bool Pingable { get; set; }           // Zeigt ob eine IP-Adresse Pingable ist (gibt true oder false zurück).
        public bool OwnDomain { get; set; }          // Zeigt ob eine IP-Adresse eine Domain hostet (gibt ture oder alse zurück).
        public static string CurrentIP { get; set; } // Beinhaltet die momentan zuletzt zugewiesene IP-Adresse.


        /// <summary>
        /// Prüft ob die im Objekt hinterlegte IP-Adresse durch ping erreichbar ist.
        /// </summary>
        /// <param name="pingObj">Benötigt ein Ping Objekt (kein Sender).</param>
        /// <param name="timeOutValue">Benötigt eine Timeout Zeitangabe (ms).</param>
        /// <param name="tries">Bestimmt, wie oft der Ping wiederholt wird.</param>
        /// <returns>Gibt true zurück wenn der Ping erfolgreich war.</returns>
        public async Task<bool> CheckPing(Ping pingObj, int timeOutValue, int tries)
        {
            for (int i = 1; i <= tries; i++)
            {
                PingReply prply = pingObj.Send(IpAddress, timeOutValue);

                if (prply.Status == IPStatus.Success)
                {
                    Pingable = true;
                    return true;
                }
            }
            Pingable = false;
            return false;
        }

        /// <summary>
        /// Prüft ob die im Objekt hinterlegte IP-Adresse über ein WebRequest (GetResponse) erreichbar ist.
        /// </summary>
        /// <param name="timeOutValue">Benötigt eine Timeout Zeitangabe (ms).</param>
        /// <param name="tries">Bestimmt, wie oft der Webzugriff wiederholt wird.</param>
        /// <returns>Gibt true zurück, wenn der WebReqest erfolg hatte.</returns>
        public async Task<bool> CheckHttp(int timeOutValue, int tries)
        {
            for (int i = 1; i <= tries; i++)
            {
                try
                {
                    WebRequest webRequestObj = WebRequest.Create("http://" + IpAddress);
                    webRequestObj.Timeout = timeOutValue;
                    webRequestObj.GetResponse();

                    DomainName = Dns.GetHostEntry(IpAddress).HostName;
                    OwnDomain = true;
                    return true;
                }
                catch (Exception e) { }
            }
            OwnDomain = false;
            return false;
        }

        /// <summary>
        /// Gibt anhand von der CurrentIP Eigenschaft die nächste IPv4 Addresse aus.
        /// Sowohl der return Wert als auch die CurrentIP Eigenschafte geben die neue IPv4 Addresse.
        /// </summary>
        /// <returns></returns>
        public static string GetNextIP()
        {
            if(CurrentIP == String.Empty)
            {
                CurrentIP = "0.0.0.0";
                return "0.0.0.0";
            }
            else
            {
                string[] strIP = CurrentIP.Split('.');
                int[] iIP = new int[4];
                
                for(int i = 0; i < strIP.Length; i++)
                {
                    iIP[i] = Convert.ToInt32(strIP[i]);
                }

                if(iIP[3] >= 255 && iIP[2] < 255)
                {
                    iIP[3] = 0;
                    iIP[2] += 1;
                }
                else if(iIP[2] >= 255 && iIP[3] >= 255 && iIP[1] < 255)
                {
                    iIP[3] = 0;
                    iIP[2] = 0;
                    iIP[1] += 1;
                }
                else if(iIP[1] >= 255 && iIP[2] >= 255 && iIP[3] >= 255 && iIP[0] < 255)
                {
                    iIP[3] = 0;
                    iIP[2] = 0;
                    iIP[1] = 0;
                    iIP[0] += 1;
                }
                else if(iIP[0] >= 255 && iIP[1] >= 255 && iIP[2] >= 255 && iIP[3] >= 255)
                {
                    for(int i = 0; i < strIP.Length; i++)
                    {
                        iIP[i] = 0;
                    }
                }
                else
                {
                    iIP[3] += 1;
                }
                CurrentIP = iIP[0] + "." + iIP[1] + "." + iIP[2] + "." + iIP[3];
                return iIP[0] + "." + iIP[1] + "." + iIP[2] + "." + iIP[3];
            }
        }
    }
}
