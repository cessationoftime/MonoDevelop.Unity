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
                return this.checkForceLocal.Active;
            }
            set
            {
                this.checkForceLocal.Active = value;
            }
        }
        public bool OpenInBrowser
        {
            get
            {
                return this.checkOpenInBrowser.Active;
            }
            set
            {
                this.checkOpenInBrowser.Active = value;
            }
        }
    }

    // Add dropdown to select documentation override (if you have both iphone and reg)
    // Add path to both docs

    public partial class DocumentationPanelWidget
    {

        private Gtk.VBox boxPreferences;

        private Gtk.Label labelDocumentationPreference;
        private Gtk.ComboBox comboDocumentationPreference;
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


            this.comboDocumentationPreference = new Gtk.ComboBox();
            this.comboDocumentationPreference.Name = "comboDocumentationPreference";
            this.comboDocumentationPreference.AppendText(GettextCatalog.GetString("Unity"));
            this.comboDocumentationPreference.AppendText(GettextCatalog.GetString("Unity iPhone"));
            if ( PropertyService.Get<string>("Unity.DocumentationPreference", "Unity") == "Unity" )
            {
                this.comboDocumentationPreference.Active = 0;
            }
            else
            {
                this.comboDocumentationPreference.Active = 1;
            }
            this.boxPreferences.Add(this.comboDocumentationPreference);

            Gtk.Box.BoxChild setupComboDocumentation = ((Gtk.Box.BoxChild)(this.boxPreferences[this.comboDocumentationPreference]));
            setupComboDocumentation.Position = 1;
            setupComboDocumentation.Expand = false;
            setupComboDocumentation.Fill = false;


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
            this.checkOpenInBrowser.Sensitive = false;
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