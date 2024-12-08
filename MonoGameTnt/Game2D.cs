using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public abstract class Game2D : Game
    {
        protected MouseInfo MouseInfo { get; set; }
        protected KeyboardInfo KeyboardInfo { get; set; }
        protected OrthographicCamera Camera { get; set; }
        protected GraphicsDeviceManager GraphicsManager { get; set; }

        // for Drawing
        protected Actor All { get; set; } = new Actor();
        protected EffectAdapter Adapter { get; set; }
        protected GenericBatch Batch { get; private set; }


        // Set any time
        protected Keys? ToggleFullScreenKey { get; set; } = Keys.F12;
        protected Keys? ExitKey { get; set; } = Keys.Escape;
        protected Color ClearColor { get; set; } = Color.Black;
        protected Color? BackgroundColor 
        {
            get => _backgroundColor;
            set { _backgroundColor = value; CreateBackgroundRect(); }
        }
        private Color? _backgroundColor = Color.CornflowerBlue;
        private RectangleDrawable background;

        // Initialized by the constructor
        protected ViewportAdapterTypes ViewportAdapterType { get; }
        protected Vector2 ScreenSize { get; }
        protected Vector2 PreferredWindowSize { get; }
        protected bool StartAsFullScreen { get; }
        public GraphicsDeviceConfig Config { get; private set; }
        public CollisionDetection CollisionDetectionUnit { get; private set; } = new CollisionDetection();

        public enum ViewportAdapterTypes { Boxing, Default, Scaling };

        public Game2D(ViewportAdapterTypes viewportAdapterType = ViewportAdapterTypes.Boxing, 
                        Vector2? virtualScreenSize = null,
                        Vector2? preferredWindowSize = null,
                        bool startAsFullScreen = false,
                        bool geometricalYAxis = false,
                        bool directLoadTextureCache = true)
        {
            GlobalConfig.GeometricalYAxis = geometricalYAxis;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GraphicsManager = new GraphicsDeviceManager(this);
            SetPreserveRenderTargetContent();

            // set parameters
            this.StartAsFullScreen = startAsFullScreen;
            this.ViewportAdapterType = viewportAdapterType;
            this.ScreenSize = virtualScreenSize ?? new Vector2(1920, 1080);
            this.PreferredWindowSize = preferredWindowSize ?? new Vector2(1920, 1080);
            TextureCache.Init(GraphicsDevice, Content, directLoadTextureCache);
        }

        private void SetPreserveRenderTargetContent()
        {
            GraphicsManager.PreparingDeviceSettings += (object s, PreparingDeviceSettingsEventArgs args) =>
            {
                args.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            };
        }

        protected override void Initialize()
        {
            SetFullScreen(StartAsFullScreen);
            SetDefaultGraphicsStates();
            InitStatic();
            CameraSetup();
            CreateBatch();
            CreateBackgroundRect();
            MouseInfo = new MouseInfo(GraphicsDevice, Camera);
            KeyboardInfo = new KeyboardInfo();
            Window.KeyDown += Game2D_KeyDown;
            //Window.KeyUp += Game2D_KeyUp;
            //Window.TextInput += Game2D_TextInput;
            CreateGlobal();

            base.Initialize(); // This method will call LoadContent().
        }
        protected virtual void Game2D_KeyDown(object sender, InputKeyEventArgs e)
        {
            //KeyboardInfo.OnKeyPressed(e.Key);

            if (ToggleFullScreenKey != null && e.Key == ToggleFullScreenKey.Value)
                ToggleFullScreen();

            if (ExitKey != null && e.Key == ExitKey)
                this.Exit();

            DispatchKeyEvents(sender, e);
        }
        private void DispatchKeyEvents(object sender, InputKeyEventArgs e)
        {

        }

/*        private void Game2D_KeyUp(object sender, InputKeyEventArgs e)
        {
            KeyboardInfo.OnKeyReleased(e.Key);
        }

        private void Game2D_TextInput(object sender, TextInputEventArgs e)
        {
            KeyboardInfo.OnTextInput(e.Key, e.Character);
        }*/

        private void CreateGlobal()
        {
            GlobalGraphicsDevice.Value = GraphicsDevice;
            GlobalMouseInfo.Value = MouseInfo;
            GlobalKeyboardInfo.Value = KeyboardInfo;
            GlobalGraphicsDeviceConfig.Value = Config;
        }

        private void InitStatic()
        {
            TextureCache.Init(GraphicsDevice, Content, directLoad: true);
        }

        protected virtual void SetDefaultGraphicsStates()
        {
            GraphicsDevice.RasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None,   // For flipping Y Axis
                // ScissorTestEnable = true    // Currently Not Use. For Clipping Text in UI Using Scissor Rectangle
            };

            GraphicsDevice.DepthStencilState = DepthStencilState.Default; // Z-buffer may not be relevant

            // for transpancy (e.g. in png)
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //GraphicsDevice.BlendState = BlendStateCustom.Tinting;

            // for crisp pixel art and tiled texture
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
        }

        protected virtual void CameraSetup()
        {
            // camera
            var viewportAdapter = CreateViewportAdapter();
            Camera = new OrthographicCamera(viewportAdapter);
            Camera.Zoom = 1.0f;
            viewportAdapter.Reset();
        }

        private void CreateBatch()
        {
            Adapter = new SpriteEffectAdapter(GraphicsDevice);
            Batch = new GenericBatch(Adapter);
            Config = new GraphicsDeviceConfig(GraphicsDevice, Batch);
        }

        private void CreateBackgroundRect()
        {
            if (BackgroundColor != null)
                background = new RectangleDrawable(BackgroundColor.Value,
                                                new RectF(0, 0, ScreenSize.X, ScreenSize.Y));
            else
                background = null;
        }

        protected virtual ViewportAdapter CreateViewportAdapter()
        {
            if (ViewportAdapterType == ViewportAdapterTypes.Boxing)
                return new BoxingViewportAdapter(Window, GraphicsDevice, (int)ScreenSize.X, (int)ScreenSize.Y);
            else if (ViewportAdapterType == ViewportAdapterTypes.Default)
                return new DefaultViewportAdapter(GraphicsDevice);
            else if (ViewportAdapterType == ViewportAdapterTypes.Scaling)
                return new ScalingViewportAdapter(GraphicsDevice, (int)ScreenSize.X, (int)ScreenSize.Y);
            else
            {
                Debug.Assert(false, "Unknown ViewportAdapterTypes : " + ViewportAdapterType);
                return new DefaultViewportAdapter(GraphicsDevice);
            }
        }



        // ถ้าจะ override ตัวนี้ ต้องเรียก base.Update() ก่อนเลย เพราะจะเรียก MouseInfo.Update() ก่อน
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseInfo.Update();
            KeyboardInfo.Update();

            float deltaTime = CalcDeltaTime(gameTime);
            Update(deltaTime);
            //Act(deltaTime);
            All?.Act(deltaTime);
            AfterAct();
            CollisionDetectionUnit.DetectAndResolve(All);
            AfterUpdateAndCollision();
        }

        protected virtual void AfterAct()
        {
        }
        protected virtual void AfterUpdateAndCollision()
        {
        }

        private float CalcDeltaTime(GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //[Obsolete("Use Act(deltaTime) instead.")]
        protected virtual void Update(float deltaTime) { }
        // protected virtual void Act(float deltaTime) { }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(ClearColor);

            Adapter.ViewMatrix = Camera.GetViewMatrix();

            DrawBackground();
            DrawAllActors();

            DrawAdditional(CalcDeltaTime(gameTime));
        }

        private void DrawBackground()
        {
            if (background == null)
                return;

            Config.SetBlendState(BlendState.Opaque);

            Batch.Begin();
            background.Draw(Batch, DrawState.Identity);
            Batch.End();

            Config.ResetBlendState();
        }

        private void DrawAllActors()
        {
            if (All == null)
                return;

            Batch.Begin();
            All.Draw(Batch, DrawState.Identity);
            Batch.End();
            //Debug.WriteLine($"batch count = {batch.BatchCount}");
        }

        protected virtual void DrawAdditional(float deltaTime) 
        { 
        }


        //******************** Toggle Full Screen **********************************************

        //https://community.monogame.net/t/get-the-actual-screen-width-and-height-on-windows-10-c-monogame/10006/2
        protected void ToggleFullScreen()
        {
            SetFullScreen(!GraphicsManager.IsFullScreen);
        }
        protected void SetFullScreen(bool bFullScreen)
        {
            if (bFullScreen)
            {
                int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                GraphicsManager.IsFullScreen = true;
                GraphicsManager.HardwareModeSwitch = false; // https://community.monogame.net/t/how-to-implement-borderless-fullscreen-on-desktopgl-project/8359
                GraphicsManager.PreferredBackBufferWidth = w;
                GraphicsManager.PreferredBackBufferHeight = h;
                GraphicsManager.ApplyChanges();
            }
            else
            {
                GraphicsManager.IsFullScreen = false;
                GraphicsManager.PreferredBackBufferWidth = (int)PreferredWindowSize.X;
                GraphicsManager.PreferredBackBufferHeight = (int)PreferredWindowSize.Y;
                GraphicsManager.ApplyChanges();
            }
            Window.AllowUserResizing = true;
        }
    }
}
