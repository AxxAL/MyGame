using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGame.GameObjects;
using MyGame.Managers;

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
            this.content = Content;
            IsMouseVisible = true;
            Window.Title = "You know what to do.";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
        }

        protected override void Initialize()
        {
            this.enemyManager = new EnemyManager(this);
            this.player = new Player(this);
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
            KeyboardState keyboardState = Keyboard.GetState();
            
            this.menu.Update(gameTime, keyboardState);
            
            if (!this.menu.isMenuActive)
            {
                this.starManager.Update(gameTime);
                this.enemyManager.Update(gameTime);
                this.player.Update(gameTime, keyboardState);
                this.hud.Update(gameTime);
                this.healthPackManager.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();
            this.menu.Draw();

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
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(this.music);
        }

        public void ResetGame()
        {
            this.menu.isMenuActive = true;
            this.enemyManager = new EnemyManager(this);
            this.player = new Player(this);
            this.hud = new HUD(this);
            MediaPlayer.Play(this.music);
        }
    }
}