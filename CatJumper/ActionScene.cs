using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatJumper
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D mushTex;
        private SpriteFont font, font2;
        private TimeString timeString;

        double timeCounter;

        float spawn = 0;
        float spawn2 = 0;
        private bool reset = false;
        List<WalkingEnemy> enemies = new List<WalkingEnemy>();
        List<FlyingEnemy> flyenemies = new List<FlyingEnemy>();


        private Background background;
        private Background background2;
        private Cat cat;
        private HeartLife heart;
        private WalkingEnemy walkEn;
        private FlyingEnemy flyingEn;
        private CollisionManager cm;
        private CollisionManager2 cm2;

        public double TimeCounter { get => timeCounter; set => timeCounter = value; }
        public float Spawn { get => spawn; set => spawn = value; }
        public float Spawn2 { get => spawn2; set => spawn2 = value; }
        public bool Reset { get => reset; set => reset = value; }
        public Cat Cat { get => cat; set => cat = value; }

        public ActionScene(Game game) : base(game)
        {

            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            Texture2D catTex = g.Content.Load<Texture2D>("images/Walk2");
            cat = new Cat(game, spriteBatch, catTex, 3);

            //mushroom background
            mushTex = g.Content.Load<Texture2D>("images/road");
            Rectangle srcRect = new Rectangle(0, 0, mushTex.Width, mushTex.Height);

            Texture2D fairyTex = g.Content.Load<Texture2D>("images/fairy");
            Vector2 pos = new Vector2(0, 0);
            Vector2 speed = new Vector2(2, 0);
            Vector2 speed2 = new Vector2(3, 0);
            background = new Background(game, spriteBatch, mushTex, srcRect, pos, speed);
            background2 = new Background(game, spriteBatch, fairyTex, srcRect, pos, speed2);

            //timer font
            font = g.Content.Load<SpriteFont>("fonts/regularFont");
            string message = "Time running: ";
            timeString = new TimeString(game, spriteBatch, font, message, Vector2.Zero, Color.White);

            //One life
            Texture2D heartTex = g.Content.Load<Texture2D>("images/heart");
            Vector2 position = new Vector2(1120 - heartTex.Width, 0);
            font2 = g.Content.Load<SpriteFont>("fonts/regularFont");
            string lifeText = "x1";
            heart = new HeartLife(game, spriteBatch, font2, lifeText, heartTex, position);


            //walking enemy
            Texture2D walkTex = g.Content.Load<Texture2D>("images/EnemyWalk");
            walkEn = new WalkingEnemy(game, spriteBatch, walkTex, 5);

            //flying enemy
            Texture2D flyTex = g.Content.Load<Texture2D>("images/Special");
            flyingEn = new FlyingEnemy(game, spriteBatch, flyTex, 3);

            //---------------------------------------------------------------------
            SoundEffect catHowl = g.Content.Load<SoundEffect>("sound/catHowl");
            //---------------------------------------------------------------------

            //collisionManager
            cm = new CollisionManager(game, walkEn, cat, catHowl);
            cm2 = new CollisionManager2(game, cat, flyingEn, catHowl);

            this.Components.Add(background);
            this.Components.Add(timeString);
            this.Components.Add(cat);
            this.Components.Add(heart);
            this.Components.Add(walkEn);
            this.Components.Add(cm);

        }

        private void AddFlyingEnemy()
        {
            if (spawn2 >= 4)
            {
                spawn2 = 0;
                if (enemies.Count() < 2)
                {
                    Vector2 position = new Vector2(1160, 200);
                    Texture2D flyTex = Game.Content.Load<Texture2D>("images/Special");
                    FlyingEnemy flyEnemy = new FlyingEnemy(Game, spriteBatch, flyTex, 3);
                    this.Components.Add(flyEnemy);
                    flyenemies.Add(flyEnemy);

                    //---------------------------------------------------------------------
                    SoundEffect catHowl = Game.Content.Load<SoundEffect>("sound/catHowl");
                    //---------------------------------------------------------------------

                    cm2 = new CollisionManager2(Game, cat, flyEnemy, catHowl);
                    this.Components.Add(cm2);
                }
            }
        }


        private void AddEnemy()
        {
            if (spawn >= 3)
            {
                spawn = 0;
                if (enemies.Count() < 2)
                {
                    Vector2 position = new Vector2(1160, 500);
                    Texture2D walkTex = Game.Content.Load<Texture2D>("images/EnemyWalk");
                    walkEn = new WalkingEnemy(Game, spriteBatch, walkTex, 3);
                    this.Components.Add(walkEn);
                    enemies.Add(walkEn);

                    //---------------------------------------------------------------------
                    SoundEffect catHowl = Game.Content.Load<SoundEffect>("sound/catHowl");
                    //---------------------------------------------------------------------
                    cm = new CollisionManager(Game, walkEn, cat, catHowl);
                    this.Components.Add(cm);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            double elapsedTime = 20;
            TimeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawn2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Round(TimeCounter) != 0)
            {
                if(Reset == true)
                {
                    Reset = false;
                    this.Components.Clear();
                    AddEnemy();
                    font = Game.Content.Load<SpriteFont>("fonts/regularFont");
                    string message = "Time running: ";
                    timeString = new TimeString(Game, spriteBatch, font, message, Vector2.Zero, Color.White);

                    this.Components.Add(background);
                    this.Components.Add(timeString);
                    this.Components.Add(heart);
                    this.Components.Add(cat);
                }

                AddEnemy();

                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    enemies[i].Update(gameTime);
                    if (enemies[i].Position.X < Shared.stage.X)
                    {
                        enemies.RemoveAt(i);
                    }
                }

                if (Math.Round(TimeCounter) == elapsedTime)
                {
                    this.Components.Clear();

                    font = Game.Content.Load<SpriteFont>("fonts/regularFont");
                    string message = "Time running: ";
                    timeString = new TimeString(Game, spriteBatch, font, message, Vector2.Zero, Color.White);

                    this.Components.Add(background2);
                    this.Components.Add(timeString);
                    this.Components.Add(heart);
                    this.Components.Add(cat);

                }

                timeString.Message = "Time Running: " + TimeCounter;
                Vector2 pos = new Vector2(Shared.stage.Y - font.MeasureString(timeString.Message).X, 0);
                if (TimeCounter > 20)
                {
                    AddFlyingEnemy();
                }
                
            }

            base.Update(gameTime);
        }
    }
}
