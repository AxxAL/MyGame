using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class HUD
    {
        private HUDPositions hudPos;
        private GameRoot game;
        private SpriteFont font;
        private Vector2 fragOrigin;
        private Player player;
        private int frags;
        private int castCooldown;
        
        public HUD(GameRoot game)
        {
            this.game = game;
            this.player = this.game.player;
            this.frags = 0;
            this.font = this.game.content.Load<SpriteFont>("sprites/font");
            this.SetHudPositions();
        }

        private void SetHudPositions()
        {
            this.hudPos = new HUDPositions
            {
                fragsPos = new Vector2(100, 40)
            };
        }

        public void Update(GameTime gameTime)
        {
            this.frags = this.player.frags;

            this.fragOrigin = this.font.MeasureString(this.frags.ToString()) / 2;
        }

        public void Draw()
        {
            this.FragCounter();
        }
        
        private void FragCounter()
        {
            this.game.spriteBatch.DrawString(this.font,
                "Frags: " + this.frags.ToString(),
                this.hudPos.fragsPos,
                Color.White,
                0,
                this.fragOrigin,
                2.0f,
                SpriteEffects.None,
                0.5f);
        }

        private struct HUDPositions
        {
            public Vector2 fragsPos;
        }
    }
}