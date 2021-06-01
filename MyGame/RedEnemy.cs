using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class RedEnemy : Enemy
    {
        public RedEnemy(GameRoot game, Vector2 position) : base(game, position)
        {
            this.texture = this.game.content.Load<Texture2D>("sprites/enemies/among-us-red");
        }

        public override void Update(GameTime gameTime)
        {
            this.hitbox = new Rectangle(
                (int) this.position.X,
                (int) this.position.Y,
                this.texture.Width,
                this.texture.Height
            );
            this.Movement(gameTime);
        }

        public override void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }

        protected override void Movement(GameTime gameTime)
        {
            float movementSpeed = this.game.enemyManager.movementSpeed;
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= movementSpeed * deltaTime;
        }
    }
}