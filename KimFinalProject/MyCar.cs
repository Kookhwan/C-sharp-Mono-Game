using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;              // Using Xna Frame
using Microsoft.Xna.Framework.Graphics;     // Using Graphic (Texture2D)
using Microsoft.Xna.Framework.Input;        // Using KeyDown
using Microsoft.Xna.Framework.Audio;        // Using SoundEffect

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

    public class MyCar : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D myCarTexture;

        private SoundEffect carHornSound;
        private Vector2 myCarPos;

        private const int SIZE_X = 50;
        private const int SIZE_Y = 100;

        private float rotation = 0f;
        private int moveLimit = 0;
        private int nSpeed = 10;
        private int nInterval = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        /// <param name="myCarTexture">texture of myCar image</param>
        public MyCar(Game game,
            SpriteBatch spriteBatch,
            Texture2D myCarTexture) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.myCarTexture = myCarTexture;

            this.carHornSound = game.Content.Load<SoundEffect>("sounds/carHorn");

            myCarPos.X = game.GraphicsDevice.Viewport.Width / 2;
            myCarPos.Y = game.GraphicsDevice.Viewport.Height - 120;

            moveLimit = game.GraphicsDevice.Viewport.Width - 45;
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(myCarTexture,
                             myCarPos, 
                             new Rectangle(0, 0, SIZE_X, SIZE_Y),
                             Color.White,
                             rotation,
                             new Vector2(myCarTexture.Width / 2, myCarTexture.Height / 2),
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
                KeyState();     // checking press key state

                if (nInterval > 500)
                {
                    this.carHornSound.Play();
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
        /// Manupulate Key Button
        /// </summary>
        private void KeyState()
        {
            KeyboardState getKey = Keyboard.GetState();

            if (!Game1.bAlive)
                return;
                
            if (getKey.IsKeyDown(Keys.Left))
            {
                if (myCarPos.X > 50)
                    myCarPos.X -= nSpeed;

                if (rotation > 0) rotation = 0f;

                if (rotation > -0.5)
                    rotation -= 0.05f;

            }
            else if (getKey.IsKeyDown(Keys.Right))
            {
                if (myCarPos.X < moveLimit)
                    myCarPos.X += nSpeed;
                else
                    myCarPos.X = moveLimit;

                if (rotation < 0) rotation = 0f;

                if (rotation < 0.5)
                    rotation += 0.05f;
            }
            else
            {
                if (Math.Round(rotation, 1) != 0)
                {
                    if (rotation > 0)
                        rotation -= 0.1f;
                    else
                        rotation += 0.1f;
                }
                else
                {
                    rotation = 0f;
                }
            }
        }

        /// <summary>
        /// Getting myCar position
        /// </summary>
        /// <returns></returns>
        public int getPositionX()
        {
            int nPosX = (int)myCarPos.X;
            return nPosX;
        }

        /// <summary>
        /// Setting myCar position
        /// </summary>
        /// <param name="nPosX"></param>
        public void setPositionX(int nPosX)
        {
            myCarPos.X = nPosX;
        }

        /// <summary>
        /// get the boundary of this image
        /// </summary>
        /// <returns>the boundary of this image</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)myCarPos.X+120, (int)myCarPos.Y, SIZE_X, SIZE_Y);
        }
    }
}
