using System;

namespace Tutorial;

public class Window : Adw.ApplicationWindow
{
    public Window()
    {
        // Window settings
        SetDefaultSize(400, 300);
        // Main box
        var contentBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
        SetContent(contentBox);
        // Header bar
        var header = Adw.HeaderBar.New();
        contentBox.Append(header);
        var styleButton = Gtk.Button.New();
        styleButton.SetLabel("Change Style");
        styleButton.OnClicked += ChangeStyleButtonClicked;
        header.PackStart(styleButton);
        // Other content
        var box = Gtk.Box.New(Gtk.Orientation.Horizontal, 12);
        box.SetHalign(Gtk.Align.Center);
        box.SetValign(Gtk.Align.Center);
        box.SetVexpand(true);
        contentBox.Append(box);
        var icon = Gtk.Image.NewFromIconName("org.gnome.Adwaita1.Demo-symbolic");
        icon.SetIconSize(Gtk.IconSize.Large);
        box.Append(icon);
        var label = Gtk.Label.New("gir.core");
        label.AddCssClass("title-1");
        box.Append(label);
    }
    
    private void ChangeStyleButtonClicked(Gtk.Button sender, EventArgs e)
    {
        var colorScheme = ((Adw.Application)GetApplication()!).StyleManager!.GetColorScheme();
        ((Adw.Application)GetApplication()!).StyleManager!.SetColorScheme(colorScheme == Adw.ColorScheme.ForceLight ? Adw.ColorScheme.ForceDark : Adw.ColorScheme.ForceLight);
    }
}