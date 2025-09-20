using System.Collections.Generic;
using System.Linq;
using MauiCodemirror.Models;

namespace MauiCodemirror.Services
{
    /// <summary>
    /// Service for managing CodeMirror 6 themes
    /// </summary>
    public static class CodeMirrorThemeService
    {
        private static readonly Dictionary<string, ThemeSpec> Themes = new Dictionary<string, ThemeSpec>
        {
            ["oneDark"] = new ThemeSpec
            {
                Name = "One Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#5c6370", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#c678dd" },
                    new ThemeStyle { Tag = "string", Color = "#98c379" },
                    new ThemeStyle { Tag = "number", Color = "#d19a66" },
                    new ThemeStyle { Tag = "function", Color = "#61afef" },
                    new ThemeStyle { Tag = "variable", Color = "#e06c75" },
                    new ThemeStyle { Tag = "type", Color = "#e5c07b" },
                    new ThemeStyle { Tag = "operator", Color = "#56b6c2" },
                    new ThemeStyle { Tag = "bracket", Color = "#abb2bf" },
                    new ThemeStyle { Tag = "meta", Color = "#7f848e" },
                    new ThemeStyle { Tag = "attribute", Color = "#d19a66" },
                    new ThemeStyle { Tag = "property", Color = "#e06c75" },
                    new ThemeStyle { Tag = "definition", Color = "#e5c07b" },
                    new ThemeStyle { Tag = "builtin", Color = "#e06c75" },
                    new ThemeStyle { Tag = "tag", Color = "#e06c75" },
                    new ThemeStyle { Tag = "invalid", Color = "#ffffff", BackgroundColor = "#e05252" }
                }
            },
            ["dracula"] = new ThemeSpec
            {
                Name = "Dracula",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#6272a4", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#ff79c6" },
                    new ThemeStyle { Tag = "string", Color = "#f1fa8c" },
                    new ThemeStyle { Tag = "number", Color = "#bd93f9" },
                    new ThemeStyle { Tag = "function", Color = "#50fa7b" },
                    new ThemeStyle { Tag = "variable", Color = "#f8f8f2" },
                    new ThemeStyle { Tag = "type", Color = "#8be9fd" },
                    new ThemeStyle { Tag = "operator", Color = "#ff79c6" },
                    new ThemeStyle { Tag = "bracket", Color = "#f8f8f2" },
                    new ThemeStyle { Tag = "meta", Color = "#6272a4" },
                    new ThemeStyle { Tag = "attribute", Color = "#50fa7b" },
                    new ThemeStyle { Tag = "property", Color = "#66d9ef" },
                    new ThemeStyle { Tag = "definition", Color = "#50fa7b" },
                    new ThemeStyle { Tag = "builtin", Color = "#8be9fd" },
                    new ThemeStyle { Tag = "tag", Color = "#ff79c6" },
                    new ThemeStyle { Tag = "invalid", Color = "#f8f8f2", BackgroundColor = "#ff5555" }
                }
            },
            ["githubDark"] = new ThemeSpec
            {
                Name = "GitHub Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#8b949e", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#ff7b72" },
                    new ThemeStyle { Tag = "string", Color = "#a5d6ff" },
                    new ThemeStyle { Tag = "number", Color = "#79c0ff" },
                    new ThemeStyle { Tag = "function", Color = "#d2a8ff" },
                    new ThemeStyle { Tag = "variable", Color = "#ffa657" },
                    new ThemeStyle { Tag = "type", Color = "#ffa657" },
                    new ThemeStyle { Tag = "operator", Color = "#79c0ff" },
                    new ThemeStyle { Tag = "bracket", Color = "#c9d1d9" },
                    new ThemeStyle { Tag = "meta", Color = "#8b949e" },
                    new ThemeStyle { Tag = "attribute", Color = "#79c0ff" },
                    new ThemeStyle { Tag = "property", Color = "#79c0ff" },
                    new ThemeStyle { Tag = "definition", Color = "#ffa657" },
                    new ThemeStyle { Tag = "builtin", Color = "#ffa657" },
                    new ThemeStyle { Tag = "tag", Color = "#7ee83f" },
                    new ThemeStyle { Tag = "invalid", Color = "#f85149", TextDecoration = "underline" }
                }
            },
            ["githubLight"] = new ThemeSpec
            {
                Name = "GitHub Light",
                Dark = false,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#6a737d", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#d73a49" },
                    new ThemeStyle { Tag = "string", Color = "#032f62" },
                    new ThemeStyle { Tag = "number", Color = "#005cc5" },
                    new ThemeStyle { Tag = "function", Color = "#6f42c1" },
                    new ThemeStyle { Tag = "variable", Color = "#e36209" },
                    new ThemeStyle { Tag = "type", Color = "#e36209" },
                    new ThemeStyle { Tag = "operator", Color = "#d73a49" },
                    new ThemeStyle { Tag = "bracket", Color = "#24292e" },
                    new ThemeStyle { Tag = "meta", Color = "#586069" },
                    new ThemeStyle { Tag = "attribute", Color = "#005cc5" },
                    new ThemeStyle { Tag = "property", Color = "#005cc5" },
                    new ThemeStyle { Tag = "definition", Color = "#e36209" },
                    new ThemeStyle { Tag = "builtin", Color = "#e36209" },
                    new ThemeStyle { Tag = "tag", Color = "#22863a" },
                    new ThemeStyle { Tag = "invalid", Color = "#cb2431", TextDecoration = "underline" }
                }
            },
            ["monokai"] = new ThemeSpec
            {
                Name = "Monokai",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#75715e", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#f92672" },
                    new ThemeStyle { Tag = "string", Color = "#e6db74" },
                    new ThemeStyle { Tag = "number", Color = "#ae81ff" },
                    new ThemeStyle { Tag = "function", Color = "#a6e22e" },
                    new ThemeStyle { Tag = "variable", Color = "#f8f8f2" },
                    new ThemeStyle { Tag = "type", Color = "#66d9ef" },
                    new ThemeStyle { Tag = "operator", Color = "#f92672" },
                    new ThemeStyle { Tag = "bracket", Color = "#f8f8f2" },
                    new ThemeStyle { Tag = "meta", Color = "#75715e" },
                    new ThemeStyle { Tag = "attribute", Color = "#a6e22e" },
                    new ThemeStyle { Tag = "property", Color = "#a6e22e" },
                    new ThemeStyle { Tag = "definition", Color = "#fd971f" },
                    new ThemeStyle { Tag = "builtin", Color = "#ae81ff" },
                    new ThemeStyle { Tag = "tag", Color = "#f92672" },
                    new ThemeStyle { Tag = "invalid", Color = "#f8f8f0", BackgroundColor = "#f92672" }
                }
            },
            ["solarizedDark"] = new ThemeSpec
            {
                Name = "Solarized Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#586e75", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#859900" },
                    new ThemeStyle { Tag = "string", Color = "#2aa198" },
                    new ThemeStyle { Tag = "number", Color = "#d33682" },
                    new ThemeStyle { Tag = "function", Color = "#268bd2" },
                    new ThemeStyle { Tag = "variable", Color = "#839496" },
                    new ThemeStyle { Tag = "type", Color = "#b58900" },
                    new ThemeStyle { Tag = "operator", Color = "#859900" },
                    new ThemeStyle { Tag = "bracket", Color = "#839496" },
                    new ThemeStyle { Tag = "meta", Color = "#586e75" },
                    new ThemeStyle { Tag = "attribute", Color = "#b58900" },
                    new ThemeStyle { Tag = "property", Color = "#268bd2" },
                    new ThemeStyle { Tag = "definition", Color = "#cb4b16" },
                    new ThemeStyle { Tag = "builtin", Color = "#d33682" },
                    new ThemeStyle { Tag = "tag", Color = "#859900" },
                    new ThemeStyle { Tag = "invalid", Color = "#dc322f", FontWeight = "bold" }
                }
            },
            ["solarizedLight"] = new ThemeSpec
            {
                Name = "Solarized Light",
                Dark = false,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#93a1a1", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#859900" },
                    new ThemeStyle { Tag = "string", Color = "#2aa198" },
                    new ThemeStyle { Tag = "number", Color = "#d33682" },
                    new ThemeStyle { Tag = "function", Color = "#268bd2" },
                    new ThemeStyle { Tag = "variable", Color = "#657b83" },
                    new ThemeStyle { Tag = "type", Color = "#b58900" },
                    new ThemeStyle { Tag = "operator", Color = "#859900" },
                    new ThemeStyle { Tag = "bracket", Color = "#657b83" },
                    new ThemeStyle { Tag = "meta", Color = "#93a1a1" },
                    new ThemeStyle { Tag = "attribute", Color = "#b58900" },
                    new ThemeStyle { Tag = "property", Color = "#268bd2" },
                    new ThemeStyle { Tag = "definition", Color = "#cb4b16" },
                    new ThemeStyle { Tag = "builtin", Color = "#d33682" },
                    new ThemeStyle { Tag = "tag", Color = "#859900" },
                    new ThemeStyle { Tag = "invalid", Color = "#dc322f", FontWeight = "bold" }
                }
            },
            ["vsCode"] = new ThemeSpec
            {
                Name = "VS Code Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#6a9955", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#569cd6" },
                    new ThemeStyle { Tag = "string", Color = "#ce9178" },
                    new ThemeStyle { Tag = "number", Color = "#b5cea8" },
                    new ThemeStyle { Tag = "function", Color = "#dcdcaa" },
                    new ThemeStyle { Tag = "variable", Color = "#9cdcfe" },
                    new ThemeStyle { Tag = "type", Color = "#4ec9b0" },
                    new ThemeStyle { Tag = "operator", Color = "#d4d4d4" },
                    new ThemeStyle { Tag = "bracket", Color = "#d4d4d4" },
                    new ThemeStyle { Tag = "meta", Color = "#569cd6" },
                    new ThemeStyle { Tag = "attribute", Color = "#9cdcfe" },
                    new ThemeStyle { Tag = "property", Color = "#9cdcfe" },
                    new ThemeStyle { Tag = "definition", Color = "#dcdcaa" },
                    new ThemeStyle { Tag = "builtin", Color = "#4ec9b0" },
                    new ThemeStyle { Tag = "tag", Color = "#569cd6" },
                    new ThemeStyle { Tag = "invalid", Color = "#f44747", TextDecoration = "underline wavy" }
                }
            },
            ["material"] = new ThemeSpec
            {
                Name = "Material Theme",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#546e7a", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#c792ea" },
                    new ThemeStyle { Tag = "string", Color = "#c3e88d" },
                    new ThemeStyle { Tag = "number", Color = "#f78c6c" },
                    new ThemeStyle { Tag = "function", Color = "#82aaff" },
                    new ThemeStyle { Tag = "variable", Color = "#eeffff" },
                    new ThemeStyle { Tag = "type", Color = "#ffcb6b" },
                    new ThemeStyle { Tag = "operator", Color = "#89ddff" },
                    new ThemeStyle { Tag = "bracket", Color = "#eeffff" },
                    new ThemeStyle { Tag = "meta", Color = "#89ddff" },
                    new ThemeStyle { Tag = "attribute", Color = "#ffcb6b" },
                    new ThemeStyle { Tag = "property", Color = "#82aaff" },
                    new ThemeStyle { Tag = "definition", Color = "#82aaff" },
                    new ThemeStyle { Tag = "builtin", Color = "#ffcb6b" },
                    new ThemeStyle { Tag = "tag", Color = "#f07178" },
                    new ThemeStyle { Tag = "invalid", Color = "#ff5370", FontWeight = "bold" }
                }
            },
            ["nord"] = new ThemeSpec
            {
                Name = "Nord",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#616e88", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#81a1c1" },
                    new ThemeStyle { Tag = "string", Color = "#a3be8c" },
                    new ThemeStyle { Tag = "number", Color = "#b48ead" },
                    new ThemeStyle { Tag = "function", Color = "#88c0d0" },
                    new ThemeStyle { Tag = "variable", Color = "#d8dee9" },
                    new ThemeStyle { Tag = "type", Color = "#8fbcbb" },
                    new ThemeStyle { Tag = "operator", Color = "#81a1c1" },
                    new ThemeStyle { Tag = "bracket", Color = "#eceff4" },
                    new ThemeStyle { Tag = "meta", Color = "#81a1c1" },
                    new ThemeStyle { Tag = "attribute", Color = "#8fbcbb" },
                    new ThemeStyle { Tag = "property", Color = "#88c0d0" },
                    new ThemeStyle { Tag = "definition", Color = "#d08770" },
                    new ThemeStyle { Tag = "builtin", Color = "#81a1c1" },
                    new ThemeStyle { Tag = "tag", Color = "#81a1c1" },
                    new ThemeStyle { Tag = "invalid", Color = "#bf616a", TextDecoration = "underline" }
                }
            },
            ["atomOneDark"] = new ThemeSpec
            {
                Name = "Atom One Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#5c6370", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#c678dd" },
                    new ThemeStyle { Tag = "string", Color = "#98c379" },
                    new ThemeStyle { Tag = "number", Color = "#d19a66" },
                    new ThemeStyle { Tag = "function", Color = "#61afef" },
                    new ThemeStyle { Tag = "variable", Color = "#e06c75" },
                    new ThemeStyle { Tag = "type", Color = "#e5c07b" },
                    new ThemeStyle { Tag = "operator", Color = "#56b6c2" },
                    new ThemeStyle { Tag = "bracket", Color = "#abb2bf" },
                    new ThemeStyle { Tag = "meta", Color = "#7f848e" },
                    new ThemeStyle { Tag = "attribute", Color = "#d19a66" },
                    new ThemeStyle { Tag = "property", Color = "#e06c75" },
                    new ThemeStyle { Tag = "definition", Color = "#e5c07b" },
                    new ThemeStyle { Tag = "builtin", Color = "#56b6c2" },
                    new ThemeStyle { Tag = "tag", Color = "#e06c75" },
                    new ThemeStyle { Tag = "invalid", Color = "#ffffff", BackgroundColor = "#e05252" }
                }
            },
            ["gruvboxDark"] = new ThemeSpec
            {
                Name = "Gruvbox Dark",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#928374", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#fb4934" },
                    new ThemeStyle { Tag = "string", Color = "#b8bb26" },
                    new ThemeStyle { Tag = "number", Color = "#d3869b" },
                    new ThemeStyle { Tag = "function", Color = "#8ec07c" },
                    new ThemeStyle { Tag = "variable", Color = "#ebdbb2" },
                    new ThemeStyle { Tag = "type", Color = "#fabd2f" },
                    new ThemeStyle { Tag = "operator", Color = "#fe8019" },
                    new ThemeStyle { Tag = "bracket", Color = "#a89984" },
                    new ThemeStyle { Tag = "meta", Color = "#928374" },
                    new ThemeStyle { Tag = "attribute", Color = "#fabd2f" },
                    new ThemeStyle { Tag = "property", Color = "#83a598" },
                    new ThemeStyle { Tag = "definition", Color = "#fabd2f" },
                    new ThemeStyle { Tag = "builtin", Color = "#fe8019" },
                    new ThemeStyle { Tag = "tag", Color = "#fb4934" },
                    new ThemeStyle { Tag = "invalid", Color = "#fb4934", TextDecoration = "underline" }
                }
            },
            ["cobalt"] = new ThemeSpec
            {
                Name = "Cobalt",
                Dark = true,
                Styles = new List<ThemeStyle>
                {
                    new ThemeStyle { Tag = "comment", Color = "#0088ff", FontStyle = "italic" },
                    new ThemeStyle { Tag = "keyword", Color = "#ff9d00" },
                    new ThemeStyle { Tag = "string", Color = "#3ad900" },
                    new ThemeStyle { Tag = "number", Color = "#ff628c" },
                    new ThemeStyle { Tag = "function", Color = "#ffee80" },
                    new ThemeStyle { Tag = "variable", Color = "#ffffff" },
                    new ThemeStyle { Tag = "type", Color = "#80ffbb" },
                    new ThemeStyle { Tag = "operator", Color = "#ff9d00" },
                    new ThemeStyle { Tag = "bracket", Color = "#cccccc" },
                    new ThemeStyle { Tag = "meta", Color = "#9effff" },
                    new ThemeStyle { Tag = "attribute", Color = "#9effff" },
                    new ThemeStyle { Tag = "property", Color = "#ffee80" },
                    new ThemeStyle { Tag = "definition", Color = "#ffc600" },
                    new ThemeStyle { Tag = "builtin", Color = "#80ffbb" },
                    new ThemeStyle { Tag = "tag", Color = "#9effff" },
                    new ThemeStyle { Tag = "invalid", Color = "#f44542", TextDecoration = "underline" }
                }
            }
        };

        /// <summary>
        /// Get theme by ID
        /// </summary>
        public static ThemeSpec GetTheme(string themeId)
        {
            if (string.IsNullOrEmpty(themeId))
                return GetTheme("oneDark"); // Default theme

            var lower = themeId.ToLower();
            return Themes.ContainsKey(lower) ? Themes[lower] : GetTheme("oneDark");
        }

        /// <summary>
        /// Get all available themes
        /// </summary>
        public static List<ThemeSpec> GetAllThemes()
        {
            return Themes.Values.ToList();
        }

        /// <summary>
        /// Get dark themes only
        /// </summary>
        public static List<ThemeSpec> GetDarkThemes()
        {
            return Themes.Values.Where(t => t.Dark).ToList();
        }

        /// <summary>
        /// Get light themes only
        /// </summary>
        public static List<ThemeSpec> GetLightThemes()
        {
            return Themes.Values.Where(t => !t.Dark).ToList();
        }

        /// <summary>
        /// Check if theme exists
        /// </summary>
        public static bool ThemeExists(string themeId)
        {
            if (string.IsNullOrEmpty(themeId))
                return false;

            return Themes.ContainsKey(themeId.ToLower());
        }

        /// <summary>
        /// Get theme names
        /// </summary>
        public static List<string> GetThemeNames()
        {
            return Themes.Keys.ToList();
        }

        /// <summary>
        /// Get theme display names
        /// </summary>
        public static Dictionary<string, string> GetThemeDisplayNames()
        {
            return Themes.ToDictionary(t => t.Key, t => t.Value.Name);
        }

        /// <summary>
        /// Get recommended theme for a language
        /// </summary>
        public static string GetRecommendedTheme(string languageId, bool preferDark = true)
        {
            // Language-specific recommendations
            var recommendations = new Dictionary<string, string[]>
            {
                ["javascript"] = new[] { "vsCode", "oneDark", "monokai" },
                ["typescript"] = new[] { "vsCode", "oneDark", "githubDark" },
                ["python"] = new[] { "monokai", "dracula", "oneDark" },
                ["csharp"] = new[] { "vsCode", "oneDark", "githubDark" },
                ["java"] = new[] { "vsCode", "material", "oneDark" },
                ["html"] = new[] { "githubLight", "atomOneDark", "material" },
                ["css"] = new[] { "material", "oneDark", "githubDark" },
                ["markdown"] = new[] { "githubLight", "solarizedLight", "githubDark" }
            };

            if (recommendations.ContainsKey(languageId?.ToLower() ?? ""))
            {
                var recommended = recommendations[languageId.ToLower()];
                foreach (var theme in recommended)
                {
                    if (Themes.ContainsKey(theme))
                    {
                        var themeSpec = Themes[theme];
                        if ((preferDark && themeSpec.Dark) || (!preferDark && !themeSpec.Dark))
                        {
                            return theme;
                        }
                    }
                }
            }

            // Default based on preference
            return preferDark ? "oneDark" : "githubLight";
        }
    }
}