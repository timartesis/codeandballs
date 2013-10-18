﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Examenmonitor
{
    public class Validatie
    {
        private static string mailPatroon = @"(?=^.{0,255}$)^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
          + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
          + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        private static string gebrNaamPatroon = @"(?=^.{0,255}$)^[a-zA-Z][-\w.]{0,255}([a-zA-Z\d]|(?<![-.])_)$";

        //Reguliere expressies die het patroon van een email en gebruikersnaam sullen controleren om te zien of deze juist zijn,
        //controleren NIET of het adres geldig is.

        private static Regex JuisteMailRegex = new Regex(mailPatroon, RegexOptions.IgnoreCase);
        private static Regex JuisteGebrNaamRegex = new Regex(gebrNaamPatroon, RegexOptions.Compiled);

        public static bool Email(string mail)
        {
            return JuisteMailRegex.IsMatch(mail);
        }

        public static bool GebruikersNaam(string naam)
        {
            return JuisteGebrNaamRegex.IsMatch(naam);
        }
    }
}