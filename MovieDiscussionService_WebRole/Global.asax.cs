using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MovieDiscussionService_WebRole.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MovieDiscussionService_WebRole
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            InitBlobs();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            FormsAuthenticationTicket ticket;
            try { ticket = FormsAuthentication.Decrypt(authCookie.Value); }
            catch { return; }
            if (ticket == null) return;

            // UserData format: "A:1;V:1"  (A=Admin, V=Verified) — jednostavan, bez JSON-a
            bool isAdmin = false, isVerified = false;
            var data = ticket.UserData ?? "";
            foreach (var kv in data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = kv.Split(':');
                if (parts.Length != 2) continue;
                var key = parts[0].Trim().ToUpperInvariant();
                var val = parts[1].Trim();
                if (key == "A") isAdmin = (val == "1" || val.Equals("true", StringComparison.OrdinalIgnoreCase));
                if (key == "V") isVerified = (val == "1" || val.Equals("true", StringComparison.OrdinalIgnoreCase));
            }

            var identity = new CustomIdentity(ticket.Name, true, isAdmin, isVerified);
            var principal = new CustomPrincipal(identity);
            HttpContext.Current.User = principal;
        }

        public void InitBlobs()
        {
            try
            {
                var storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference("userimages");
                container.CreateIfNotExists();
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            catch (WebException)
            {
            }
        }
    }
}
