using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class EnemyManager : List<Impostor>
    {
        private Random randUtil;
        private GameRoot game;
        private int enemyCap;
        private int spawnDelay;
        private double lastSpawned;
        public float movementSpeed;
        
        public EnemyManager(GameRoot game)
        {
            this.game = game;
            this.randUtil = new Random();
            this.enemyCap = 25;
            this.spawnDelay = 1000;
            this.movementSpeed = 80.0f;
        }

        public void Update(GameTime gTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].position.X < (0 - this[i].texture.Width))
                {
                    this.Remove(this[i]);
                }
            }

            if (this.Count < this.enemyCap)
            {
                this.CreateEnemy(gTime);
            }
            
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gTime);
            }
            
            this.DifficultyIncrement(gTime);
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }
        
        private void CreateEnemy(GameTime gTime)
        {
            if (gTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.Add(new Impostor(this.RandomPosition(), this.game));
                this.lastSpawned = gTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private double lastSpeedIncrement;
        private void DifficultyIncrement(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastSpeedIncrement + 1000)
            {
                this.movementSpeed += 5.0f;
                this.enemyCap++;
                if (this.spawnDelay > 0)
                    this.spawnDelay -= 10;
                this.lastSpeedIncrement = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private Vector2 RandomPosition()
        {
            int maxX = this.game.graphics.PreferredBackBufferWidth + 100;
            int maxY = randUtil.Next(0, this.game.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
    }
}