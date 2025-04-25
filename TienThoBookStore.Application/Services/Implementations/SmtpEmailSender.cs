using Azure.Core;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.Services.Interfaces;

namespace TienThoBookStore.Application.Services.Implementations
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _cfg;
        public SmtpEmailSender(IConfiguration cfg) => _cfg = cfg;
        public async Task  SendAsync(string to, string subject, string htmlMessage)
        {
            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(_cfg["Smtp:From"]));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            msg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var client = new SmtpClient();
            await client.ConnectAsync(_cfg["Smtp:Host"], int.Parse(_cfg["Smtp:Port"]), true);
            await client.AuthenticateAsync(_cfg["Smtp:User"], _cfg["Smtp:Pass"]);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
