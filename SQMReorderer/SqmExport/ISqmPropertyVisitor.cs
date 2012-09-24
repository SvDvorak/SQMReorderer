using System.Collections.Generic;

namespace SQMReorderer.SqmExport
{
    public interface ISqmPropertyVisitor
    {
        string Visit(string propertyName, string value);
        string Visit(string propertyName, Vector value);
        string Visit(string propertyName, int? nullableValue);
        string Visit(string propertyName, double? nullableValue);
        string Visit(string propertyName, List<int> intItems);
        string Visit(string propertyName, List<string> stringItems);
    }
}