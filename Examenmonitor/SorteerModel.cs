using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class SorteerModel
    {
        public static List<Examen> SorterenOplopend(List<Examen> origineel, string id)
        {
            List<Examen> resultaat = origineel;
            switch (id)
            {
                case "OplopendLocatie":
                case "AflopendLocatie":
                    resultaat = origineel.OrderBy(e => e.Locatie).ToList();
                    break;
                case "OplopendDatum":
                case "AflopendDatum":
                    resultaat = origineel.OrderBy(e => e.HaalDatumOp()).ToList();
                    break;
                case "OplopendBeginUur":
                case "AflopendBeginUur":
                    resultaat = origineel.OrderBy(e => e.HaalBeginUurOp()).ToList();
                    break;
                case "OplopendEindUur":
                case "AflopendEindUur":
                    resultaat = origineel.OrderBy(e => e.HaalEindUurOp()).ToList();
                    break;
                case "OplopendDuur":
                case "AflopendDuur":
                    resultaat = origineel.OrderBy(e => e.Lengte).ToList();
                    break;
                case "OplopendDigitaal":
                case "AflopendDigitaal":
                    resultaat = origineel.OrderBy(e => e.IsDigitaal()).ToList();
                    break;
                case "OplopendTotaalVrij":
                case "AflopendTotaalVrij":
                    resultaat = origineel.OrderBy(e => e.VrijeSlots()).ToList();
                    break;
            }
            return resultaat;
        }
    }
}