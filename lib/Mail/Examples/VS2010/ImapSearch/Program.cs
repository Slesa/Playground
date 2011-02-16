using System;
using System.Collections.Generic;
using Lesnikowski.Client.IMAP;
using Lesnikowski.Mail;

namespace ImapSearch
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
                imap.Connect(_server);              // Use overloads or ConnectSSL if you need to specify different port or SSL.
                imap.Login(_user, _password);       // You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
                                                    // or use UseBestLogin method if you want Mail.dll to choose for you.
                imap.SelectInbox();
                                                    // All search methods return list of unique ids of found messages.

                List<long> unseen = imap.SearchFlag(Flag.Unseen);           // Simple 'by flag' search.

                List<long> unseenReports = imap.Search(new SimpleImapQuery  // Simple 'by query object' search.
                {
                    Subject = "report",
                    Unseen = true,
                });

                List<long> unseenReportsNotFromAccounting = imap.Search(    // Most advanced search using ExpressionAPI.
                    Expression.And(
                        Expression.Subject("Report"),
                        Expression.HasFlag(Flag.Unseen),
                        Expression.Not(
                            Expression.From("accounting@company.com"))
                ));

                foreach (long uid in unseenReportsNotFromAccounting)        // Download emails from the last result.
                {
                    IMail email = new MailBuilder().CreateFromEml(
                        imap.GetMessageByUID(uid));
                    Console.WriteLine(email.Subject);
                }

                imap.Close();
            }
        }
    };
}
