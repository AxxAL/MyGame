using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Projectile
    {
        private GameRoot game;
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        private float velocity;

        public Projectile(Vector2 position, GameRoot game)
        {
            this.game = game;
            this.texture = this.game.content.Load<Texture2D>("sprites/projectiles/fire-blast");
            this.position = position;
            this.velocity = 8.0f;
        }
        
        public void Update(GameTime gameTime)
        {
            this.hitbox = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
            this.position.X += this.velocity;
        }

        public void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }
    }
}