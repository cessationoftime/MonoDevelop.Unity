//
// MonoDevelop.Unity/Helpers.cs
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
using System.Reflection;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;

namespace MonoDevelop.Unity
{
    public class Helpers
    {
		public enum OS
		{
			Mac,
			Windows
		}
		public static OS WhatOS()
		{
			System.OperatingSystem OpSys;
			OpSys = System.Environment.OSVersion;
			
			// Current Mac Response
			if (OpSys.Platform == PlatformID.Unix)
			{
				return OS.Mac;
			}
			else if (OpSys.Platform == PlatformID.Win32NT || OpSys.Platform == PlatformID.Win32Windows )
			{
				return OS.Windows;
			}
			else
			{
				return OS.Mac;
			}
		}

		/// <summary>
		/// Returns  the currently selected text within the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string SelectedText()
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Return trimmed selection
            return editor.SelectedText.Trim();
        }

		/// <summary>
		/// Is there currently selected text within the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool HasSelectedText()
        {
            if ( Helpers.HasActiveDocument() )
            {
                if ( Helpers.SelectedText() != null && Helpers.SelectedText().Length > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// Is there an active document in the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool HasActiveDocument()
        {
            Document doc = IdeApp.Workbench.ActiveDocument;
            return doc != null && doc.GetContent<IEditableTextBuffer> () != null;
        }
		

		public static bool CheckConnection()
		{
			System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
			System.Net.NetworkInformation.PingOptions pingOptions = new System.Net.NetworkInformation.PingOptions();
			
			// Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            pingOptions.DontFragment = true;

			
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes (data);
            int timeout = 120;
			 
			try
			{
				System.Net.NetworkInformation.PingReply reply = pingSender.Send (Settings.NETWORK_PING_HOST, timeout, buffer, pingOptions);
				if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
	            {
					PropertyService.Set("Unity.Connection", true);
					return true;
				}
				else
				{
					PropertyService.Set("Unity.Connection", false);
					return false;
				}
			}
			catch
			{
				PropertyService.Set("Unity.Connection", true);
				return false;
			}

		}
    }
}