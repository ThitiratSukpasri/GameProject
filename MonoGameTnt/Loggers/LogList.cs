using System.Collections.Generic;
using System.Text;

namespace ThanaNita.MonoGameTnt
{
    public class LogList
    {
        private int size;
        private List<string> list = new List<string>();

        public LogList(int size)
        {
            this.size = size;
        }
        public void Log(string message)
        {
            list.Add(message);
            TrimHead();
        }

        private void TrimHead()
        {
            if (list.Count > size)
                list.RemoveAt(0);
        }

        public string GetCombined()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; ++i)
            {
                sb.Append(list[i]);
                if (i < list.Count - 1)
                    sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
