using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class GameOverScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D deadTex;
        private SpriteFont spriteFont;
        private String message;

        public string Message { get => message; set => message = value; }

        public GameOverScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            deadTex = g.Content.Load<Texture2D>("images/deadForest");
            spriteFont = g.Content.Load<SpriteFont>("fonts/regularFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(deadTex, Vector2.Zero, Color.White);
            spriteBatch.DrawString(spriteFont, message, new Vector2(500, 250), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
