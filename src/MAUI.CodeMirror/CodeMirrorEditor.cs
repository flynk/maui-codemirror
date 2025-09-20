using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Flynk.Apps.Maui.Codemirror.Models;

namespace Flynk.Apps.Maui.Codemirror
{
    /// <summary>
    /// Comprehensive CodeMirror 6 editor wrapper with full API support
    /// </summary>
    public class CodeMirrorEditor : CodeMirrorEditorView
    {
        #region Events

        // View update events
        public event EventHandler<ViewUpdateEventArgs> OnViewUpdate;
        public event EventHandler<SelectionChangeEventArgs> OnSelectionChange;
        public event EventHandler<CursorActivityEventArgs> OnCursorActivity;

        // Content events
        public event EventHandler<ContentChangeEventArgs> OnContentChange;
        public event EventHandler<ContentChangeEventArgs> BeforeChange;

        // Focus events  
        public event EventHandler<CodeMirrorFocusEventArgs> OnFocus;
        public event EventHandler<CodeMirrorFocusEventArgs> OnBlur;

        // Scroll events
        public event EventHandler<ScrollEventArgs> OnScroll;

        // Drop events
        public event EventHandler<CodeMirrorDropEventArgs> OnDrop;
        public event EventHandler<CodeMirrorDropEventArgs> OnDragOver;

        // Completion events
        public event EventHandler<CompletionEventArgs> OnCompletion;
        public event EventHandler<CompletionEventArgs> OnCompletionSelected;

        // Lint events
        public event EventHandler<LintEventArgs> OnLint;
        public event EventHandler<LintEventArgs> OnLintFixed;

        // Fold events
        public event EventHandler<FoldEventArgs> OnFold;
        public event EventHandler<FoldEventArgs> OnUnfold;

        // Command events
        public event EventHandler<CommandEventArgs> OnCommand;
        public event EventHandler<CommandEventArgs> BeforeCommand;

        // Mouse events
        public event EventHandler<MouseEventArgs> OnMouseDown;
        public event EventHandler<MouseEventArgs> OnMouseUp;
        public event EventHandler<MouseEventArgs> OnMouseMove;
        public event EventHandler<MouseEventArgs> OnContextMenu;

        // Key events
        public event EventHandler<KeyEventArgs> OnKeyDown;
        public event EventHandler<KeyEventArgs> OnKeyUp;
        public event EventHandler<KeyEventArgs> OnKeyPress;

        // Paste event
        public event EventHandler<PasteEventArgs> OnPaste;

        #endregion

        #region Properties

        private CodeMirrorOptions _options;
        private List<Decoration> _decorations = new List<Decoration>();
        private Dictionary<string, CodeMirrorCommand> _commands = new Dictionary<string, CodeMirrorCommand>();
        private Dictionary<string, KeyBinding> _keyBindings = new Dictionary<string, KeyBinding>();
        private Dictionary<string, Extension> _extensions = new Dictionary<string, Extension>();
        private List<Panel> _panels = new List<Panel>();
        private List<Tooltip> _tooltips = new List<Tooltip>();

        public CodeMirrorOptions Options
        {
            get => _options;
            set
            {
                _options = value;
                if (_isInitialized)
                {
                    _ = UpdateOptions(value);
                }
            }
        }

        public bool IsReady => _isInitialized;

        #endregion

        #region Constructor

        public CodeMirrorEditor() : base()
        {
            _options = new CodeMirrorOptions();
            InitializeEventHandlers();
        }

        public CodeMirrorEditor(CodeMirrorOptions options) : base()
        {
            _options = options ?? new CodeMirrorOptions();
            InitializeEventHandlers();
        }

        #endregion

        #region Initialization

        private void InitializeEventHandlers()
        {
            // Override base CodeChanged event to provide typed event args
            base.CodeChanged += (sender, code) =>
            {
                OnContentChange?.Invoke(this, new ContentChangeEventArgs
                {
                    Text = code
                });
            };
        }

        protected async Task InitializeEditor()
        {
            await base.InitializeEditor();

            if (_options != null)
            {
                await UpdateOptions(_options);
            }

            await RegisterEventListeners();
            await LoadExtensions();
        }

        private async Task RegisterEventListeners()
        {
            var script = @"
                if (window.cm6Editor) {
                    // Register view update listener
                    window.cm6Editor.viewUpdateListener = window.cm6Editor.view.updateListener.of((update) => {
                        window.invokeCM6Event('OnViewUpdate', {
                            docChanged: update.docChanged,
                            focusChanged: update.focusChanged,
                            geometryChanged: update.geometryChanged,
                            heightChanged: update.heightChanged,
                            selectionSet: update.selectionSet,
                            viewportChanged: update.viewportChanged
                        });
                    });

                    // Selection change listener
                    window.cm6Editor.selectionListener = window.cm6Editor.view.updateListener.of((update) => {
                        if (update.selectionSet) {
                            const selection = update.state.selection;
                            window.invokeCM6Event('OnSelectionChange', {
                                ranges: selection.ranges.map(r => ({
                                    anchor: r.anchor,
                                    head: r.head,
                                    from: r.from,
                                    to: r.to,
                                    empty: r.empty
                                })),
                                main: selection.main
                            });
                        }
                    });

                    // Content change listener
                    window.cm6Editor.contentListener = window.cm6Editor.view.updateListener.of((update) => {
                        if (update.docChanged) {
                            update.changes.iterChanges((from, to, fromB, toB, text) => {
                                window.invokeCM6Event('OnContentChange', {
                                    from: from,
                                    to: to,
                                    text: text.toString(),
                                    isUndo: update.transactions.some(t => t.isUserEvent('undo')),
                                    isRedo: update.transactions.some(t => t.isUserEvent('redo'))
                                });
                            });
                        }
                    });

                    // Focus listeners
                    window.cm6Editor.view.contentDOM.addEventListener('focus', () => {
                        window.invokeCM6Event('OnFocus', { hasFocus: true, timestamp: Date.now() });
                    });

                    window.cm6Editor.view.contentDOM.addEventListener('blur', () => {
                        window.invokeCM6Event('OnBlur', { hasFocus: false, timestamp: Date.now() });
                    });

                    // Scroll listener
                    window.cm6Editor.view.scrollDOM.addEventListener('scroll', () => {
                        const rect = window.cm6Editor.view.scrollDOM.getBoundingClientRect();
                        window.invokeCM6Event('OnScroll', {
                            left: window.cm6Editor.view.scrollDOM.scrollLeft,
                            top: window.cm6Editor.view.scrollDOM.scrollTop,
                            width: rect.width,
                            height: rect.height
                        });
                    });

                    // Mouse events
                    window.cm6Editor.view.contentDOM.addEventListener('mousedown', (e) => {
                        const pos = window.cm6Editor.view.posAtCoords({ x: e.clientX, y: e.clientY });
                        window.invokeCM6Event('OnMouseDown', {
                            button: e.button,
                            clientX: e.clientX,
                            clientY: e.clientY,
                            pos: pos,
                            modifiers: {
                                ctrlKey: e.ctrlKey,
                                shiftKey: e.shiftKey,
                                altKey: e.altKey,
                                metaKey: e.metaKey
                            }
                        });
                    });

                    // Key events
                    window.cm6Editor.view.contentDOM.addEventListener('keydown', (e) => {
                        window.invokeCM6Event('OnKeyDown', {
                            key: e.key,
                            code: e.code,
                            keyCode: e.keyCode,
                            ctrlKey: e.ctrlKey,
                            shiftKey: e.shiftKey,
                            altKey: e.altKey,
                            metaKey: e.metaKey
                        });
                    });

                    // Paste event
                    window.cm6Editor.view.contentDOM.addEventListener('paste', (e) => {
                        const text = e.clipboardData.getData('text/plain');
                        const selection = window.cm6Editor.view.state.selection;
                        window.invokeCM6Event('OnPaste', {
                            text: text,
                            from: selection.main.from,
                            to: selection.main.to,
                            source: 'clipboard'
                        });
                    });
                }

                // Define event invocation helper
                window.invokeCM6Event = function(eventName, eventData) {
                    console.log('CM6 Event:', eventName, eventData);
                    // This would be connected to C# event handlers
                };
            ";

            await _webView.EvaluateJavaScriptAsync(script);
        }

        private async Task LoadExtensions()
        {
            if (_options?.Extensions != null && _options.Extensions.Any())
            {
                foreach (var extension in _options.Extensions)
                {
                    await LoadExtension(extension);
                }
            }
        }

        #endregion

        #region Core Editor Methods

        /// <summary>
        /// Update editor options dynamically
        /// </summary>
        public async Task UpdateOptions(CodeMirrorOptions options)
        {
            if (!_isInitialized) return;

            var json = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            await _webView.EvaluateJavaScriptAsync($"window.updateCM6Options && window.updateCM6Options({json})");
        }

        /// <summary>
        /// Get the current value of the editor
        /// </summary>
        public async Task<string> GetValue()
        {
            return await GetEditorValue();
        }

        /// <summary>
        /// Set the value of the editor
        /// </summary>
        public async Task SetValue(string value)
        {
            await SetValueSafely(value);
        }

        /// <summary>
        /// Get text in a specific range
        /// </summary>
        public async Task<string> GetRange(int from, int to)
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(
                $"window.cm6Editor ? window.cm6Editor.view.state.sliceDoc({from}, {to}) : null");
            return result?.ToString();
        }

        /// <summary>
        /// Replace text in a range
        /// </summary>
        public async Task ReplaceRange(string text, int from, int to)
        {
            if (!_isInitialized) return;

            var escaped = JsonSerializer.Serialize(text);
            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor) {{
                    window.cm6Editor.view.dispatch({{
                        changes: {{from: {from}, to: {to}, insert: {escaped}}}
                    }});
                }}
            ");
        }

        /// <summary>
        /// Get the current document
        /// </summary>
        public async Task<string> GetDocument()
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(
                "window.cm6Editor ? window.cm6Editor.view.state.doc.toString() : null");
            return result?.ToString();
        }

        /// <summary>
        /// Get line content
        /// </summary>
        public async Task<string> GetLine(int line)
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(
                $"window.cm6Editor ? window.cm6Editor.view.state.doc.line({line}).text : null");
            return result?.ToString();
        }

        /// <summary>
        /// Get line count
        /// </summary>
        public async Task<int> GetLineCount()
        {
            if (!_isInitialized) return 0;

            var result = await _webView.EvaluateJavaScriptAsync(
                "window.cm6Editor ? window.cm6Editor.view.state.doc.lines : 0");
            if (int.TryParse(result?.ToString(), out var count))
            {
                return count;
            }
            return 0;
        }

        #endregion

        #region Position & Selection Methods

        /// <summary>
        /// Get the current cursor position
        /// </summary>
        public async Task<Position> GetCursorPosition()
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    const pos = window.cm6Editor.view.state.selection.main.head;
                    const line = window.cm6Editor.view.state.doc.lineAt(pos);
                    return JSON.stringify({
                        line: line.number,
                        column: pos - line.from,
                        offset: pos
                    });
                }
                return null;
            ");

            if (result != null)
            {
                return JsonSerializer.Deserialize<Position>(result.ToString());
            }
            return null;
        }

        /// <summary>
        /// Set the cursor position
        /// </summary>
        public async Task SetCursorPosition(int line, int column)
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor) {{
                    const lineInfo = window.cm6Editor.view.state.doc.line({line});
                    const pos = lineInfo.from + Math.min({column}, lineInfo.length);
                    window.cm6Editor.view.dispatch({{
                        selection: {{anchor: pos, head: pos}}
                    }});
                }}
            ");
        }

        /// <summary>
        /// Get the current selection
        /// </summary>
        public async Task<Selection> GetSelection()
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    const selection = window.cm6Editor.view.state.selection;
                    return JSON.stringify({
                        anchor: selection.main.anchor,
                        head: selection.main.head,
                        ranges: selection.ranges.map(r => ({from: r.from, to: r.to})),
                        main: selection.mainIndex
                    });
                }
                return null;
            ");

            if (result != null)
            {
                return JsonSerializer.Deserialize<Selection>(result.ToString());
            }
            return null;
        }

        /// <summary>
        /// Set the selection
        /// </summary>
        public async Task SetSelection(int from, int to)
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor) {{
                    window.cm6Editor.view.dispatch({{
                        selection: {{anchor: {from}, head: {to}}}
                    }});
                }}
            ");
        }

        /// <summary>
        /// Select all text
        /// </summary>
        public async Task SelectAll()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    window.cm6Editor.view.dispatch({
                        selection: {anchor: 0, head: window.cm6Editor.view.state.doc.length}
                    });
                }
            ");
        }

        /// <summary>
        /// Clear selection
        /// </summary>
        public async Task ClearSelection()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    const pos = window.cm6Editor.view.state.selection.main.head;
                    window.cm6Editor.view.dispatch({
                        selection: {anchor: pos, head: pos}
                    });
                }
            ");
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Scroll to a specific line
        /// </summary>
        public async Task ScrollToLine(int line)
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor) {{
                    const lineInfo = window.cm6Editor.view.state.doc.line({line});
                    window.cm6Editor.view.dispatch({{
                        scrollIntoView: true,
                        selection: {{anchor: lineInfo.from}}
                    }});
                }}
            ");
        }

        /// <summary>
        /// Scroll to cursor position
        /// </summary>
        public async Task ScrollToCursor()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    window.cm6Editor.view.dispatch({
                        scrollIntoView: true
                    });
                }
            ");
        }

        /// <summary>
        /// Go to line
        /// </summary>
        public async Task GotoLine(int line)
        {
            await SetCursorPosition(line, 0);
            await ScrollToLine(line);
        }

        #endregion

        #region Decoration Methods

        /// <summary>
        /// Add decorations to the editor
        /// </summary>
        public async Task<List<string>> AddDecorations(List<Decoration> decorations)
        {
            if (!_isInitialized) return new List<string>();

            var json = JsonSerializer.Serialize(decorations);
            var result = await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.addCM6Decorations) {{
                    return JSON.stringify(window.addCM6Decorations({json}));
                }}
                return '[]';
            ");

            if (result != null)
            {
                var ids = JsonSerializer.Deserialize<List<string>>(result.ToString());
                _decorations.AddRange(decorations);
                return ids;
            }
            return new List<string>();
        }

        /// <summary>
        /// Remove decorations
        /// </summary>
        public async Task RemoveDecorations(List<string> decorationIds)
        {
            if (!_isInitialized) return;

            var json = JsonSerializer.Serialize(decorationIds);
            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.removeCM6Decorations) {{
                    window.removeCM6Decorations({json});
                }}
            ");
        }

        /// <summary>
        /// Clear all decorations
        /// </summary>
        public async Task ClearDecorations()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor && window.clearCM6Decorations) {
                    window.clearCM6Decorations();
                }
            ");
            _decorations.Clear();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Register a command
        /// </summary>
        public async Task RegisterCommand(CodeMirrorCommand command)
        {
            if (!_isInitialized) return;

            _commands[command.Name] = command;
            var json = JsonSerializer.Serialize(command);

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.registerCM6Command) {{
                    window.registerCM6Command({json});
                }}
            ");
        }

        /// <summary>
        /// Execute a command
        /// </summary>
        public async Task<bool> ExecuteCommand(string commandName, Dictionary<string, object> args = null)
        {
            if (!_isInitialized) return false;

            var argsJson = args != null ? JsonSerializer.Serialize(args) : "{}";
            var result = await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.executeCM6Command) {{
                    return window.executeCM6Command('{commandName}', {argsJson});
                }}
                return false;
            ");

            return result?.ToString() == "true";
        }

        #endregion

        #region Extension Methods

        /// <summary>
        /// Load an extension
        /// </summary>
        public async Task LoadExtension(string extensionName)
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.loadCM6Extension) {{
                    window.loadCM6Extension('{extensionName}');
                }}
            ");
        }

        /// <summary>
        /// Unload an extension
        /// </summary>
        public async Task UnloadExtension(string extensionName)
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.unloadCM6Extension) {{
                    window.unloadCM6Extension('{extensionName}');
                }}
            ");
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Focus the editor
        /// </summary>
        public new async Task Focus()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync("window.cm6Editor && window.cm6Editor.view.focus()");
        }

        /// <summary>
        /// Check if the editor has focus
        /// </summary>
        public async Task<bool> HasFocus()
        {
            if (!_isInitialized) return false;

            var result = await _webView.EvaluateJavaScriptAsync(
                "window.cm6Editor ? window.cm6Editor.view.hasFocus : false");
            return result?.ToString() == "true";
        }

        /// <summary>
        /// Undo last operation
        /// </summary>
        public async Task Undo()
        {
            await ExecuteCommand("undo");
        }

        /// <summary>
        /// Redo last operation
        /// </summary>
        public async Task Redo()
        {
            await ExecuteCommand("redo");
        }

        /// <summary>
        /// Find text
        /// </summary>
        public async Task Find(string searchText)
        {
            if (!_isInitialized) return;

            var escaped = JsonSerializer.Serialize(searchText);
            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.openCM6SearchPanel) {{
                    window.openCM6SearchPanel({escaped});
                }}
            ");
        }

        /// <summary>
        /// Replace text
        /// </summary>
        public async Task Replace(string searchText, string replaceText)
        {
            if (!_isInitialized) return;

            var searchEscaped = JsonSerializer.Serialize(searchText);
            var replaceEscaped = JsonSerializer.Serialize(replaceText);
            await _webView.EvaluateJavaScriptAsync($@"
                if (window.cm6Editor && window.openCM6ReplacePanel) {{
                    window.openCM6ReplacePanel({searchEscaped}, {replaceEscaped});
                }}
            ");
        }

        /// <summary>
        /// Format the document
        /// </summary>
        public async Task Format()
        {
            await ExecuteCommand("format");
        }

        /// <summary>
        /// Comment/uncomment selection
        /// </summary>
        public async Task ToggleComment()
        {
            await ExecuteCommand("toggleComment");
        }

        /// <summary>
        /// Get editor statistics
        /// </summary>
        public async Task<Dictionary<string, object>> GetStatistics()
        {
            if (!_isInitialized) return null;

            var result = await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    const doc = window.cm6Editor.view.state.doc;
                    const selection = window.cm6Editor.view.state.selection;
                    return JSON.stringify({
                        lines: doc.lines,
                        characters: doc.length,
                        selections: selection.ranges.length,
                        language: window.cm6Editor.language || 'plain',
                        readOnly: window.cm6Editor.view.state.readOnly
                    });
                }
                return null;
            ");

            if (result != null)
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(result.ToString());
            }
            return null;
        }

        /// <summary>
        /// Destroy the editor
        /// </summary>
        public async Task Destroy()
        {
            if (!_isInitialized) return;

            await _webView.EvaluateJavaScriptAsync(@"
                if (window.cm6Editor) {
                    window.cm6Editor.destroy();
                    window.cm6Editor = null;
                }
            ");

            _decorations.Clear();
            _commands.Clear();
            _keyBindings.Clear();
            _extensions.Clear();
            _panels.Clear();
            _tooltips.Clear();
        }

        #endregion
    }
}