# MAUI.CodeMirror — A .NET MAUI wrapper for the CodeMirror editor

> Rich, fast, and extensible code editing in your .NET MAUI apps (Android · iOS · macOS · Windows). Powered by **CodeMirror 6**.

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

* **Native-feeling editor** inside MAUI via WebView/BlazorWebView with a clean C# API.
* **CodeMirror 6** under the hood: modern architecture, great performance, and a huge extension ecosystem.
* **Cross‑platform**: Android, iOS, macOS (Catalyst), and Windows (WebView2).
* **Feature‑rich**: themes, language packs, syntax highlighting, autocomplete, search/replace, line numbers, read‑only, folding, linting, formatting hooks, and more.

> This project wraps CodeMirror’s JavaScript API and exposes a strongly‑typed MAUI control so you can stay in C# for most scenarios.

---

## 📦 Packages

* **MAUI.CodeMirror** — the main control for XAML/C# apps.
* **MAUI.CodeMirror.Blazor** *(optional)* — convenience helpers for `BlazorWebView` apps.

> NuGet packages will be published once the API stabilizes.

---

## 🧰 Requirements

* .NET **8.0** or **9.0**
* .NET MAUI **8/9 toolchain**
* Windows: **WebView2 Runtime** (auto‑installed by VS; otherwise install from Microsoft)
* iOS/macOS: **WKWebView** is used automatically

---

## 🚀 Getting Started

### 1) Install

```powershell
# coming soon
# dotnet add package MAUI.CodeMirror
```

### 2) Add the control

You can host CodeMirror using either **WebView** or **BlazorWebView**. Both options are supported.

#### Option A — XAML + WebView (recommended minimal setup)

```xml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:cm="clr-namespace:MAUI.CodeMirror;assembly=MAUI.CodeMirror"
    x:Class="Demo.EditorPage">

    <cm:CodeMirrorView x:Name="Editor"
                       HeightRequest="600"
                       Language="csharp"
                       Theme="oneDark"
                       ShowLineNumbers="True"
                       AutoComplete="True" />
</ContentPage>
```

```csharp
protected override async void OnAppearing()
{
    base.OnAppearing();
    await Editor.SetValueAsync("// Hello from MAUI.CodeMirror!\nConsole.WriteLine(\"Hi\");");
    // Subscribe to changes
    Editor.TextChanged += (_, e) => Console.WriteLine($"New length: {e.Text.Length}");
}
```

#### Option B — BlazorWebView

```razor
@page "/editor"
@using MAUI.CodeMirror.Blazor

<CodeMirrorComponent @ref="editor"
                     Language="typescript"
                     Theme="githubDark" />

@code {
    private CodeMirrorComponent? editor;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && editor is not null)
        {
            await editor.SetValueAsync("const x: number = 42;\n");
        }
    }
}
```

> Under the hood we load CodeMirror’s JS/CSS from embedded assets by default. You can switch to CDN or ship a custom bundle; see **Assets & Bundling** below.

---

## 🧩 Features

* **Languages**: JS/TS, JSON, HTML, CSS, C#, Java, Python, Go, SQL, Markdown, YAML, and more via CM6 language packages.
* **Themes**: One Dark, Dracula, GitHub Light/Dark, Solarized, … or your own.
* **Editing**: multi‑cursor, selections, undo/redo, indent guides, bracket matching.
* **Navigation**: minimap (optional add‑on), go‑to‑line, search/replace panel.
* **UX**: read‑only, word wrap, line numbers, rulers, highlight active line, gutter decorations.
* **Folding & Outline**: fold regions, basic outline (via language support).
* **Linting**: hook your own analyzers, or wire to ESLint, prettier, etc.
* **Autocomplete**: built‑in or custom providers from C#.
* **Formatting**: call into Prettier/your formatter; expose as `FormatAsync()`.
* **Clipboard & Drag‑Drop**: optional integrations.

> Availability depends on which extensions you enable in your config.

---

## 🧪 Quick API Tour

### Properties

```csharp
Editor.Language        // string (e.g. "csharp", "typescript")
Editor.Theme           // string (e.g. "oneDark")
Editor.ReadOnly        // bool
Editor.ShowLineNumbers // bool
Editor.WordWrap        // bool
Editor.TabSize         // int
Editor.AutoComplete    // bool
Editor.Value           // string (getter caches via last sync)
```

### Methods

```csharp
Task SetValueAsync(string text);
Task<string> GetValueAsync();
Task SetSelectionAsync(int from, int to);
Task<(int from, int to)> GetSelectionAsync();
Task SetOptionAsync(string key, object? value);  // advanced
Task FocusAsync();
Task FormatAsync();
Task ScrollToLineAsync(int line, int column = 1);
```

### Events

```csharp
Editor.TextChanged += (s, e) => { string newText = e.Text; };
Editor.CursorMoved += (s, e) => { int line = e.Line; int column = e.Column; };
Editor.SelectionChanged += ...;
Editor.Ready += ...; // fired after the webview & cm are initialized
```

---

## ⚙️ Configuration

Configuration is provided via attached properties and a `CodeMirrorOptions` object.

```csharp
var options = new CodeMirrorOptions
{
    Language = "csharp",
    Theme = "oneDark",
    TabSize = 4,
    ReadOnly = false,
    WordWrap = true,
    Extensions = new[] { CodeMirrorExtensions.Folding, CodeMirrorExtensions.SearchPanel }
};
await Editor.ConfigureAsync(options);
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

## 🔌 Calling JS and Receiving Events

Internally the control uses platform WebViews:

* **Windows**: WebView2 `CoreWebView2.ExecuteScriptAsync`
* **iOS/macOS**: `WKWebView.EvaluateJavaScript`
* **Android**: `WebView.EvaluateJavascript`

For custom integrations you can:

```csharp
await Editor.InvokeJsAsync("myCustomFunction", new { arg1 = 123 });
Editor.RegisterMessageHandler("lintResults", payload => { /* handle */ });
```

…and from JS send messages back with `window.MauiBridge.postMessage({ type: 'lintResults', data })`.

> With **BlazorWebView**, you can alternatively use standard `IJSRuntime` interop through the Blazor wrapper.

---

## 📁 Assets & Bundling

By default the package ships with a curated CM6 bundle as **embedded resources** to avoid CDN dependency.

**Options**

1. **Embedded (default)** — no configuration required.
2. **CDN** — set `Editor.AssetMode = AssetMode.Cdn` and make sure your app allows external network access.
3. **Custom bundle** — tree‑shake only what you need:

   * Build with `rollup` or `esbuild`.
   * Place output under `Resources/Raw/codemirror` (or any folder) and set `Editor.AssetPath` accordingly.

---

## 📐 Large Files & Performance Tips

* Prefer a **custom bundle** with only the languages/features you need.
* Disable features you don’t use (minimap, heavy linters) for very large files.
* Use `ReadOnly=true` when viewing huge files to reduce event traffic.
* Debounce `TextChanged` when syncing back to C#.

---

## 🧭 Samples

* **samples/MauiApp** — XAML + CodeMirrorView
* **samples/BlazorMauiApp** — BlazorWebView + CodeMirrorComponent

Run locally:

```bash
# from repo root
dotnet workload restore
dotnet build
```

Open the sample app project for your platform and run.

---

## 🛣️ Roadmap

* [ ] Diagnostics panel API (squiggles, Problems list)
* [ ] Minimap add‑on
* [ ] Diff view (two‑pane)
* [ ] Vim/Emacs keymaps
* [ ] More language packs & auto‑detect
* [ ] Virtualized very‑large‑file viewer mode

Have a feature request? File an issue!

---

## 🤝 Contributing

Contributions are welcome! Please:

1. **Open an issue** to discuss major changes first.
2. Follow the existing coding style and add **unit/integration tests**.
3. Run `dotnet format` and ensure CI passes locally.

### Dev environment

* MAUI workloads installed for your target platforms
* Node 18+ for building custom JS bundles (if you touch assets)
* `pnpm` or `npm` for JS build scripts

Repo layout:

```
/ src
  / MAUI.CodeMirror
  / MAUI.CodeMirror.Blazor (optional)
/ samples
/ tools (build tasks, bundle scripts)
```

---

## 🔒 Security

If you discover a security issue, please **do not** open a public issue. Email the maintainer instead: `{your-email}@example.com`.

---

## 📄 License

**MIT** — see `LICENSE` file.

---

## 🙏 Acknowledgements

* [CodeMirror](https://codemirror.net/) and its maintainers
* .NET MAUI team & community

---

## 📸 Screenshots

> *Add platform screenshots here*

* Android — dark theme
* Windows — light theme
* iPadOS — split view
