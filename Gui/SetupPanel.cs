//
// UnitySetupPanel.cs
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
using MonoDevelop.Core.Gui;
using MonoDevelop.Core.Gui.Dialogs;

namespace MonoDevelop.Unity
{
    public class SetupPanel : OptionsPanel
    {
        SetupPanelWidget widget;
        
        public override Widget CreatePanelWidget ()
        {
            this.widget = new SetupPanelWidget();
            return widget;
        }

        public override void ApplyChanges ()
        {
            this.widget.Save();
        }
    }

    public partial class SetupPanelWidget : Gtk.Bin
    {
        private Gtk.VBox boxPreferences;
		private Gtk.HBox boxReset;
		
        private Gtk.Label labelBasePath;
        private MonoDevelop.Components.FolderEntry folderBasePath;
        private Gtk.Label labeliPhonePath;
        private MonoDevelop.Components.FolderEntry folderiPhonePath;
        
		public SetupPanelWidget()
        {
            this.Build();
        }
        
		protected virtual void Build()
        {
            Stetic.Gui.Initialize(this);
            Stetic.BinContainer.Attach(this);

            this.Name = "SetupPanelWidget";

            // Create Boxes
            this.boxPreferences = new Gtk.VBox();
            this.boxPreferences.Name = "boxPreferences";
            this.boxPreferences.Spacing = 6;
			this.boxReset = new Gtk.HBox();
			this.boxReset.Name = "boxReset";
			this.boxReset.Spacing = 6;

            // Base Path
            this.labelBasePath = new Gtk.Label();
            this.labelBasePath.Name = "labelBasePath";
            this.labelBasePath.Xalign = 0F;
            this.labelBasePath.Yalign = 0F;
            this.labelBasePath.LabelProp = GettextCatalog.GetString("Unity Path");
            this.boxPreferences.Add(this.labelBasePath);

            Gtk.Box.BoxChild setupLabelBasePath = ((Gtk.Box.BoxChild)(this.boxPreferences[this.labelBasePath]));
            setupLabelBasePath.Position = 0;
            setupLabelBasePath.Expand = false;
            setupLabelBasePath.Fill = false;

            this.folderBasePath = new MonoDevelop.Components.FolderEntry();
            this.folderBasePath.Name = "folderBasePath";
            this.folderBasePath.Path = PropertyService.Get<string>("Unity.Base.Path");
            this.boxPreferences.Add(this.folderBasePath);

            Gtk.Box.BoxChild setupFolderBasePath = ((Gtk.Box.BoxChild)(this.boxPreferences[this.folderBasePath]));
            setupFolderBasePath.Position = 1;
            setupFolderBasePath.Expand = false;
            setupFolderBasePath.Fill = false;


            // iPhone Path
            this.labeliPhonePath = new Gtk.Label();
            this.labeliPhonePath.Name = "labeliPhonePath";
            this.labeliPhonePath.Xalign = 0F;
            this.labeliPhonePath.Yalign = 0F;
            this.labeliPhonePath.LabelProp = GettextCatalog.GetString("Unity iPhone Path");
            this.boxPreferences.Add(this.labeliPhonePath);

            Gtk.Box.BoxChild setupLabeliPhonePath = ((Gtk.Box.BoxChild)(this.boxPreferences[this.labeliPhonePath]));
            setupLabeliPhonePath.Position = 2;
            setupLabeliPhonePath.Expand = false;
            setupLabeliPhonePath.Fill = false;

            this.folderiPhonePath = new MonoDevelop.Components.FolderEntry();
            this.folderiPhonePath.Name = "folderiPhonePath";
            this.folderiPhonePath.Path = PropertyService.Get<string>("Unity.iPhone.Path");

            this.boxPreferences.Add(this.folderiPhonePath);



            Gtk.Box.BoxChild setupFolderiPhonePath = ((Gtk.Box.BoxChild)(this.boxPreferences[this.folderiPhonePath]));
            setupFolderiPhonePath.Position = 3;
            setupFolderiPhonePath.Expand = false;
            setupFolderiPhonePath.Fill = false;

            this.Add(this.boxPreferences);


            if ((this.Child != null)) {
                this.Child.ShowAll();
            }

            this.Show();
        }


        public void Save ()
        {
            PropertyService.Set ("Unity.Base.Path", this.folderBasePath.Path.Trim());
            PropertyService.Set ("Unity.iPhone.Path", this.folderiPhonePath.Path.Trim());
        }
    }
}