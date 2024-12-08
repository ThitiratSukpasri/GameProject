using Microsoft.Xna.Framework;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

public class CameraMan : Actor
{
    OrthographicCamera camera;
    Vector2 screenSize;

    public CameraMan(OrthographicCamera camera, Vector2 screenSize)
    {
        this.camera = camera;
        this.screenSize = screenSize;
    }

    public override void Act(float deltaTime)
    {
        base.Act(deltaTime);

        var myGlobalPosition = Parent.GlobalTransform.Transform(Position);
        camera.Position = myGlobalPosition - screenSize / 2;
    }
}
