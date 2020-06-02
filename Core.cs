using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.EC;

namespace Zen
{
    public class Core : Game
    {
        public static new GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        GraphicsDeviceManager _graphicsManager;
        Machine _machine;

        public Machine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                _machine.Content = Content;
                _machine.Init(this);
            }
        }

        public Core(int width = 1920, int height = 1080, bool isFullScreen = false)
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphicsManager = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = width,
				PreferredBackBufferHeight = height,
				IsFullScreen = isFullScreen
			};

            Screen.Initialize(_graphicsManager);
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
