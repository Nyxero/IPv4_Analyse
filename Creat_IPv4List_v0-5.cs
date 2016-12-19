using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Creat_IPv4List
{
    class Creat_IPv4List
    {
        static void Main(string[] args)
        {
            // User Information for Threads and Devices.
            short shThreads = Get_Threads();
            short shDevices = Get_Devices();
            short shNowThreads = Get_NowThreads(shDevices, shThreads);

            // Creating Directory for the List.
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IPv4_List"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IPv4_List");
            string strDirecotry = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IPv4_List";

            // Array for the IP-Addresses.
            short[] shIpAdress = new short[4] { 0, 0, 0, 0 };

            // Boolean for checking if the "A" block in the Black-List.
            bool boReady = false;

            // Array of IP-Black-List.
            short[] shBlackList = new short[]
            {0,10,127,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,
            244,245,246,247,248,249,250,251,252,253,254,255,3,4,8,9,12,15,16,17,18,19,20,32,34,38,44,48,
            56,6,7,11,21,22,26,28,29,30,33,55,214,215,1,2,5,14,23,24,25,27,31,36,37,39,41,42,43,45,46,49,
            50,51,53,57,60,61,88,111,114,107,120,128,169,172,177,179,181,183,186,187,189,190,191,192,198,200,201,202,203,218};

            Thread[] oThreads = Creat_Threads(shNowThreads, boReady, shDevices, shThreads,shBlackList, shIpAdress);

            // ---------------------------------------------------------------------------------------------------------------
            Creat_ListFiles(shThreads, shDevices, strDirecotry);

            for (int i = 0; i < oThreads.Length; i++)
            {
                oThreads[i].Start();
            }
            Console.ReadKey();
        }

        // Creading the List Files.
        static private void Creat_ListFiles(short shTreads, short shDevices, string strDirectory)
        {
            string strFileName = "";

            for (short a = 1; a <= shDevices; a++)
            {
                for (short b = 1; b <= shTreads; b++)
                {
                    strFileName = "Device" + a + "_Thread" + b + ".txt";
                    try
                    {
                        File.Create(strDirectory + "\\" + strFileName);
                    }
                    catch
                    {
                        Console.Write("Critical error, the List files can´t create! (0)");
                        Directory.Delete(strDirectory);
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    strFileName = "";
                }
            }
        }

        // Get the number of Devices from the user.
        static private short Get_Devices()
        {
            short shDevices = 0;
            while (true)
            {
                Console.Write("How many devices do you want to use: ");
                try
                {
                    shDevices = Convert.ToInt16(Console.ReadLine());
                    if (shDevices == 0)
                    {
                        Console.WriteLine("Zero devices are not allowed!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                        break;
                }
                catch { }
            }
            return shDevices;
        }

        // Get the number of threads from the user.
        static private short Get_Threads()
        {
            short shThreads = 0;
            while (true)
            {
                Console.Write("How many threads do you want to use: ");
                try
                {
                    shThreads = Convert.ToInt16(Console.ReadLine());
                    if (shThreads == 0)
                    {
                        Console.WriteLine("Zero threads are not allowes!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                        break;
                }
                catch { }
            }
            return shThreads;
        }

        static private short Get_NowThreads(short shDevices, short shThreads)
        {
            short shNowThreads = 0;
            int sum = shDevices * shThreads;
            double doSum;
            while (true)
            {
                Console.Write("How many threads do you want to user for creating the Lists: ");
                try
                {
                    shNowThreads = Convert.ToInt16(Console.ReadLine());
                    doSum = Convert.ToDouble(sum) / Convert.ToDouble(shNowThreads);
                    if (doSum % 1 != 0 || sum < shNowThreads)
                    {
                        Console.WriteLine("Try another value!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                        break;
                }
                catch { }
            }
            return shNowThreads;
        }

        static private Thread[] Creat_Threads(short shNowThreads, bool boReady, short shDevices, short shThreads, short[] shBlackList, short[] shIpAdress)
        {
            Thread[] nowThread = new Thread[shNowThreads];

            for (short i = 0; i < shNowThreads; i++)
            {
                nowThread[i] = new Thread(() => Fill_List(nowThread, boReady, shDevices, shThreads, shBlackList, shIpAdress));
            }

            return nowThread;
        }

        static private void Fill_List(Thread[] oNowThreads, bool boReady, short shDevices, short shThreads, short[] shBlackList, short[] shIpAdress)
        {
            short doAnteil = Convert.ToInt16(shThreads * shDevices / oNowThreads.Length);
            string strDirecotry = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IPv4_List";
            int iCountFiles = Directory.GetFiles(strDirecotry).Length;
            string[] strFileNames = Directory.GetFiles(strDirecotry);
            short shThreadNumber = 0;

            double[] lol = new double[oNowThreads.Length];

            for(short s = 0; s < oNowThreads.Length; s++)
            {
                lol[s] = (doAnteil * s);
            };

            if (iCountFiles != shThreads * shDevices)
            {
                Console.Clear();
                Console.WriteLine("Critical error (1)");
                Environment.Exit(1);
            }
            else
            {
                for(int i = 0; i < doAnteil; i++)
                {

                }
            }
        }
    }
}
