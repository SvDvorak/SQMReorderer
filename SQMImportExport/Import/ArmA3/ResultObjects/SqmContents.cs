namespace SQMImportExport.Import.ArmA3.ResultObjects
{
    public class SqmContents : ISqmContents
    {
        public int? Version { get; set; }

        public MissionState Mission { get; set; }
        public MissionState Intro { get; set; }
        public MissionState OutroWin { get; set; }
        public MissionState OutroLose { get; set; }

        public void Accept(ISqmContentsVisitor visitor)
        {
            visitor.Visit(this);
        }

        public T Accept<T>(ISqmContentsVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}