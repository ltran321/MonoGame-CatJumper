using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace CatJumper
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        private AboutScene aboutScene;
        private GameOverScene gameOverScene;

        public const int QUIT = 3;

        public ActionScene ActionScene { get => actionScene; set => actionScene = value; }
        public StartScene StartScene { get => startScene; set => startScene = value; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1152;
            _graphics.PreferredBackBufferHeight = 648;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            gameOverScene = new GameOverScene(this);
            this.Components.Add(gameOverScene);
            

            //-------------------------------------------------------------------
            Song backgroundMusic = this.Content.Load<Song>("sound/piano");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            //-------------------------------------------------------------------

            startScene.show();

        }

        private void hideAllScenes()
        {
            foreach (GameScene item in Components)
            {
                item.hide();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                    MediaPlayer.Stop();
                    Song fairyMusic = this.Content.Load<Song>("sound/fairy");
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(fairyMusic);
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if(selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if(selectedIndex == QUIT && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene.Enabled || aboutScene.Enabled || gameOverScene.Enabled)
            {
                
                if (ks.IsKeyDown(Keys.Escape))
                {
                    actionScene.Cat.Lives = 1;
                    actionScene.Reset = true;
                    actionScene.TimeCounter = 0;
                    actionScene.Spawn = 0;
                    actionScene.Spawn2 = 0;
                    hideAllScenes();
                    startScene.show();
                    
                    MediaPlayer.Stop();
                    Song backgroundMusic = this.Content.Load<Song>("sound/piano");
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(backgroundMusic);
                }
                else if(actionScene.Cat.Lives == 0)
                {
                    gameOverScene.Message = "Game Over" + "\n" + "Your score: " + actionScene.TimeCounter + "\n" + "Press escape to go back to start screen";
                    
                    gameOverScene.show();
                    actionScene.Spawn = 0;
                    actionScene.Spawn2 = 0;
                    actionScene.TimeCounter = 0;
                    actionScene.Reset = true;
                    MediaPlayer.Stop();
                    Song horrorMusic = this.Content.Load<Song>("sound/horror");
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(horrorMusic);

                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        actionScene.Cat.Lives = 1;
                        hideAllScenes();
                        startScene.show();

                        MediaPlayer.Stop();
                        Song backgroundMusic = this.Content.Load<Song>("sound/piano");
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Play(backgroundMusic);
                    }

                    
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
