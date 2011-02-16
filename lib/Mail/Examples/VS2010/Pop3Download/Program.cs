using System;
using System.Collections.Generic;
using Lesnikowski.Client;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;

namespace Pop3Download
{
    class Program
    {
        private const string _server = "pop3.server.com";
        private const string _user = "user";
        private const string _password = "password";

        static void Main()
        {
            using (Pop3 pop3 = new Pop3())
            {
                pop3.Connect(_server);                      // Use overloads or ConnectSSL if you need to specify different port or SSL.
                pop3.Login(_user, _password);               // You can also use: LoginAPOP, LoginPLAIN, LoginCRAM, LoginDIGEST methods,
                                                            // or use UseBestLogin method if you want Mail.dll to choose for you.

                List<string> uidList = pop3.GetAll();       // Get unique-ids of all messages.

                foreach (string uid in uidList)
                {
                    IMail email = new MailBuilder().CreateFromEml(  // Download and parse each message.
                        pop3.GetMessageByUID(uid));

                    ProcessMessage(email);                          // Display email data, save attachments.
                }
                pop3.Close();
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
