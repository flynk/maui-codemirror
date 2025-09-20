using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flynk.Apps.Maui.Codemirror.Models;

namespace Flynk.Apps.Maui.Codemirror.Services
{
    /// <summary>
    /// Service for managing CodeMirror 6 autocomplete and IntelliSense features
    /// </summary>
    public class CodeMirrorCompletionService
    {
        private readonly Dictionary<string, List<CompletionOption>> _customCompletions;
        private readonly Dictionary<string, Func<string, int, Task<List<CompletionOption>>>> _completionProviders;

        public CodeMirrorCompletionService()
        {
            _customCompletions = new Dictionary<string, List<CompletionOption>>();
            _completionProviders = new Dictionary<string, Func<string, int, Task<List<CompletionOption>>>>();
            InitializeBuiltInCompletions();
        }

        /// <summary>
        /// Initialize built-in completions for common languages
        /// </summary>
        private void InitializeBuiltInCompletions()
        {
            // JavaScript/TypeScript completions
            var jsCompletions = new List<CompletionOption>
            {
                new CompletionOption { Label = "console", Type = "namespace", Detail = "Console API", Info = "Provides access to the browser's debugging console" },
                new CompletionOption { Label = "console.log", Type = "method", Detail = "Log to console", Apply = "console.log($0)" },
                new CompletionOption { Label = "console.error", Type = "method", Detail = "Log error to console", Apply = "console.error($0)" },
                new CompletionOption { Label = "console.warn", Type = "method", Detail = "Log warning to console", Apply = "console.warn($0)" },
                new CompletionOption { Label = "console.info", Type = "method", Detail = "Log info to console", Apply = "console.info($0)" },
                new CompletionOption { Label = "document", Type = "variable", Detail = "Document object", Info = "The Document interface represents the entire HTML or XML document" },
                new CompletionOption { Label = "window", Type = "variable", Detail = "Window object", Info = "The Window interface represents a window containing a DOM document" },
                new CompletionOption { Label = "Array", Type = "class", Detail = "Array constructor", Info = "The JavaScript Array class is a global object that is used in the construction of arrays" },
                new CompletionOption { Label = "Object", Type = "class", Detail = "Object constructor", Info = "The Object class represents one of JavaScript's data types" },
                new CompletionOption { Label = "String", Type = "class", Detail = "String constructor", Info = "The String global object is a constructor for strings" },
                new CompletionOption { Label = "Number", Type = "class", Detail = "Number constructor", Info = "The Number JavaScript object is a wrapper object allowing you to work with numerical values" },
                new CompletionOption { Label = "Boolean", Type = "class", Detail = "Boolean constructor", Info = "The Boolean object is an object wrapper for a boolean value" },
                new CompletionOption { Label = "Promise", Type = "class", Detail = "Promise constructor", Info = "The Promise object represents the eventual completion (or failure) of an asynchronous operation" },
                new CompletionOption { Label = "async", Type = "keyword", Detail = "Async function", Apply = "async function $1() {\n  $0\n}" },
                new CompletionOption { Label = "await", Type = "keyword", Detail = "Await expression", Apply = "await $0" },
                new CompletionOption { Label = "function", Type = "keyword", Detail = "Function declaration", Apply = "function $1($2) {\n  $0\n}" },
                new CompletionOption { Label = "const", Type = "keyword", Detail = "Constant declaration", Apply = "const $1 = $0" },
                new CompletionOption { Label = "let", Type = "keyword", Detail = "Variable declaration", Apply = "let $1 = $0" },
                new CompletionOption { Label = "if", Type = "keyword", Detail = "If statement", Apply = "if ($1) {\n  $0\n}" },
                new CompletionOption { Label = "for", Type = "keyword", Detail = "For loop", Apply = "for (let $1 = 0; $1 < $2; $1++) {\n  $0\n}" },
                new CompletionOption { Label = "while", Type = "keyword", Detail = "While loop", Apply = "while ($1) {\n  $0\n}" },
                new CompletionOption { Label = "switch", Type = "keyword", Detail = "Switch statement", Apply = "switch ($1) {\n  case $2:\n    $0\n    break;\n  default:\n    break;\n}" },
                new CompletionOption { Label = "try", Type = "keyword", Detail = "Try-catch block", Apply = "try {\n  $0\n} catch (error) {\n  \n}" },
                new CompletionOption { Label = "class", Type = "keyword", Detail = "Class declaration", Apply = "class $1 {\n  constructor() {\n    $0\n  }\n}" },
                new CompletionOption { Label = "import", Type = "keyword", Detail = "Import statement", Apply = "import $1 from '$0'" },
                new CompletionOption { Label = "export", Type = "keyword", Detail = "Export statement", Apply = "export $0" },
                new CompletionOption { Label = "return", Type = "keyword", Detail = "Return statement", Apply = "return $0" },
                new CompletionOption { Label = "this", Type = "keyword", Detail = "This keyword", Info = "The JavaScript this keyword refers to the object it belongs to" },
                new CompletionOption { Label = "super", Type = "keyword", Detail = "Super keyword", Info = "The super keyword is used to access and call functions on an object's parent" },
            };

            _customCompletions["javascript"] = jsCompletions;
            _customCompletions["typescript"] = jsCompletions;

            // Python completions
            var pythonCompletions = new List<CompletionOption>
            {
                new CompletionOption { Label = "print", Type = "function", Detail = "Print function", Apply = "print($0)" },
                new CompletionOption { Label = "len", Type = "function", Detail = "Length function", Apply = "len($0)" },
                new CompletionOption { Label = "range", Type = "function", Detail = "Range function", Apply = "range($0)" },
                new CompletionOption { Label = "str", Type = "class", Detail = "String type", Apply = "str($0)" },
                new CompletionOption { Label = "int", Type = "class", Detail = "Integer type", Apply = "int($0)" },
                new CompletionOption { Label = "float", Type = "class", Detail = "Float type", Apply = "float($0)" },
                new CompletionOption { Label = "list", Type = "class", Detail = "List type", Apply = "list($0)" },
                new CompletionOption { Label = "dict", Type = "class", Detail = "Dictionary type", Apply = "dict($0)" },
                new CompletionOption { Label = "tuple", Type = "class", Detail = "Tuple type", Apply = "tuple($0)" },
                new CompletionOption { Label = "set", Type = "class", Detail = "Set type", Apply = "set($0)" },
                new CompletionOption { Label = "def", Type = "keyword", Detail = "Function definition", Apply = "def $1($2):\n    $0" },
                new CompletionOption { Label = "class", Type = "keyword", Detail = "Class definition", Apply = "class $1:\n    def __init__(self):\n        $0" },
                new CompletionOption { Label = "if", Type = "keyword", Detail = "If statement", Apply = "if $1:\n    $0" },
                new CompletionOption { Label = "elif", Type = "keyword", Detail = "Elif statement", Apply = "elif $1:\n    $0" },
                new CompletionOption { Label = "else", Type = "keyword", Detail = "Else statement", Apply = "else:\n    $0" },
                new CompletionOption { Label = "for", Type = "keyword", Detail = "For loop", Apply = "for $1 in $2:\n    $0" },
                new CompletionOption { Label = "while", Type = "keyword", Detail = "While loop", Apply = "while $1:\n    $0" },
                new CompletionOption { Label = "try", Type = "keyword", Detail = "Try-except block", Apply = "try:\n    $0\nexcept Exception as e:\n    pass" },
                new CompletionOption { Label = "with", Type = "keyword", Detail = "With statement", Apply = "with $1 as $2:\n    $0" },
                new CompletionOption { Label = "import", Type = "keyword", Detail = "Import statement", Apply = "import $0" },
                new CompletionOption { Label = "from", Type = "keyword", Detail = "From import", Apply = "from $1 import $0" },
                new CompletionOption { Label = "return", Type = "keyword", Detail = "Return statement", Apply = "return $0" },
                new CompletionOption { Label = "yield", Type = "keyword", Detail = "Yield statement", Apply = "yield $0" },
                new CompletionOption { Label = "lambda", Type = "keyword", Detail = "Lambda function", Apply = "lambda $1: $0" },
                new CompletionOption { Label = "async", Type = "keyword", Detail = "Async function", Apply = "async def $1($2):\n    $0" },
                new CompletionOption { Label = "await", Type = "keyword", Detail = "Await expression", Apply = "await $0" },
            };

            _customCompletions["python"] = pythonCompletions;

            // C# completions
            var csharpCompletions = new List<CompletionOption>
            {
                new CompletionOption { Label = "Console", Type = "class", Detail = "Console class", Info = "Represents the standard input, output, and error streams" },
                new CompletionOption { Label = "Console.WriteLine", Type = "method", Detail = "Write line to console", Apply = "Console.WriteLine($0);" },
                new CompletionOption { Label = "Console.ReadLine", Type = "method", Detail = "Read line from console", Apply = "Console.ReadLine()" },
                new CompletionOption { Label = "string", Type = "type", Detail = "String type", Apply = "string $0" },
                new CompletionOption { Label = "int", Type = "type", Detail = "Integer type", Apply = "int $0" },
                new CompletionOption { Label = "bool", Type = "type", Detail = "Boolean type", Apply = "bool $0" },
                new CompletionOption { Label = "double", Type = "type", Detail = "Double type", Apply = "double $0" },
                new CompletionOption { Label = "float", Type = "type", Detail = "Float type", Apply = "float $0" },
                new CompletionOption { Label = "decimal", Type = "type", Detail = "Decimal type", Apply = "decimal $0" },
                new CompletionOption { Label = "var", Type = "keyword", Detail = "Implicitly typed variable", Apply = "var $1 = $0" },
                new CompletionOption { Label = "public", Type = "keyword", Detail = "Public modifier", Apply = "public $0" },
                new CompletionOption { Label = "private", Type = "keyword", Detail = "Private modifier", Apply = "private $0" },
                new CompletionOption { Label = "protected", Type = "keyword", Detail = "Protected modifier", Apply = "protected $0" },
                new CompletionOption { Label = "internal", Type = "keyword", Detail = "Internal modifier", Apply = "internal $0" },
                new CompletionOption { Label = "static", Type = "keyword", Detail = "Static modifier", Apply = "static $0" },
                new CompletionOption { Label = "class", Type = "keyword", Detail = "Class declaration", Apply = "public class $1\n{\n    $0\n}" },
                new CompletionOption { Label = "interface", Type = "keyword", Detail = "Interface declaration", Apply = "public interface I$1\n{\n    $0\n}" },
                new CompletionOption { Label = "struct", Type = "keyword", Detail = "Struct declaration", Apply = "public struct $1\n{\n    $0\n}" },
                new CompletionOption { Label = "enum", Type = "keyword", Detail = "Enum declaration", Apply = "public enum $1\n{\n    $0\n}" },
                new CompletionOption { Label = "namespace", Type = "keyword", Detail = "Namespace declaration", Apply = "namespace $1\n{\n    $0\n}" },
                new CompletionOption { Label = "using", Type = "keyword", Detail = "Using directive", Apply = "using $0;" },
                new CompletionOption { Label = "if", Type = "keyword", Detail = "If statement", Apply = "if ($1)\n{\n    $0\n}" },
                new CompletionOption { Label = "else", Type = "keyword", Detail = "Else statement", Apply = "else\n{\n    $0\n}" },
                new CompletionOption { Label = "for", Type = "keyword", Detail = "For loop", Apply = "for (int $1 = 0; $1 < $2; $1++)\n{\n    $0\n}" },
                new CompletionOption { Label = "foreach", Type = "keyword", Detail = "Foreach loop", Apply = "foreach (var $1 in $2)\n{\n    $0\n}" },
                new CompletionOption { Label = "while", Type = "keyword", Detail = "While loop", Apply = "while ($1)\n{\n    $0\n}" },
                new CompletionOption { Label = "switch", Type = "keyword", Detail = "Switch statement", Apply = "switch ($1)\n{\n    case $2:\n        $0\n        break;\n    default:\n        break;\n}" },
                new CompletionOption { Label = "try", Type = "keyword", Detail = "Try-catch block", Apply = "try\n{\n    $0\n}\ncatch (Exception ex)\n{\n    \n}" },
                new CompletionOption { Label = "async", Type = "keyword", Detail = "Async method", Apply = "async Task $1()\n{\n    $0\n}" },
                new CompletionOption { Label = "await", Type = "keyword", Detail = "Await expression", Apply = "await $0" },
                new CompletionOption { Label = "return", Type = "keyword", Detail = "Return statement", Apply = "return $0;" },
                new CompletionOption { Label = "new", Type = "keyword", Detail = "New operator", Apply = "new $0()" },
                new CompletionOption { Label = "this", Type = "keyword", Detail = "This keyword", Info = "Refers to the current instance of the class" },
                new CompletionOption { Label = "base", Type = "keyword", Detail = "Base keyword", Info = "Accesses members of the base class" },
                new CompletionOption { Label = "override", Type = "keyword", Detail = "Override modifier", Apply = "public override $0" },
                new CompletionOption { Label = "virtual", Type = "keyword", Detail = "Virtual modifier", Apply = "public virtual $0" },
                new CompletionOption { Label = "abstract", Type = "keyword", Detail = "Abstract modifier", Apply = "public abstract $0" },
                new CompletionOption { Label = "List<T>", Type = "class", Detail = "Generic List", Apply = "List<$1> $2 = new List<$1>();" },
                new CompletionOption { Label = "Dictionary<TKey, TValue>", Type = "class", Detail = "Generic Dictionary", Apply = "Dictionary<$1, $2> $3 = new Dictionary<$1, $2>();" },
                new CompletionOption { Label = "Task", Type = "class", Detail = "Task class", Info = "Represents an asynchronous operation" },
                new CompletionOption { Label = "LINQ", Type = "namespace", Detail = "Language Integrated Query", Info = "Provides query capabilities" },
            };

            _customCompletions["csharp"] = csharpCompletions;
        }

        /// <summary>
        /// Register custom completions for a language
        /// </summary>
        public void RegisterCompletions(string language, List<CompletionOption> completions)
        {
            if (string.IsNullOrEmpty(language) || completions == null)
                return;

            if (_customCompletions.ContainsKey(language))
            {
                _customCompletions[language].AddRange(completions);
            }
            else
            {
                _customCompletions[language] = completions;
            }
        }

        /// <summary>
        /// Register a dynamic completion provider
        /// </summary>
        public void RegisterCompletionProvider(string language, Func<string, int, Task<List<CompletionOption>>> provider)
        {
            if (string.IsNullOrEmpty(language) || provider == null)
                return;

            _completionProviders[language] = provider;
        }

        /// <summary>
        /// Get completions for a specific context
        /// </summary>
        public async Task<List<CompletionOption>> GetCompletions(string language, string context, int position)
        {
            var completions = new List<CompletionOption>();

            // Add static completions
            if (_customCompletions.ContainsKey(language))
            {
                completions.AddRange(_customCompletions[language]);
            }

            // Add dynamic completions
            if (_completionProviders.ContainsKey(language))
            {
                var dynamicCompletions = await _completionProviders[language](context, position);
                if (dynamicCompletions != null)
                {
                    completions.AddRange(dynamicCompletions);
                }
            }

            // Filter based on context
            if (!string.IsNullOrEmpty(context))
            {
                var prefix = GetPrefixAtPosition(context, position);
                if (!string.IsNullOrEmpty(prefix))
                {
                    completions = completions
                        .Where(c => c.Label.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
            }

            // Sort by relevance
            return completions
                .OrderBy(c => c.Boost ?? 0)
                .ThenBy(c => c.Label)
                .ToList();
        }

        /// <summary>
        /// Get the prefix at the current position
        /// </summary>
        private string GetPrefixAtPosition(string text, int position)
        {
            if (string.IsNullOrEmpty(text) || position <= 0 || position > text.Length)
                return string.Empty;

            var start = position - 1;
            while (start >= 0 && (char.IsLetterOrDigit(text[start]) || text[start] == '_' || text[start] == '.'))
            {
                start--;
            }

            return text.Substring(start + 1, position - start - 1);
        }

        /// <summary>
        /// Get snippet completions for a language
        /// </summary>
        public List<CompletionOption> GetSnippets(string language)
        {
            var snippets = new List<CompletionOption>();

            switch (language?.ToLower())
            {
                case "javascript":
                case "typescript":
                    snippets.AddRange(GetJavaScriptSnippets());
                    break;
                case "python":
                    snippets.AddRange(GetPythonSnippets());
                    break;
                case "csharp":
                    snippets.AddRange(GetCSharpSnippets());
                    break;
                case "html":
                    snippets.AddRange(GetHtmlSnippets());
                    break;
            }

            return snippets;
        }

        private List<CompletionOption> GetJavaScriptSnippets()
        {
            return new List<CompletionOption>
            {
                new CompletionOption { Label = "log", Type = "snippet", Detail = "Console log", Apply = "console.log($0);" },
                new CompletionOption { Label = "func", Type = "snippet", Detail = "Function", Apply = "function $1($2) {\n  $0\n}" },
                new CompletionOption { Label = "arrow", Type = "snippet", Detail = "Arrow function", Apply = "($1) => {\n  $0\n}" },
                new CompletionOption { Label = "promise", Type = "snippet", Detail = "Promise", Apply = "new Promise((resolve, reject) => {\n  $0\n});" },
                new CompletionOption { Label = "fetch", Type = "snippet", Detail = "Fetch request", Apply = "fetch('$1')\n  .then(response => response.json())\n  .then(data => {\n    $0\n  });" },
                new CompletionOption { Label = "timeout", Type = "snippet", Detail = "setTimeout", Apply = "setTimeout(() => {\n  $0\n}, $1);" },
                new CompletionOption { Label = "interval", Type = "snippet", Detail = "setInterval", Apply = "setInterval(() => {\n  $0\n}, $1);" },
                new CompletionOption { Label = "map", Type = "snippet", Detail = "Array map", Apply = "$1.map(($2) => {\n  return $0;\n});" },
                new CompletionOption { Label = "filter", Type = "snippet", Detail = "Array filter", Apply = "$1.filter(($2) => {\n  return $0;\n});" },
                new CompletionOption { Label = "reduce", Type = "snippet", Detail = "Array reduce", Apply = "$1.reduce(($2, $3) => {\n  return $0;\n}, $4);" },
            };
        }

        private List<CompletionOption> GetPythonSnippets()
        {
            return new List<CompletionOption>
            {
                new CompletionOption { Label = "main", Type = "snippet", Detail = "Main block", Apply = "if __name__ == '__main__':\n    $0" },
                new CompletionOption { Label = "init", Type = "snippet", Detail = "Constructor", Apply = "def __init__(self$1):\n    $0" },
                new CompletionOption { Label = "prop", Type = "snippet", Detail = "Property", Apply = "@property\ndef $1(self):\n    return self._$1" },
                new CompletionOption { Label = "deco", Type = "snippet", Detail = "Decorator", Apply = "def $1(func):\n    def wrapper(*args, **kwargs):\n        $0\n        return func(*args, **kwargs)\n    return wrapper" },
                new CompletionOption { Label = "comp", Type = "snippet", Detail = "List comprehension", Apply = "[$2 for $1 in $3 if $0]" },
                new CompletionOption { Label = "gen", Type = "snippet", Detail = "Generator", Apply = "def $1():\n    for $2 in $3:\n        yield $0" },
                new CompletionOption { Label = "context", Type = "snippet", Detail = "Context manager", Apply = "class $1:\n    def __enter__(self):\n        $0\n        return self\n    def __exit__(self, exc_type, exc_val, exc_tb):\n        pass" },
                new CompletionOption { Label = "dataclass", Type = "snippet", Detail = "Data class", Apply = "@dataclass\nclass $1:\n    $0: $2" },
            };
        }

        private List<CompletionOption> GetCSharpSnippets()
        {
            return new List<CompletionOption>
            {
                new CompletionOption { Label = "prop", Type = "snippet", Detail = "Property", Apply = "public $1 $2 { get; set; }" },
                new CompletionOption { Label = "propfull", Type = "snippet", Detail = "Full property", Apply = "private $1 _$2;\npublic $1 $2\n{\n    get { return _$2; }\n    set { _$2 = value; }\n}" },
                new CompletionOption { Label = "ctor", Type = "snippet", Detail = "Constructor", Apply = "public $1()\n{\n    $0\n}" },
                new CompletionOption { Label = "method", Type = "snippet", Detail = "Method", Apply = "public $1 $2($3)\n{\n    $0\n}" },
                new CompletionOption { Label = "asyncmethod", Type = "snippet", Detail = "Async method", Apply = "public async Task<$1> $2Async($3)\n{\n    $0\n}" },
                new CompletionOption { Label = "region", Type = "snippet", Detail = "Region", Apply = "#region $1\n$0\n#endregion" },
                new CompletionOption { Label = "test", Type = "snippet", Detail = "Test method", Apply = "[Test]\npublic void $1_Should$2_When$3()\n{\n    // Arrange\n    $0\n    // Act\n    \n    // Assert\n}" },
                new CompletionOption { Label = "singleton", Type = "snippet", Detail = "Singleton pattern", Apply = "public class $1\n{\n    private static $1 _instance;\n    private $1() { }\n    public static $1 Instance => _instance ??= new $1();\n    $0\n}" },
            };
        }

        private List<CompletionOption> GetHtmlSnippets()
        {
            return new List<CompletionOption>
            {
                new CompletionOption { Label = "html5", Type = "snippet", Detail = "HTML5 template", Apply = "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>$1</title>\n</head>\n<body>\n    $0\n</body>\n</html>" },
                new CompletionOption { Label = "div", Type = "snippet", Detail = "Div element", Apply = "<div class=\"$1\">\n    $0\n</div>" },
                new CompletionOption { Label = "link", Type = "snippet", Detail = "Link tag", Apply = "<link rel=\"stylesheet\" href=\"$0\">" },
                new CompletionOption { Label = "script", Type = "snippet", Detail = "Script tag", Apply = "<script src=\"$1\"></script>" },
                new CompletionOption { Label = "form", Type = "snippet", Detail = "Form element", Apply = "<form action=\"$1\" method=\"$2\">\n    $0\n</form>" },
                new CompletionOption { Label = "input", Type = "snippet", Detail = "Input element", Apply = "<input type=\"$1\" name=\"$2\" id=\"$3\" value=\"$0\">" },
            };
        }
    }
}