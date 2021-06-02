using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class HealthPack
    {
        private GameRoot game;
        private float backfallSpeed;
        public Vector2 position;
        public Texture2D texture;
        public Rectangle hitbox;

        public HealthPack(GameRoot game, Vector2 position)
        {
            this.game = game;
            this.position = position;
            this.texture = this.game.content.Load<Texture2D>("sprites/items/health");
            this.backfallSpeed = 60.0f;
        }

        public void Update(GameTime gameTime)
        {
            this.hitbox = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
            this.BackFall(gameTime);
        }

        public void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }
        
        private void BackFall(GameTime gTime)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= this.backfallSpeed * deltaTime;
        }
    }
}