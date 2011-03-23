using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using NetFacebookGraphApiSDK;
using NetFacebookGraphApiSDK.Model;

namespace MVCSample.Controllers
{
    public class AuthController : Controller
    {
        private FacebookAPI fb;

        public AuthController()
        {
            var appId = ConfigurationManager.AppSettings.Get("FB_APP_ID");
            var appSecret = ConfigurationManager.AppSettings.Get("FB_APP_SECRET");
            fb = new FacebookAPI(appId, appSecret);
        }
        //
        // GET: /Auth/

        public ActionResult Index()
        {
            return View();
        }

        public void Login()
        {
            fb.Authorize(Response, "http://joblooter.com/Auth/Callback");
        }

        public ActionResult Callback()
        {
            if (fb.RetrieveAccessToken(Request.QueryString["code"], "http://joblooter.com/Auth/Callback"))
            {
                FacebookUser user = fb.GetUser();
                Session["User"] = string.Format("{0} {1}", user.first_name, user.last_name);
            }
            return View("Index");
        }

    }
}
