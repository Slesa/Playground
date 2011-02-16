using System;
using System.Security.Cryptography.X509Certificates;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Fluent;
using Lesnikowski.Mail.Headers;

namespace SignedEmails
{
    class Program
    {
        static void Main(string[] args)
        {
            IMail email = Mail
                .Html(@"<img src=""cid:lemon@id"" align=""left"" /> This is simple 
                        <strong>HTML email</strong> with an image and attachment")
                .Subject("Subject")
                .AddVisual("Lemon.jpg").SetContentId("lemon@id")
                .AddAttachment("Attachment.txt").SetFileName("Invoice.txt")
                .From(new MailBox("mail@in_the_certificate.com", "Lemon Ltd"))
                .To(new MailBox("john.smith@gmail.com", "John Smith"))
                .SignWith(new X509Certificate2("TestCertificate.pfx", ""))
                .Create();

            email.Save(@"SignedEmail.eml");                         // You can save the email for preview.

            Console.WriteLine("Email was saved in SignedEmail.eml");
            Console.ReadLine();

            // For sure you'll need to send such email, take a look at SmtpSend and SmtpFluentSend samples.
        }
    };
}
