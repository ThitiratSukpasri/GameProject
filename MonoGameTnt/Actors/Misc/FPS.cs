using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class FPS : Label
    {
        float time = 0;
        int count = 0;
        public FPS()
            : base("consola.ttf", 30, Color.Blue, "")
        {
            UpdateFPS();
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            time += deltaTime;
            Debug.WriteLine(deltaTime);
            count++;
            if (time >= 1)
            {
                UpdateFPS();
                time = 0;
                count = 0;
            }
        }
        private void UpdateFPS()
        {
            Text = "FPS:" + count.ToString().PadLeft(4, ' ');
        }
    }
}
