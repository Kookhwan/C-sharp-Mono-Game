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

    public class HudMessage : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private SpriteFont hudFont;

        private Texture2D levelTexture;
        private Texture2D scoreTexture;

        private string strMessage;
        private bool bShowMessage;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        public HudMessage(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            levelTexture = game.Content.Load<Texture2D>("images/level");
            scoreTexture = game.Content.Load<Texture2D>("images/score");    

            spriteFont = game.Content.Load<SpriteFont>("fonts/SpriteFont");
            hudFont = game.Content.Load<SpriteFont>("fonts/HudFont");
        }

        /// <summary>
        /// /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(levelTexture, new Vector2(5, 5), Color.White);
            spriteBatch.Draw(scoreTexture, new Vector2(320, 5), Color.White);

            spriteBatch.DrawString(spriteFont, Game1.nLevel.ToString(), new Vector2(100, 8), Color.WhiteSmoke);
            spriteBatch.DrawString(spriteFont, Game1.nScore.ToString(), new Vector2(400, 8), Color.White);

            if (bShowMessage)
            {
                strMessage = "Press Spacebar to Restart";
                spriteBatch.DrawString(hudFont, strMessage, new Vector2(90, 550), Color.Azure);

                if(Game1.nScore > Game1.nHighScore)
                {
                    strMessage = "New Record: " + Game1.nScore.ToString();
                    spriteBatch.DrawString(hudFont, strMessage, new Vector2(140, 600), Color.Azure);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Display the restarting message
        /// </summary>
        /// <param name="bValue"></param>
        public void MessgeShow(bool bValue)
        {
            bShowMessage = bValue;
        }
    }
}
