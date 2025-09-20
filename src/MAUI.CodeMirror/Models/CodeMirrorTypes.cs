using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flynk.Apps.Maui.Codemirror.Models
{
    /// <summary>
    /// Core type definitions for CodeMirror 6
    /// </summary>
    
    public class Range
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        public Range() { }

        public Range(int from, int to)
        {
            From = from;
            To = to;
        }
    }

    public class Selection
    {
        [JsonPropertyName("anchor")]
        public int Anchor { get; set; }

        [JsonPropertyName("head")]
        public int Head { get; set; }

        [JsonPropertyName("ranges")]
        public List<Range> Ranges { get; set; }

        [JsonPropertyName("main")]
        public int Main { get; set; }
    }

    public class LineInfo
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Decoration
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("inclusive")]
        public bool Inclusive { get; set; }

        [JsonPropertyName("inclusiveStart")]
        public bool InclusiveStart { get; set; }

        [JsonPropertyName("inclusiveEnd")]
        public bool InclusiveEnd { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("widget")]
        public WidgetDecoration Widget { get; set; }

        [JsonPropertyName("mark")]
        public MarkDecoration Mark { get; set; }

        [JsonPropertyName("line")]
        public LineDecoration Line { get; set; }

        [JsonPropertyName("replace")]
        public ReplaceDecoration Replace { get; set; }
    }

    public class WidgetDecoration
    {
        [JsonPropertyName("widget")]
        public string Widget { get; set; }

        [JsonPropertyName("side")]
        public int Side { get; set; }

        [JsonPropertyName("block")]
        public bool Block { get; set; }
    }

    public class MarkDecoration
    {
        [JsonPropertyName("inclusive")]
        public bool Inclusive { get; set; }

        [JsonPropertyName("inclusiveStart")]
        public bool InclusiveStart { get; set; }

        [JsonPropertyName("inclusiveEnd")]
        public bool InclusiveEnd { get; set; }

        [JsonPropertyName("attributes")]
        public Dictionary<string, string> Attributes { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("tagName")]
        public string TagName { get; set; }
    }

    public class LineDecoration
    {
        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("attributes")]
        public Dictionary<string, string> Attributes { get; set; }
    }

    public class ReplaceDecoration
    {
        [JsonPropertyName("widget")]
        public string Widget { get; set; }

        [JsonPropertyName("inclusive")]
        public bool Inclusive { get; set; }

        [JsonPropertyName("inclusiveStart")]
        public bool InclusiveStart { get; set; }

        [JsonPropertyName("inclusiveEnd")]
        public bool InclusiveEnd { get; set; }

        [JsonPropertyName("block")]
        public bool Block { get; set; }
    }

    public class StateEffect
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }

    public class StateField
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }

    public class Transaction
    {
        [JsonPropertyName("changes")]
        public List<Change> Changes { get; set; }

        [JsonPropertyName("selection")]
        public Selection Selection { get; set; }

        [JsonPropertyName("effects")]
        public List<StateEffect> Effects { get; set; }

        [JsonPropertyName("scrollIntoView")]
        public bool ScrollIntoView { get; set; }

        [JsonPropertyName("filter")]
        public bool Filter { get; set; }

        [JsonPropertyName("sequential")]
        public bool Sequential { get; set; }

        [JsonPropertyName("userEvent")]
        public string UserEvent { get; set; }
    }

    public class Change
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("insert")]
        public string Insert { get; set; }
    }

    public class Facet
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("combine")]
        public string Combine { get; set; }

        [JsonPropertyName("compareInput")]
        public string CompareInput { get; set; }

        [JsonPropertyName("compare")]
        public string Compare { get; set; }

        [JsonPropertyName("static")]
        public bool Static { get; set; }
    }

    public class Extension
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("facets")]
        public List<Facet> Facets { get; set; }

        [JsonPropertyName("precedence")]
        public string Precedence { get; set; }
    }

    public class Tooltip
    {
        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("end")]
        public int? End { get; set; }

        [JsonPropertyName("create")]
        public string Create { get; set; }

        [JsonPropertyName("above")]
        public bool Above { get; set; }

        [JsonPropertyName("arrow")]
        public bool Arrow { get; set; }
    }

    public class Panel
    {
        [JsonPropertyName("dom")]
        public string Dom { get; set; }

        [JsonPropertyName("mount")]
        public string Mount { get; set; }

        [JsonPropertyName("top")]
        public bool Top { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }
    }

    public class CodeMirrorCommand
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("preventDefault")]
        public bool PreventDefault { get; set; }

        [JsonPropertyName("stopPropagation")]
        public bool StopPropagation { get; set; }

        [JsonPropertyName("run")]
        public string Run { get; set; }

        [JsonPropertyName("shift")]
        public string Shift { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }

    public class KeyBinding
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("run")]
        public string Run { get; set; }

        [JsonPropertyName("preventDefault")]
        public bool PreventDefault { get; set; }

        [JsonPropertyName("stopPropagation")]
        public bool StopPropagation { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("shift")]
        public string Shift { get; set; }
    }

    public class Snippet
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("apply")]
        public string Apply { get; set; }

        [JsonPropertyName("boost")]
        public int? Boost { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }
    }

    public class LanguageDescription
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alias")]
        public List<string> Alias { get; set; }

        [JsonPropertyName("extensions")]
        public List<string> Extensions { get; set; }

        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("load")]
        public string Load { get; set; }

        [JsonPropertyName("support")]
        public LanguageSupport Support { get; set; }
    }

    public class LanguageSupport
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("support")]
        public List<string> Support { get; set; }

        [JsonPropertyName("languageData")]
        public LanguageData LanguageData { get; set; }
    }

    public class LanguageData
    {
        [JsonPropertyName("commentTokens")]
        public CommentTokens CommentTokens { get; set; }

        [JsonPropertyName("closeBrackets")]
        public CloseBrackets CloseBrackets { get; set; }

        [JsonPropertyName("autocomplete")]
        public string Autocomplete { get; set; }

        [JsonPropertyName("wordChars")]
        public string WordChars { get; set; }

        [JsonPropertyName("indentOnInput")]
        public bool IndentOnInput { get; set; }
    }

    public class CommentTokens
    {
        [JsonPropertyName("line")]
        public string Line { get; set; }

        [JsonPropertyName("block")]
        public BlockComment Block { get; set; }
    }

    public class BlockComment
    {
        [JsonPropertyName("open")]
        public string Open { get; set; }

        [JsonPropertyName("close")]
        public string Close { get; set; }
    }

    public class CloseBrackets
    {
        [JsonPropertyName("brackets")]
        public List<string> Brackets { get; set; }

        [JsonPropertyName("before")]
        public string Before { get; set; }
    }

    public class ThemeSpec
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dark")]
        public bool Dark { get; set; }

        [JsonPropertyName("styles")]
        public List<ThemeStyle> Styles { get; set; }
    }

    public class ThemeStyle
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonPropertyName("fontStyle")]
        public string FontStyle { get; set; }

        [JsonPropertyName("fontWeight")]
        public string FontWeight { get; set; }

        [JsonPropertyName("textDecoration")]
        public string TextDecoration { get; set; }
    }
}