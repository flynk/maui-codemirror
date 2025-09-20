using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flynk.Apps.Maui.Codemirror.Models
{
    /// <summary>
    /// Comprehensive CodeMirror 6 editor options with full API support
    /// </summary>
    public class CodeMirrorOptions
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = "javascript";

        [JsonPropertyName("theme")]
        public string Theme { get; set; } = "oneDark";

        [JsonPropertyName("placeholder")]
        public string Placeholder { get; set; }

        // Display Options
        [JsonPropertyName("lineNumbers")]
        public bool LineNumbers { get; set; } = true;

        [JsonPropertyName("highlightActiveLineGutter")]
        public bool HighlightActiveLineGutter { get; set; } = true;

        [JsonPropertyName("highlightActiveLine")]
        public bool HighlightActiveLine { get; set; } = true;

        [JsonPropertyName("highlightSelectionMatches")]
        public bool HighlightSelectionMatches { get; set; } = true;

        [JsonPropertyName("highlightSpecialChars")]
        public bool HighlightSpecialChars { get; set; } = true;

        [JsonPropertyName("drawSelection")]
        public bool DrawSelection { get; set; } = true;

        [JsonPropertyName("dropCursor")]
        public bool DropCursor { get; set; } = true;

        [JsonPropertyName("crosshairCursor")]
        public bool CrosshairCursor { get; set; } = false;

        [JsonPropertyName("rectangularSelection")]
        public bool RectangularSelection { get; set; } = false;

        // Editor Behavior
        [JsonPropertyName("readOnly")]
        public bool ReadOnly { get; set; } = false;

        [JsonPropertyName("editable")]
        public bool Editable { get; set; } = true;

        [JsonPropertyName("tabSize")]
        public int TabSize { get; set; } = 4;

        [JsonPropertyName("indentUnit")]
        public string IndentUnit { get; set; } = "  ";

        [JsonPropertyName("indentOnInput")]
        public bool IndentOnInput { get; set; } = true;

        [JsonPropertyName("bracketMatching")]
        public bool BracketMatching { get; set; } = true;

        [JsonPropertyName("closeBrackets")]
        public bool CloseBrackets { get; set; } = true;

        [JsonPropertyName("autocompletion")]
        public bool Autocompletion { get; set; } = true;

        [JsonPropertyName("lineWrapping")]
        public bool LineWrapping { get; set; } = false;

        [JsonPropertyName("foldGutter")]
        public bool FoldGutter { get; set; } = true;

        [JsonPropertyName("allowMultipleSelections")]
        public bool AllowMultipleSelections { get; set; } = true;

        // Search Options
        [JsonPropertyName("search")]
        public SearchOptions Search { get; set; } = new SearchOptions();

        // Linting Options
        [JsonPropertyName("lint")]
        public LintOptions Lint { get; set; } = new LintOptions();

        // History Options
        [JsonPropertyName("history")]
        public HistoryOptions History { get; set; } = new HistoryOptions();

        // Autocomplete Options
        [JsonPropertyName("autocomplete")]
        public AutocompleteOptions Autocomplete { get; set; } = new AutocompleteOptions();

        // Tooltip Options
        [JsonPropertyName("tooltip")]
        public TooltipOptions Tooltip { get; set; } = new TooltipOptions();

        // Scrollbar Options
        [JsonPropertyName("scrollbar")]
        public ScrollbarOptions Scrollbar { get; set; } = new ScrollbarOptions();

        // Gutter Options
        [JsonPropertyName("gutter")]
        public GutterOptions Gutter { get; set; } = new GutterOptions();

        // Performance Options
        [JsonPropertyName("performance")]
        public PerformanceOptions Performance { get; set; } = new PerformanceOptions();

        // Extensions to enable
        [JsonPropertyName("extensions")]
        public List<string> Extensions { get; set; } = new List<string>();

        // Key bindings
        [JsonPropertyName("keyBindings")]
        public string KeyBindings { get; set; } = "default"; // default, vim, emacs

        // Custom CSS class for the editor
        [JsonPropertyName("className")]
        public string ClassName { get; set; }
    }

    public class SearchOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("highlightAll")]
        public bool HighlightAll { get; set; } = true;

        [JsonPropertyName("caseSensitive")]
        public bool CaseSensitive { get; set; } = false;

        [JsonPropertyName("wholeWord")]
        public bool WholeWord { get; set; } = false;

        [JsonPropertyName("regexp")]
        public bool Regexp { get; set; } = false;

        [JsonPropertyName("replace")]
        public bool Replace { get; set; } = true;

        [JsonPropertyName("gotoLine")]
        public bool GotoLine { get; set; } = true;
    }

    public class LintOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = false;

        [JsonPropertyName("onTyping")]
        public bool OnTyping { get; set; } = true;

        [JsonPropertyName("delay")]
        public int Delay { get; set; } = 750;

        [JsonPropertyName("markerSeverity")]
        public string MarkerSeverity { get; set; } = "error"; // hint, info, warning, error

        [JsonPropertyName("tooltips")]
        public bool Tooltips { get; set; } = true;
    }

    public class HistoryOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("maxHistory")]
        public int MaxHistory { get; set; } = 100;

        [JsonPropertyName("newGroupDelay")]
        public int NewGroupDelay { get; set; } = 500;
    }

    public class AutocompleteOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("activateOnTyping")]
        public bool ActivateOnTyping { get; set; } = true;

        [JsonPropertyName("activateOnTypingDelay")]
        public int ActivateOnTypingDelay { get; set; } = 100;

        [JsonPropertyName("selectOnOpen")]
        public bool SelectOnOpen { get; set; } = true;

        [JsonPropertyName("closeOnBlur")]
        public bool CloseOnBlur { get; set; } = true;

        [JsonPropertyName("maxRenderedOptions")]
        public int MaxRenderedOptions { get; set; } = 100;

        [JsonPropertyName("defaultKeymap")]
        public bool DefaultKeymap { get; set; } = true;

        [JsonPropertyName("aboveCursor")]
        public bool AboveCursor { get; set; } = false;

        [JsonPropertyName("icons")]
        public bool Icons { get; set; } = true;

        [JsonPropertyName("addToOptions")]
        public List<CompletionOption> AddToOptions { get; set; } = new List<CompletionOption>();
    }

    public class CompletionOption
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("apply")]
        public string Apply { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("boost")]
        public int? Boost { get; set; }
    }

    public class TooltipOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("hover")]
        public bool Hover { get; set; } = true;

        [JsonPropertyName("hoverTime")]
        public int HoverTime { get; set; } = 300;

        [JsonPropertyName("above")]
        public bool Above { get; set; } = false;

        [JsonPropertyName("arrow")]
        public bool Arrow { get; set; } = true;
    }

    public class ScrollbarOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("alwaysVisible")]
        public bool AlwaysVisible { get; set; } = false;

        [JsonPropertyName("thickness")]
        public int Thickness { get; set; } = 10;

        [JsonPropertyName("scrollByPage")]
        public bool ScrollByPage { get; set; } = false;
    }

    public class GutterOptions
    {
        [JsonPropertyName("lineNumbers")]
        public bool LineNumbers { get; set; } = true;

        [JsonPropertyName("foldGutter")]
        public bool FoldGutter { get; set; } = true;

        [JsonPropertyName("highlightActiveLineGutter")]
        public bool HighlightActiveLineGutter { get; set; } = true;

        [JsonPropertyName("lineNumberFormatter")]
        public string LineNumberFormatter { get; set; }

        [JsonPropertyName("domEventHandlers")]
        public Dictionary<string, bool> DomEventHandlers { get; set; } = new Dictionary<string, bool>();

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("renderEmptyElements")]
        public bool RenderEmptyElements { get; set; } = false;

        [JsonPropertyName("elementClass")]
        public string ElementClass { get; set; }

        [JsonPropertyName("markers")]
        public List<GutterMarker> Markers { get; set; } = new List<GutterMarker>();
    }

    public class GutterMarker
    {
        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("toDOM")]
        public string ToDOM { get; set; }

        [JsonPropertyName("elementClass")]
        public string ElementClass { get; set; }
    }

    public class PerformanceOptions
    {
        [JsonPropertyName("debounceTime")]
        public int DebounceTime { get; set; } = 250;

        [JsonPropertyName("maxFileSize")]
        public int MaxFileSize { get; set; } = 10485760; // 10MB

        [JsonPropertyName("maxHighlightLength")]
        public int MaxHighlightLength { get; set; } = 100000;

        [JsonPropertyName("bigFileMode")]
        public bool BigFileMode { get; set; } = false;

        [JsonPropertyName("syntaxHighlightingThreshold")]
        public int SyntaxHighlightingThreshold { get; set; } = 20000;
    }
}