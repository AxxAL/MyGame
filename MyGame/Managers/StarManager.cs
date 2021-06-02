using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class StarManager : List<Star>
    {
        private GameRoot game;
        private double timeSinceLastStarWasCreated;

        public StarManager(GameRoot game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gameTime);

                if (this[i].position.X < (0 - this[i].texture.Width))
                {
                    this.Remove(this[i]);
                }
            }
            
            if (gameTime.TotalGameTime.TotalMilliseconds > this.timeSinceLastStarWasCreated + 50)
            {
                this.Add(new Star(this.RandomPosition(), this.game));
                this.timeSinceLastStarWasCreated = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }
    
        private Vector2 RandomPosition()
        {
            int maxX = this.game.graphics.PreferredBackBufferWidth + 100;
            int maxY = new Random().Next(0, this.game.graphics.PreferredBackBufferHeight);
            return new Vector2(maxX, maxY);
        }
    }
}