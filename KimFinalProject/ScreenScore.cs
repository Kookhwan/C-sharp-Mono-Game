using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    public class ScreenScore : Screen
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private SpriteFont scoreFont;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        public ScreenScore(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            texture = game.Content.Load<Texture2D>("images/screenScore");
            scoreFont = game.Content.Load<SpriteFont>("fonts/ScoreFont");
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            spriteBatch.DrawString(scoreFont, Game1.nHighScore.ToString(), new Vector2(250, 620), Color.White);
            spriteBatch.DrawString(scoreFont, Game1.nHighLevel.ToString(), new Vector2(290, 720), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
