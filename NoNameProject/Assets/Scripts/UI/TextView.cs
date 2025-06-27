using System;
using TMPro;

namespace UI
{
    public class TextView
    {
        public Action<string> TextChanged;

        private TextMeshProUGUI _text;

        public TextView(TextMeshProUGUI text)
        {
            _text = text;
        }

        public void TextChange(string text)
        {
            _text.text = text;

            TextChanged?.Invoke(_text.text);
        }
    }
}
