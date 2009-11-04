//
// MonoDevelop.Unity/CommandHandlers.cs
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
using MonoDevelop.Ide.Gui;

namespace MonoDevelop.Unity
{

    #region Documentation Handlers
    public class ViewManualHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.Manual);
        }
    }
    public class ViewReferenceHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.Reference);
        }
    }
    public class ViewScriptReferenceHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.ScriptReference);
        }
    }

    public class SearchManualHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.Manual, Helpers.SelectedText());
        }
        protected override void Update(CommandInfo info)
        {
            // Disable searching of Local Manual
            if ( PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                !PropertyService.Get<bool>("Unity.Connection", false))
            {
                info.Enabled = false;
            }
            else
            {
                info.Enabled = Helpers.HasSelectedText();
            }
        }
    }
    public class SearchReferenceHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.Reference, Helpers.SelectedText());
        }
        protected override void Update(CommandInfo info)
        {
            // Disable searching of local Reference Manual
            if ( PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                !PropertyService.Get<bool>("Unity.Connection", false))
            {
                info.Enabled = false;
            }
            else
            {
                info.Enabled = Helpers.HasSelectedText();
            }
        }
    }
    public class SearchScriptReferenceHandler : CommandHandler
    {
        private Base handler;
        protected override void Run (object data)
        {
            this.handler = new Base();
            handler.Open(Settings.Documentation.ScriptReference, Helpers.SelectedText());
        }
 		protected override void Update(CommandInfo info)
        {
            // Disable searching of local Reference Manual
            if ( PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false) ||
                !PropertyService.Get<bool>("Unity.Connection", false))
            {
                info.Enabled = false;
            }
            else
            {
                info.Enabled = Helpers.HasSelectedText();
            }
        }
    }
    #endregion


}