using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public abstract class Enemy
    {
        public GameRoot game;
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public int healthpoints;

        public Enemy(GameRoot game, Vector2 position)
        {
            this.game = game;
            this.position = position;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

        protected void Movement(GameTime gameTime)
        {
            float movementSpeed = this.game.EnemyManager.movementSpeed;
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= movementSpeed * deltaTime;
        }
    }
}