using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThanaNita.MonoGameTnt
{
    // จะจอง Buffer เท่ากับ Size ที่ระบุ ปัดเศษขึ้น
    // หากจะทำ pixel art game แบบเขียนลง buffer ก่อน ก็แค่ set scale ให้ใหญ่ขึ้น
    // Todo: ต้อง crop การเช็ค mouse event ของ Children ด้วย
    public class CropActor : Actor
    {
        Vector2 Size { get; }
        RenderTarget2D offScreen = null;
        GraphicsDevice device;
        SpriteDrawable sprite;
        EffectAdapter adapter;

        public CropActor(Vector2 size, EffectAdapter adapter, GraphicsDevice device = null)
        {
            Size = size;
            this.device = device ?? GlobalGraphicsDevice.Value;
            this.adapter = adapter;
        }

        public override void Draw(DrawTarget target, DrawState state)
        {
            var local = this.GetMatrix();
            var combine = state.Combine(local, (ColorF)Color);
            CheckCreateOffScreen();

            target.Flush();
            var viewMemento = new ViewMemento(adapter, device);
            device.SetRenderTarget(offScreen); // device.Viewport ก็จะเปลี่ยนไปด้วย เช่นเป็น(0,0,180,180)
            device.Clear(Color.Transparent);
            adapter.ViewMatrix = Matrix.Identity;
            adapter.ProjectionMatrixRecalculate();

            base.DrawChildren(target, DrawState.Identity); // Children draw into an off-screen buffer

            target.Flush();
            device.SetRenderTarget(null);
            viewMemento.Restore(adapter, device);

            sprite.Draw(target, combine);
        }

        private class ViewMemento
        {
            Matrix viewMatrix;
            Matrix projection;
            Viewport viewport;
            public ViewMemento(EffectAdapter adapter, GraphicsDevice device)
            {
                viewMatrix = adapter.ViewMatrix;
                projection = adapter.ProjectionMatrix;
                viewport = device.Viewport;
            }
            public void Restore(EffectAdapter adapter, GraphicsDevice device)
            {
                adapter.ViewMatrix = viewMatrix;
                adapter.ProjectionMatrix = projection;
                device.Viewport = viewport;   // เพราะเมื่อปรับไปมา ทำให้ Viewport เพี้ยนไป ไม่ถูก boxing
            }
        }

        private void CheckCreateOffScreen()
        {
            if (offScreen != null)
                return;

            // combine.

            offScreen = new RenderTarget2D(device, (int)MathF.Ceiling(Size.X), (int)MathF.Ceiling(Size.Y),
                                            false, SurfaceFormat.Color, DepthFormat.None);
            sprite = new SpriteDrawable(offScreen);
        }
    }
}
