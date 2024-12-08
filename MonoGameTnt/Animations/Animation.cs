
namespace ThanaNita.MonoGameTnt
{
    public class Animation : Action
    {
        private SpriteActor sprite;
        private TextureRegion[] subtextures;
        private float durationStep;
        private float time = 0;
        public bool Running { get; set; } = true;
        public bool Repeat { get; set; } = true;
        public int CurrentIndex { get; private set; }

        public Animation(SpriteActor sprite, float duration, TextureRegion[] subtextures)
        {
            this.sprite = sprite;
            this.subtextures = subtextures;
            this.durationStep = duration / subtextures.Length;
            SetSubTexture(0); // เริ่มต้นมาก็ set ภาพแรกก่อนเลย
        }
        private void SetSubTexture(int index)
        {
            CurrentIndex = index;
            sprite.SetTextureRegion(subtextures[index]);
        }


        public void Restart()
        {
            time = 0;
            SetSubTexture(0);
        }
        public void Pause() { Running = false; }
        public void Play() { Running = true; }

        private void NormalizeTime()
        {
            float duration = durationStep * subtextures.Length;
            while (time >= duration)
                time -= duration;
        }

        public bool Act(float deltaTime)
        {
            if (!Running)
                return false;

            time += deltaTime;
            int i = (int)System.MathF.Floor(time / durationStep);
            if (i < subtextures.Length)
                SetSubTexture(i);
            else
            {
                if (!Repeat)
                {
                    SetSubTexture(subtextures.Length - 1); // set ซ้ำอีกที เผื่อว่า frame มันไวมากจนเรียกครั้งแรกก็เลยเวลาจบแล้ว
                    Running = false;
                }
                else
                {
                    NormalizeTime();
                    int j = (int)System.MathF.Floor(time / durationStep);
                    SetSubTexture(j);
                }
            }
            return false;
        }
        public bool IsFinished() => false;

    }
}
