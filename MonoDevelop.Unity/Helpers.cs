//
// MonoDevelop.Unity/Helpers.cs
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
using MonoDevelop.Ide.Gui.Content;

namespace MonoDevelop.Unity
{
    public class Helpers
    {

        #region Initilization
		
		/// <summary>
		/// Attempts to find the location of Unity and record its path to the property 
		/// "Unity.Base.Path".
		/// </summary>
        public static void FindUnity()
        {
            // Default Mac Install
            if(FileService.IsValidPath("/Applications/Unity/Unity.app"))
            {
                PropertyService.Set("Unity.Base.Path", "/Applications/Unity");

            }

            // Default x64 Windows Install
            else if(FileService.IsValidPath("c:\\Program Files (x86)\\Unity"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files (x86)\\Unity\\Editor");
				PropertyService.Set("Unity.Base.Documentation.Path", "\\Data");

            }

            // Default x86 Windows Install
            else if(FileService.IsValidPath("c:\\Program Files\\Unity"))
            {
                PropertyService.Set("Unity.Base.Path", "c:\\Program Files\\Unity\\Editor");
				PropertyService.Set("Unity.Base.Documentation.Path", "\\Data");
            }
        }

		/// <summary>
		/// Attempts to fine the location of Unity iPhone and record its path to the 
		/// property "Unity.iPhone.Path".
		/// </summary>
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

        #endregion

		/// <summary>
		/// Returns  the currently selected text within the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string SelectedText()
        {
            // Get active documents text buffer
            IEditableTextBuffer editor = IdeApp.Workbench.ActiveDocument.GetContent <IEditableTextBuffer>();

            // Return trimmed selection
            return editor.SelectedText.Trim();
        }

		/// <summary>
		/// Is there currently selected text within the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
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

		/// <summary>
		/// Is there an active document in the current IDE content window.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool HasActiveDocument()
        {
            Document doc = IdeApp.Workbench.ActiveDocument;
            return doc != null && doc.GetContent<IEditableTextBuffer> () != null;
        }



        #region Alert/Error Handling

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

        #endregion

        public static void DebugHook(string filePath, int lineNumber, int columnNumber)
        {
            // Load that sucker
            IdeApp.Workbench.OpenDocument(filePath, lineNumber, columnNumber, true);
        }
    }
}