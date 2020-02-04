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
    public class ScreenMain : Screen
    {
        private MenuComponent menu;
        private Game game;

        public MenuComponent Menu
        {
            get
            {
                return menu;
            }
            set
            {
                menu = value;
            }
        }

        private SpriteBatch spriteBatch;
        string[] menuItems = {"START",
                            "HIGH SCORE",
                            "How To Play",
                            "HELP",
                            "ABOUT",
                            "QUIT"};

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        public ScreenMain(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game,
                                    spriteBatch,
                                    game.Content.Load<SpriteFont>("fonts/SpriteFont"),
                                    game.Content.Load<SpriteFont>("fonts/HUDfont"),
                                    menuItems);

            this.Components.Add(menu);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(game.Content.Load<Texture2D>("images/screenMain"), new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
