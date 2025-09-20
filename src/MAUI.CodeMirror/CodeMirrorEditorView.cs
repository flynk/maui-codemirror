using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace Flynk.Apps.Maui.Codemirror
{
    public class CodeMirrorEditorView : Grid
    {
        public static readonly BindableProperty CodeProperty = BindableProperty.Create(
            nameof(Code),
            typeof(string),
            typeof(CodeMirrorEditorView),
            string.Empty,
            propertyChanged: OnCodePropertyChanged);

        public static readonly BindableProperty LanguageProperty = BindableProperty.Create(
            nameof(Language),
            typeof(string),
            typeof(CodeMirrorEditorView),
            "javascript",
            propertyChanged: OnLanguagePropertyChanged);

        public static readonly BindableProperty ThemeProperty = BindableProperty.Create(
            nameof(Theme),
            typeof(string),
            typeof(CodeMirrorEditorView),
            "dracula",
            propertyChanged: OnThemePropertyChanged);

        public static readonly BindableProperty ReadOnlyProperty = BindableProperty.Create(
            nameof(ReadOnly),
            typeof(bool),
            typeof(CodeMirrorEditorView),
            false,
            propertyChanged: OnReadOnlyPropertyChanged);

        public string Code
        {
            get => (string)GetValue(CodeProperty);
            set => SetValue(CodeProperty, value);
        }

        public string Language
        {
            get => (string)GetValue(LanguageProperty);
            set => SetValue(LanguageProperty, value);
        }

        public string Theme
        {
            get => (string)GetValue(ThemeProperty);
            set => SetValue(ThemeProperty, value);
        }

        public bool ReadOnly
        {
            get => (bool)GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        public event EventHandler<string> CodeChanged;

        protected WebView _webView;
        protected ActivityIndicator _loadingIndicator;
        protected Label _loadingLabel;
        protected Grid _loadingOverlay;
        protected string _cachedCode;
        protected string _cachedLanguage;
        protected string _cachedTheme;
        protected bool _isInitialized = false;

        public CodeMirrorEditorView()
        {
            BuildUI();
            InitializeWebView();
        }

        private void BuildUI()
        {
            // Use helper to create editor with loading overlay
            var container = WebViewEditorHelper.CreateEditorWithLoadingOverlay(
                _webView = new WebView(),
                out _loadingIndicator,
                out _loadingLabel,
                out _loadingOverlay
            );

            // Add the container to this grid
            this.Children.Add(container);
        }

        private void InitializeWebView()
        {
            var html = GetCodeMirrorHtml();
            var baseUrl = "https://localhost";

            #if WINDOWS
            _webView.Source = new HtmlWebViewSource { Html = html, BaseUrl = baseUrl };
            #else
            _webView.Source = new HtmlWebViewSource { Html = html };
            #endif

            _webView.Navigated += OnNavigated;
        }

        private async void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result == WebNavigationResult.Success && !_isInitialized)
            {
                _isInitialized = true;
                Console.WriteLine("[CodeMirrorEditorView] WebView navigated successfully");

                // Initial delay for CodeMirror to load
                await Task.Delay(500);

                // Update loading label
                WebViewEditorHelper.ShowLoadingOverlay(_loadingOverlay, _loadingIndicator, _loadingLabel, "Initializing CodeMirror editor...");

                // Check if editor is ready with exponential backoff
                int delay = 100;
                bool editorReady = false;
                for (int i = 0; i < 15; i++)
                {
                    var ready = await _webView.EvaluateJavaScriptAsync("window.editorReady === true");
                    Console.WriteLine($"[CodeMirrorEditorView] Editor ready check {i}: {ready} (delay: {delay}ms)");

                    if (ready?.ToString()?.ToLower() == "true")
                    {
                        Console.WriteLine("[CodeMirrorEditorView] Editor is ready!");
                        editorReady = true;
                        break;
                    }

                    // Update loading progress
                    WebViewEditorHelper.UpdateLoadingProgress(_loadingLabel, i + 1, 15);

                    await Task.Delay(delay);
                    // Exponential backoff with max delay of 2000ms
                    delay = Math.Min(delay * 2, 2000);
                }

                if (editorReady)
                {
                    await InitializeEditor();
                    await ApplyCachedContent();

                    // Hide loading overlay
                    WebViewEditorHelper.HideLoadingOverlay(_loadingOverlay, _loadingIndicator);
                }
                else
                {
                    // Show error if editor failed to load
                    WebViewEditorHelper.ShowLoadingError(_loadingOverlay, _loadingLabel, "Failed to load CodeMirror editor");
                }

                // Get debug logs
                var logs = await _webView.EvaluateJavaScriptAsync("JSON.stringify(window.debugLog || [])");
                Console.WriteLine($"[CodeMirrorEditorView] Debug logs from JavaScript: {logs}");
            }
        }

        private async Task ApplyCachedContent()
        {
            Console.WriteLine("[CodeMirrorEditorView] Checking for cached content...");

            // Apply cached theme if exists
            if (!string.IsNullOrEmpty(_cachedTheme))
            {
                Console.WriteLine($"[CodeMirrorEditorView] Applying cached theme: {_cachedTheme}");
                var theme = GetCodeMirrorTheme(_cachedTheme);
                await _webView.EvaluateJavaScriptAsync($"window.setEditorTheme('{theme}')");
            }

            // Apply cached language if exists
            if (!string.IsNullOrEmpty(_cachedLanguage))
            {
                Console.WriteLine($"[CodeMirrorEditorView] Applying cached language: {_cachedLanguage}");
                var mode = GetCodeMirrorMode(_cachedLanguage);
                await _webView.EvaluateJavaScriptAsync($"window.setEditorMode('{mode}')");
            }

            // Apply cached code if exists
            if (!string.IsNullOrEmpty(_cachedCode))
            {
                Console.WriteLine($"[CodeMirrorEditorView] Applying cached code: {_cachedCode.Length} chars");
                await SetValueSafely(_cachedCode);
                _cachedCode = null; // Clear cache after applying
            }
        }

        protected virtual async Task InitializeEditor()
        {
            // Use idempotent setters for initial configuration
            var theme = GetCodeMirrorTheme(Theme);
            var mode = GetCodeMirrorMode(Language);

            await _webView.EvaluateJavaScriptAsync($"window.setEditorTheme('{theme}')");
            await _webView.EvaluateJavaScriptAsync($"window.setEditorMode('{mode}')");

            // Set read-only if needed
            if (ReadOnly)
            {
                await _webView.EvaluateJavaScriptAsync($@"
                    window.queueOrExecute(function() {{
                        if (window.editor) {{
                            window.editor.setOption('readOnly', true);
                        }}
                    }});
                ");
            }

            // Set the code using the safer method
            if (!string.IsNullOrEmpty(Code))
            {
                await SetValueSafely(Code);
            }
        }

        private static async void OnCodePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CodeMirrorEditorView editor)
            {
                var code = newValue as string ?? "";

                if (editor._isInitialized)
                {
                    // Editor is ready, set directly
                    await editor.SetValueSafely(code);
                }
                else
                {
                    // Editor not ready, cache for later
                    Console.WriteLine($"[CodeMirrorEditorView] Editor not ready, caching code ({code.Length} chars)");
                    editor._cachedCode = code;
                }
            }
        }

        private static async void OnLanguagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CodeMirrorEditorView editor && newValue != null)
            {
                if (editor._isInitialized)
                {
                    var mode = editor.GetCodeMirrorMode(newValue.ToString());
                    // Use idempotent mode setter
                    await editor._webView.EvaluateJavaScriptAsync($"window.setEditorMode('{mode}')");
                }
                else
                {
                    // Cache for later
                    Console.WriteLine($"[CodeMirrorEditorView] Editor not ready, caching language: {newValue}");
                    editor._cachedLanguage = newValue.ToString();
                }
            }
        }

        private static async void OnThemePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CodeMirrorEditorView editor && newValue != null)
            {
                if (editor._isInitialized)
                {
                    var theme = editor.GetCodeMirrorTheme(newValue.ToString());
                    // Use idempotent theme setter
                    await editor._webView.EvaluateJavaScriptAsync($"window.setEditorTheme('{theme}')");
                }
                else
                {
                    // Cache for later
                    Console.WriteLine($"[CodeMirrorEditorView] Editor not ready, caching theme: {newValue}");
                    editor._cachedTheme = newValue.ToString();
                }
            }
        }

        private static async void OnReadOnlyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CodeMirrorEditorView editor && editor._isInitialized)
            {
                await editor._webView.EvaluateJavaScriptAsync($"if (window.editor) {{ window.editor.setOption('readOnly', {newValue.ToString().ToLower()}); }}");
            }
        }

        public async Task<string> GetEditorValue()
        {
            if (_isInitialized)
            {
                var result = await _webView.EvaluateJavaScriptAsync("window.editor ? window.editor.getValue() : ''");
                return result?.ToString() ?? string.Empty;
            }
            return Code;
        }

        public async Task<bool> SetValueSafely(string code)
        {
            try
            {
                Console.WriteLine($"[CodeMirrorEditorView] SetValueSafely called with code length: {code?.Length ?? 0}");

                if (!_isInitialized)
                {
                    Console.WriteLine("[CodeMirrorEditorView] Editor not initialized, waiting...");
                    await Task.Delay(1000);
                }

                var cleanCode = CodeEditorHelper.CleanLambdaCode(code);
                Console.WriteLine($"[CodeMirrorEditorView] Cleaned code length: {cleanCode.Length}");

                // Method 1: Try base64 encoding
                var base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cleanCode));
                var script = $"window.setEditorValue('{base64}')";
                var result = await _webView.EvaluateJavaScriptAsync(script);
                Console.WriteLine($"[CodeMirrorEditorView] Base64 method result: {result}");

                if (result?.ToString()?.Contains("success") == true)
                {
                    return true;
                }

                // Method 2: Try direct with heavy escaping
                var escaped = System.Text.Json.JsonSerializer.Serialize(cleanCode);
                script = $"window.setEditorValueDirect({escaped})";
                result = await _webView.EvaluateJavaScriptAsync(script);
                Console.WriteLine($"[CodeMirrorEditorView] Direct method result: {result}");

                if (result?.ToString()?.Contains("success") == true)
                {
                    return true;
                }

                // Method 3: Fallback to property
                Console.WriteLine("[CodeMirrorEditorView] Falling back to property setter");
                Code = cleanCode;

                // Get debug logs
                var logs = await _webView.EvaluateJavaScriptAsync("JSON.stringify(window.debugLog || [])");
                Console.WriteLine($"[CodeMirrorEditorView] JavaScript debug logs: {logs}");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CodeMirrorEditorView] Error in SetValueSafely: {ex.Message}");
                return false;
            }
        }

        private string GetCodeMirrorMode(string language)
        {
            return language?.ToLower() switch
            {
                "javascript" or "js" => "javascript",
                "typescript" or "ts" => "text/typescript",
                "csharp" or "cs" => "text/x-csharp",
                "python" or "py" => "python",
                "html" => "htmlmixed",
                "css" => "css",
                "xml" => "xml",
                "json" => "application/json",
                "sql" => "text/x-sql",
                "markdown" or "md" => "markdown",
                "yaml" or "yml" => "yaml",
                _ => "text/plain"
            };
        }

        private string GetCodeMirrorTheme(string theme)
        {
            return theme?.ToLower() switch
            {
                "light" => "default",
                "dark" or "dracula" => "dracula",
                "monokai" => "monokai",
                "material" => "material-darker",
                _ => "dracula"
            };
        }

        private string GetCodeMirrorHtml()
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>CodeMirror Editor</title>
    <!-- CodeMirror CSS -->
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/codemirror.min.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/theme/dracula.min.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/theme/monokai.min.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/theme/material-darker.min.css"">

    <!-- Addons -->
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/fold/foldgutter.min.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/dialog/dialog.min.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/search/matchesonscrollbar.min.css"">

    <style>
        body {
            margin: 0;
            padding: 0;
            overflow: hidden;
            height: 100vh;
        }
        .CodeMirror {
            height: 100vh;
            font-family: 'Consolas', 'Monaco', 'Courier New', monospace;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <textarea id=""code""></textarea>

    <!-- CodeMirror JS -->
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/codemirror.min.js""></script>

    <!-- Language modes -->
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/javascript/javascript.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/xml/xml.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/css/css.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/htmlmixed/htmlmixed.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/python/python.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/clike/clike.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/sql/sql.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/markdown/markdown.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/mode/yaml/yaml.min.js""></script>

    <!-- Addons -->
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/edit/closebrackets.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/edit/matchbrackets.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/fold/foldcode.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/fold/foldgutter.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/fold/brace-fold.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/fold/comment-fold.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/search/searchcursor.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/search/search.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/dialog/dialog.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.65.12/addon/selection/active-line.min.js""></script>

    <script>
        // Debug logging
        window.debugLog = [];
        window.logDebug = function(msg) {
            console.log('[CodeMirror]', msg);
            window.debugLog.push(msg);
        };

        // Queue system for operations
        window.operationQueue = [];
        window.isProcessingQueue = false;
        window.currentLanguage = 'javascript';
        window.currentTheme = 'dracula';

        // Process queued operations
        window.processQueue = function() {
            if (window.isProcessingQueue || !window.editorReady || window.operationQueue.length === 0) {
                return;
            }

            window.isProcessingQueue = true;
            window.logDebug('Processing queue with ' + window.operationQueue.length + ' operations');

            while (window.operationQueue.length > 0) {
                var operation = window.operationQueue.shift();
                try {
                    operation();
                } catch (e) {
                    window.logDebug('Error processing queued operation: ' + e.toString());
                }
            }

            window.isProcessingQueue = false;
        };

        // Queue an operation or execute immediately if ready
        window.queueOrExecute = function(operation) {
            if (window.editorReady) {
                try {
                    operation();
                } catch (e) {
                    window.logDebug('Error executing operation: ' + e.toString());
                }
            } else {
                window.logDebug('Editor not ready, queuing operation');
                window.operationQueue.push(operation);
            }
        };

        // Idempotent mode setter
        window.setEditorMode = function(mode) {
            window.queueOrExecute(function() {
                if (!window.editor) return;

                var currentMode = window.editor.getOption('mode');
                if (currentMode !== mode) {
                    window.logDebug('Changing mode from ' + currentMode + ' to ' + mode);
                    window.editor.setOption('mode', mode);
                    window.currentLanguage = mode;
                } else {
                    window.logDebug('Mode already set to ' + mode + ', skipping');
                }
            });
        };

        // Idempotent theme setter
        window.setEditorTheme = function(theme) {
            window.queueOrExecute(function() {
                if (!window.editor) return;

                var currentTheme = window.editor.getOption('theme');
                if (currentTheme !== theme) {
                    window.logDebug('Changing theme from ' + currentTheme + ' to ' + theme);
                    window.editor.setOption('theme', theme);
                    window.currentTheme = theme;
                } else {
                    window.logDebug('Theme already set to ' + theme + ', skipping');
                }
            });
        };

        // Create a robust setValue function
        window.setEditorValue = function(base64Value) {
            window.logDebug('setEditorValue called with base64 length: ' + (base64Value ? base64Value.length : 0));

            var setValue = function() {
                try {
                    if (!window.editor) {
                        window.logDebug('Editor still not available');
                        return 'not-ready';
                    }

                    // Decode from base64
                    var decoded = atob(base64Value);
                    window.logDebug('Decoded length: ' + decoded.length);

                    // Convert to proper UTF-8
                    var code = decodeURIComponent(escape(decoded));
                    window.logDebug('Final code length: ' + code.length);

                    // Set the value
                    window.editor.setValue(code);
                    window.logDebug('Value set successfully');

                    return 'success';
                } catch (e) {
                    window.logDebug('Error in setValue: ' + e.toString());
                    return 'error: ' + e.toString();
                }
            };

            if (window.editorReady) {
                return setValue();
            } else {
                window.logDebug('Editor not ready, queuing setValue operation');
                window.queueOrExecute(setValue);
                return 'queued';
            }
        };

        // Alternative direct set method
        window.setEditorValueDirect = function(value) {
            try {
                window.logDebug('setEditorValueDirect called with length: ' + (value ? value.length : 0));

                if (!window.editor) {
                    window.logDebug('Editor not initialized yet (direct)');
                    window.pendingValueDirect = value;
                    return 'pending';
                }

                window.editor.setValue(value);
                window.logDebug('Direct value set successfully');
                return 'success';
            } catch (e) {
                window.logDebug('Error in setEditorValueDirect: ' + e.toString());
                return 'error: ' + e.toString();
            }
        };

        // Check if editor is truly ready
        window.isEditorReady = function() {
            return window.editor &&
                   window.editor.getValue !== undefined &&
                   window.editor.setOption !== undefined;
        };

        window.logDebug('Starting CodeMirror initialization');

        window.editor = CodeMirror.fromTextArea(document.getElementById('code'), {
            mode: 'javascript',
            theme: 'dracula',
            lineNumbers: true,
            lineWrapping: true,
            foldGutter: true,
            gutters: [""CodeMirror-linenumbers"", ""CodeMirror-foldgutter""],
            matchBrackets: true,
            autoCloseBrackets: true,
            styleActiveLine: true,
            indentUnit: 4,
            tabSize: 4,
            indentWithTabs: false,
            extraKeys: {
                ""Ctrl-Space"": ""autocomplete"",
                ""F11"": function(cm) {
                    cm.setOption(""fullScreen"", !cm.getOption(""fullScreen""));
                },
                ""Esc"": function(cm) {
                    if (cm.getOption(""fullScreen"")) cm.setOption(""fullScreen"", false);
                }
            }
        });

        window.logDebug('Editor created successfully');

        // Set up change event
        window.editor.on('change', function(cm) {
            if (window.onEditorChange) {
                window.onEditorChange(cm.getValue());
            }
        });

        // Wait a bit for the editor to fully initialize
        setTimeout(function() {
            if (window.isEditorReady()) {
                // Signal that editor is ready
                window.editorReady = true;
                window.logDebug('Editor fully initialized and ready');

                // Process any queued operations
                window.processQueue();
            } else {
                window.logDebug('Editor not fully ready, waiting more...');
                // Try again
                setTimeout(function() {
                    window.editorReady = true;
                    window.logDebug('Editor ready after extended wait');
                    window.processQueue();
                }, 500);
            }
        }, 100);

        // Ensure editor fills the viewport
        window.addEventListener('resize', function() {
            window.editor.refresh();
        });

        // Error handler
        window.onerror = function(msg, url, lineNo, columnNo, error) {
            window.logDebug('JavaScript error: ' + msg);
            return false;
        };
    </script>
</body>
</html>";
        }
    }
}