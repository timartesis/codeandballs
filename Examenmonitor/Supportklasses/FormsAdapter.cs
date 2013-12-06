using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;

namespace Examenmonitor
{
    public class FormControlAdapter : ControlAdapter
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(new RewriteFormHtmlTextWriter(writer));
        }

        public class RewriteFormHtmlTextWriter : HtmlTextWriter
        {
            public RewriteFormHtmlTextWriter(HtmlTextWriter writer)
                : base(writer)
            {
                this.InnerWriter = writer.InnerWriter;
            }

            public override void WriteAttribute(string name, string value,
                                                bool fEncode)
            {
                if (name.Equals("action") && string.IsNullOrEmpty(value))
                {
                    value = "default.aspx";
                }
                base.WriteAttribute(name, value, fEncode);
            }
        }
    }
}