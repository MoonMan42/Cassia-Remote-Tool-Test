using Cassia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteTool_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string computerId;
            do
            {
                Console.Write("Enter the computer Id: ");
                computerId = Console.ReadLine();

                if (computerId != "0")
                {
                    LogUserOut(computerId);
                }else if (computerId == "0")
                {
                    break;
                }

            } while (true);
        }


        static void LogUserOut(string computerId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetRemoteServer(computerId))
            {
                server.Open(); foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    Console.WriteLine("Hi there, " + session.UserAccount + " on session " + session.SessionId);
                    Console.WriteLine("It looks like you logged on at " + session.LoginTime + " and are now " + session.ConnectionState);

                    if (session.UserAccount != null)
                    {
                        try
                        {

                            session.Disconnect();
                            Console.WriteLine("Logged the user out.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("General failure.");
                        }
                    }
                }
            }
        }
    }
}
