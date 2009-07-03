using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;

using MonoDevelop.Ide.Gui;

namespace MonoDevelop.Unity
{
    public enum Commands
    {
        ViewManual,
        ViewReference,
        ViewScriptReference,
        SearchManual,
        SearchReference,
        SearchScriptReference,
    }


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
            info.Enabled = Helpers.HasSelectedText();
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
            info.Enabled = Helpers.HasSelectedText();
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
