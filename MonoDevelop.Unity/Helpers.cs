using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;

namespace MonoDevelop.Unity
{
    public class Helpers
    {
        public static string SelectedText()
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Return trimmed selection
            return editor.SelectedText.Trim();
        }

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

        public static bool HasActiveDocument()
        {
            Document doc = IdeApp.Workbench.ActiveDocument;
                return doc != null && doc.GetContent<IEditableTextBuffer> () != null;
        }

        public static void ShowMessage(string message)
        {
            MessageService.ShowMessage(message);
        }
        public static void ShowMessage(string title, string message)
        {
            MessageService.ShowError(title, message);
        }

        public static void DebugHook(string filePath, int lineNumber, int columnNumber)
        {
            // Load that sucker
            IdeApp.Workbench.OpenDocument(filePath, lineNumber, columnNumber, true);
        }
    }
}