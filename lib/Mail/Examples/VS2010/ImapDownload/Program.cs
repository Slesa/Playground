using System;
using System.Collections.Generic;
using Lesnikowski.Client.IMAP;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;

namespace ImapDownload
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

                imap.SelectInbox();                                 // You can select other folders, e.g. Sent folder: imap.Select("Sent");

                List<long> uids = imap.SearchFlag(Flag.Unseen);     // Find all unseen messages.
                
                Console.WriteLine("Number of unseen messages is: " + uids.Count);

                foreach (long uid in uids)
                {
                    IMail email = new MailBuilder().CreateFromEml(  // Download and parse each message.
                        imap.GetMessageByUID(uid));

                    ProcessMessage(email);                          // Display email data, save attachments.
                }
                imap.Close();
            }
        }

        private static void ProcessMessage(IMail email)
        {
            Console.WriteLine("Subject: " + email.Subject);
            Console.WriteLine("From: " + JoinAddresses(email.From));
            Console.WriteLine("To: " + JoinAddresses(email.To));
            Console.WriteLine("Cc: " + JoinAddresses(email.Cc));
            Console.WriteLine("Bcc: " + JoinAddresses(email.Bcc));

            Console.WriteLine("Text: " + email.TextDataString);
            Console.WriteLine("HTML: " + email.HtmlDataString);

            Console.WriteLine("Attachments: ");
            foreach (MimeData attachment in email.Attachments)
            {
                Console.WriteLine(attachment.FileName);
                attachment.Save(@"c:\" + attachment.SafeFileName);
            }
        }

        private static string JoinAddresses(IMailBoxList addresses)
        {
            return string.Join(",",
                addresses.ConvertAll(m => string.Format("{0} <{1}>", m.Name, m.Address))
                .ToArray());
        }
    };
}
