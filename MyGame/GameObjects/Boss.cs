using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Boss : Enemy
    {
        private int sizeMultiplier;
        private float movementSpeed;
        
        public Boss(Vector2 position, GameRoot game) : base(game, position)
        {
            this.texture = this.game.content.Load<Texture2D>("sprites/enemies/among-us-red");
            this.sizeMultiplier = 4;
            this.healthPoints = 100.0f;
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
            this.Movement(gameTime);
        }

        public override void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        }
        
        private enum Direction
        { GoingUp, GoingDown, GoingLeft, GoingRight }

        private Direction verticalDirection = Direction.GoingUp, horizontalDirection = Direction.GoingLeft;
        protected override void Movement(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (this.position.Y < 0)
                this.verticalDirection = Direction.GoingDown;

            if (this.position.Y > this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height)
                this.verticalDirection = Direction.GoingUp;

            if (this.position.X < 0)
                this.horizontalDirection = Direction.GoingRight;

            if (this.position.X > this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width)
                this.horizontalDirection = Direction.GoingLeft;

            switch (this.verticalDirection)
            {
                case Direction.GoingUp:
                    this.position.Y -= this.movementSpeed * deltaTime;
                    break;
                case Direction.GoingDown:
                    this.position.Y += this.movementSpeed * deltaTime;
                    break;
            }

            switch (this.horizontalDirection)
            {
                case Direction.GoingRight:
                    this.position.X += this.movementSpeed * deltaTime;
                    break;
                case Direction.GoingLeft:
                    this.position.X -= this.movementSpeed * deltaTime;
                    break;
            }
        }
    }
}