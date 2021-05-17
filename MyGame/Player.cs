using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class Player
    {
        private GameRoot game;
        private Texture2D[] textures = new Texture2D[3];
        public Texture2D currentTexture;
        public Vector2 position;
        public int frags;
        public ProjectileManager projectileManager;
        private Rectangle hitbox;
        private float speed;
        private float health;
        

        public Player(GameRoot game)
        {
            this.game = game;
            this.textures[0] = this.game.content.Load<Texture2D>("sprites/player/TrollFace-64x64");
            this.textures[1] = this.game.content.Load<Texture2D>("sprites/player/Trollfacehurt1");
            this.textures[2] = this.game.content.Load<Texture2D>("sprites/player/Trollfacehurt2");
            this.currentTexture = this.textures[0];
            this.position = new Vector2(100, 400);
            this.projectileManager = new ProjectileManager(this, this.game);
            this.frags = 0;
            this.speed = 500.0f;
            this.health = 3.0f;
        } // Constructor sets all objects and values
        
        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.hitbox = new Rectangle((int) this.position.X, (int) this.position.Y, this.currentTexture.Width, this.currentTexture.Height); // Sets a new hitbox 60 times per second.
            this.Movement(gTime, kState); // Calls the movement method.
            this.BackFall(gTime, kState); // Calls the backfall method.
            this.projectileManager.Update(gTime); // Calls the projectile manager's update method.
            this.Boundaries(); // Calls the boundaries method.
            this.Collision(); // Calls the collision function.
        } // Runs code block 60 times per second.

        public void Draw()
        {
            this.game.spriteBatch.Draw(this.currentTexture, this.hitbox, Color.White); // Draws this object on the screen.
            this.projectileManager.Draw(); // Calls the projectile manager's drawing method.
        } // Draws ship on the screen

        private void Collision()
        {
            switch (this.health)
            {
                case 3:
                    this.currentTexture = this.textures[0];
                    break;
                case 2:
                    this.currentTexture = this.textures[1];
                    break;
                case 1:
                    this.currentTexture = this.textures[2];
                    break;
                default:
                    System.Environment.Exit(0);
                    break;
            }
            
            for (int i = 0; i < this.game.EnemyManager.Count; i++)
            {
                if (this.hitbox.Intersects(this.game.EnemyManager[i].hitbox))
                {
                    this.health--;
                    Console.WriteLine("Deducted health");
                    this.game.EnemyManager.Remove(this.game.EnemyManager[i]);
                }
            }
        } // Loops through the enemy manager and checks if the player collides with an enemy.

        private void Boundaries()
        {
            if (this.position.X > this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width)
            {
                this.position.X = this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width;
            }

            if (this.position.X < 0)
            {
                this.position.X = 0;
            }

            if (this.position.Y > this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height)
            {
                this.position.Y = this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height;
            }

            if (this.position.Y < 0)
            {
                this.position.Y = 0;
            }
        } // Keeps player within bounds.

        private void Movement(GameTime gTime, KeyboardState kState)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.A))
            {
                this.position.X -= this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.D))
            {
                this.position.X += this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.W))
            {
                this.position.Y -= this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.S))
            {
                this.position.Y += this.speed * deltaTime;
            }

        } // Moves the ship object

        private void BackFall(GameTime gTime, KeyboardState kState)
        {
            float backFallSpeed = 50.0f;
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            if (kState.IsKeyDown(Keys.D) || this.position.X < 50)
            {
                return;
            }
            this.position.X -= backFallSpeed * deltaTime;
        } // Pushes the ship back while it's idle
    }
}