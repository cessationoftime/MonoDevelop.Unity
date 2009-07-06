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
            info.Enabled = Helpers.HasSelectedText();
        }
    }
    #endregion


}