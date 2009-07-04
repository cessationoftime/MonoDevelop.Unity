using Gtk;

using MonoDevelop.Core;
using MonoDevelop.Core.Gui.Dialogs;

namespace MonoDevelop.Unity
{
    class PreferencesPanel : OptionsPanel
    {
        DocumentationPanelWidget widget;
        
        public override Gtk.Widget CreatePanelWidget ()
        {
            return widget = new DocumentationPanelWidget ();
        }

        public override void ApplyChanges ()
        {
            widget.Save ();
        }
    }

    partial class DocumentationPanelWidget : Gtk.Bin
    {
        CheckButton forceLocalCheckButton = new CheckButton();
        CheckButton openInBrowserCheckButton = new CheckButton();
        public DocumentationPanelWidget()
        {
            VBox vbox = new VBox();

            forceLocalCheckButton.Label = GettextCatalog.GetString("Force Local Content Only (No Internet Connection)");
            forceLocalCheckButton.Active = PropertyService.Get<bool>("Unity.ForceLocal", false);
            vbox.PackStart(forceLocalCheckButton, false, false, 0);

            openInBrowserCheckButton.Label = GettextCatalog.GetString("Open Documentation in a Web Browser");
            openInBrowserCheckButton.Active = PropertyService.Get<bool>("Unity.OpenInBrowser", true);
            vbox.PackStart(openInBrowserCheckButton, false, false, 0);

            vbox.ShowAll ();
            /*this.Build ();

            nameEntry.Text = AuthorInformation.Default.Name ?? "";
            emailEntry.Text = AuthorInformation.Default.Email ?? "";
            copyrightEntry.Text = AuthorInformation.Default.Copyright ?? "";*/
        }
        
        public void Save ()
        {
            PropertyService.Set ("Unity.ForceLocal", forceLocalCheckButton.Active);
            PropertyService.Set ("Unity.OpenInBrowser", openInBrowserCheckButton.Active);
        }
    }
}