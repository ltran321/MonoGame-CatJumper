using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTex;
        private Texture2D backTex;
        private Texture2D descriptTex;

        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            backTex = g.Content.Load<Texture2D>("images/bamboo");
            helpTex = g.Content.Load<Texture2D>("images/spacebar");
            descriptTex = g.Content.Load<Texture2D>("images/desciption");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backTex, Vector2.Zero, Color.White);
            spriteBatch.Draw(helpTex, new Vector2(Shared.stage.X / 2 + 200, Shared.stage.Y / 2 - 50), Color.White);
            spriteBatch.Draw(descriptTex, new Vector2(Shared.stage.X / 2 - descriptTex.Width, Shared.stage.Y / 2 - 50), Color.White);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
