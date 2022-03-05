using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatJumper
{
    public class Cat : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private int delay;
        private int delayCounter;

        private const int ROW = 1;
        private const int COL = 6;

        private bool hit = false;
        private bool updated = true;
        private int lives;

        public Vector2 Position { get => position; set => position = value; }
        public bool Hit { get => hit; set => hit = value; }
        public bool Updated { get => updated; set => updated = value; }
        public int Lives { get => lives; set => lives = 4; }

        private Vector2 velocity;
        private bool jumped;

        private int thisPosX;
        private int thisPosY;


        public Cat(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(60, 500);
            this.delay = delay;
            this.lives = 1;
            this.Lives = 1;
            this.dimension = new Vector2(tex.Width / COL, tex.Height);
            createFrames();
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
        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public void show()
        {
            this.Enabled = true;
            this.Visible = true;
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

            position.Y += velocity.Y;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space) && jumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -15f;
                jumped = true;
            }

            if (jumped == true)
            {
                float i = 2;
                velocity.Y += 0.15f * i;
            }
            if (position.Y >= 500)
            {
                jumped = false;
            }
            if (jumped == false)
            {
                velocity.Y = 0f;
            }

            thisPosY = (int)position.Y;
            thisPosX = (int)position.X;

            if(hit == true && updated == false)
            {
                lives--;
                updated = true;
                hit = false;
            }


            base.Update(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle(thisPosX, thisPosY, tex.Width/24, tex.Height);
        }
    }
}
