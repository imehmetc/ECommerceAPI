using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Concretes
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "BA Boost Final Project", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();

            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendApproveSellerMailAsync(AppUser appUser)
        {

            StringBuilder mail = new();
            mail.AppendLine("Merhaba " + appUser.CompanyName + ",");
            mail.AppendLine();
            mail.AppendLine("Harika haberlerimiz var! 🎉 Başvurunuz başarıyla onaylandı ve artık Shoppica platformunda mağaza sahibi olarak yerinizi aldınız.");
            mail.AppendLine();
            mail.AppendLine("Artık ürünlerinizi sergileyebilir, müşterilerinizle buluşabilir ve işinizi büyütmek için ilk adımlarınızı atabilirsiniz. Sizinle çalışmak için sabırsızlanıyoruz!");
            mail.AppendLine();
            mail.AppendLine("Başvurunuzun onaylandığını bilmek bizleri çok mutlu etti. Size bol kazançlı ve keyifli satışlar dileriz. Herhangi bir sorunuz veya yardım ihtiyacınız olursa, bize her zaman ulaşabilirsiniz.");
            mail.AppendLine();
            mail.AppendLine("İyi çalışmalar!");
            mail.AppendLine();
            mail.AppendLine("Saygılarımızla,");
            mail.AppendLine("Shoppica Ekibi");

            string subject = "Shoppica Başvurunuz Onaylandı! 🎉";
            string body = mail.ToString();

            await SendMailAsync(appUser.Email, subject, body);
        }

        public async Task SendRejectSellerMailAsync(AppUser appUser)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba " + appUser.CompanyName + ",");
            mail.AppendLine();
            mail.AppendLine("Üzgünüz, e-ticaret platformumuzda mağaza sahibi olarak başvurunuz maalesef onaylanamadı.");
            mail.AppendLine();
            mail.AppendLine("Başvurunuzun neden onaylanmadığını daha iyi anlamak ve gelecek başvurularınızda size yardımcı olabilmek için lütfen bizimle iletişime geçin. Sizden gelen geri bildirimler bizim için çok değerli.");
            mail.AppendLine();
            mail.AppendLine("Gelecekte platformumuzda tekrar başvuruda bulunmak isterseniz, size yardımcı olmaktan memnuniyet duyarız. Herhangi bir sorunuz varsa, lütfen bize ulaşmaktan çekinmeyin.");
            mail.AppendLine();
            mail.AppendLine("İyi çalışmalar!");
            mail.AppendLine();
            mail.AppendLine("Saygılarımızla,");
            mail.AppendLine("Shoppica Ekibi");

            string subject = "Shoppica Başvurunuz Hakkında Önemli Bilgilendirme";
            string body = mail.ToString();

            await SendMailAsync(appUser.Email, subject, body);
        }
    }
}
