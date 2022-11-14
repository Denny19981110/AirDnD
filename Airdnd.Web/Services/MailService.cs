using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Airdnd.Core.Helper
{
    public class MailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public MailService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public void SendVerifyMail(string email, string name)
        {
            SmtpClient client = InitailClient();
            var mailSubject = "Airdnd帳號驗證信";
            var mailBody = $@"
                <a href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}'>
                    <img src =''>
                </a>
                <div style='color: #000;'>
                    <p>{name} 你好:</p>
                    <p> 我們收到您註冊帳號的申請。</p>
                    <p> 如果您未提出申請，無須理會此訊息; 否則，請。</p>
                </div>
                <div style='background-color: #ff5a5f;display: inline-block; padding: 10px 15px; text-align: center;vertical-align: middle;cursor: pointer;border-radius: 5px;'>
                    <a style='color: #fff; text-decoration: none;' href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}/Login/ResetPassword?email={email}'>重設密碼</a>
                </div>
                <div style='color: #000;'>                    
                    <p> 謝謝！</p>
                    <p> Airdnd 團隊</p>
                </div>
            ";
            MailMessage mail = CreateNormalMail(email, mailSubject, mailBody);
            client.Send(mail);
        }

        public void SendResetPassword(string email, string userName)
        {
            SmtpClient client = InitailClient();
            var mailSubject = "Airdnd密碼重設信";
            var mailBody = $@"
                <a href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}'>
                    <img src =''>
                </a>
                <div style='color: #000;'>
                    <p>{userName} 你好:</p>
                    <p> 我們收到您重設密碼的申請。</p>
                    <p> 如果您未提出申請，無須理會此訊息; 否則，請重設密碼。</p>
                </div>
                <div style='background-color: #ff5a5f;display: inline-block; padding: 10px 15px; text-align: center;vertical-align: middle;cursor: pointer;border-radius: 5px;'>
                    <a style='color: #fff; text-decoration: none;' href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}/Login/ResetPassword?email={email}'>重設密碼</a>
                </div>
                <div style='color: #000;'>                    
                    <p> 謝謝！</p>
                    <p> Airdnd 團隊</p>
                </div>
            ";
            MailMessage mail = CreateNormalMail(email, mailSubject, mailBody);
            client.Send(mail);
        }


        public void SendOrderInfo(string email)
        {
            SmtpClient client = InitailClient();
            var mailSubject = "Airdnd預定通知信";
            var mailBody = $@"
                <a href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}'>
                    <img src =''>
                </a>
                <div style='color: #000;'>
                    <p> 你好:</p>
                    <p> 我們已收到您的預定。</p>
                    <p> 請至「我的預定」查看詳細資訊。 </p>
                </div>
                <div style='background-color: #ff5a5f;display: inline-block; padding: 10px 15px; text-align: center;vertical-align: middle;cursor: pointer;border-radius: 5px;'>
                    <a style='color: #fff; text-decoration: none;' href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}/Trip/Index'>我的預定</a>
                </div>
                <div style='color: #000;'>                    
                    <p> 謝謝！</p>
                    <p> Airdnd 團隊</p>
                </div>
            ";
            MailMessage mail = CreateNormalMail(email, mailSubject, mailBody);
            client.Send(mail);
        }


        private SmtpClient InitailClient()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(_config["MailCredential:UserName"], _config["MailCredential:Password"]);
            client.EnableSsl = true;
            return client;
        }

        /// <summary>
        /// 建立信件
        /// </summary>
        /// <param name="email">收件者</param>
        /// <param name="mailSubject">信件主旨</param>
        /// <param name="mailBody">信件內容</param>
        /// <returns></returns>
        private MailMessage CreateNormalMail(string email, string mailSubject, string mailBody)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_config["MailCredential:UserName"], _config["MailCredential:DisplayName"]);
            mail.To.Add(email);
            mail.Priority = MailPriority.Normal;
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = mailBody;   
            return mail;
        }                
    }
}
