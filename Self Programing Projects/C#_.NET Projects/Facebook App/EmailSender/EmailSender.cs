using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public class EmailSender
    {
        public string UserAddress{ get; set; }

        public List<string> UserNameAndPassword{ get; set; }

        public string ReceiverAddress{ get; set; }

        public EmailSender(string i_UserAddress, string i_ReceiverAddress)
        {
            UserAddress = i_UserAddress;
            ReceiverAddress = i_ReceiverAddress;
        }

        public void SendEmail(string i_MailBody, string i_Attachment)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(UserAddress);
                mail.To.Add(ReceiverAddress);
                mail.Subject = "Mail from your facebook friend";
                mail.Body = i_MailBody;
                Attachment attachment = new System.Net.Mail.Attachment(i_Attachment);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(UserNameAndPassword[0], UserNameAndPassword[1]);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch(Exception ex)
            {
            }
        }
    }
}
