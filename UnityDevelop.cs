using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Threading;

using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Components.Commands;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Text;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;
using MonoDevelop.Projects.Dom.Output;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Projects.CodeGeneration;
using MonoDevelop.Ide.Gui.Dialogs;
using MonoDevelop.Ide.FindInFiles;

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

    public class ViewManualHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            System.Diagnostics.Process.Start("http://unity3d.com/support/documentation/Manual/index.html");
        }
    }
    public class ViewReferenceHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            System.Diagnostics.Process.Start("http://unity3d.com/support/documentation/Components/index.html");
        }
    }
    public class ViewScriptReferenceHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            System.Diagnostics.Process.Start("http://unity3d.com/support/documentation/ScriptReference/index.html");
        }
    }

    public class SearchManualHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Check for selection
            if ( editor.SelectedText != null && editor.SelectedText.Length > 0 )
            {
                 // Create a search url string with the selected text as query:
                 System.Diagnostics.Process.Start("http://www.google.com/search?hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FManual%2F+"
                    + System.Web.HttpUtility.UrlEncode(editor.SelectedText));
            }
        }
    }
    public class SearchReferenceHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Check for selection
            if ( editor.SelectedText != null && editor.SelectedText.Length > 0 )
            {
                 // Create a search url string with the selected text as query:
                 System.Diagnostics.Process.Start("http://www.google.com/search?hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FComponents%2F+"
                    + System.Web.HttpUtility.UrlEncode(editor.SelectedText));
            }
        }
    }
    public class SearchScriptReferenceHandler : CommandHandler
    {
        protected override void Run (object data)
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Check for selection
            if ( editor.SelectedText != null && editor.SelectedText.Length > 0 )
            {
                 // Create a search url string with the selected text as query:
                 System.Diagnostics.Process.Start("http://unity3d.com/support/documentation/ScriptReference/30_search.html?q="
                    + System.Web.HttpUtility.UrlEncode(editor.SelectedText));
            }
        }
    }
}