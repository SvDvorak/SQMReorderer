using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQMReorderer.MultiSelectTreeView;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.ResultObjects;
using SQMReorderer.ViewModels;

namespace SQMReorderer
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            var missionReader = new FileToStringsReader();
            var missionText = missionReader.Read("mission.sqm");
            SqmStream stream = new SqmStream(missionText);

            var sqmParser = new SqmParser.SqmParser();
            var parseResult = sqmParser.Parse(stream);

            var sqmViewModelCreator = new SqmViewModelCreator();
            Mission = sqmViewModelCreator.CreateMissionViewModel(parseResult.Mission);
        }

        public MissionViewModel Mission { get; set; }
    }
}