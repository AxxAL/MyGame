using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class EnemyManager : List<RedEnemy>
    {
        public float movementSpeed;
        private Boss bossEnemy;
        private bool bossActive;
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
            this.bossActive = false;
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

            if (this.fragsUntilBoss <= 0)
            {
                this.SpawnBoss();
                this.fragsUntilBoss = 25;
            }

            if (this.bossEnemy != null)
            {
                this.bossEnemy.Update(gTime);
            }
            
            this.DifficultyIncrement(gTime);
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }

            if (this.bossEnemy != null)
            {
                this.bossEnemy.Draw();
            }
        }
        
        private void CreateEnemy(GameTime gTime)
        {
            if (gTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.Add(new RedEnemy(this.game, this.RandomPosition()));
                this.lastSpawned = gTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void SpawnBoss()
        {
            this.bossEnemy = new Boss(new Vector2(600, 400), this.game);
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