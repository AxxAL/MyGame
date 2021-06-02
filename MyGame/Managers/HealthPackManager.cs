using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class HealthPackManager : List<HealthPack>
    {
        private Random randUtil;
        private GameRoot game;
        private int spawnDelay;
        private double lastSpawned;
        
        public HealthPackManager(GameRoot game)
        {
            this.game = game;
            this.randUtil = new Random();
            this.spawnDelay = 30 * 1000;
        }
        
        public void Update(GameTime gTime)
        {
            if (gTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.CreateHealthPack(gTime);
            }

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].position.X < (0 - this[i].texture.Width))
                {
                    this.Remove(this[i]);
                }
            }

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gTime);
            }

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].hitbox.Intersects(this.game.player.hitbox) && this.game.player.health < 3)
                {
                    this.game.player.health++;
                    this.Remove(this[i]);
                }
            }
        }
        
        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }
        
        private void CreateHealthPack(GameTime gTime)
        {
            this.Add(new HealthPack(this.game, this.RandomPosition()));
            lastSpawned = gTime.TotalGameTime.TotalMilliseconds;
        }
        
        private Vector2 RandomPosition()
        {
            int maxX = this.game.graphics.PreferredBackBufferWidth + 100;
            int maxY = randUtil.Next(0, this.game.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
    }
}