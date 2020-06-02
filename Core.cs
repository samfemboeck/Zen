using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.EC;
using Zen.Util;

namespace Zen
{
    public abstract class Core : Game
    {
        public new static GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        private Machine _machine;
        private TimerManager _timerManager = new TimerManager();

        protected Machine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                _machine.Content = Content;
                _machine.Init(this);
            }
        }

        protected Core(int width = 1920, int height = 1080, bool isFullScreen = false)
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
            GraphicsDevice = base.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
            Start();
        }

        public virtual void Start() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            _timerManager.Update();
            
            Input.Update();
            
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
