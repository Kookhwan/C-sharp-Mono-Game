using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;              // Using Xna Frame
using Microsoft.Xna.Framework.Graphics;     // Using Graphic (Texture2D)
using Microsoft.Xna.Framework.Audio;        // Using SoundEffect
using KimFinalProject;

/// <summary>
/// Kookhwan Im
/// PROG2370 Final Project
/// Dec 30, 2017
/// 
/// This program is Road Fight game.
/// </summary>

namespace KimFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class RaceCar : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D raceCarsTexture;

        private Vector2 raceCarPos;

        private const int SIZE_X = 50;
        private const int SIZE_Y = 100;

        private int RangeMin = 220;
        private int RangeMax = 496;

        private int nCarType = 0;
        private int nInterval = 0;
        private int nSpeed = 0;

        private List<Rectangle> raceCarFrames;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        /// <param name="raceCarsTexture">The texture of road cars image</param>
        public RaceCar(Game game,
            SpriteBatch spriteBatch,
            Texture2D raceCarsTexture) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.raceCarsTexture = raceCarsTexture;

            raceCarFrames = new List<Rectangle>(6);

            for (int nCnt = 0; nCnt < 6; nCnt++)
            {
                raceCarFrames.Add(new Rectangle(SIZE_X * nCnt, 0, SIZE_X, SIZE_Y));
            }

            raceCarPos.X = getRandomPosX();
            raceCarPos.Y = -SIZE_Y * 2;
            nCarType = getCarType();
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(raceCarsTexture,       // texture
                             raceCarPos,            // position
                             raceCarFrames.ElementAt<Rectangle>(nCarType),  // SourceRectangle
                             Color.White,           // Color
                             0,                     // Rotation
                             new Vector2(raceCarsTexture.Width / 2, raceCarsTexture.Height / 2),    // Origin
                             1f,
                             SpriteEffects.None,
                             0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Game1.bAlive)
            {
                raceCarPos.Y += nSpeed;

                if (nInterval > 10)
                {
                    setSpeed();
                    nInterval = 0;
                }
                else
                {
                    nInterval++;
                }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// getting random road car's x position
        /// </summary>
        /// <returns></returns>
        private int getRandomPosX()
        {
            Random rndNum = new Random();
            int nPos = rndNum.Next(RangeMin, RangeMax);
            return nPos;
        }

        /// <summary>
        /// getting a car type
        /// </summary>
        /// <returns></returns>
        private int getCarType()
        {
            Random rndNum = new Random();

            int nType = rndNum.Next(0, 6);

            return nType;
        }

        /// <summary>
        /// setting car speed
        /// </summary>
        private void setSpeed()
        {
            if (nSpeed > 80)
                return;

            nSpeed = Game1.nLevel * 5;
        }

        /// <summary>
        /// getting a car's current position Y
        /// </summary>
        /// <returns></returns>
        public int getPositionY()
        {
            int nPosY = (int)raceCarPos.Y;
            return nPosY;
        }

        /// <summary>
        /// get the boundary of this image
        /// </summary>
        /// <returns>the boundary of this image</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)raceCarPos.X, (int)raceCarPos.Y, SIZE_X, SIZE_Y);
        }
    }
}
