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
        public IPv4Address(string IpAddress)
        {
            this.IpAddress = IpAddress;
            DomainName = "";
        }

        public string IpAddress { get; set; }       // Hinterlegte IP-Adresse
        public string DomainName { get; set; }      // Beinhaltet den Domainnamen einer IP-Adresse (wird in CheckHttp bestimmt, Standart = "")
        

        /// <summary>
        /// Prüft ob die im Objekt hinterlegte IP-Adresse durch ping erreichbar ist.
        /// </summary>
        /// <param name="pingObj">Benötigt ein Ping Objekt (kein Sender).</param>
        /// <param name="timeOutValue">Benötigt eine Timeout Zeitangabe (ms).</param>
        /// <param name="tries">Bestimmt, wie oft der Ping wiederholt wird.</param>
        /// <returns>Gibt true zurück wenn der Ping erfolgreich war.</returns>
        public async Task<bool> CheckPing(Ping pingObj, int timeOutValue, int tries)
        {
            for(int i = 1; i <= tries; i++)
            {
                PingReply prply = pingObj.Send(IpAddress, timeOutValue);

                if (prply.Status == IPStatus.Success)
                {
                    return true;
                }  
            }
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
            for(int i = 1; i <= tries; i++)
            {
                try
                {
                    WebRequest webRequestObj = WebRequest.Create("http://" + IpAddress);
                    webRequestObj.Timeout = timeOutValue;
                    webRequestObj.GetResponse();

                    DomainName = Dns.GetHostEntry(IpAddress).HostName;
                    return true;
                }
                catch(Exception e) {  }
            }
            return false;
        }
    }
}
