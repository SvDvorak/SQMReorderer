using System.Collections.Generic;
using System.Text;

namespace SQMReorderer.Core.SqmExport
{
    public class SqmPropertyVisitor : ISqmPropertyVisitor
    {
        public string Visit(string propertyName, string value)
        {
            if (value == null)
            {
                return "";
            }

            return propertyName + "=\"" + value + "\";\n";
        }

        public string Visit(string propertyName, Vector value)
        {
            if(value == null)
            {
                return "";
            }

            return propertyName + "[]={" +
                value.X.ToStringInvariant() + "," +
                value.Y.ToStringInvariant() + "," +
                value.Z.ToStringInvariant() + "};\n";
        }

        public string Visit(string propertyName, int? nullableValue)
        {
            if (!nullableValue.HasValue)
            {
                return "";
            }

            return propertyName + "=" + nullableValue.Value + ";\n";
        }

        public string Visit(string propertyName, double? nullableValue)
        {
            if (!nullableValue.HasValue)
            {
                return "";
            }

            return propertyName + "=" + nullableValue.Value.ToStringInvariant() + ";\n";
        }

        public string Visit(string propertyName, List<int> intItems)
        {
            if (intItems == null || intItems.Count == 0)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(propertyName);
            stringBuilder.Append("[]={");

            for (int i = 0; i < intItems.Count; i++)
            {
                stringBuilder.Append(intItems[i]);

                var isLastItem = i != intItems.Count - 1;
                if (isLastItem)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string Visit(string propertyName, List<string> stringItems)
        {
            if (stringItems == null || stringItems.Count == 0)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(propertyName);
            stringBuilder.Append("[]=\n{\n");

            for (int i = 0; i < stringItems.Count; i++)
            {
                stringBuilder.Append("\"");
                stringBuilder.Append(stringItems[i]);
                stringBuilder.Append("\"");

                var isLastItem = i != stringItems.Count - 1;
                if (isLastItem)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("\n");
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }
    }
}
