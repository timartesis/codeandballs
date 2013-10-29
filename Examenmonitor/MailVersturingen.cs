using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Examenmonitor
{
    public class MailVersturingen
    {
        private const string ZENDER = "noreply@codeandballs.be";
        private const string LOGIN = "codeandballs@gmail.com";
        private const string PASSWOORD = "plantesis";
        private const string SMTPCLIENT = "smtp.gmail.com";
        private const string ONDERWERPREGISTRATIE = "Account activatie";
        private const string ONDERWERPASWOORDRECOVERY = "Resetten paswoord";
        private const int POORT = 587; 

        private SmtpClient client;
        private NetworkCredential loginInfo;
        private static MailVersturingen registratieMail;

        private MailVersturingen()
        {
            client = new SmtpClient(SMTPCLIENT, POORT);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            loginInfo = new NetworkCredential(LOGIN, PASSWOORD);
            client.Credentials = loginInfo;
        }

        private bool ZendMail(string naam, string ontvangerMail, string randomLink, string onderwerp, StringBuilder body)
        {
            MailMessage mailBericht = new MailMessage(new MailAddress(LOGIN, ZENDER), new MailAddress(ontvangerMail, naam));
            mailBericht.Subject = onderwerp;
            mailBericht.Body = body.ToString();
            mailBericht.IsBodyHtml = true;

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

        //Onderstaande methode gebruiken voor het zenden van de registratie mail

        public bool ZendRegistratieMail(string naam, string ontvangerMail, string randomLink)
        {
            return ZendMail(naam, ontvangerMail, randomLink, ONDERWERPREGISTRATIE, OpstellenBerichtRegistratie(naam, randomLink));
        }

        //Onderstaande methode gebruiken voor het zenden van de paswoord recovery mail

        public bool ZendPaswoordResetMail(string naam, string ontvangerMail, string randomLink)
        {
            return ZendMail(naam, ontvangerMail, randomLink, ONDERWERPASWOORDRECOVERY, OpstellenBerichtResetten(naam, randomLink));
        }

        //Onderstaande methode zal het bericht gaan opstellen met de randomlink

        private StringBuilder OpstellenBerichtResetten(string naam, string randomLink)
        {
            StringBuilder bericht = new StringBuilder();

            const string beginUrl = "http://localhost:50157/";

            bericht.Append("<h2>Beste " + naam + ", </h2>");
            bericht.Append("<br /><br /><p> U ontvangt deze mail omdat u een paswoord reset heeft aangevraagd.</p>");
            bericht.Append("<br /><br /><p> Navigeer naar onderstaande link om uw paswoord te resetten. Deze link is 2 dagen geldig.</p>");
            bericht.Append("<a href=" + beginUrl + randomLink + ">Reset Paswoord</a>");
            bericht.Append("<br /><br /> Indien bovenstaande link niet werkt, kopieer dan volgende link in je browser: " + beginUrl + randomLink);
            bericht.Append("<br /><br /> Indien u deze reset niet had aangevraagd, gelieve deze mail dan te negeren!");
            bericht.Append("<br /><br /> Vriendelijke Groeten,");
            bericht.Append("<br /> Team Codeandballs.");

            return bericht;
        }

        private StringBuilder OpstellenBerichtRegistratie(string naam, string randomLink)
        {
            StringBuilder bericht = new StringBuilder();

            const string beginUrl = "http://localhost:50157/";

            bericht.Append("<h2>Beste " + naam + ", </h2>");
            bericht.Append("<br /><br /><p> Bedankt voor het aanmaken van een account.</p>");
            bericht.Append("<br /><br /><p> Navigeer naar onderstaande link om uw account te activeren. Deze link is 2 dagen geldig.</p>");
            bericht.Append("<a href=" + beginUrl + randomLink + ">Activeer Account</a>");
            bericht.Append("<br /><br /> Indien bovenstaande link niet werkt, kopieer dan volgende link in je browser: " + beginUrl + randomLink);
            bericht.Append("<br /><br /> Indien u deze registratie niet had aangevraagd, gelieve deze mail dan te negeren!");
            bericht.Append("<br /><br /> Vriendelijke Groeten,");
            bericht.Append("<br /> Team Codeandballs.");

            return bericht;
        }

        //Singleton voor deze klasse, aangezien hier maar 1 instantie van nodig is

        public static MailVersturingen getInstance()
        {
            if (registratieMail == null)
                registratieMail = new MailVersturingen();

            return registratieMail;


        }
    }
}