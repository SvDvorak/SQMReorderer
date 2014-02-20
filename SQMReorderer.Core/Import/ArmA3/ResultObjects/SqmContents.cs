namespace SQMReorderer.Core.Import.ArmA3.ResultObjects
{
    public class SqmContents
    {
        public int? Version { get; set; }

        public MissionState Mission { get; set; }
        public MissionState Intro { get; set; }
        public MissionState OutroWin { get; set; }
        public MissionState OutroLose { get; set; }
    }
}