using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Boss : Enemy
    {
        public int healthPoints;
        private int sizeMultiplier;
        private float movementSpeed;
        
        public Boss(Vector2 position, GameRoot game) : base(game, position)
        {
            this.texture = this.game.content.Load<Texture2D>("sprites/enemies/among-us-red");
            this.sizeMultiplier = 4;
            this.healthPoints = 100;
            this.movementSpeed = 80.0f;
        }

        public override void Update(GameTime gameTime)
        {
            this.hitbox = new Rectangle(
                (int) this.position.X,
                (int) this.position.Y,
                this.texture.Width * this.sizeMultiplier,
                this.texture.Height * this.sizeMultiplier
            );
            this.BossMovement(gameTime);
        }

        public override void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }

        private void BossMovement(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.TotalGameTime.TotalMilliseconds;
            this.position.X -= this.movementSpeed * deltaTime;
        }
    }
}