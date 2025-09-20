# MAUI.CodeMirror — A comprehensive .NET MAUI wrapper for CodeMirror 6

> Enterprise-ready code editor for .NET MAUI apps with full CodeMirror 6 API support. Build powerful code editing experiences for Android, iOS, macOS, and Windows.

<p align="left">
  <a href="#">
    <img alt="NuGet" src="https://img.shields.io/nuget/vpre/MAUI.CodeMirror.svg?label=NuGet&logo=nuget">
  </a>
  <a href="#">
    <img alt="Build" src="https://img.shields.io/github/actions/workflow/status/your-org/MAUI.CodeMirror/ci.yml?branch=main">
  </a>
  <a href="#">
    <img alt="License" src="https://img.shields.io/badge/license-MIT-blue.svg">
  </a>
</p>

---

## ✨ Why MAUI.CodeMirror?

* **Complete CodeMirror 6 Integration** - Full access to the modern CodeMirror 6 API
* **Native-feeling editor** - WebView/BlazorWebView with a clean C# API
* **20+ Language Support** - JavaScript, TypeScript, Python, C#, Java, Go, Rust, and more
* **14 Professional Themes** - Including One Dark, Dracula, GitHub Light/Dark, VS Code, and more
* **Advanced IntelliSense** - Context-aware autocomplete with custom completion providers
* **Rich Event System** - Comprehensive events for all editor interactions
* **Cross-Platform** - Works seamlessly on Android, iOS, macOS, and Windows

> This project wraps CodeMirror's JavaScript API and exposes a strongly‑typed MAUI control so you can stay in C# for most scenarios.

---

## 📦 Project Structure

```
Maui-Codemirror/
├── src/
│   ├── MAUI.CodeMirror/          # Main library
│   └── MAUI.CodeMirror.Blazor/   # Blazor components
├── samples/
│   ├── MauiApp/                  # XAML sample app
│   └── BlazorMauiApp/            # Blazor sample app
└── README.md
```

### Packages

* **MAUI.CodeMirror** — the main control for XAML/C# apps
* **MAUI.CodeMirror.Blazor** — convenience helpers for BlazorWebView apps

> NuGet packages will be published once the API stabilizes.

---

## 🧰 Requirements

* .NET **8.0** or **9.0**
* .NET MAUI **8/9 toolchain**
* Windows: **WebView2 Runtime** (auto‑installed by VS; otherwise install from Microsoft)
* iOS/macOS: **WKWebView** is used automatically

---

## 🚀 Quick Start

### Installation

```bash
# Clone the repository
git clone https://github.com/your-org/maui-codemirror.git

# Build the library
cd src/MAUI.CodeMirror
dotnet build

# Or install via NuGet (coming soon)
# dotnet add package MAUI.CodeMirror
```

### Basic Usage - XAML

```xml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:cm="clr-namespace:MauiCodemirror;assembly=MAUI.CodeMirror"
    x:Class="YourApp.EditorPage">

    <cm:CodeMirrorEditor x:Name="Editor"
                         Language="javascript"
                         Theme="oneDark" />
</ContentPage>
```

```csharp
using MauiCodemirror;
using MauiCodemirror.Models;

public partial class EditorPage : ContentPage
{
    public EditorPage()
    {
        InitializeComponent();
        InitializeEditor();
    }

    private async void InitializeEditor()
    {
        // Set initial code
        await Editor.SetValue(@"
function hello() {
    console.log('Hello, World!');
}

hello();
");

        // Subscribe to events
        Editor.OnContentChange += (s, e) =>
        {
            Console.WriteLine($"Content changed: {e.Text.Length} chars");
        };

        Editor.OnCursorActivity += (s, e) =>
        {
            Console.WriteLine($"Cursor at Line {e.Line}, Col {e.Column}");
        };
    }

    private async void OnFormatClicked(object sender, EventArgs e)
    {
        await Editor.Format();
    }
}
```

### Basic Usage - Blazor

```razor
@page "/editor"
@using MAUI.CodeMirror.Blazor

<CodeMirrorComponent @ref="editor"
                     Language="typescript"
                     Theme="githubDark"
                     LineNumbers="true"
                     @bind-Value="@codeContent" />

<button @onclick="FormatCode">Format</button>
<button @onclick="GetStatistics">Get Stats</button>

@code {
    private CodeMirrorComponent? editor;
    private string codeContent = "const x: number = 42;";

    private async Task FormatCode()
    {
        if (editor != null)
        {
            await editor.FormatAsync();
        }
    }

    private async Task GetStatistics()
    {
        if (editor != null)
        {
            var value = await editor.GetValueAsync();
            var pos = await editor.GetCursorPositionAsync();
            Console.WriteLine($"Length: {value.Length}, Position: {pos}");
        }
    }
}
```

---

## 🎨 Supported Languages

| Language | Autocomplete | Linting | Formatting | Syntax Highlighting |
|----------|-------------|---------|------------|-------------------|
| JavaScript | ✅ | ✅ | ✅ | ✅ |
| TypeScript | ✅ | ✅ | ✅ | ✅ |
| Python | ✅ | ✅ | ✅ | ✅ |
| C# | ✅ | ✅ | ✅ | ✅ |
| Java | ✅ | ✅ | ✅ | ✅ |
| C++ | ✅ | ✅ | ✅ | ✅ |
| Go | ✅ | ✅ | ✅ | ✅ |
| Rust | ✅ | ✅ | ✅ | ✅ |
| PHP | ✅ | ✅ | ✅ | ✅ |
| Ruby | ✅ | ✅ | ✅ | ✅ |
| Swift | ✅ | ✅ | ✅ | ✅ |
| Kotlin | ✅ | ✅ | ✅ | ✅ |
| HTML/XML | ✅ | ❌ | ✅ | ✅ |
| CSS/SCSS | ✅ | ✅ | ✅ | ✅ |
| JSON | ❌ | ✅ | ✅ | ✅ |
| YAML | ❌ | ✅ | ✅ | ✅ |
| SQL | ✅ | ❌ | ✅ | ✅ |
| Markdown | ❌ | ❌ | ✅ | ✅ |

---

## 🎨 Available Themes

### Dark Themes
- `oneDark` - Atom One Dark
- `dracula` - Dracula
- `githubDark` - GitHub Dark
- `monokai` - Monokai
- `solarizedDark` - Solarized Dark
- `vsCode` - VS Code Dark
- `material` - Material Theme
- `nord` - Nord
- `atomOneDark` - Atom One Dark Pro
- `gruvboxDark` - Gruvbox Dark
- `cobalt` - Cobalt

### Light Themes
- `githubLight` - GitHub Light
- `solarizedLight` - Solarized Light

---

## 📚 Advanced Features

### Custom Autocomplete

```csharp
using MauiCodemirror.Services;

var completionService = new CodeMirrorCompletionService();

// Register custom completions
completionService.RegisterCompletions("javascript", new List<CompletionOption>
{
    new() { Label = "myFunction", Type = "function", Detail = "Custom function" },
    new() { Label = "MY_CONSTANT", Type = "constant", Detail = "Custom constant" }
});

// Register dynamic completion provider
completionService.RegisterCompletionProvider("javascript", async (context, position) =>
{
    // Fetch completions from your API or service
    var completions = await FetchCompletionsAsync(context);
    return completions;
});
```

### Working with Decorations

```csharp
// Add line decorations
var decorations = new List<Decoration>
{
    new()
    {
        From = 0,
        To = 10,
        Line = new LineDecoration { Class = "highlighted-line" }
    },
    new()
    {
        From = 50,
        To = 60,
        Mark = new MarkDecoration { Class = "error-underline" }
    }
};

var decorationIds = await Editor.AddDecorations(decorations);

// Remove decorations later
await Editor.RemoveDecorations(decorationIds);
```

### Custom Commands and Key Bindings

```csharp
// Register a custom command
var command = new CodeMirrorCommand
{
    Name = "insertDate",
    Key = "Ctrl-D",
    Run = "insertCurrentDate"
};

await Editor.RegisterCommand(command);

// Execute commands
await Editor.ExecuteCommand("insertDate");
await Editor.ExecuteCommand("selectAll");
await Editor.ExecuteCommand("format");
```

### Event Handling

```csharp
// Content events
Editor.OnContentChange += (s, e) =>
{
    Console.WriteLine($"Changed: {e.From} to {e.To}, Text: {e.Text}");
};

// Selection events
Editor.OnSelectionChange += (s, e) =>
{
    Console.WriteLine($"Selection: {e.Ranges.Count} ranges");
};

// Focus events
Editor.OnFocus += (s, e) => Console.WriteLine("Editor focused");
Editor.OnBlur += (s, e) => Console.WriteLine("Editor blurred");

// Mouse events
Editor.OnMouseDown += (s, e) =>
{
    Console.WriteLine($"Click at position {e.Pos}");
};

// Key events
Editor.OnKeyDown += (s, e) =>
{
    if (e.CtrlKey && e.Key == "s")
    {
        // Handle save
        e.PreventDefault = true;
    }
};
```

---

## 🏃 Running the Samples

### XAML Sample App

```bash
cd samples/MauiApp
dotnet build
dotnet run --framework net9.0-windows
```

### Blazor Sample App

```bash
cd samples/BlazorMauiApp
dotnet build
dotnet run --framework net9.0-windows
```

---

## 📖 API Reference

### Core Methods

| Method | Description |
|--------|-------------|
| `SetValue(string)` | Set the editor content |
| `GetValue()` | Get the current content |
| `GetDocument()` | Get the full document |
| `GetLine(int)` | Get specific line content |
| `GetLineCount()` | Get total line count |
| `GetRange(int, int)` | Get text in range |
| `ReplaceRange(string, int, int)` | Replace text in range |

### Position & Selection

| Method | Description |
|--------|-------------|
| `GetCursorPosition()` | Get current cursor position |
| `SetCursorPosition(int, int)` | Set cursor to line/column |
| `GetSelection()` | Get current selection |
| `SetSelection(int, int)` | Set selection range |
| `SelectAll()` | Select all text |
| `ClearSelection()` | Clear selection |

### Navigation

| Method | Description |
|--------|-------------|
| `ScrollToLine(int)` | Scroll to specific line |
| `ScrollToCursor()` | Scroll to cursor position |
| `GotoLine(int)` | Go to specific line |
| `Focus()` | Focus the editor |
| `HasFocus()` | Check if editor has focus |

### Editing

| Method | Description |
|--------|-------------|
| `Undo()` | Undo last operation |
| `Redo()` | Redo last operation |
| `Format()` | Format the code |
| `ToggleComment()` | Toggle line comment |
| `Find(string)` | Find text |
| `Replace(string, string)` | Replace text |

---

## 🔧 Configuration Options

```csharp
var options = new CodeMirrorOptions
{
    Language = "javascript",
    Theme = "oneDark",
    LineNumbers = true,
    FoldGutter = true,
    AutoComplete = true,
    BracketMatching = true,
    HighlightActiveLine = true,
    TabSize = 4,
    IndentUnit = "  ",
    LineWrapping = false,
    ReadOnly = false,

    // Advanced options
    Lint = new LintOptions { Enabled = true },
    History = new HistoryOptions { MaxHistory = 100 },
    Autocomplete = new AutocompleteOptions
    {
        ActivateOnTyping = true,
        Icons = true
    },
    Performance = new PerformanceOptions
    {
        DebounceTime = 250,
        MaxFileSize = 10485760
    }
};

Editor.Options = options;
```

You can also provide JSON if you prefer to pass raw CM6 extension config:

```csharp
await Editor.ConfigureRawAsync("""
{
  "language": "json",
  "theme": "dracula",
  "extensions": ["folding", "lineNumbers", "search"]
}
""");
```

---

## 📁 Assets & Bundling

By default the package ships with a curated CM6 bundle as **embedded resources** to avoid CDN dependency.

**Options**

1. **Embedded (default)** — no configuration required
2. **CDN** — set `Editor.AssetMode = AssetMode.Cdn` and ensure network access
3. **Custom bundle** — tree‑shake only what you need:
   * Build with `rollup` or `esbuild`
   * Place output under `Resources/Raw/codemirror` and set `Editor.AssetPath`

---

## 📐 Performance Tips

* Prefer a **custom bundle** with only needed languages/features
* Disable heavy features (minimap, linters) for large files
* Use `ReadOnly=true` when viewing huge files to reduce event traffic
* Debounce `TextChanged` when syncing back to C#

---

## 🔌 Custom JS Integration

For custom integrations you can:

```csharp
// Call custom JS functions
await Editor.InvokeJsAsync("myCustomFunction", new { arg1 = 123 });

// Register message handlers
Editor.RegisterMessageHandler("lintResults", payload => { /* handle */ });
```

From JS send messages back with:
```javascript
window.MauiBridge.postMessage({ type: 'lintResults', data });
```

> With **BlazorWebView**, you can alternatively use standard `IJSRuntime` interop.

---

## 🛣️ Roadmap

- [ ] Diagnostics panel API (squiggles, Problems list)
- [ ] Minimap add‑on
- [ ] Diff view (two‑pane)
- [ ] Vim/Emacs keymaps
- [ ] More language packs & auto‑detect
- [ ] Virtualized very‑large‑file viewer mode
- [ ] IntelliSense providers for more languages
- [ ] Integrated terminal
- [ ] Multi-file tabbed editing

---

## 🤝 Contributing

Contributions are welcome! Please:

1. **Open an issue** to discuss major changes first
2. Follow the existing coding style and add **unit/integration tests**
3. Run `dotnet format` and ensure CI passes locally

### Development Environment

* MAUI workloads installed for your target platforms
* Node 18+ for building custom JS bundles (if you touch assets)
* `pnpm` or `npm` for JS build scripts

---

## 🔒 Security

If you discover a security issue, please **do not** open a public issue. Email the maintainer instead: `security@example.com`.

---

## 📄 License

**MIT** — see LICENSE file for details.

---

## 🙏 Acknowledgments

* [CodeMirror](https://codemirror.net/) - The amazing editor that powers this wrapper
* .NET MAUI team for the fantastic cross-platform framework

---

Built with ❤️ for the .NET MAUI community