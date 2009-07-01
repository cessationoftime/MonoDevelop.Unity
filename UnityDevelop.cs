

using System;
using System.IO;
using System.Collections.Generic;

using MonoDevelop.Core;
using MonoDevelop.Core.AddIns;
using MonoDevelop.Core.Execution;
using MonoDevelop.Core.Gui;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;





// disable ability to compile
// auto include file types / references
// iphone specific project templates vs regular

namespace dotBunny.UnityDevelop
{
    /*
    public abstract class SearchScriptReference :  AbstractMenuCommand
    {
        public override void Run()
        {

            // Get active window:
            IWorkbenchWindow window = MonoDevelop.Gui.WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;
                
            // Check if it is a sourceeditor:
            if (window != null && window.ViewContent is SourceEditorDisplayBindingWrapper)
            {
                // Get access to the editor control
                SourceEditor editor = (SourceEditor) ((SourceEditorDisplayBindingWrapper)window.ViewContent).Control;
                string selectedText = editor.Buffer.GetSelectedText();
                                    
                if (selectedText != null && selectedText.Length > 0)
                {
                    // Create a search url string with the selected text as query:
                    string search_url = "http://unity3d.com/support/documentation/ScriptReference/30_search.html?"
                        +System.Web.HttpUtility.UrlEncode(selectedText);

                    // Open the file using a file service
                    IFileService fileService = (IFileService)MonoDevelop.Core.Services.ServiceManager.GetService(typeof(IFileService));
                    fileService.OpenFile(search_url);
                }
                        
            } // (if window != null)            
        } // Run()
    }*/
}