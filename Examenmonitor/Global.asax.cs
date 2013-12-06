using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Examenmonitor
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {     
            //The first Session "Logged" which is an indicator to the
            //status of the user
            Session["Logged"] = "No";
            //The second Session "User" stores the name of the current user
            Session["User"] = "";

            //The third Session "URL" stores the URL of the
            //requested WebForm before Logging In
            Session["URL"] = "Default.aspx";

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
            var app = (HttpApplication)sender;
            
            
            if (app.Context.Request.Url.LocalPath.EndsWith("/"))
            {
                try
                {

                    app.Context.RewritePath(
                             string.Concat(app.Context.Request.Url.LocalPath, "Login.aspx"));
                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeInjection(app.Context.Request.Url.LocalPath), "Login.aspx"));
                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeHtml(app.Context.Request.Url.LocalPath), "Login.aspx"));
                }
                catch (HttpRequestValidationException)
                {

                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeHtml(app.Context.Request.Url.LocalPath), "Login.aspx"));
                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeInjection(app.Context.Request.Url.LocalPath), "Login.aspx"));
                }
                catch (HttpException) {
                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeHtml(app.Context.Request.Url.LocalPath), "Login.aspx"));
                    app.Context.RewritePath(
                             string.Concat(IOConverter.SanitizeInjection(app.Context.Request.Url.LocalPath), "Login.aspx"));
                }
            }

           /* try
            {
                string mappedPath = Request.MapPath(inputPath.Text,
                                                     Request.ApplicationPath, false);
            }
            catch (HttpException)
            {
                // Cross-application mapping attempted
            }*/
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}