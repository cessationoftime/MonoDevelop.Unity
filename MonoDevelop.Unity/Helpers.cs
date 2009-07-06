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



        public static void FindUnity()
        {
            // Default Mac Install
            if(FileService.IsValidPath("/Applications/Unity/Unity.app"))
            {
                PropertyService.Set("Unity.Base.Path", "/Applications/Unity");
                PropertyService.Set("Unity.OS", "OSX");
            }

            // Default x64 Windows Install
            else if(FileService.IsValidPath("c:\\Program Files (x86)\\Unity"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files (x86)\\Unity\\Editor\\Dat");
                PropertyService.Set("Unity.OS", "WIN");
            }

            // Default x86 Windows Install
            else if(FileService.IsValidPath("c:\\Program Files\\Unity"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files\\Unity\\Editor\\Data");
                PropertyService.Set("Unity.OS", "WIN");
            }
        }

        public static void FindUnityiPhone()
        {
            // Default Mac Install
            if(FileService.IsValidPath("/Applications/Unity iPhone/Unity iPhone.app"))
            {
                PropertyService.Set("Unity.iPhone.Path", "/Applications/Unity iPhone");
            }
            else
            {
                PropertyService.Set("Unity.iPhone.Path", null);
            }
        }






        public static void ShowMessage(string message)
        {
            MessageService.ShowMessage(message);
        }
        public static void ShowMessage(string title, string message)
        {
            MessageService.ShowError(title, message);
        }
        public static bool AskYesNoQuestion(string title, string question)
        {
            if ( MessageService.AskQuestion(title, question, AlertButton.Proceed, AlertButton.Cancel) == AlertButton.Proceed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DebugHook(string filePath, int lineNumber, int columnNumber)
        {
            // Load that sucker
            IdeApp.Workbench.OpenDocument(filePath, lineNumber, columnNumber, true);
        }
    }
}