//
// MonoDevelop.Unity/Settings.cs
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

namespace MonoDevelop.Unity
{
    public static class Settings
    {
        // Handy Easy to Read Documentation Enumeration
        public enum Documentation
        {
            Manual,
            Reference,
            ScriptReference
        }

        // Remote URIs for Unity Documentation
        public const string REMOTE_VIEW_MANUAL = "http://unity3d.com/support/documentation/Manual/index.html";
        public const string REMOTE_VIEW_REFERENCE = "http://unity3d.com/support/documentation/Components/index.html";
        public const string REMOTE_VIEW_SCRIPT_REFERENCE = "http://unity3d.com/support/documentation/ScriptReference/index.html";
        public const string REMOTE_SEARCH_MANUAL = "http://www.google.ca/search?btnG=Search&aq=f&oq=&aqi=&hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FManual%2F+";
        public const string REMOTE_SEARCH_REFERENCE = "http://www.google.com/search?btnG=Search&aq=f&oq=&aqi=&hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FComponents%2F+";
        public const string REMOTE_SEARCH_SCRIPT_REFERENCE = "http://unity3d.com/support/documentation/ScriptReference/30_search.html?q=";

		// Mac Configuration
		public const string MAC_VIEW_MANUAL = "/../Documentation/Manual/index.html";
        public const string MAC_VIEW_REFERENCE = "/../Documentation/Components/index.html";
        public const string MAC_VIEW_SCRIPT_REFERENCE = "/../Documentation|ScriptReference/index.html";
        public const string MAC_SEARCH_SCRIPT_REFERENCE = "/../Documentation/ScriptReference/30_search.html?q="; 
		
		// PC Configuration
		public const string WIN_VIEW_MANUAL = "\\Data\\Documentation\\Manual\\index.html";
        public const string WIN_VIEW_REFERENCE = "\\Data\\Documentation\\Components\\index.html";
        public const string WIN_VIEW_SCRIPT_REFERENCE = "\\Data\\Documentation\\ScriptReference\\index.html";
        public const string WIN_SEARCH_SCRIPT_REFERENCE = "\\Data\\Documentation\\ScriptReference\\30_search.html?q=";	
		public const string NETWORK_PING_HOST = "google.com";
    }
}