using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SQMReorderer.SqmExport
{
    public class SqmPropertyVisitor
    {
        public string Visit(string propertyName, string value)
        {
            return propertyName + "=\"" + value + "\";\n";
        }

        public string Visit(string propertyName, Vector value)
        {
            return propertyName + "[]={" + value.X + "," + value.Y + "," + value.Z + "};\n";
        }

        public string Visit(string propertyName, int? nullableValue)
        {
            return propertyName + "=" + nullableValue.Value + ";\n";
        }

        public string Visit(string propertyName, double? nullableValue)
        {
            return propertyName + "=" + nullableValue.Value.ToStringInvariant() + ";\n";
        }

        public string Visit(string propertyName, List<int> intItems)
        {
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
    }
}
