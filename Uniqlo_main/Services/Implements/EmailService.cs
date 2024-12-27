using Azure.Core;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using Uniqlo_main.Helpers;
using Uniqlo_main.Services.Abstracts;

namespace Uniqlo_main.Services.Implements
{
    //public class EmailService:IEmailService
    //{

    //    readonly SmtpClient _client;
    //    readonly MailAddress _from;
    //    readonly HttpContext context;
    //    public EmailService(IOptions<SmtpOptions> options,IHttpContextAccessor acc)
    //    {
    //        var opt = options.Value;
    //        _client = new(opt.Host, opt.Port);

    //        _client.Credentials= new NetworkCredential(opt.Username,opt.Password);
    //        _client.EnableSsl = true;
    //        _from = new MailAddress(opt.Username, "Uniqlo");
    //        context = acc.HttpContext;

    //    }
    //   public void SendEmailConfirmation(string receiver, string name, string token)
    //    {
    //        MailAddress to= new(receiver);
    //        MailMessage msg= new MailMessage(_from,to);
    //        msg.Subject = "Confirm your email adress";
    //        string url = context.Request.Scheme + "://" + context.Request.Host + "/Account/VerifyEmail?Token=" + token+"&user="+name;
    //        msg.Body =EmailTemplates.VerifyEmail.Replace("__$name",name).Replace("__$link",url);
    //        msg.IsBodyHtml = true;
    //        _client.Send(msg);
    /////// //msg.Body = EmailTemplates.VerifyEmail;

    ////////context.Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
    ////// //return Task.CompletedTask; }
    //public Task SendAsync(string receiver,string body)
    //{


    //    MailAddress to = new(receiver);
    //    MailMessage msg = new MailMessage(_from, to);
    //    msg.Subject = "Mail";
    //    msg.Body = "<p>Mail gonderildi From this <a>link</a></p>";
    //    msg.IsBodyHtml = true;


    //    return Task.CompletedTask;
    //}

    //    }
    //}
}