using System.Collections.Generic;
using System.IO;

namespace SQMReorderer
{
    public class FileToStringsReader : IFileToStringsReader
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

        public List<string> Read(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}