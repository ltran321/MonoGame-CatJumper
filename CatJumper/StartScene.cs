using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;

        public MenuComponent Menu { get => menu; set => menu = value; }

        private SpriteBatch spriteBatch;
        string[] menuItems = { "Start Game", "Help", "Credit", "Quit" };

        private Texture2D startTex;

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/highlightFont");
            menu = new MenuComponent(g, spriteBatch, regularFont, highlightFont, menuItems);
            this.Components.Add(menu);
            startTex = g.Content.Load<Texture2D>("images/bamboo");
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(startTex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
