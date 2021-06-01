﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MyGame
{
    public class ProjectileManager : List<Projectile>
    {
        private GameRoot game;
        private int castDelay;
        private double lastCast;
        private Player caster;

        public ProjectileManager(Player player, GameRoot game)
        {
            this.game = game;
            this.caster = player;
            this.castDelay = 500;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gameTime);
            }
            
            this.CastFirebolt(gameTime);
            this.Hit();
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }

        private void CastFirebolt(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!keyboardState.IsKeyDown(Keys.Space)) 
            {
                return;
            } // Checks if player is pressing spacebar.
            
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastCast + this.castDelay)
            {
                this.Add(new Projectile(new Vector2(this.caster.position.X, this.caster.position.Y + (this.caster.currentTexture.Height / 2)), this.game));
                this.lastCast = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private void Hit()
        {
            List<Projectile> newProjectileList = new List<Projectile>(this.game.player.projectileManager);
            List<RedEnemy> newEnemyList = new List<RedEnemy>(this.game.enemyManager);
            Boss boss = this.game.enemyManager.bossEnemy;
            
            foreach (var firebolt in this)
            {
                foreach (var enemy in this.game.enemyManager)
                {
                    if (firebolt.hitbox.Intersects(enemy.hitbox))
                    {
                        newEnemyList.Remove(enemy);
                        newProjectileList.Remove(firebolt);
                        this.caster.frags++;
                        this.game.enemyManager.fragsUntilBoss--;
                    }
                }

                if (boss != null)
                {
                    if (firebolt.hitbox.Intersects(boss.hitbox))
                    {
                        newProjectileList.Remove(firebolt);
                        boss.healthPoints -= 10;
                    }
                }
            }

            this.game.player.projectileManager.Clear();
            this.game.enemyManager.Clear();
            this.game.player.projectileManager.AddRange(newProjectileList);
            this.game.enemyManager.AddRange(newEnemyList);
        }
    }
}