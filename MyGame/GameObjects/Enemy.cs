using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public abstract class Enemy
    {
        protected GameRoot game;
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public float healthPoints;

        protected Enemy(GameRoot game, Vector2 position)
        {
            this.game = game;
            this.position = position;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
        protected abstract void Movement(GameTime gameTime);
    }
}