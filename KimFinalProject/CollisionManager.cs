using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

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

    public class CollisionManager : GameComponent
    {
        private Game game;
        private MyCar myCar;            // the object will occur collision 
        private List<RaceCar> raceCars; // the object will occur collision 

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code</param>
        /// <param name="myCar">the myCar checking the collision</param>
        /// <param name="raceCars">the car checking the collision</param>
        public CollisionManager(Game game,
                                MyCar myCar,
                                List<RaceCar> raceCars) : base(game)
        {
            // Initialize all member variables
            this.game = game;
            this.myCar = myCar;
            this.raceCars = raceCars;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Rectangle myCarRect = myCar.getBounds();

            // When my car and race cars collide,
            foreach (RaceCar raceCar in raceCars)
            {
                Rectangle raceCarRect = raceCar.getBounds();

                if (myCarRect.Intersects(raceCarRect))
                {
                    Game1.bAlive = false;
                }
            }

            // When my car take off the line, 
            int nPosition = myCar.getPositionX();

            if (nPosition < 90 || nPosition > 390)
            {
                Game1.bAlive = false;
            }

            base.Update(gameTime);
        }
    }
}
