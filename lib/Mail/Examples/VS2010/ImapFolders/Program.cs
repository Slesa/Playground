using System;
using System.Collections.Generic;
using Lesnikowski.Client.IMAP;

namespace ImapFolders
{
    class Program
    {
        private const string _server = "imap.server.com";
        private const string _user = "user";
        private const string _password = "password";

        static void Main()
        {
            using (Imap imap = new Imap())
            {
                imap.Connect(_server);                              // Use overloads or ConnectSSL if you need to specify different port or SSL.
                imap.Login(_user, _password);                       // You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
                                                                    // or use UseBestLogin method if you want Mail.dll to choose for you.

                List<FolderInfo> folders = imap.GetFolders();       // List all folders on the IMAP server

                Console.WriteLine("Folders on IMAP server: ");
                foreach (FolderInfo folder in folders)
                {
                    FolderStatus status = imap.Examine(folder.Name);    // Examine each folder for number of total and recent messages

                    Console.WriteLine(                                  // Display folder information
                        string.Format("{0}, Recent: {1}, Total: {2}",
                            folder.Name,
                            status.MessageCount,
                            status.Recent));
                }

                // You can also Create, Rename and Delete folders:
                imap.CreateFolder("Temporary");
                imap.RenameFolder("Temporary", "Temp");
                imap.DeleteFolder("Temp");

                imap.Close();
            }
        }
    };
}
