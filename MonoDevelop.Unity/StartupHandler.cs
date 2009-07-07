//
// MonoDevelop.Unity/StartupHandler.cs
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
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;

namespace MonoDevelop.Unity
{
    public class StartupHandler : CommandHandler
    {
		private System.OperatingSystem OS;
        protected override void Run()
        {

			this.OS = System.Environment.OSVersion;
			
            // Internet Connection
            // TODO: Add pinging of something when Mono is fixed for Mac
            //       http://bugs.dotbunny.com/view.php?id=31
            PropertyService.Set("Unity.Connection", true);
			

            #region First Time Initialization

            // Find Unity
            if (PropertyService.Get<string>("Unity.Base.Path", null) == null)
            {
                Helpers.FindUnity();
            }

            // Find IPhone Unity (so far Mac Only)
            if (PropertyService.Get<string>("Unity.iPhone.Path", null) == null 
			    && OS.Platform == PlatformID.MacOSX)

            {
                Helpers.FindUnityiPhone();
            }

            #endregion
        }
    }
}