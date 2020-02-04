using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;        // Using SoundEffect
using Microsoft.Xna.Framework.Input;        // Using KeyDown
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

/// <summary>
/// Kookhwan Im
/// PROG2370 Final Project
/// Dec 30, 2017
/// 
/// This program is Snake vs Blocks game.
/// </summary>

namespace KimFinalProject
{
    /// <summary>
    /// This is a game component that inherited Screen class.
    /// </summary>
    public class ScreenStart : Screen
    {
        private SpriteBatch spriteBatch;
        private Game game;
        private MyCar myCar;
        private Road road;
        private Explode explode;
        private HudMessage hudMessage;

        private List<RaceCar> raceCarLists;
        private CollisionManager colManager;

        private int nLevelCnt = 0;      //  For going up Level, So count racing cars
        private int nMaxInterval = 70;  //  Default Interval is 70
        private int nInterval = 0;

        private bool bExplode = false;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        public ScreenStart(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            Initialize();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            Game1.nLevel = 1;

            road = new Road(game, spriteBatch, game.Content.Load<Texture2D>("images/road"));
            this.Components.Add(road);

            myCar = new MyCar(game, spriteBatch, game.Content.Load<Texture2D>("images/myCar"));
            this.Components.Add(myCar);

            explode = new Explode(game, spriteBatch, game.Content.Load<Texture2D>("images/explode"));
            this.Components.Add(explode);

            raceCarLists = new List<RaceCar>();

            colManager = new CollisionManager(game, myCar, raceCarLists);
            this.Components.Add(colManager);

            hudMessage = new HudMessage(game, spriteBatch);
            this.Components.Add(hudMessage);

            base.Initialize();
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Game1.bAlive)
            {
                if (!bExplode)
                {
                    bExplode = true;
                    Game1.startSound.Stop();
                    Game1.bReady = false;
                    RemoveCars();

                    //int nPos = myCar.getPositionX();
                    explode.setImgShow(myCar.getPositionX());

                    myCar.setPositionX(-100);   //  Hidden myCar
                }

                if (Game1.bReady)
                {
                    newGameStart();
                }
            }
            else
            {
                Game1.startSound.Play();

                if (nInterval > nMaxInterval)
                {
                    GenRaceCars();  // Generate race cars
                    SetLevel();     // Setting level and Animation Speed

                    nInterval = 0;
                }
                else
                {
                    nInterval++;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Restarting of new game
        /// </summary>
        private void newGameStart()
        {
            hudMessage.MessgeShow(true);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game1.ScoreSave();
                gameInit();
            }
        }

        /// <summary>
        /// Initialize of game
        /// </summary>
        public void gameInit()
        {
            Game1.bAlive = true;
            Game1.nLevel = 1;
            Game1.nScore = 0;

            hudMessage.MessgeShow(false);

            // Vaiant Initial
            nLevelCnt = 0;
            nMaxInterval = 70;
            nInterval = 0;
            bExplode = false;

            RemoveCars();
            road.setZeroSpeed();
            myCar.setPositionX(game.GraphicsDevice.Viewport.Width / 2);
        }

        /// <summary>
        /// Generating a road car (enemy car)
        /// </summary>
        private void GenRaceCars()
        {
            RaceCar raceCars;

            raceCars = new RaceCar(game,
                spriteBatch,
                game.Content.Load<Texture2D>("images/raceCar"));

            this.Components.Add(raceCars);

            raceCarLists.Add(raceCars);

            foreach (RaceCar raceCar in raceCarLists)
            {
                int nValue = raceCar.getPositionY();

                if (nValue >= game.GraphicsDevice.Viewport.Height)
                {
                    this.Components.Remove(raceCar);
                }
            }
        }

        /// <summary>
        /// Removing road car (enemy car)
        /// </summary>
        private void RemoveCars()
        {
            foreach (RaceCar raceCar in raceCarLists)
            {
                this.Components.Remove(raceCar);
            }

            raceCarLists.Clear();
        }

        /// <summary>
        /// Setting a level
        /// </summary>
        private void SetLevel()
        {
            if (Game1.nLevel > 8)
                return;

            nLevelCnt++;

            Game1.nLevel = (nLevelCnt / 10) + 1;

            nMaxInterval = 70 - (Game1.nLevel * 2);

        }
    }
}
