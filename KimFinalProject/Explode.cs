using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;              // Using Xna Frame
using Microsoft.Xna.Framework.Graphics;     // Using Graphic (Texture2D)
using Microsoft.Xna.Framework.Audio;        // Using SoundEffect
using Microsoft.Xna.Framework.Input;        // Using KeyDown

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

    public class Explode : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D explodeTexture;
        private Vector2 explodePos;
        private SoundEffect explodeSound;

        private const int SIZE_X = 100;
        private const int SIZE_Y = 100;

        private int nExplodeType = 0;

        private List<Rectangle> explodeFrames;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        /// <param name="explodeTexture">The texture of explosion images</param>
        public Explode(Game game,
                    SpriteBatch spriteBatch,
                    Texture2D explodeTexture) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.explodeTexture = explodeTexture;

            explodeFrames = new List<Rectangle>(81);

            for (int nCol = 0; nCol < 9; nCol++)
            {
                for (int nRow = 0; nRow < 9; nRow++)
                {
                    explodeFrames.Add(new Rectangle(SIZE_X * nRow, SIZE_Y * nCol, SIZE_X, SIZE_Y));
                }
            }

            this.explodeSound = game.Content.Load<SoundEffect>("sounds/explosion");

            explodePos.X = 0;
            explodePos.Y = 250 + game.GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (!Game1.bAlive && !Game1.bReady)
            {
                spriteBatch.Draw(explodeTexture,       // texture
                             explodePos,                // position
                             explodeFrames.ElementAt<Rectangle>(nExplodeType),  // SourceRectangle
                             Color.White,               // Color
                             0,                         // Rotation
                             new Vector2(explodeTexture.Width / 2, explodeTexture.Height / 2),    // Origin
                             1f,
                             SpriteEffects.None,
                             0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Game1.bAlive && !Game1.bReady)
            {
                if (nExplodeType < 80)
                {
                    nExplodeType++;
                }
                else
                {
                    nExplodeType = 0;
                    explodePos.X = 0;
                    Game1.bReady = true;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Show explosion image
        /// </summary>
        /// <param name="nPos">Current myCar X Position</param>
        public void setImgShow(int nPos = 0)
        {
            explodePos.X = nPos + 400; // MyCar Current pos + 400
            this.explodeSound.Play();
        }
    }
}
