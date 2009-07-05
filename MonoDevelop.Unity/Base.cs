using System;
using System.Collections.Generic;
using System.Text;


using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Core.Gui.WebBrowser;

namespace MonoDevelop.Unity
{


    public class Base
    {
        private static IWebBrowser browser;
        #region Overloaded Open

        public bool Open(Settings.Documentation target)
        {
            if ((PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                !PropertyService.Get<bool>("Unity.Connection", false) )

                &&

                (PropertyService.Get<string>("Unity.Base.Documentation.Path", "Not Found") != "Not Found" ||
                PropertyService.Get<string>("Unity.iPhone.Documentation.Path", "Not Found") != "Not Found"))
            {
                switch (target)
                {
                    case Settings.Documentation.Manual:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Documentation.Path") +
                            Settings.LOCAL_VIEW_MANUAL_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Settings.Documentation.Reference:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Documentation.Path") +
                            Settings.LOCAL_VIEW_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Settings.Documentation.ScriptReference:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Documentation.Path") +
                            Settings.LOCAL_SEARCH_SCRIPT_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                }

            }
            else
            {
                switch (target)
                {
                    case Settings.Documentation.Manual:
                        return this.Open(Settings.REMOTE_VIEW_MANUAL_URI);
                    case Settings.Documentation.Reference:
                        return this.Open(Settings.REMOTE_VIEW_REFERENCE_URI);
                    case Settings.Documentation.ScriptReference:
                        return this.Open(Settings.REMOTE_VIEW_SCRIPT_REFERENCE_URI);
                }
            }
            return false;
        }

        public bool Open(Settings.Documentation target, string query)
        {
            // Searching
            if (query != null && query.Length > 0)
            {
                if ((PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                    !PropertyService.Get<bool>("Unity.Connection", false) )

                    &&

                    (PropertyService.Get<string>("Unity.Base.Documentation.Path", "Not Found") != "Not Found" ||
                     PropertyService.Get<string>("Unity.iPhone.Documentation.Path", "Not Found") != "Not Found"))
                {
                    switch (target)
                    {
                        case Settings.Documentation.ScriptReference:
                            return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Documentation.Path") +
                                Settings.LOCAL_SEARCH_SCRIPT_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar) +
                                System.Web.HttpUtility.UrlEncode(query));
                    }

                }
                else
                {
                    switch (target)
                    {
                        case Settings.Documentation.Manual:
                            return this.Open(Settings.REMOTE_SEARCH_MANUAL_URI + System.Web.HttpUtility.UrlEncode(query));
                        case Settings.Documentation.Reference:
                            return this.Open(Settings.REMOTE_SEARCH_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                        case Settings.Documentation.ScriptReference:
                            return this.Open(Settings.REMOTE_SEARCH_SCRIPT_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                    }
                }
            }
            return false;
        }

        public bool Open(string address)
        {
            if ( PropertyService.Get<bool>("Unity.Base.Documentation.OpenInBrowser", true))
            {
                Services.PlatformService.ShowUrl(address);
                return true;
            }
            else
            {
                if (MonoDevelop.Core.Gui.WebBrowserService.CanGetWebBrowser)
                {

                    browser = WebBrowserService.GetWebBrowser();
                    browser.LoadUrl(address);
                    return true;
                }
                else
                {
                    Helpers.ShowMessage(GettextCatalog.GetString("Missing Extension"),
                        GettextCatalog.GetString("The Mono.WebBrowser Extension (part of Mono) is required for this functionality to work. We'll set your \"Open In Browser\" setting back to open with your default browser."));
                    PropertyService.Set("Unity.Base.Documentation.OpenInBrowser", true);
                    return false;
                }

            }
        }

        #endregion
    }

}