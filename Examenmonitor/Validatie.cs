using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Text.RegularExpressions;

namespace Examenmonitor
{
    public class Validatie
    {
        private static Regex JuisteMailRegex = MaakJuisteEmailRegex();

        private static Regex MaakJuisteEmailRegex()
        {

            string mailPatroon = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        //tussen 8 en 20 karakters, bevat minstens 1 cijfer, minstens 1 hoofdletter en minstens 1 kleine letter en geen whitespaces.
        private static string regExWachtwoord = @"(?=^.{8,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$";
        static Regex expressieWachtwoord = new Regex(regExWachtwoord, RegexOptions.Compiled);

        public static bool CheckWachtwoord(string wachtwoord)
        {
            return expressieWachtwoord.IsMatch(wachtwoord);
        }

            //Reguliere expressie die het patroon van een email zal controleren om te zien of deze juist is,
            //controleert NIET of het adres geldig is.

            return new Regex(mailPatroon, RegexOptions.IgnoreCase);
        }

        public static bool Email(string mail)
        {
            return JuisteMailRegex.IsMatch(mail);
        }
    }
}