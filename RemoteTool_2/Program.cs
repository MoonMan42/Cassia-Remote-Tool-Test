using Cassia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteTool_2
{
    // installed Cassia by Matthew Ferreira in Nuget packages. 
    //https://github.com/danports/cassia/blob/master/Samples/SessionInfo/Source/SessionInfo/Program.cs
    class Program
    {
        private static ITerminalServicesManager manager = new TerminalServicesManager();

        static void Main(string[] args)
        {
            string computerId;
            do
            {
                Console.Write("Enter the computer Id: ");
                computerId = Console.ReadLine();

                if (computerId == "0")
                {
                    break;
                }

                LogUserOut(computerId);
               

            } while (true);
        }


        static void LogUserOut(string computerId)
        {
            
            using (ITerminalServer server = manager.GetRemoteServer(computerId))
            {
                if (!server.IsOpen) // check to see if RPC is enabled. 
                    return;

                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    // Example snipit 
                    //Console.WriteLine("Hi there, " + session.UserAccount + " on session " + session.SessionId);
                    //Console.WriteLine("It looks like you logged on at " + session.LoginTime + " and are now " + session.ConnectionState);

                    if (server.IsOpen) // check if server is up
                    {
                        if (session.UserAccount != null) // skip any windows accounts 
                        {
                            try // try to disconnect sessions 
                            {

                                session.Disconnect();
                                Console.WriteLine("Logged the user out.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("General failure.");
                            }
                        }
                    } else
                    {
                        Console.WriteLine("Could not ping server.");
                    }
                }
            }

            
        }
        static void RebootServer(string computerId)
        {
            using (ITerminalServer server = manager.GetRemoteServer(computerId))
            {
                server.Open();
                server.Shutdown(ShutdownType.Reboot);
            }
        }
    }
}
