using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame 
{
    public class GameRoot : Game
    {
        private Song music;
        private HUD hud;
        private Menu menu;
        private HealthPackManager healthPackManager;
        private StarManager starManager;
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public SpriteBatch spriteBatch;
        public EnemyManager enemyManager;
        public Player player;

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
            this.enemyManager = new EnemyManager(this);
            this.hud = new HUD(this);
            this.healthPackManager = new HealthPackManager(this);
            this.menu = new Menu(this);
            this.starManager = new StarManager(this);
            this.Music();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            if (this.menu.isMenuActive)
            {
                this.menu.Update(gameTime);
            }
            
            if (!this.menu.isMenuActive)
            {
                if (kState.IsKeyDown(Keys.Escape))
                {
                    this.menu.isMenuActive = true;
                }
                
                this.starManager.Update(gameTime);
                this.player.Update(gameTime, kState);
                this.enemyManager.Update(gameTime);
                this.hud.Update(gameTime);
                this.healthPackManager.Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();

            if (this.menu.isMenuActive)
            {
                this.menu.Draw();
            }

            if (!this.menu.isMenuActive)
            {
                GraphicsDevice.Clear(new Color(0, 0, 5));
                this.starManager.Draw();
                this.healthPackManager.Draw();
                this.player.Draw();
                this.enemyManager.Draw();
                this.hud.Draw();
            }
            
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void Music()
        {
            this.music = this.content.Load<Song>("sprites/Shadilay");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(this.music);
        }

        public void ResetGame()
        {
            this.menu.isMenuActive = true;
            this.player = new Player(this);
            this.enemyManager = new EnemyManager(this);
            this.hud = new HUD(this);
            MediaPlayer.Play(this.music);
        }
    }
}