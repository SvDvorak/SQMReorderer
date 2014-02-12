using System.Collections.Generic;
using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    public class StreamToStringsReader : IStreamToStringsReader
    {
        public List<string> Read(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            var missionText = new List<string>();

            while (!streamReader.EndOfStream)
            {
                missionText.Add(streamReader.ReadLine());
            }

            return missionText;
        }
    }
}