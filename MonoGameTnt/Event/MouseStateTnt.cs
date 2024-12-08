using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace ThanaNita.MonoGameTnt
{
    public struct MouseStateTnt
    {
        public MouseState State { get; }
        public Vector2 ScreenPosition { get; }
        public Vector2 WorldPosition { get; }   // ใช้ได้แม้กล้องจะ rotate และ flip แกน Y

        public MouseStateTnt(MouseState state, GraphicsDevice device, OrthographicCamera camera)
        {
            State = state;
            ScreenPosition = state.GetScreenPosition(device);
            WorldPosition = camera.ScreenToWorld(ScreenPosition);
        }
    }
}
