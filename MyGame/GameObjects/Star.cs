using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.GameObjects
{
    public class Star
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        private float speed;
        private GameRoot game;
        
        public Star(Vector2 position, GameRoot game)
        {
            this.game = game;
            this.position = position;
            this.texture = this.game.Content.Load<Texture2D>("sprites/star");
            this.speed = new Random().Next(30, 50);
        }

        public void Update(GameTime gameTime)
        {
            this.hitbox = new Rectangle(
                (int) this.position.X,
                (int) this.position.Y,
                this.texture.Width,
                this.texture.Height
            );
            this.Movement(gameTime);
        }

        public void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }

        private void Movement(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= this.speed * deltaTime;
        }
    }
}