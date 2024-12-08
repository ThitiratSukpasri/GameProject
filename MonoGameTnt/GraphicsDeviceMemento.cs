using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class GraphicsDeviceMemento
    {
        RasterizerState rasterizerState;
        DepthStencilState depthStencilState;
        BlendState blendState;
        SamplerState samplerState;

        public GraphicsDeviceMemento(GraphicsDevice device)
        {
            SaveStates(device);
        }
        public void SaveStates(GraphicsDevice device)
        {
            rasterizerState = device.RasterizerState;
            depthStencilState = device.DepthStencilState;
            blendState = device.BlendState;
            samplerState = device.SamplerStates[0];
        }

        public void RestoreStates(GraphicsDevice device)
        {
            device.RasterizerState = rasterizerState;
            device.DepthStencilState = depthStencilState;
            device.BlendState = blendState;
            device.SamplerStates[0] = samplerState;
        }

        // from MyGame2D
        //GraphicsDevice.RasterizerState = RasterizerState.CullNone;    // For flipping Y Axis
        //GraphicsDevice.DepthStencilState = DepthStencilState.Default; // Z-buffer may not be relevant
        //GraphicsDevice.BlendState = BlendState.AlphaBlend;            // for transpancy (e.g. in png)
        //GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;


    }
}
