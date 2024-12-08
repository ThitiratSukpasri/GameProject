using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class OnScreenLogger : Actor, Logger
    {
        private Text text;
        private LogList logList;

        public OnScreenLogger(int maxLine, string fontName, float fontSize, Color fontColor)
        {
            logList = new LogList(maxLine);
            text = new Text(fontName, fontSize, fontColor, "");

            Add(text);
        }
        public void Log(string message)
        {
            logList.Log(message);
            text.Str = logList.GetCombined();
        }
    }
}
