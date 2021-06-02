using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Menu
    {
        private GameRoot game;
        private Texture2D button;
        private SpriteFont font;
        private MouseState mouseState;
        private Rectangle mouseHitbox;
        private Vector2[] buttonPos;
        private Rectangle[] buttonHitbox;
        public bool isMenuActive;

        public Menu(GameRoot game)
        {
            this.game = game;
            this.isMenuActive = true;
            this.button = this.game.content.Load<Texture2D>("sprites/button");
            this.font = this.game.content.Load<SpriteFont>("sprites/font");
            
            this.buttonPos = new Vector2[]
            {
                new Vector2(480, 300),
                new Vector2(480, 420)
            };
            
            this.buttonHitbox = new Rectangle[]
            {
                new Rectangle((int) this.buttonPos[0].X,(int) this.buttonPos[0].Y, this.button.Width, this.button.Height),
                new Rectangle((int) this.buttonPos[1].X,(int) this.buttonPos[1].Y, this.button.Width, this.button.Height)
            };
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            this.mouseState = Mouse.GetState();
            this.mouseHitbox = new Rectangle(this.mouseHitbox.X, this.mouseState.Y, 10, 10);

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.isMenuActive = true;
            }
            
            if (this.mouseState.X > this.buttonPos[0].X && this.mouseState.X < this.buttonPos[0].X + this.button.Width && this.mouseState.Y > this.buttonPos[0].Y 
                && this.mouseState.Y < this.buttonPos[0].Y + this.button.Height && mouseState.LeftButton == ButtonState.Pressed)
            {
                this.isMenuActive = false;
            }

            if (this.mouseState.X > this.buttonPos[1].X && this.mouseState.X < this.buttonPos[1].X + this.button.Width && this.mouseState.Y > this.buttonPos[1].Y 
                && this.mouseState.Y < this.buttonPos[1].Y + this.button.Height && mouseState.LeftButton == ButtonState.Pressed)
            {
                this.game.Exit();
            }
        }

        public void Draw()
        {
            if (this.isMenuActive)
            {
                this.game.GraphicsDevice.Clear(new Color(0, 86, 86));
                this.game.spriteBatch.Draw(this.button, this.buttonHitbox[0], Color.White);
                this.game.spriteBatch.Draw(this.button, this.buttonHitbox[1], Color.White);
            
                this.game.spriteBatch.DrawString(this.font, "Play", new Vector2(this.buttonPos[0].X + 80, this.buttonPos[0].Y + 40), Color.Black);
                this.game.spriteBatch.DrawString(this.font, "Exit", new Vector2(this.buttonPos[1].X + 80, this.buttonPos[1].Y + 40), Color.Black);
            }
        }
    }
}