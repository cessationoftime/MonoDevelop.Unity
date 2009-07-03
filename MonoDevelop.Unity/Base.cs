using System;
using System.Collections.Generic;
using System.Text;


using MonoDevelop.Core;
using MonoDevelop.Core.Gui;

namespace MonoDevelop.Unity
{

    public class Base
    {
        #region Overloaded Open

        public bool Open(Settings.Documentation target)
        {
            // Currently Mono does not have System.Net.NetworkInformation.Ping Implemented so we have to assume connection
            if (
                PropertyService.Get<bool>("Unity.ForceLocal", false) ||
                PropertyService.Get<bool>("Unity.Connection", true) )
            {
                switch (target)
                {
                    case Settings.Documentation.Manual:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Path") +
                            Settings.LOCAL_VIEW_MANUAL_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Settings.Documentation.Reference:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Path") +
                            Settings.LOCAL_VIEW_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Settings.Documentation.ScriptReference:
                        return this.Open("file://" + PropertyService.Get<string>("Unity.Path") +
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
                if (
                    PropertyService.Get<bool>("Unity.ForceLocal", false) ||
                    PropertyService.Get<bool>("Unity.Connection", true) )
                {
                    switch (target)
                    {
                        case Settings.Documentation.ScriptReference:
                            return this.Open("file://" + PropertyService.Get<string>("Unity.Path") +
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
            if ( PropertyService.Get<bool>("Unity.OpenInBrowser", true))
            {
                Services.PlatformService.ShowUrl(address);
                return true;
            }
            return false;
        }

        #endregion
    }

}