using System.Collections.Generic;
using System.IO;
using System.Windows;
using SQMReorderer.SqmParser;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            var sqmParser = new SqmParser.SqmParser();

            var streamReader = new StreamReader("mission.sqm");
            var missionText = new List<string>();

            while(!streamReader.EndOfStream)
            {
                missionText.Add(streamReader.ReadLine());
            }

            var parseResult = sqmParser.Parse(missionText);

            ViewModel = new MainViewModel();
            ViewModel.Groups = parseResult.Mission.Groups;

            InitializeComponent();
        }

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)DataContext; }
            set { DataContext = value; }
        }
    }

    public class MainViewModel
    {
        public List<Item> Groups { get; set; }
    }
}
