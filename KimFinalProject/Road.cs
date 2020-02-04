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

    public class Road : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D roadTexture;
        private Vector2 roadPos;

        private const int SIZE_X = 464;
        private const int SIZE_Y = 824;

        private int nSpeed = 0;
        private int nInterval = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        /// <param name="roadTexture">The texture of road images</param>
        public Road(Game game,
            SpriteBatch spriteBatch,
            Texture2D roadTexture) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.roadTexture = roadTexture;

            roadPos.X = 0;
            roadPos.Y = -(SIZE_Y*2);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(roadTexture, new Vector2(roadPos.X, roadPos.Y), Color.White);
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
                roadPos.Y += nSpeed;

                if (roadPos.Y >= -SIZE_Y)
                {
                    roadPos.Y -= SIZE_Y;
                }

                if (nInterval > 10)
                {
                    setSpeed();
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
        /// settomg road's move speed
        /// </summary>
        private void setSpeed()
        {
            if (!Game1.bAlive)
                return;

            Game1.nScore++;

            if (nSpeed > 80)
                return;

            int nMaxSpeed = (Game1.nLevel * 10) + 10;

            if (nSpeed <= nMaxSpeed)
            {
                nSpeed++;
            }
        }

        /// <summary>
        /// setting road's movin stop
        /// </summary>
        public void setZeroSpeed()
        {
            nSpeed = 0;
        }
    }
}
