using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// Kookhwan Im
/// PROG2370 Final Project
/// Dec 30, 2017
/// 
/// This program is Road Fight game.
/// 
/// Reference for game logic and images: 
///     https://www.crazygames.com/game/road-fight
/// Reference for how to use menu Component
///     https://conestoga.desire2learn.com/d2l/le/content/166184/viewContent/3768913/View
/// 
/// </summary>


namespace KimFinalProject
{
    /// <summary>
    /// This is the main type for this game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ScreenMain screenMain;
        private ScreenStart screenStart;
        private ScreenScore screenScore;
        private ScreenHowPlay screenHowPlay;
        private ScreenHelp screenHelp;
        private ScreenAbout screenAbout;

        // Screen Size
        public const int SCREEN_WIDTH = 464;
        public const int SCREEN_HEIGHT = 824;

        // Background Sounds Components
        public static SoundEffectInstance startSound;
        public static SoundEffectInstance mainSound;

        public static Vector2 gameStage;

        public static bool bAlive = true;
        public static bool bReady = true;
        public static int nLevel = 1;
        public static int nScore = 0;
        public static int nHighScore = 0;
        public static int nHighLevel = 0;

        /// <summary>
        /// Main Structor of class
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Getting game screen
            gameStage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenMain = new ScreenMain(this, spriteBatch);
            Components.Add(screenMain);

            screenStart = new ScreenStart(this, spriteBatch);
            Components.Add(screenStart);

            screenScore = new ScreenScore(this, spriteBatch);
            Components.Add(screenScore);

            screenHowPlay = new ScreenHowPlay(this, spriteBatch);
            Components.Add(screenHowPlay);

            screenHelp = new ScreenHelp(this, spriteBatch);
            Components.Add(screenHelp);

            screenAbout = new ScreenAbout(this, spriteBatch);
            Components.Add(screenAbout);

            mainSound = this.Content.Load<SoundEffect>("sounds/mainSound").CreateInstance();
            startSound = this.Content.Load<SoundEffect>("sounds/startSound").CreateInstance();

            ScoreLoad();    // Bring high score

            screenMain.isVisible(true);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();

            int nIndex = 0;
            KeyboardState curKey = Keyboard.GetState();

            if (screenMain.Enabled)
            {
                nIndex = screenMain.Menu.SelectedIndex;
                startSound.Stop();
                mainSound.Play();

                if (curKey.IsKeyDown(Keys.Enter))
                {
                    SelectedMenu(nIndex);
                }
            }
            else
            {
                if (curKey.IsKeyDown(Keys.Escape))
                {
                    if (screenStart.Enabled)
                    {
                        ScoreSave();
                    }

                    hideAllScreen();
                    screenMain.isVisible(true);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Select menu
        /// </summary>
        /// <param name="nIndex">Menu index number</param>
        private void SelectedMenu(int nIndex)
        {
            switch (nIndex)
            {
                case 0:     //  Screen Start
                    hideAllScreen();
                    mainSound.Stop();
                    screenStart.gameInit();
                    screenStart.isVisible(true);
                    break;
                case 1:     //  High Score
                    hideAllScreen();
                    screenScore.isVisible(true);
                    break;
                case 2:     //  How To Play
                    hideAllScreen();
                    screenHowPlay.isVisible(true);
                    break;
                case 3:     // Help
                    hideAllScreen();
                    screenHelp.isVisible(true);
                    break;
                case 4:     // About
                    hideAllScreen();
                    screenAbout.isVisible(true);
                    break;
                default:
                    this.Exit();
                    break;
            }
        }

        /// <summary>
        ///  Hiding all game screen
        /// </summary>
        private void hideAllScreen()
        {
            Screen screen = null;
            foreach (GameComponent item in Components)
            {
                if (item is Screen)
                {
                    screen = (Screen)item;
                    screen.isVisible(false);
                }
            }
        }

        /// <summary>
        /// Saving a high score 
        /// </summary>
        private void ScoreLoad()
        {
            try
            {
                StreamReader strFile = new StreamReader("HighScore.txt");

                for (int nCnt = 0; nCnt < 2; nCnt++)
                {
                    string line = strFile.ReadLine();
                    string[] readValue = line.Split(':');

                    if (readValue[0] == "HighScore") nHighScore = int.Parse(readValue[1]);
                    if (readValue[0] == "HighLevel") nHighLevel = int.Parse(readValue[1]);
                }

                strFile.Close();    //  File close
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Loading a high score
        /// </summary>
        public static void ScoreSave()
        {
            try
            {
                if (nScore > nHighScore)
                {
                    nHighScore = nScore;
                    nHighLevel = nLevel;

                    StreamWriter strFile = new StreamWriter("HighScore.txt");
                    strFile.WriteLine("HighScore:" + nScore.ToString());
                    strFile.WriteLine("HighLevel:" + nLevel.ToString());
                    strFile.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
