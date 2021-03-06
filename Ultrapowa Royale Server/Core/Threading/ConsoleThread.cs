using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using UCS.Helpers;

namespace UCS.Core.Threading
{
    internal class ConsoleThread
    {
        private static Thread T { get; set; }
        private static string Command { get; set; }

        public static void Start()
        {
            T = new Thread(() =>
            {
                Console.Title = "Ultrapowa Royale Server v" + Assembly.GetExecutingAssembly().GetName().Version;
                Console.WriteLine(
                    @"
    888     888 888    88888888888 8888888b.         d8888 8888888b.   .d88888b.  888       888        d8888
    888     888 888        888     888   Y88b       d88888 888   Y88b d88P' 'Y88b 888   o   888       d88888
    888     888 888        888     888    888      d88P888 888    888 888     888 888  d8b  888      d88P888
    888     888 888        888     888   d88P     d88P 888 888   d88P 888     888 888 d888b 888     d88P 888
    888     888 888        888     8888888P'     d88P  888 8888888P'  888     888 888d88888b888    d88P  888
    888     888 888        888     888 T88b     d88P   888 888        888     888 88888P Y88888   d88P   888
    Y88b. .d88P 888        888     888  T88b   d8888888888 888        Y88b. .d88P 8888P   Y8888  d8888888888
     'Y88888P'  88888888   888     888   T88b d88P     888 888         'Y88888P'  888P     Y888 d88P     888
                  ");
                Console.WriteLine("[UCR]    -> This Program is made by the Ultrapowa Network Developer Team!");
                Console.WriteLine("[UCR]    -> You can find the source at www.ultrapowa.com and www.github.com/ultrapowa/ucr");
                Console.WriteLine("[UCR]    -> Don't forget to visit www.ultrapowa.com daily for news update !");
                Console.WriteLine("[UCR]    -> UCR is now starting...");
                Console.WriteLine("");
                Debugger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
                Logger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
                MemoryThread.Start();
                NetworkThread.Start();
                while ((Command = Console.ReadLine()) != null)
                    CommandParser.Parse(Command);
            });
            T.Start();
        }

        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }
    }
}