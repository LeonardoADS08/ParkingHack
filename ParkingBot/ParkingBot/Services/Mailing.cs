using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ParkingBot.Services
{
    public class Mailing
    {
        //Send correo de compra PPU 
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        public static async Task<bool> SendEmail(string Email, string NombreDestino, string body, string sujeto)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("prupsa1@gmail.com");

                // The important part -- configuring the SMTP client
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
                smtp.UseDefaultCredentials = false; // [3] Changed this
                smtp.Credentials = new NetworkCredential(mail.From.Address, "promoupsa123");  // [4] Added this. Note, first parameter is NOT string.
                smtp.Host = "smtp.gmail.com";

                //notificacion
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                //recipient address
                mail.To.Add(new MailAddress(Email.Trim(), NombreDestino));

                //Formatted mail body
                mail.IsBodyHtml = true;
                string st = body;

                mail.Subject = sujeto;
                mail.Body = st;
                smtp.Send(mail);
                return true;
            }
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            {
                return false;
            }
        }
    }
}