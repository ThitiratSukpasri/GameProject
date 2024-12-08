using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class BatchSplitter : Actor
    {
        private GraphicsDeviceMemento memento;
        private GraphicsDeviceConfig device;
        public BatchSplitter(GraphicsDeviceConfig device)
        {
            this.device = device;
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            // ไม่จำเป็นต้องเรียก flush โดยตรงแล้ว เพราะเมื่อใด DeviceConfig มีการปรับ state จะแจ้งไปที่ Batch
            // แล้ว Batch จะ flush ข้อมูลออกก่อน
            //batch?.Flush(); 
            ChangeStates();

            base.Draw(target, state); // Draw Children

            //batch?.Flush();
            RestoreStates();
        }
        private void ChangeStates()
        {
            device.SetSamplerState(SamplerState.AnisotropicWrap);
        }

        protected virtual void RestoreStates()
        {
            device.ResetSamplerState();
        }
    }
}
