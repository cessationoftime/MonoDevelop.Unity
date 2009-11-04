//
// UnityDocumentationPanel.cs
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
using System.Collections.Generic;

using Gtk;

using MonoDevelop.Core;
using MonoDevelop.Core.Gui.Dialogs;

namespace MonoDevelop.Unity
{
    public class DocumentationPanel : OptionsPanel
    {
        DocumentationPanelWidget widget;
        
        public override Widget CreatePanelWidget ()
        {
            this.widget = new DocumentationPanelWidget();
            return widget;
        }

        public override void ApplyChanges ()
        {
            this.widget.Save();
        }
    }

    public partial class DocumentationPanelWidget : Gtk.Bin
    {

        private Gtk.VBox boxPreferences;
		private Gtk.HBox boxDocumentationPreference;

        private Gtk.Label labelDocumentationPreference;
        private Gtk.ComboBox comboDocumentationPreference;
        private Gtk.CheckButton checkForceLocal;
        private Gtk.CheckButton checkOpenInBrowser;
		
		
		private Gtk.HBox boxInstall;

		public DocumentationPanelWidget()
        {
            this.Build();
        }


        protected virtual void Build()
        {
            Stetic.Gui.Initialize(this);
            Stetic.BinContainer.Attach(this);

            this.Name = "DocumentationPanelWidget";

            // Create Boxes
            this.boxPreferences = new Gtk.VBox();
            this.boxPreferences.Name = "boxPreferences";
            this.boxPreferences.Spacing = 6;
			
			this.boxDocumentationPreference = new Gtk.HBox();
			this.boxDocumentationPreference.Name = "boxDocumentationPreference";
			this.boxDocumentationPreference.Spacing = 6;
			
			this.boxInstall = new Gtk.HBox();
			this.boxInstall.Name = "boxInstall";
			this.boxInstall.Spacing = 6;

            // Documentation Preference
            this.labelDocumentationPreference = new Gtk.Label();
            this.labelDocumentationPreference.Name = "labelDocumentationPreference";
            this.labelDocumentationPreference.Xalign = 0F;
            this.labelDocumentationPreference.Yalign = 0F;
            this.labelDocumentationPreference.LabelProp = GettextCatalog.GetString("Documentation Preference");
            this.boxPreferences.Add(this.labelDocumentationPreference);

            Gtk.Box.BoxChild setupLabelDocumentationPreference = ((Gtk.Box.BoxChild)(this.boxPreferences[this.labelDocumentationPreference]));
            setupLabelDocumentationPreference.Position = 0;
            setupLabelDocumentationPreference.Expand = false;
            setupLabelDocumentationPreference.Fill = false;
				
            this.comboDocumentationPreference = Gtk.ComboBox.NewText();
            this.comboDocumentationPreference.Name = "comboDocumentationPreference";
			this.comboDocumentationPreference.AppendText(GettextCatalog.GetString("Unity"));
			if ( PropertyService.Get<string>("Unity.iPhone.Path", "") != "" )
			{
            	this.comboDocumentationPreference.AppendText(GettextCatalog.GetString("Unity iPhone"));
				if ( PropertyService.Get<string>("Unity.DocumentationPreference", "Unity") == "Unity" )
	            {
	                this.comboDocumentationPreference.Active = 0;
	            }
	            else
	            {
	                this.comboDocumentationPreference.Active = 1;
	            }
			}
			else
			{
				this.comboDocumentationPreference.Sensitive = false;
			}
			this.boxDocumentationPreference.Add(this.comboDocumentationPreference);
			
			Gtk.Box.BoxChild setupComboDocumentation = ((Gtk.Box.BoxChild)(this.boxDocumentationPreference[this.comboDocumentationPreference]));
            setupComboDocumentation.Position = 0;
            setupComboDocumentation.Expand = false;
            setupComboDocumentation.Fill = false;
			
            this.boxPreferences.Add(this.boxDocumentationPreference);

            Gtk.Box.BoxChild setupBoxDocumentationPreference = ((Gtk.Box.BoxChild)(this.boxPreferences[this.boxDocumentationPreference]));
            setupBoxDocumentationPreference.Position = 1;
            setupBoxDocumentationPreference.Expand = false;
            setupBoxDocumentationPreference.Fill = false;


            // Force Local Content
            this.checkForceLocal = new Gtk.CheckButton();
            this.checkForceLocal.Label = GettextCatalog.GetString("Force Local Content Only (Offline Mode)");
            this.checkForceLocal.Active = PropertyService.Get<bool>("Unity.Base.Documentation.ForceLocal", false);
            this.checkForceLocal.CanFocus = true;
            this.checkForceLocal.Name = "checkForceLocal";
            this.checkForceLocal.DrawIndicator = true;
            this.checkForceLocal.UseUnderline = true;
            this.boxPreferences.Add(this.checkForceLocal);

            Gtk.Box.BoxChild setupCheckForceLocal = ((Gtk.Box.BoxChild)(this.boxPreferences[this.checkForceLocal]));
            setupCheckForceLocal.Position = 2;
            setupCheckForceLocal.Expand = false;
            setupCheckForceLocal.Fill = false;

            this.checkOpenInBrowser = new Gtk.CheckButton();

			if (!MonoDevelop.Core.Gui.WebBrowserService.CanGetWebBrowser)
			{
				this.checkOpenInBrowser.Sensitive = false;
			}

            this.checkOpenInBrowser.CanFocus = true;
            this.checkOpenInBrowser.Name = "checkOpenInBrowser";
            this.checkOpenInBrowser.Label = GettextCatalog.GetString("Open Documentation in a Web Browser");
            this.checkOpenInBrowser.Active = PropertyService.Get<bool>("Unity.Base.Documentation.OpenInBrowser", true);
            this.checkOpenInBrowser.DrawIndicator = true;
            this.checkOpenInBrowser.UseUnderline = true;
            this.boxPreferences.Add(this.checkOpenInBrowser);

            Gtk.Box.BoxChild setupCheckOpenInBrowser = ((Gtk.Box.BoxChild)(this.boxPreferences[this.checkOpenInBrowser]));
            setupCheckOpenInBrowser.Position = 3;
            setupCheckOpenInBrowser.Expand = false;
            setupCheckOpenInBrowser.Fill = false;

            this.Add(this.boxPreferences);


			
			
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }

            this.Show();
        }

        public void Save ()
        {
            PropertyService.Set ("Unity.Base.Documentation.ForceLocal", this.checkForceLocal.Active);
            PropertyService.Set ("Unity.Base.Documentation.OpenInBrowser", this.checkOpenInBrowser.Active);
            PropertyService.Set ("Unity.DocumentationPreference", this.comboDocumentationPreference.ActiveText);
        }
    }
}