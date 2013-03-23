using System.Text;

namespace SQMReorderer
{
    public class MultiLineTextBuilder
    {
        private readonly StringBuilder _text = new StringBuilder();

        public void AddLine(string line)
        {
            if(line == null)
            {
                return;
            }

            if (_text.Length != 0)
            {
                _text.Append("\n");
            }

            _text.Append(line);
        }

        public override string ToString()
        {
            return _text.ToString();
        }
    }
}