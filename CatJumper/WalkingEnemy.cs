using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatJumper
{
    public class WalkingEnemy : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;
        private Vector2 position1, position2;
        private Rectangle srcRect;
        private Vector2 speed;
        
        int randX;
        Random random = new Random();
        public bool isVisible = true;

        private int delay;
        private int delayCounter;

        private const int ROW = 1;
        private const int COL = 6;

        private int thisPosX;
        private int thisPosY;

        public Vector2 Position { get => position; set => position = value; }

        public WalkingEnemy(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(1160, 503);
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COL, tex.Height);
            createFrames();

            randX = random.Next(-5, -3);
            speed = new Vector2(randX, 500);

        }

        public void restart()
        {
            frameIndex = 0;
            delayCounter = 0;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }

            position.X += speed.X;

            thisPosX = (int)position.X;
            thisPosY = (int)position.Y;
            if (position.X < 0 - tex.Width)
            {
                isVisible = false;
            }

            base.Update(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle(thisPosX, thisPosY, tex.Width/24, tex.Height-19);
        }
    }
}
