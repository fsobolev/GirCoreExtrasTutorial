# Blueprints

[Blueprint](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/) is a markup language for GTK interfaces that can help you create UI in a faster and more elegant way than writing everything in code or using XML.

Install `blueprint-compiler` and `libadwaita-devel` (packages names can be different depending on your distribution) to get started. Then we can use the compiler in [our project](src/Tutorial.csproj):

```xml
<Target Name="PreBuild" BeforeTargets="PreBuildEvent">
    ...
    <Exec Command="blueprint-compiler batch-compile ./blueprints ./blueprints ./blueprints/*.blp" />
</Target>

<Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="rm ./blueprints/*.ui" />
</Target>

...

<Target Name="EmbedUIFiles" BeforeTargets="BeforeResGen">
    <ItemGroup>
        <EmbeddedResource Include="blueprints\*.ui" Type="Non-Resx" WithCulture="false" />
    </ItemGroup>
</Target>
```

The blueprint compiler creates `.ui` (XML) files out of `.blp` files that we are going write (well, for this tutorial we will only write one file). We are embedding produced files and to be able to use them in code.

The content of `window.blp` is relatively long so just look for it [there](src/blueprints/window.blp). Notice how all widgets and their properties we previously were setting in code are now in blueprint file. Most widgets don't have names, except of 2 (these will be our variable names in code):

* `_root` - that is our window
* `_changeStyleButton` - a button that we are interating with in the code

Take a look at [Builder.cs](src/Builder.cs). This is a static helper method that looks for a file in the assembly, reads it and creates `Gtk.Builder` object with the content of the file.

Now let's connect everything in `Window.cs`!

```csharp
[Gtk.Connect] private readonly Gtk.Button _changeStyleButton;

private Window(Gtk.Builder builder) : base(builder.GetPointer("_root"), false)
{
    builder.Connect(this);
    _changeStyleButton!.OnClicked += ChangeStyleButtonClicked;
}

public Window() : this(Builder.FromFile("window.ui"))
{
}
```

`[Gtk.Connect]` is an attribute that will connect the field to a widget defined in blueprint file.

The public constructor now does only one thing: calls the private constructor, passing `Gtk.Builder` object created with the helper method to it. Notice that the file name for the builder is `window.ui`, not `window.blp`, because this is what was created with blieprint compiler and embedded to the assembly.

The private constructor uses base (`Adw.ApplicationWindow`) constructor to which we pass the pointer retrieved from `Gtk.Builder` - this is why we needed to name our window (`_root`) in the blueprint. Then `builder.Connect` is called and now we can work with all our widgets like if we wrote everything in code.

Run the project and you will see the same result as in previous part of the tutorial.