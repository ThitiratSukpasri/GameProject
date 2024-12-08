using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThanaNita.MonoGameTnt
{
    public static class MouseStateExtension
    {
        // Idea from: MonoGame Extended
        public static bool IsButtonDown(this MouseState state, MouseButtons button)
        {
            return button switch
            {
                MouseButtons.Left => state.LeftButton == ButtonState.Pressed,
                MouseButtons.Right => state.RightButton == ButtonState.Pressed,
                MouseButtons.Middle => state.MiddleButton == ButtonState.Pressed,
                _ => false,
            };
        }
        public static bool IsButtonUp(this MouseState state, MouseButtons button)
        {
            return !IsButtonDown(state, button);
        }
        public static Vector2 GetScreenPosition(this MouseState state, GraphicsDevice device)
        {
            var pos = state.Position.ToVector2();
            if (GlobalConfig.GeometricalYAxis)
                pos.Y = 2 * device.Viewport.Y + device.Viewport.Height - pos.Y;  // กลับทิศ กรณีกลับแกน Y
            return pos;
        }
    }
}
