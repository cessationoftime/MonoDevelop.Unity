using System;
using System.Collections.Generic;
using System.Text;


namespace UnityDevelop
{

    public class UnityHandler
    {
        private bool openInBrowser = true;
        /*System.Web.HttpUtility.UrlEncode(text));*/

        #region Overloaded Open

        public  bool Open(Unity.Documentation target)
        {
            if ( this.openInBrowser )
            {
                switch (target)
                {
                    case Unity.Documentation.Manual:
                        return this.Open(Unity.REMOTE_VIEW_MANUAL_URI);
                    case Unity.Documentation.Reference:
                        return this.Open(Unity.REMOTE_VIEW_REFERENCE_URI);
                    case Unity.Documentation.ScriptReference:
                        return this.Open(Unity.REMOTE_VIEW_SCRIPT_REFERENCE_URI);
                }
            }
            return false;
        }

        public  bool Open(Unity.Documentation target, string query)
        {
            // Searching
            if (query != null && query.Length > 0)
            {
                switch (target)
                {
                    case Unity.Documentation.Manual:
                        return this.Open(Unity.REMOTE_SEARCH_MANUAL_URI + System.Web.HttpUtility.UrlEncode(query));
                    case Unity.Documentation.Reference:
                        return this.Open(Unity.REMOTE_SEARCH_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                    case Unity.Documentation.ScriptReference:
                        return this.Open(Unity.REMOTE_SEARCH_SCRIPT_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                }
            }
            return false;
        }

        public bool Open(string address)
        {
            if ( this.openInBrowser )
            {
                System.Diagnostics.Process.Start(address);
                return true;
            }
            return false;
        }

        #endregion
    }

}