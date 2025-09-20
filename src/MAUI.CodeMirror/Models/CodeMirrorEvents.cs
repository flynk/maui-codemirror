using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flynk.Apps.Maui.Codemirror.Models
{
    /// <summary>
    /// Event arguments for CodeMirror events
    /// </summary>
    
    public class ViewUpdateEventArgs : EventArgs
    {
        [JsonPropertyName("docChanged")]
        public bool DocChanged { get; set; }

        [JsonPropertyName("focusChanged")]
        public bool FocusChanged { get; set; }

        [JsonPropertyName("geometryChanged")]
        public bool GeometryChanged { get; set; }

        [JsonPropertyName("heightChanged")]
        public bool HeightChanged { get; set; }

        [JsonPropertyName("selectionSet")]
        public bool SelectionSet { get; set; }

        [JsonPropertyName("viewportChanged")]
        public bool ViewportChanged { get; set; }

        [JsonPropertyName("transactions")]
        public List<TransactionInfo> Transactions { get; set; }
    }

    public class TransactionInfo
    {
        [JsonPropertyName("newDoc")]
        public bool NewDoc { get; set; }

        [JsonPropertyName("isUserEvent")]
        public bool IsUserEvent { get; set; }

        [JsonPropertyName("effects")]
        public List<string> Effects { get; set; }

        [JsonPropertyName("scrollIntoView")]
        public bool ScrollIntoView { get; set; }
    }

    public class SelectionChangeEventArgs : EventArgs
    {
        [JsonPropertyName("ranges")]
        public List<SelectionRange> Ranges { get; set; }

        [JsonPropertyName("main")]
        public int Main { get; set; }
    }

    public class SelectionRange
    {
        [JsonPropertyName("anchor")]
        public int Anchor { get; set; }

        [JsonPropertyName("head")]
        public int Head { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("empty")]
        public bool Empty { get; set; }
    }

    public class CursorActivityEventArgs : EventArgs
    {
        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("line")]
        public int Line { get; set; }

        [JsonPropertyName("column")]
        public int Column { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }

    public class Position
    {
        [JsonPropertyName("line")]
        public int Line { get; set; }

        [JsonPropertyName("column")]
        public int Column { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }

    public class ContentChangeEventArgs : EventArgs
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("inserted")]
        public List<string> Inserted { get; set; }

        [JsonPropertyName("isUndo")]
        public bool IsUndo { get; set; }

        [JsonPropertyName("isRedo")]
        public bool IsRedo { get; set; }
    }

    public class CodeMirrorFocusEventArgs : EventArgs
    {
        [JsonPropertyName("hasFocus")]
        public bool HasFocus { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class ScrollEventArgs : EventArgs
    {
        [JsonPropertyName("left")]
        public double Left { get; set; }

        [JsonPropertyName("top")]
        public double Top { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }
    }

    public class CodeMirrorDropEventArgs : EventArgs
    {
        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("files")]
        public List<FileInfo> Files { get; set; }
    }

    public class FileInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class CompletionEventArgs : EventArgs
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("completion")]
        public CompletionInfo Completion { get; set; }
    }

    public class CompletionInfo
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("apply")]
        public string Apply { get; set; }
    }

    public class LintEventArgs : EventArgs
    {
        [JsonPropertyName("diagnostics")]
        public List<Diagnostic> Diagnostics { get; set; }
    }

    public class Diagnostic
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("renderMessage")]
        public string RenderMessage { get; set; }

        [JsonPropertyName("actions")]
        public List<DiagnosticAction> Actions { get; set; }
    }

    public class DiagnosticAction
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("apply")]
        public string Apply { get; set; }
    }

    public class FoldEventArgs : EventArgs
    {
        [JsonPropertyName("line")]
        public int Line { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("folded")]
        public bool Folded { get; set; }
    }

    public class PasteEventArgs : EventArgs
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }

    public class CommandEventArgs : EventArgs
    {
        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("args")]
        public Dictionary<string, object> Args { get; set; }

        [JsonPropertyName("preventDefault")]
        public bool PreventDefault { get; set; }
    }

    public class MouseEventArgs : EventArgs
    {
        [JsonPropertyName("button")]
        public int Button { get; set; }

        [JsonPropertyName("clientX")]
        public double ClientX { get; set; }

        [JsonPropertyName("clientY")]
        public double ClientY { get; set; }

        [JsonPropertyName("pos")]
        public int Pos { get; set; }

        [JsonPropertyName("modifiers")]
        public MouseModifiers Modifiers { get; set; }
    }

    public class MouseModifiers
    {
        [JsonPropertyName("ctrlKey")]
        public bool CtrlKey { get; set; }

        [JsonPropertyName("shiftKey")]
        public bool ShiftKey { get; set; }

        [JsonPropertyName("altKey")]
        public bool AltKey { get; set; }

        [JsonPropertyName("metaKey")]
        public bool MetaKey { get; set; }
    }

    public class KeyEventArgs : EventArgs
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("keyCode")]
        public int KeyCode { get; set; }

        [JsonPropertyName("ctrlKey")]
        public bool CtrlKey { get; set; }

        [JsonPropertyName("shiftKey")]
        public bool ShiftKey { get; set; }

        [JsonPropertyName("altKey")]
        public bool AltKey { get; set; }

        [JsonPropertyName("metaKey")]
        public bool MetaKey { get; set; }

        [JsonPropertyName("preventDefault")]
        public bool PreventDefault { get; set; }
    }
}