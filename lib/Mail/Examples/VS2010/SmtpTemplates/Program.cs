using System;
using System.IO;
using Lesnikowski.Mail.Fluent;
using Lesnikowski.Mail.Headers;
using Lesnikowski.Mail.Templates;

namespace SmtpTemplates
{
    class Program
    {
        private const string _server = "smtp.company.com";
        private const string _user = "user";
        private const string _password = "password";

        static void Main()
        {
            // Create test data for the template:
            Order order = new Order();
            order.OrderId = 7;
            order.CustomerName = "John Smith";
            order.Currency = "USD";
            order.Items.Add(new OrderItem { Name = "Yellow Lemons", Quantity = "22 lbs", Price = 149 });
            order.Items.Add(new OrderItem { Name = "Green Lemons", Quantity = "23 lbs", Price = 159 });

            // Load and render the template with test data:
            string html = Template
                .FromFile("Order.template")
                .DataFrom(order)
                .PermanentDataFrom(DateTime.Now)    // Year is used in the email footer/
                .Render();

            // You can save the HTML for a preview:
            File.WriteAllText("Order.html", html);

            // ...or use this one line to send an email:

            Mail.Html(Template
                    .FromFile("Order.template")
                    .DataFrom(order)
                    .PermanentDataFrom(DateTime.Now)
                    .Render())
                .Text("This is text version of the message.")
                .AddVisual("Lemon.jpg").SetContentId("lemon@id")        // Here we attach an image and assign the content-id.
                .AddAttachment("Attachment.txt").SetFileName("Invoice.txt")
                .From(new MailBox("orders@company.com", "Lemon Ltd"))
                .To(new MailBox("john.smith@gmail.com", "John Smith"))
                .Subject("Your order")
                .UsingNewSmtp()
                .Server(_server)
                .WithSSL()
                .WithCredentials(_user, _password)
                .Send();
        }
    };
}
