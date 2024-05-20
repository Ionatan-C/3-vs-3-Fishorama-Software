using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FishORamaEngineLibrary;

namespace FishORama
{
    /// CLASS: OrangeFish - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class Piranha : IDraw
    {

        // CLASS VARIABLES
        // Variables hold the information for the class
        // NOTE - these variables must be present for the class to act as a TOKEN for the FishORama engine
        protected string textureID;               // Holds a string to identify asset used for this token
        protected float xPosition;                // Holds the X coordinate for token position on screen
        protected float yPosition;                // Holds the Y coordinate for token position on screen
        protected int xDirection;                 // Holds the direction the token is currently moving - X value should be either -1 (left) or 1 (right)
        protected int yDirection;                 // Holds the direction the token is currently moving - Y value should be either -1 (down) or 1 (up)
        protected Screen screen;                  // Holds a reference to the screen dimansions (width and height)
        protected ITokenManager tokenManager;     // Holds a reference to the TokenManager - for access to ChickenLeg variable

        // *** ADD YOUR CLASS VARIABLES HERE *** 
        protected float Speed = 5;
        protected Random rand;
        protected int imageWidth;
        protected int imageHeight;
        // CLASS VARIABLES
        // Variables hold the information for the class
        // NOTE - these variables must be present for the class to act as a TOKEN for the FishORama engine

        float startingPosition;
        float maxY;
        float minY;
        int Team;
        int fishNum;
        float initX;
        float initY;
        float angle;
        /// CONSTRUCTOR: OrangeFish Constructor
        /// The elements in the brackets are PARAMETERS, which will be covered later in the course
        public Piranha(string pTextureID, float pXpos, float pYpos, float Speed, Screen pScreen, ITokenManager pTokenManager, int pTeam, int pFishNum)
        {
            // State initialisation (setup) for the object
            textureID = pTextureID;
            xPosition = pXpos;
            yPosition = pYpos;
            xDirection = 1;
            yDirection = 1;
            screen = pScreen;
            tokenManager = pTokenManager;
            Team = pTeam;
            fishNum = pFishNum;
            initX = xPosition;
            initY = yPosition;
            angle = 0;


            // *** ADD OTHER INITIALISATION (class setup) CODE HERE ***
            // Hard code OrangeFish image dimensions
            imageWidth = 128;
            imageHeight = 64;

            startingPosition = yPosition;
            maxY = startingPosition + 50;
            minY = startingPosition - 50;

        }


        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        /// Write the movement control code here
        public void Update()
        {
            // *** ADD YOUR MOVEMENT/BEHAVIOUR CODE HERE ***
            // screen edge collision detection

            //makes them go up and down


            //changes the way they are facing
            if (Team == 1)
            {
                xDirection = 1;
            }
            else if (Team == 2)
            {
                xDirection = -1;
            }
            
            
            // UPDATE POSITION
            // horizontal movement
            //xPosition += Speed * xDirection;
            // vertical movement
            //yPosition += Speed * yDirection;

            // sin will workout your Y axis and cos will workout your X

            if (tokenManager.ChickenLeg != null)
            {
                float startingPos = xPosition;

                Vector2 fishPos = new Vector2(xPosition, yPosition);

                Vector2 mov_vec = Vector2.Subtract(tokenManager.ChickenLeg.Position, fishPos);

                Vector2 norm_mov_vec = Vector2.Normalize(mov_vec);

                Vector2 final_vec = norm_mov_vec * Speed;

                xPosition += final_vec.X;
                yPosition += final_vec.Y;

                if (mov_vec.Length() <= 30.0)
                {
                    Console.WriteLine("Fish {0} from Team {1} has Won", fishNum, Team);
                    tokenManager.ChickenLeg.Remove();
                }

            }

            if(tokenManager.ChickenLeg == null)
            {
                float radius = 50;

                float CircleX = initX + (float)Math.Cos(angle) * radius;
                float CircleY = initY + (float)Math.Sin(angle) * radius;

                angle += (float)0.08;

                xPosition = Convert.ToInt32(CircleX);
                yPosition = Convert.ToInt32(CircleY);

            }

        }
       
        /// METHOD: Draw - Called repeatedly by FishORama engine to draw token on screen
        /// DO NOT ALTER, and ensure this Draw method is in each token (fish) class
        /// Comments explain the code - read and try and understand each lines purpose
        public virtual void Draw(IGetAsset pAssetManager, SpriteBatch pSpriteBatch)
        {
            Asset currentAsset = pAssetManager.GetAssetByID(textureID); // Get this token's asset from the AssetManager

            SpriteEffects horizontalDirection; // Stores whether the texture should be flipped horizontally

            if (xDirection < 0)
            {
                // If the token's horizontal direction is negative, draw it reversed
                horizontalDirection = SpriteEffects.FlipHorizontally;
            }
            else
            {
                // If the token's horizontal direction is positive, draw it regularly
                horizontalDirection = SpriteEffects.None;
            }

            // Draw an image centered at the token's position, using the associated texture / position
            pSpriteBatch.Draw(currentAsset.Texture,                                             // Texture
                              new Vector2(xPosition, yPosition * -1),                                // Position
                              null,                                                             // Source rectangle (null)
                              Color.White,                                                      // Background colour
                              0f,                                                               // Rotation (radians)
                              new Vector2(currentAsset.Size.X / 2, currentAsset.Size.Y / 2),    // Origin (places token position at centre of sprite)
                              new Vector2(1, 1),                                                // scale (resizes sprite)
                              horizontalDirection,                                              // Sprite effect (used to reverse image - see above)
                              1);                                                               // Layer depth
        }
    }
}
