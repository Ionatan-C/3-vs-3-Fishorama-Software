using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FishORamaEngineLibrary;

namespace FishORama
{
    /// CLASS: Simulation class - the main class users code in to set up a FishORama simulation
    /// All tokens to be displayed in the scene are added here
    public class Simulation : IUpdate, ILoadContent
    {
        // CLASS VARIABLES
        // Variables store the information for the class
        private IKernel kernel;                 // Holds a reference to the game engine kernel which calls the draw method for every token you add to it
        private Screen screen;                  // Holds a reference to the screeen dimensions (width, height)
        private ITokenManager tokenManager;     // Holds a reference to the TokenManager - for access to ChickenLeg variable
        private Camera camera;
        
        
        /// PROPERTIES
        public ITokenManager TokenManager      // Property to access chickenLeg variable
        {
            set { tokenManager = value; }
        }

        // *** ADD YOUR CLASS VARIABLES HERE ***

        // EXAMPLE: DECLARATION of an OrangeFish list
        List<Piranha> Team1 = new List<Piranha>();
        List<Piranha> Team2 = new List<Piranha>();

        Random rand = new Random();

          


        /// CONSTRUCTOR - for the Simulation class - run once only when an object of the Simulation class is INSTANTIATED (created)
        /// Use constructors to set up the state of a class
        public Simulation(IKernel pKernel)
        {
            kernel = pKernel;                   // Stores the game engine kernel which is passed to the constructor when this class is created
            screen = kernel.Screen;             // Sets the screen variable in Simulation so the screen dimensions are accessible

            // *** ADD OTHER INITIALISATION (class setup) CODE HERE ***
            
        }

        
        
        /// METHOD: LoadContent - called once at start of program
        /// Create all token objects and 'insert' them into the FishORama engine
        public void LoadContent(IGetAsset pAssetManager)
        {
            // *** ADD YOUR NEW TOKEN CREATION CODE HERE ***

            // Create 5 OrangeFish in loop
            for (int i = 0; i < 3; i++)
            {
                int Team = 1;

                // Left side
                Piranha Fish1 = new Piranha("Piranha1", -300, 150 - 150 * i, 5,screen, tokenManager, Team, i + 1);
                // Add to list
                Team1.Add(Fish1);
                // Insert into game engine
                kernel.InsertToken(Fish1);


            }

            for (int i = 0; i < 3; i++)
            {
                int Team = 2;

                // Right Side
                Piranha Fish1 = new Piranha("Piranha2", 300, 150 - 150 * i, 5, screen, tokenManager, Team, i + 1);
                // Add to list
                Team2.Add(Fish1);
                // Insert into game engine
                kernel.InsertToken(Fish1);

            }

        }
        // Make the vector of the chicken leg and the vector of the fish take away from each other and then it will give you the line which u need to know
        public void PlaceLeg()
        {

            int randNum = rand.Next(1, 200);

            if (randNum == 3)
            {
                if (tokenManager.ChickenLeg == null)
                {

                    ChickenLeg newChickenLeg = new ChickenLeg("ChickenLeg", 0, 0);
                    tokenManager.SetChickenLeg(newChickenLeg);
                    kernel.InsertToken(newChickenLeg);

                }
            }

        }

        /// METHOD: Update - called 60 times a second by the FishORama engine when the program is running
        /// Add all tokens so Update is called on them regularly
        public void Update(GameTime gameTime)
        {

            // *** ADD YOUR UPDATE CODE HERE ***

            // Loop over list and call update on every fish
            foreach (Piranha fish in Team1)
            {
                fish.Update();
            }

            foreach (Piranha fish in Team2)
            {
                fish.Update();
            }


            PlaceLeg();

        }
    }
}
