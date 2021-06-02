using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class EnemyManager : List<Enemy>
    {
        public float movementSpeed;
        public int fragsUntilBoss;
        private Random randUtil;
        private GameRoot game;
        private int enemyCap;
        private int spawnDelay;
        private double lastSpawned;

        public EnemyManager(GameRoot game)
        {
            this.game = game;
            this.randUtil = new Random();
            this.enemyCap = 25;
            this.spawnDelay = 1000;
            this.movementSpeed = 80.0f;
            this.fragsUntilBoss = 25;
        }

        public void Update(GameTime gameTime)
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
                this.CreateEnemy(gameTime);
            }
            
            for (int i = 0; i < this.Count; i++)
            {
                if (GUtility.OfTypeBoss(this[i]))
                {
                    if (this[i].healthPoints <= 0)
                    {
                        this.Remove(this[i]);
                    }
                }
                
                this[i].Update(gameTime);
            }

            this.BossManager(gameTime);
            
            this.DifficultyIncrement(gameTime);
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }
        
        private void CreateEnemy(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.Add(new RedEnemy(this.game, this.RandomPosition()));
                this.lastSpawned = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
        
        private void BossManager(GameTime gameTime)
        {
            if (this.fragsUntilBoss <= 0)
            {
                this.Add(new Boss(new Vector2(800, new Random().Next(0, 600)), this.game));
                this.fragsUntilBoss = 25;
            }
        }

        private double lastSpeedIncrement;
        private void DifficultyIncrement(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastSpeedIncrement + 1000)
            {
                this.movementSpeed += 5.0f;
                this.enemyCap += 1;
                if (this.spawnDelay > 0)
                    this.spawnDelay -= 5;
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