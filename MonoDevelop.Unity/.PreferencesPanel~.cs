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

        public DocumentationPanelWidget()
        {
            this.Build();
        }

        public bool ForceLocal
        {
            get
            {
                return checkForceLocal.Active;
            }
            set
            {
                checkForceLocal.Active = value;
            }
        }
        public bool OpenInBrowser
        {
            get
            {
                return checkOpenInBrowser.Active;
            }
            set
            {
                checkOpenInBrowser.Active = value;
            }
        }
    }

    // Add dropdown to select documentation override (if you have both iphone and reg)
    // Add path to both docs

    public partial class PreferencesPanelWidget
    {

        private Gtk.VBox boxPreferences;

        private Gtk.Label labelDocumentation;
        private Gtk.CheckButton checkForceLocal;
        private Gtk.CheckButton checkOpenInBrowser;




        protected virtual void Build()
        {
            Stetic.Gui.Initialize(this);
            Stetic.BinContainer.Attach(this);

            this.Name = "MonoDevelop.Unity.DocumentationPanelWidget";

            // Create Boxes
            this.boxPreferences = new Gtk.VBox();
            this.boxPreferences.Name = "boxPreferences";
            this.boxPreferences.Spacing = 6;


            // Create Documentation Label
            this.labelDocumentation = new Gtk.Label();
            this.labelDocumentation.Name = "labelDocumentation";
            this.labelDocumentation.Xalign = 0F;
            this.labelDocumentation.Yalign = 0F;
            this.labelDocumentation.LabelProp = GettextCatalog.GetString("<b>Documentation</b>");
            this.labelDocumentation.UseMarkup = true;
            this.boxPreferences.Add(this.labelDocumentation);

            Gtk.Box.BoxChild setupLabelDocumentation = ((Gtk.Box.BoxChild)(this.boxPreferences[this.labelDocumentation]));
            setupLabelDocumentation.Position = 0;
            setupLabelDocumentation.Expand = false;
            setupLabelDocumentation.Fill = false;


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
            setupCheckForceLocal.Position = 1;
            setupCheckForceLocal.Expand = false;
            setupCheckForceLocal.Fill = false;

            this.checkOpenInBrowser = new Gtk.CheckButton();
            this.checkOpenInBrowser.Sensitive = false;
            this.checkOpenInBrowser.CanFocus = true;
            this.checkOpenInBrowser.Name = "checkOpenInBrowser";
            this.checkOpenInBrowser.Label = GettextCatalog.GetString("Open Documentation in a Web Browser");
            this.checkOpenInBrowser.Active = PropertyService.Get<bool>("Unity.Base.Documentation.OpenInBrowser", true);
            this.checkOpenInBrowser.DrawIndicator = true;
            this.checkOpenInBrowser.UseUnderline = true;
            this.boxPreferences.Add(this.checkOpenInBrowser);

            Gtk.Box.BoxChild setupCheckOpenInBrowser = ((Gtk.Box.BoxChild)(this.boxPreferences[this.checkOpenInBrowser]));
            setupCheckOpenInBrowser.Position = 2;
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
        }
    }
}