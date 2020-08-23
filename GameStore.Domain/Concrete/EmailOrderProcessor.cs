using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{

    public class EmailSettings
    {
        public string MailToAddress = "sobirjonovz@mail.ru";
        public string MailFromAdress = "zokir5857@gmail.com";
        public bool UseSsl = true;
        public string UserName = "Zokir5857";
        public string PassWord = "2432350";
        public string ServerName = "smtp.exaample.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string Filelocation = @"f:\email";
    }
   public class EmailOrderProcessor: IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials =
                    new NetworkCredential(emailSettings.UserName, emailSettings.PassWord);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.Filelocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары: ");

               foreach(var line in cart.Lines)
                {
                    var subTotal = line.Game.Price * line.Quantity;
                    body.AppendFormat("{0}x{1} (итоги {2:c})",
                        line.Quantity, line.Game.Name, subTotal);
                }
                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                     .AppendLine("---")
                     .AppendLine("Доставка")
                     .AppendLine(shippingInfo.Name)
                     .AppendLine(shippingInfo.Line1)
                     .AppendLine(shippingInfo.Line2 ?? "")
                     .AppendLine(shippingInfo.Line3 ?? "")
                     .AppendLine(shippingInfo.City)
                     .AppendLine(shippingInfo.Country)
                     .AppendLine("---")
                     .AppendFormat("Подарочная упаковка: {0}",
                     shippingInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAdress,
                    emailSettings.MailToAddress,
                    "Новый заказ отправлен",
                    body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }
                smtpClient.Send(mailMessage);
             }
        }
    }
}
