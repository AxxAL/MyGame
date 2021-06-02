using System.Collections.Generic;
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
        private EnemyManager enemyManager;

        public ProjectileManager(Player player, GameRoot game)
        {
            this.game = game;
            this.caster = player;
            this.castDelay = 500;
            this.enemyManager = game.enemyManager;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gameTime);
            }
            
            this.CastFirebolt(gameTime);
            this.CollisionChecker();
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

        private void CollisionChecker()
        {
            for (int i = 0; i < this.enemyManager.Count; i++)
            {
                for (int j = 0; j < this.Count; j++)
                {
                    if (this[j].hitbox.Intersects(this.enemyManager[i].hitbox) && !GUtility.OfTypeBoss(this.enemyManager[i]))
                    {
                        this.enemyManager.Remove(this.enemyManager[i]);
                        this.Remove(this[j]);
                        this.caster.frags++;
                        this.enemyManager.fragsUntilBoss--;
                        break;
                    }
                    
                    if (this[j].hitbox.Intersects(this.enemyManager[i].hitbox))
                    {
                        this.enemyManager[i].healthPoints -= 10;
                        this.Remove(this[j]);
                    }
                }
            }
        }
    }
}