using System;
using System.Collections.Generic;
using System.Linq;
using MauiCodemirror.Models;

namespace MauiCodemirror.Services
{
    /// <summary>
    /// Service for managing CodeMirror 6 language support
    /// </summary>
    public static class CodeMirrorLanguageService
    {
        private static readonly Dictionary<string, LanguageDescription> Languages = new Dictionary<string, LanguageDescription>
        {
            ["javascript"] = new LanguageDescription
            {
                Name = "JavaScript",
                Alias = new List<string> { "js", "jsx" },
                Extensions = new List<string> { ".js", ".jsx", ".mjs" },
                Support = new LanguageSupport
                {
                    Language = "javascript",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString(), "`" },
                            Before = ")]}'\"`"
                        },
                        IndentOnInput = true,
                        WordChars = "$"
                    }
                }
            },
            ["typescript"] = new LanguageDescription
            {
                Name = "TypeScript",
                Alias = new List<string> { "ts", "tsx" },
                Extensions = new List<string> { ".ts", ".tsx", ".d.ts" },
                Support = new LanguageSupport
                {
                    Language = "typescript",
                    Support = new List<string> { "autocomplete", "lint", "format", "types" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString(), "`" },
                            Before = ")]}'\"`"
                        },
                        IndentOnInput = true,
                        WordChars = "$"
                    }
                }
            },
            ["python"] = new LanguageDescription
            {
                Name = "Python",
                Alias = new List<string> { "py" },
                Extensions = new List<string> { ".py", ".pyw" },
                Support = new LanguageSupport
                {
                    Language = "python",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "#",
                            Block = new BlockComment { Open = "'''", Close = "'''" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString(), "'''" },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["csharp"] = new LanguageDescription
            {
                Name = "C#",
                Alias = new List<string> { "cs", "c#" },
                Extensions = new List<string> { ".cs" },
                Support = new LanguageSupport
                {
                    Language = "csharp",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["java"] = new LanguageDescription
            {
                Name = "Java",
                Alias = new List<string> { "java" },
                Extensions = new List<string> { ".java" },
                Support = new LanguageSupport
                {
                    Language = "java",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["cpp"] = new LanguageDescription
            {
                Name = "C++",
                Alias = new List<string> { "c++", "cc", "cxx" },
                Extensions = new List<string> { ".cpp", ".cc", ".cxx", ".hpp", ".h" },
                Support = new LanguageSupport
                {
                    Language = "cpp",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["html"] = new LanguageDescription
            {
                Name = "HTML",
                Alias = new List<string> { "htm" },
                Extensions = new List<string> { ".html", ".htm" },
                Support = new LanguageSupport
                {
                    Language = "html",
                    Support = new List<string> { "autocomplete", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Block = new BlockComment { Open = "<!--", Close = "-->" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "<", "'", '"'.ToString() },
                            Before = ">'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["css"] = new LanguageDescription
            {
                Name = "CSS",
                Alias = new List<string> { "scss", "less" },
                Extensions = new List<string> { ".css", ".scss", ".less" },
                Support = new LanguageSupport
                {
                    Language = "css",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["json"] = new LanguageDescription
            {
                Name = "JSON",
                Alias = new List<string> { "json5" },
                Extensions = new List<string> { ".json", ".json5" },
                Support = new LanguageSupport
                {
                    Language = "json",
                    Support = new List<string> { "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens(),
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "[", "{", '"'.ToString() },
                            Before = "]}\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["xml"] = new LanguageDescription
            {
                Name = "XML",
                Alias = new List<string> { "xsl", "xsd" },
                Extensions = new List<string> { ".xml", ".xsl", ".xsd", ".xaml" },
                Support = new LanguageSupport
                {
                    Language = "xml",
                    Support = new List<string> { "autocomplete", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Block = new BlockComment { Open = "<!--", Close = "-->" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "<", "'", '"'.ToString() },
                            Before = ">'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["markdown"] = new LanguageDescription
            {
                Name = "Markdown",
                Alias = new List<string> { "md" },
                Extensions = new List<string> { ".md", ".markdown" },
                Support = new LanguageSupport
                {
                    Language = "markdown",
                    Support = new List<string> { "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens(),
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "*", "_", "`" },
                            Before = ")]}*_`"
                        },
                        IndentOnInput = false
                    }
                }
            },
            ["sql"] = new LanguageDescription
            {
                Name = "SQL",
                Alias = new List<string> { "mysql", "pgsql", "sqlite" },
                Extensions = new List<string> { ".sql" },
                Support = new LanguageSupport
                {
                    Language = "sql",
                    Support = new List<string> { "autocomplete", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "--",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "'", '"'.ToString() },
                            Before = ")]'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["yaml"] = new LanguageDescription
            {
                Name = "YAML",
                Alias = new List<string> { "yml" },
                Extensions = new List<string> { ".yaml", ".yml" },
                Support = new LanguageSupport
                {
                    Language = "yaml",
                    Support = new List<string> { "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "#"
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "[", "{", "'", '"'.ToString() },
                            Before = "]}\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["rust"] = new LanguageDescription
            {
                Name = "Rust",
                Alias = new List<string> { "rs" },
                Extensions = new List<string> { ".rs" },
                Support = new LanguageSupport
                {
                    Language = "rust",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["go"] = new LanguageDescription
            {
                Name = "Go",
                Alias = new List<string> { "golang" },
                Extensions = new List<string> { ".go" },
                Support = new LanguageSupport
                {
                    Language = "go",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString(), "`" },
                            Before = ")]}'\"`"
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["php"] = new LanguageDescription
            {
                Name = "PHP",
                Alias = new List<string> { "php", "php3", "php4", "php5" },
                Extensions = new List<string> { ".php", ".php3", ".php4", ".php5", ".phtml" },
                Support = new LanguageSupport
                {
                    Language = "php",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true,
                        WordChars = "$"
                    }
                }
            },
            ["ruby"] = new LanguageDescription
            {
                Name = "Ruby",
                Alias = new List<string> { "rb" },
                Extensions = new List<string> { ".rb" },
                Support = new LanguageSupport
                {
                    Language = "ruby",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "#",
                            Block = new BlockComment { Open = "=begin", Close = "=end" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true,
                        WordChars = "@$"
                    }
                }
            },
            ["swift"] = new LanguageDescription
            {
                Name = "Swift",
                Alias = new List<string> { "swift" },
                Extensions = new List<string> { ".swift" },
                Support = new LanguageSupport
                {
                    Language = "swift",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            },
            ["kotlin"] = new LanguageDescription
            {
                Name = "Kotlin",
                Alias = new List<string> { "kt" },
                Extensions = new List<string> { ".kt", ".kts" },
                Support = new LanguageSupport
                {
                    Language = "kotlin",
                    Support = new List<string> { "autocomplete", "lint", "format" },
                    LanguageData = new LanguageData
                    {
                        CommentTokens = new CommentTokens
                        {
                            Line = "//",
                            Block = new BlockComment { Open = "/*", Close = "*/" }
                        },
                        CloseBrackets = new CloseBrackets
                        {
                            Brackets = new List<string> { "(", "[", "{", "'", '"'.ToString() },
                            Before = ")]}'\""
                        },
                        IndentOnInput = true
                    }
                }
            }
        };

        /// <summary>
        /// Get language description by name or alias
        /// </summary>
        public static LanguageDescription GetLanguage(string languageId)
        {
            if (string.IsNullOrEmpty(languageId))
                return null;

            var lower = languageId.ToLower();

            // Direct match
            if (Languages.ContainsKey(lower))
                return Languages[lower];

            // Check aliases
            var match = Languages.FirstOrDefault(l => l.Value.Alias?.Contains(lower) == true);
            return match.Value;
        }

        /// <summary>
        /// Get language by file extension
        /// </summary>
        public static LanguageDescription GetLanguageByExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;

            if (!extension.StartsWith("."))
                extension = "." + extension;

            var lower = extension.ToLower();
            var match = Languages.FirstOrDefault(l => l.Value.Extensions?.Contains(lower) == true);
            return match.Value;
        }

        /// <summary>
        /// Get language by filename
        /// </summary>
        public static LanguageDescription GetLanguageByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            var extension = System.IO.Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(extension))
            {
                var lang = GetLanguageByExtension(extension);
                if (lang != null)
                    return lang;
            }

            // Special cases for files without extensions
            var lowerName = fileName.ToLower();
            if (lowerName == "makefile" || lowerName == "gnumakefile")
                return GetLanguage("makefile");
            if (lowerName == "dockerfile")
                return GetLanguage("dockerfile");
            if (lowerName == "jenkinsfile")
                return GetLanguage("groovy");

            return null;
        }

        /// <summary>
        /// Get all supported languages
        /// </summary>
        public static List<LanguageDescription> GetAllLanguages()
        {
            return Languages.Values.ToList();
        }

        /// <summary>
        /// Check if language supports a specific feature
        /// </summary>
        public static bool SupportsFeature(string languageId, string feature)
        {
            var lang = GetLanguage(languageId);
            return lang?.Support?.Support?.Contains(feature.ToLower()) == true;
        }

        /// <summary>
        /// Get CodeMirror 6 language mode string
        /// </summary>
        public static string GetCodeMirrorMode(string languageId)
        {
            var lang = GetLanguage(languageId);
            return lang?.Support?.Language ?? "text";
        }
    }
}