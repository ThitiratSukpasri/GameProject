using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class GraphicsDeviceConfig
    {
        private GraphicsDevice device;

        private BlendState defaultBlendState;
        private SamplerState defaultSamplerState;

        public interface ConfigListener
        {
            void ConfigChanged();
        }
        private ConfigListener configListener;

        public GraphicsDeviceConfig(GraphicsDevice device, ConfigListener listener)
        {
            this.device = device;
            this.defaultBlendState = device.BlendState;
            this.defaultSamplerState = device.SamplerStates[0];
            this.configListener = listener;
        }
        private void Changed()
        {
            configListener?.ConfigChanged();
        }
        public void SetBlendState(BlendState blendState)
        {
            Changed();
            device.BlendState = blendState;
        }
        public void ResetBlendState()
        {
            Changed();
            device.BlendState = defaultBlendState;
        }
        public void SetSamplerState(SamplerState samplerState)
        {
            Changed();
            device.SamplerStates[0] = samplerState;
        }
        public void ResetSamplerState()
        {
            Changed();
            device.SamplerStates[0] = defaultSamplerState;
        }
    }
}
