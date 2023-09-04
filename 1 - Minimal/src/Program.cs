using System;

namespace Tutorial;

public class Program
{
    private readonly Adw.Application _application;
    private Adw.ApplicationWindow? _window;
    
    public static void Main(string[] args) => new Program().Run();

    public Program()
    {
        _application = Adw.Application.New("io.github.fsobolev.GirCoreExtrasTutorial", Gio.ApplicationFlags.DefaultFlags);
        _application.StyleManager!.SetColorScheme(Adw.ColorScheme.ForceLight);
        _application.OnActivate += OnActivate;
    }
    
    public int Run()
    {
        try
        {
            return _application.RunWithSynchronizationContext();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }
    
    private void OnActivate(Gio.Application sender, EventArgs e)
    {
        if (_window == null)
        {
            _window = new Window();
            _application.AddWindow(_window);
        }
        _window.Present();
    }
}