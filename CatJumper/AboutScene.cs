using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D aboutTex;
        private Texture2D backTex;

        private MenuComponent name;
        public MenuComponent Name { get => name; set => name = value; }
        string[] names = { "Created by:", "Lina Tran", "Maria Tran" };


        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/highlightFont");
            name = new MenuComponent(g, spriteBatch, regularFont, highlightFont, names);
            this.Components.Add(name);
            backTex = g.Content.Load<Texture2D>("images/bamboo");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backTex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
