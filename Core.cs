using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.Util;

namespace Zen
{
    public abstract class Core : Game
    {
        public new static GraphicsDevice GraphicsDevice;
        public static BuddhaBatcher Batcher;
        public static SpriteBatch SpriteBatch;
        public IRenderer Renderer { get; protected set; }

        public Core(int width = 1600, int height = 1200, bool isFullScreen = false)
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
            Batcher = new BuddhaBatcher(GraphicsDevice);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Renderer = new DefaultRenderer();
            ContentLoader.Init(Content);
            Physics.Init(200);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            TimerManager.Update();
            EntityManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            Renderer.Draw();

            base.Draw(gameTime);
        }
    }
}
