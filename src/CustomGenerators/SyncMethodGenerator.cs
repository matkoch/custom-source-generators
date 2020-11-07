using System.Diagnostics;
using System.Linq;
using System.Text;
using CustomGenerators.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CustomGenerators
{
    [Generator]
    public class SyncMethodGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            //{
            //    var assembly = typeof(NukeAssemblyAttributesGenerator).Assembly;
            //    var location = Path.GetDirectoryName(assembly.Location);
            //    var requestedAssembly = Path.Combine(location, e.Name.Split(",").First() + ".dll");
            //    return File.Exists(requestedAssembly)
            //        ? Assembly.LoadFrom(requestedAssembly)
            //        : null;
            //};
        }

        public void Execute(GeneratorExecutionContext context)
            //public void Execute(SourceGeneratorContext context)
        {
            var sourceBuilder = new StringBuilder();

            var compilation = context.Compilation;
            var allTypes = compilation.Assembly.GlobalNamespace.GetAllTypes();

            foreach (var type in allTypes)
            {
                var asyncMethods = type.GetMembers()
                    .OfType<IMethodSymbol>()
                    .Where(x => x.IsAsync).ToList();

                // if (asyncMethods.Count == 0)
                //     continue;

                sourceBuilder
                    .AppendLine($"namespace {type.ContainingNamespace}")
                    .AppendLine("{")
                    .AppendLine($"    partial class {type.Name}")
                    .AppendLine("    {");

                foreach (var method in asyncMethods)
                {
                    var accessibility = method.DeclaredAccessibility.ToString().ToLowerInvariant();
                    var staticness = method.IsStatic ? "static" : string.Empty;
                    var returnType = ((INamedTypeSymbol) method.ReturnType).TypeArguments.SingleOrDefault()
                        ?.ToDisplayString() ?? "void";
                    var name = method.Name.TrimEnd("Async");
                    var parameters = method.Parameters
                        .Select(x => $"{x.Type} {x.Name}")
                        .Join(", ");
                    var arguments = method.Parameters.Select(x => x.Name).Join(", ");
                    var statementPrefix = returnType != "void" ? "return " : string.Empty;

                    sourceBuilder
                        .AppendLine($"         {accessibility} {staticness} {returnType} {name}({parameters})")
                        .AppendLine("         {")
                        .AppendLine($"             {statementPrefix}{method.Name}({arguments}).GetAwaiter().GetResult();")
                        .AppendLine("         }");
                }

                sourceBuilder
                    .AppendLine("    }")
                    .AppendLine("}");
            }


            sourceBuilder.AppendLine(
                $"public static partial class Hello {{ public static void World2() {{ System.Console.WriteLine(\"Hello!\"); }} }}");
            var source = SourceText.From(sourceBuilder.ToString(), Encoding.UTF8);
            context.AddSource(nameof(SyncMethodGenerator), source);
            // File.WriteAllText("output.txt", sourceBuilder.ToString());
        }
    }
}
