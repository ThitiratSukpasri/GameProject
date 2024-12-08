using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// MOST of the code are DUPLICATED from the MonoGame SpriteEffect
// Thana Nitayarerk : Create SpriteEffect2 that can flip Y Axis

namespace ThanaNita.MonoGameTnt;

/// <summary>
/// The default effect used by SpriteBatch.
/// </summary>
public class SpriteEffect2 : SpriteEffect
{
    private EffectParameter _matrixParam;
    private Viewport _lastViewport;
    private Matrix _projection;

    // the Projection value may be recalculated on OnApply() when the Graphics Viewport is changed.
    public Matrix Projection {  get { return _projection; } set { _projection = value; } }

    public SpriteEffect2(GraphicsDevice device)
        : base(device)
    {
        CacheEffectParameters();
    }

    /// <summary>
    /// Looks up shortcut references to our effect parameters.
    /// </summary>
    void CacheEffectParameters()
    {
        _matrixParam = Parameters["MatrixTransform"];
    }

    /// <summary>
    /// Lazily computes derived parameter values immediately before applying the effect.
    /// </summary>
    protected override void OnApply()
    {
        var vp = GraphicsDevice.Viewport;
        if ((vp.Width != _lastViewport.Width) || (vp.Height != _lastViewport.Height))
        {
            // Normal 3D cameras look into the -z direction (z = 1 is in front of z = 0). The
            // sprite batch layer depth is the opposite (z = 0 is in front of z = 1).
            // --> We get the correct matrix with near plane 0 and far plane -1.
            ProjectionMatrixRecalculate(vp);

            if (GraphicsDevice.UseHalfPixelOffset)
            {
                _projection.M41 += -0.5f * _projection.M11;
                _projection.M42 += -0.5f * _projection.M22;
            }

            _lastViewport = vp;
        }

        if (TransformMatrix.HasValue)
            _matrixParam.SetValue(TransformMatrix.GetValueOrDefault() * _projection);
        else
            _matrixParam.SetValue(_projection);
    }

    public void ProjectionMatrixRecalculate()
    {
        ProjectionMatrixRecalculate(GraphicsDevice.Viewport);
    }
    private void ProjectionMatrixRecalculate(Viewport vp)
    {
        if (!GlobalConfig.GeometricalYAxis)
            Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0, -1, out _projection);
        else
            Matrix.CreateOrthographicOffCenter(0, vp.Width, 0, vp.Height, 0, -1, out _projection);
    }
}
