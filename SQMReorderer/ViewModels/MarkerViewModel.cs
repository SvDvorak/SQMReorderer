using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Gui.ViewModels
{
    public class MarkerViewModel
    {
        private readonly Marker _marker;

        public MarkerViewModel(Marker marker)
        {
            _marker = marker;
        }

        public string Text
        {
            get { return _marker.Text; }
            set { _marker.Text = value; }
        }

        public string Name
        {
            get { return _marker.Name; }
            set { _marker.Name = value; }
        }
    }
}