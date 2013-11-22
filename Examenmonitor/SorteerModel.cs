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
                    resultaat = origineel.OrderBy(e => e.Locatie).ToList();
                    break;
                case "AflopendLocatie":
                    resultaat = origineel.OrderByDescending(e => e.Locatie).ToList();
                    break;
                case "OplopendDatum":
                    resultaat = origineel.OrderBy(e => e.HaalDatumOp()).ToList();
                    break;
                case "AflopendDatum":
                    resultaat = origineel.OrderByDescending(e => e.HaalDatumOp()).ToList();
                    break;
                case "OplopendBeginUur":
                    resultaat = origineel.OrderBy(e => e.HaalBeginUurOp()).ToList();
                    break;
                case "AflopendBeginUur":
                    resultaat = origineel.OrderByDescending(e => e.HaalBeginUurOp()).ToList();
                    break;
                case "OplopendEindUur":
                    resultaat = origineel.OrderBy(e => e.HaalEindUurOp()).ToList();
                    break;
                case "AflopendEindUur":
                    resultaat = origineel.OrderByDescending(e => e.HaalEindUurOp()).ToList();
                    break;
                case "OplopendDuur":
                    resultaat = origineel.OrderBy(e => e.Lengte).ToList();
                    break;
                case "AflopendDuur":
                    resultaat = origineel.OrderByDescending(e => e.Lengte).ToList();
                    break;
                case "OplopendDigitaal":
                    resultaat = origineel.OrderBy(e => e.IsDigitaal()).ToList();
                    break;
                case "AflopendDigitaal":
                    resultaat = origineel.OrderByDescending(e => e.IsDigitaal()).ToList();
                    break;
                case "OplopendTotaalVrij":
                    resultaat = origineel.OrderBy(e => e.VrijeSlots()).ToList();
                    break;
                case "AflopendTotaalVrij":
                    resultaat = origineel.OrderByDescending(e => e.VrijeSlots()).ToList();
                    break;
            }
            return resultaat;
        }
    }
}