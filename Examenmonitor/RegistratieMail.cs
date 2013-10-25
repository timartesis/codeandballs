using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Examenmonitor
{
    public class RegistratieMail
    {
        private const string ZENDER = "noreply@codeandballs.be";
        private const string LOGIN = "codeandballs@gmail.com";
        private const string PASSWOORD = "plantesis";
        private const string SMTPCLIENT = "smtp.gmail.com";
        private const string ONDERWERP = "Account activatie";
        private const int POORT = 587; 

        private SmtpClient client;
        private NetworkCredential loginInfo;
        private MailMessage mailBericht;
        private static RegistratieMail registratieMail;

        private RegistratieMail()
        {
            client = new SmtpClient(SMTPCLIENT, POORT);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            loginInfo = new NetworkCredential(LOGIN, PASSWOORD);
            client.Credentials = loginInfo;
            mailBericht = new MailMessage();
        }

        public bool ZendRegistratieMail(string naam, string ontvanger, string randomLink)
        {
            mailBericht.From = new MailAddress(ZENDER);
            mailBericht.To.Add(ontvanger);
            mailBericht.Subject = ONDERWERP;
            mailBericht.Body = OpstellenBericht(naam, randomLink).ToString();
            mailBericht.IsBodyHtml = false;

            try
            {
                client.Send(mailBericht);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private StringBuilder OpstellenBericht(string naam, string randomLink)
        {
            StringBuilder bericht = new StringBuilder();
            bericht.Append("Beste " + naam + ",\n\nDank u voor het maken van een account.\nNavigeer naar de volgende link om uw account te activeren.\nDeze link is 2 dagen geldig.\n\n"
                + randomLink + "\nIndien u deze registratie niet had aangevraagd, gelieve dan deze mail te negeren.");
            return bericht;
        }

        public static RegistratieMail getInstance()
        {
            if (registratieMail == null)
                registratieMail = new RegistratieMail();

            return registratieMail;


        }
    }
}