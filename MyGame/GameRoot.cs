using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class GameRoot : Game
    {
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public SpriteBatch spriteBatch;
        public EnemyManager EnemyManager;
        public Player player;
        public HUD hud;
        
        
        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;
            Window.Title = "KILL THE IMPOSTOR!";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
        }

        protected override void Initialize()
        {
            this.player = new Player(this);
            this.EnemyManager = new EnemyManager(this);
            this.hud = new HUD(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.player.Update(gameTime, kState);
            this.EnemyManager.Update(gameTime);
            this.hud.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(69, 34, 86));
            this.spriteBatch.Begin();

            this.player.Draw();
            this.EnemyManager.Draw();
            this.hud.Draw();

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}