using System;
using System.Collections.Generic;

namespace SQMReorderer
{
    public class StringListComparer
    {
        public ComparisonResult Compare(List<string> list1, List<string> list2)
        {
            var maxSize = Math.Max(list1.Count, list2.Count);

            for (int i = 0; i < maxSize; i++)
            {
                if(list1.Count == i)
                {
                    return CreateErrorResult(i, "END_OF_LIST", list2[i]);
                }

                if (list2.Count == i)
                {
                    return CreateErrorResult(i, list1[i], "END_OF_LIST");
                }

                if (list1[i] != list2[i])
                {
                    return CreateErrorResult(i, list1[i], list2[i]);
                }
            }

            var result = new ComparisonResult();
            result.IsSame = true;

            return result;
        }

        private static ComparisonResult CreateErrorResult(int i, string errorRowInList1, string errorRowInList2)
        {
            var result = new ComparisonResult();

            result.IsSame = false;
            result.ErrorRowNumber = i;
            result.ErrorRowInList1 = errorRowInList1;
            result.ErrorRowInList2 = errorRowInList2;

            return result;
        }
    }
}