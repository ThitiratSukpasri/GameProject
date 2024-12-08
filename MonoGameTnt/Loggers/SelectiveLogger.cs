using System.Collections.Generic;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class SelectiveLogger
    {
        private int count = 0;

        private static Dictionary<string, SelectiveLogger> dict = new Dictionary<string, SelectiveLogger>();
        public static SelectiveLogger Get(string key)
        {
            SelectiveLogger value;
            if(dict.TryGetValue(key, out value))
                return value;

            value = new SelectiveLogger();
            dict.Add(key, value);
            return value;
        }

        private SelectiveLogger()
        {
        }

        public void LogFirstTime(string message)
        {
            if (count >= 1)
                return;

            Debug.WriteLine(message);
            count++;
        }

        public void LogNTime(int times, string message)
        {
            if (count >= times)
                return;

            Debug.WriteLine(message);
            count++;
        }
    }

    public class FirstTimeLogger
    {
        public static void Log(string key, string message)
        {
            SelectiveLogger.Get(key).LogFirstTime(message);
        }
    }
}
