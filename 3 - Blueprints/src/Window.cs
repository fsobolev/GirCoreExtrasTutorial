using System;

namespace Tutorial;

public class Window : Adw.ApplicationWindow
{
    [Gtk.Connect] private readonly Gtk.Button _changeStyleButton;
    
    private Window(Gtk.Builder builder) : base(builder.GetPointer("_root"), false)
    {
        builder.Connect(this);
        _changeStyleButton!.OnClicked += ChangeStyleButtonClicked;
    }
    
    public Window() : this(Builder.FromFile("window.ui"))
    {
    }
    
    private void ChangeStyleButtonClicked(Gtk.Button sender, EventArgs e)
    {
        var colorScheme = ((Adw.Application)GetApplication()!).StyleManager!.GetColorScheme();
        ((Adw.Application)GetApplication()!).StyleManager!.SetColorScheme(colorScheme == Adw.ColorScheme.ForceLight ? Adw.ColorScheme.ForceDark : Adw.ColorScheme.ForceLight);
    }
}