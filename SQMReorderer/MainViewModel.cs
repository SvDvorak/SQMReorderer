using System.Collections.Generic;
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

            Groups = parseResult.Mission.Groups;
        }

        public List<Item> Groups { get; set; }

        public Item SelectedItem { get; set; }
    }
}