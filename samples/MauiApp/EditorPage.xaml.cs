using MauiCodemirror;
using MauiCodemirror.Models;
using MauiCodemirror.Services;

namespace CodeMirrorSample;

public partial class EditorPage : ContentPage
{
    private readonly Dictionary<string, string> sampleCode = new()
    {
        ["javascript"] = @"// JavaScript Example
function fibonacci(n) {
    if (n <= 1) return n;
    return fibonacci(n - 1) + fibonacci(n - 2);
}

const result = fibonacci(10);
console.log(`Fibonacci(10) = ${result}`);

// Array operations
const numbers = [1, 2, 3, 4, 5];
const doubled = numbers.map(x => x * 2);
console.log(doubled);",

        ["typescript"] = @"// TypeScript Example
interface User {
    id: number;
    name: string;
    email: string;
}

class UserService {
    private users: User[] = [];

    addUser(user: User): void {
        this.users.push(user);
    }

    getUser(id: number): User | undefined {
        return this.users.find(u => u.id === id);
    }
}

const service = new UserService();
service.addUser({ id: 1, name: 'John Doe', email: 'john@example.com' });",

        ["python"] = @"# Python Example
def quick_sort(arr):
    if len(arr) <= 1:
        return arr
    
    pivot = arr[len(arr) // 2]
    left = [x for x in arr if x < pivot]
    middle = [x for x in arr if x == pivot]
    right = [x for x in arr if x > pivot]
    
    return quick_sort(left) + middle + quick_sort(right)

# Test the function
numbers = [3, 6, 8, 10, 1, 2, 1]
print(f'Original: {numbers}')
print(f'Sorted: {quick_sort(numbers)}')",

        ["csharp"] = @"// C# Example
using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        var sum = numbers.Sum();
        
        Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
        Console.WriteLine($"Sum: {sum}");
        
        // LINQ query syntax
        var query = from n in numbers
                    where n > 2
                    select n * n;
        
        Console.WriteLine($"Squares > 2: {string.Join(", ", query)}");
    }
}",

        ["html"] = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Sample Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Welcome to CodeMirror</h1>
        <p>This is a sample HTML document.</p>
        <button onclick=""alert('Hello!')"">Click Me</button>
    </div>
</body>
</html>"
    };

    public EditorPage()
    {
        InitializeComponent();
        LanguagePicker.SelectedIndex = 0;
        ThemePicker.SelectedIndex = 0;
        _ = InitializeEditor();
    }

    private async Task InitializeEditor()
    {
        // Set initial sample code
        await Task.Delay(500); // Wait for editor to initialize
        await OnLoadSampleClicked(null, null);
        
        // Subscribe to events
        Editor.OnContentChange += OnContentChanged;
        Editor.OnCursorActivity += OnCursorChanged;
        Editor.OnFocus += (s, e) => StatusLabel.Text = "Editor focused";
        Editor.OnBlur += (s, e) => StatusLabel.Text = "Editor blurred";
    }

    private void OnContentChanged(object sender, ContentChangeEventArgs e)
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            var value = await Editor.GetValue();
            LengthLabel.Text = $"{value?.Length ?? 0} characters";
        });
    }

    private void OnCursorChanged(object sender, CursorActivityEventArgs e)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            PositionLabel.Text = $"Line {e.Line}, Col {e.Column}";
        });
    }

    private async void OnLanguageChanged(object sender, EventArgs e)
    {
        if (LanguagePicker.SelectedItem is string language)
        {
            Editor.Language = language;
            await OnLoadSampleClicked(null, null);
        }
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem is string theme)
        {
            Editor.Theme = theme;
            
            // Update editor background based on theme
            var isDark = theme.ToLower().Contains("dark") || 
                        theme == "dracula" || 
                        theme == "monokai" || 
                        theme == "vsCode";
            
            Editor.BackgroundColor = isDark ? Color.FromHex("#282c34") : Color.FromHex("#ffffff");
        }
    }

    private void OnLineNumbersChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Editor.Options != null)
        {
            Editor.Options.LineNumbers = e.Value;
        }
    }

    private void OnReadOnlyChanged(object sender, CheckedChangedEventArgs e)
    {
        Editor.ReadOnly = e.Value;
        StatusLabel.Text = e.Value ? "Read-only mode" : "Edit mode";
    }

    private async void OnLoadSampleClicked(object sender, EventArgs e)
    {
        var language = LanguagePicker.SelectedItem as string ?? "javascript";
        
        if (sampleCode.TryGetValue(language, out var code))
        {
            await Editor.SetValue(code);
            StatusLabel.Text = $"Loaded {language} sample";
        }
    }

    private async void OnClearClicked(object sender, EventArgs e)
    {
        await Editor.SetValue("");
        StatusLabel.Text = "Editor cleared";
    }

    private async void OnGetValueClicked(object sender, EventArgs e)
    {
        var value = await Editor.GetValue();
        await DisplayAlert("Editor Content", 
            $"Length: {value.Length} characters\n\nFirst 200 chars:\n{value.Substring(0, Math.Min(200, value.Length))}...", 
            "OK");
    }

    private async void OnFormatClicked(object sender, EventArgs e)
    {
        await Editor.Format();
        StatusLabel.Text = "Code formatted";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // You can also set the value programmatically
        // await Editor.SetValue("// Hello from MAUI.CodeMirror!\nConsole.WriteLine(\"Hi\");");
        
        // Subscribe to changes (alternative to event)
        // Editor.CodeChanged += (_, code) => Console.WriteLine($"New length: {code.Length}");
    }
}