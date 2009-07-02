using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;

using MonoDevelop.Ide.Gui;

namespace UnityDevelop
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
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.Manual);
        }
    }
    public class ViewReferenceHandler : CommandHandler
    {
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.Reference);
        }
    }
    public class ViewScriptReferenceHandler : CommandHandler
    {
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.ScriptReference);
        }
    }

    public class SearchManualHandler : CommandHandler
    {
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.Manual, Helpers.CurrentlySelectedText());
        }
    }
    public class SearchReferenceHandler : CommandHandler
    {
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.Reference, Helpers.CurrentlySelectedText());
        }
    }
    public class SearchScriptReferenceHandler : CommandHandler
    {
        private UnityHandler handler;
        protected override void Run (object data)
        {
            this.handler = new UnityHandler();
            handler.Open(Unity.Documentation.ScriptReference, Helpers.CurrentlySelectedText());
        }
    }
    #endregion

}
