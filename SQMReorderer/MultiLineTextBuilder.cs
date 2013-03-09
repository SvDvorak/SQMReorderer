namespace SQMReorderer
{
    public class MultiLineTextBuilder
    {
        private string _text = "";

        public void AddLine(string line)
        {
            if(line == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_text))
            {
                _text += "\n";
            }

            _text += line;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}