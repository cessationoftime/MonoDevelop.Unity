//
// MonoDevelop.Unity/Base.cs
//
// Author:
//   Matthew Davey <matthew.davey@dotbunny.com>
//
// Copyright (c) 2009 dotBunny Inc. (http://www.dotbunny.com)
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Core.Gui.WebBrowser;

namespace MonoDevelop.Unity 
{
    public class Base 
	{
        private IWebBrowser browser;
		
        #region Overloaded Open

        public bool Open(Settings.Documentation target)
        {
			
			// TODO : THIS REALLLY REALLY NEEDS CLEANING UP
            if ((PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                !PropertyService.Get<bool>("Unity.Connection", false) )

                &&

                (PropertyService.Get<string>("Unity.Base.Path", "") != "" ||
                PropertyService.Get<string>("Unity.iPhone.Path", "") != ""))
            {
				if ( Helpers.WhatOS() == Helpers.OS.Mac )
				{
					switch ( PropertyService.Get<string>("Unity.DocumentationPreference") ) {
					case "Unity iPhone":
						 switch (target)
	                		{
		                    case Settings.Documentation.Manual:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.iPhone.Path") +
		                            Settings.MAC_VIEW_MANUAL);
		                    case Settings.Documentation.Reference:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.iPhone.Path") +
		                            Settings.MAC_VIEW_REFERENCE);
		                    case Settings.Documentation.ScriptReference:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.iPhone.Path") +
		                            Settings.MAC_SEARCH_SCRIPT_REFERENCE);
	                		}
						break;
					case "Unity":
					default:
						switch (target)
		                {
		                    case Settings.Documentation.Manual:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
		                            Settings.MAC_VIEW_MANUAL);
		                    case Settings.Documentation.Reference:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
		                            Settings.MAC_VIEW_REFERENCE);
		                    case Settings.Documentation.ScriptReference:
		                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
		                            Settings.MAC_SEARCH_SCRIPT_REFERENCE);
		                }
						break;
					}
	               
				}
				else if (Helpers.WhatOS() == Helpers.OS.Windows)
				{
					switch (target)
	                {
	                    case Settings.Documentation.Manual:
	                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
	                            Settings.WIN_VIEW_MANUAL);
	                    case Settings.Documentation.Reference:
	                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
	                            Settings.WIN_VIEW_REFERENCE);
	                    case Settings.Documentation.ScriptReference:
	                        return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
	                            Settings.WIN_SEARCH_SCRIPT_REFERENCE);
	                }
				}

            }
            else
            {
                switch (target)
                {
                    case Settings.Documentation.Manual:
                        return this.Open(Settings.REMOTE_VIEW_MANUAL);
                    case Settings.Documentation.Reference:
                        return this.Open(Settings.REMOTE_VIEW_REFERENCE);
                    case Settings.Documentation.ScriptReference:
                        return this.Open(Settings.REMOTE_VIEW_SCRIPT_REFERENCE);
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

                    (PropertyService.Get<string>("Unity.Base.Path", "") != "" ||
                     PropertyService.Get<string>("Unity.iPhone.Path", "") != ""))
                {
                    switch (target)
                    {
                        case Settings.Documentation.ScriptReference:
							if ( Helpers.WhatOS() == Helpers.OS.Mac )
							{
								if( PropertyService.Get<string>("Unity.DocumentationPreference") == "Unity iPhone" ) {
								return this.Open("file://" + PropertyService.Get<string>("Unity.iPhone.Path") +
                                Settings.MAC_SEARCH_SCRIPT_REFERENCE +
                                System.Web.HttpUtility.UrlEncode(query));
								}
								else{
								return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
                                Settings.MAC_SEARCH_SCRIPT_REFERENCE +
                                System.Web.HttpUtility.UrlEncode(query));
								}
							    
							}
							else if ( Helpers.WhatOS() == Helpers.OS.Windows)
							{
							    return this.Open("file://" + PropertyService.Get<string>("Unity.Base.Path") +
                                 Settings.WIN_SEARCH_SCRIPT_REFERENCE +
                                System.Web.HttpUtility.UrlEncode(query));
							}
							break;

                    }
                }
                else
                {
                    switch (target)
                    {
                        case Settings.Documentation.Manual:
                            return this.Open(Settings.REMOTE_SEARCH_MANUAL + System.Web.HttpUtility.UrlEncode(query));
                        case Settings.Documentation.Reference:
                            return this.Open(Settings.REMOTE_SEARCH_REFERENCE + System.Web.HttpUtility.UrlEncode(query));
                        case Settings.Documentation.ScriptReference:
                            return this.Open(Settings.REMOTE_SEARCH_SCRIPT_REFERENCE + System.Web.HttpUtility.UrlEncode(query));
                    }
                }
            }
            return false;
        }

        public bool Open(string address)
        {
            if ( PropertyService.Get<bool>("Unity.Base.Documentation.OpenInBrowser", true))
            {
				// TODO: 	Change back to using DesktopService.ShowUrl(address)
				// 			PlatformService was replaced with DesktopService
				// 			Windows MonoDevelop is not updated with this change yet.
				
				
			
				System.Diagnostics.Process.Start(address);
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
					MessageService.ShowMessage(GettextCatalog.GetString("Missing Extension"),
                   		GettextCatalog.GetString("The Mono.WebBrowser Extension (part of Mono) is required for this functionality to work. We'll set your \"Open In Browser\" setting back to open with your default browser."));
                    PropertyService.Set("Unity.Base.Documentation.OpenInBrowser", true);
                    return false;
                }

            }
        }

        #endregion
		
		 #region Initilization
		
		/// <summary>
		/// Attempts to find the location of Unity and record its path to the property 
		/// "Unity.Base.Path".
		/// </summary>
        public void FindUnity()
        {
            // Default Mac Install
            if(File.Exists("/Applications/Unity/Unity.app/Contents/MacOS/Unity"))
            {
                PropertyService.Set("Unity.Base.Path", "/Applications/Unity/Unity.app");

            }

            // Default x64 Windows Install
            else if(File.Exists("c:\\Program Files (x86)\\Unity\\Editor\\Unity.exe"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files (x86)\\Unity\\Editor");

            }

            // Default x86 Windows Install
        	else if(File.Exists("c:\\Program Files\\Unity\\Editor\\Unity.exe"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files\\Unity\\Editor");
            }
        }

		/// <summary>
		/// Attempts to fine the location of Unity iPhone and record its path to the 
		/// property "Unity.iPhone.Path".
		/// </summary>
        public void FindUnityiPhone()
        {
            // Default Mac Install
            if(File.Exists("/Applications/Unity iPhone/Unity iPhone.app/Contents/MacOS/Unity"))
            {
                PropertyService.Set("Unity.iPhone.Path", "/Applications/Unity iPhone/Unity iPhone.app");
            }
            else
            {
                PropertyService.Set("Unity.iPhone.Path", "");
            }
        }

        #endregion
    }

}