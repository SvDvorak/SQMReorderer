namespace SQMReorderer.SqmParser.ResultObjects
{
    public class ParseResult
    {
        public int Version { get; set; }

        public MissionState Mission { get; set; }
        public MissionState Intro { get; set; }
        public MissionState OutroWin { get; set; }
        public MissionState OutroLoose { get; set; }
    }
}