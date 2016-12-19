using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace IPv4_Analyse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Directory where the List files storaged
            string strFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IPv4_List";

            // Array and String for the IP-Adresses.
            short[] shIpAddress = new short[4] { 1, 1, 1, 1 };
            string strIpAddress = null;

            // Ping objects.
            Ping oPing = new Ping();
            PingReply oResult;

            // Satistic variables.
            long lTry = 0;
            long lCounter = 0;

            if (File.Exists(strFile))
            {
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                File.Create(strFile);
            }

            for (short a = 0; a < 254; a++, shIpAddress[0] +=1)
            {
                shIpAddress[1] = 1;
                shIpAddress[2] = 1;
                shIpAddress[3] = 1;
                for (short b = 0; b < 254; b++, shIpAddress[1] += 1)
                {
                    shIpAddress[2] = 1;
                    shIpAddress[3] = 1;
                    for (short c = 0; a < 254; c++, shIpAddress[2] +=1)
                    {
                        shIpAddress[3] = 1; 
                        for(short d = 0; d < 254; d++, shIpAddress[3] +=1)
                        {
                            strIpAddress = shIpAddress[0] + "." + shIpAddress[1] + "." + shIpAddress[2] + "." + shIpAddress[3];
                            Console.Write("Try to Ping IP_Address: " + strIpAddress + " - ");
                            try
                            {
                                oResult = oPing.Send(strIpAddress);
                                if(oResult.Status == IPStatus.Success)
                                {
                                    Console.Write("true\n");
                                    lCounter++;
                                    File.AppendAllText(strFile, strIpAddress + " - " + DateTime.Now + Environment.NewLine);
                                }
                                else
                                {
                                    Console.Write("false\n");
                                }
                            }catch { }
                        }
                    }
                }
            }
        }
    }
}
