using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace CustomGenerators.Utilities
{
    public static class CodeAnalysisExtensions
    {
        public static IEnumerable<INamespaceSymbol> GetAllNamespaces(this INamespaceSymbol namespaceSymbol)
        {
            return namespaceSymbol.DescendantsAndSelf(x => x.GetNamespaceMembers());
        }

        public static IEnumerable<ITypeSymbol> GetAllTypes(this INamespaceSymbol namespaceSymbol)
        {
            return namespaceSymbol
                .GetAllNamespaces()
                .SelectMany(x => x.GetTypeMembers())
                .SelectMany(x => x.DescendantsAndSelf(x => x.GetTypeMembers()));
        }

        public static string GetFullName(this INamespaceOrTypeSymbol namespaceOrTypeSymbol)
        {
            return namespaceOrTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
    }
}
