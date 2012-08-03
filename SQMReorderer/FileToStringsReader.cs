using System.Collections.Generic;
using System.IO;
using SQMReorderer.SqmParser;

namespace SQMReorderer
{
    public class FileToStringsReader
    {
        public List<string> Read(string fileName)
        {
            var streamReader = new StreamReader("mission.sqm");
            var missionText = new List<string>();

            while (!streamReader.EndOfStream)
            {
                missionText.Add(streamReader.ReadLine());
            }

            return missionText;
        }
    }
}