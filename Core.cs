using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.Util;

namespace Zen
{
    public abstract class Core : Game
    {
        public new static GraphicsDevice GraphicsDevice;
        public static Zen.Batching.Batcher Batcher;
        private Machine _machine;
        private TimerManager _timerManager = TimerManager.Instance;

        protected Machine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                _machine.Init(this);
            }
        }

        public Core(int width = 1920, int height = 1080, bool isFullScreen = false)
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var graphicsDeviceManager = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = width,
				PreferredBackBufferHeight = height,
				IsFullScreen = isFullScreen
			};

            Screen.Initialize(graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            base.Initialize();
            GraphicsDevice = base.GraphicsDevice;
            Batcher = new Zen.Batching.Batcher(GraphicsDevice);
            Start();
        }

        public abstract void Start();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            _timerManager.Update();
            
            Machine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Machine.Draw();
            base.Draw(gameTime);
        }
    }
}
