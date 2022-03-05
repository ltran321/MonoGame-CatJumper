using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class HeartLife : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position, position2;
        private SpriteFont spriteFont;
        private String message;

        public HeartLife(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            String message,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.message = message;
            this.tex = tex;
            this.position = position;
            this.position2 = new Vector2(position.X + 13, 10);
        }

        protected string Message { get => message; set => message = value; }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.DrawString(spriteFont, message, position2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
