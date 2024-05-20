using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FishORamaEngineLibrary
{
    /// <summary>
    /// Kernel class - This is the main class for your game.
    /// Version 2.0 - 09/09/20
    /// Author - Chris Blythe
    /// </summary>
    public class Kernel : Game, IKernel
    {
        private GraphicsDeviceManager graphics;     // Used to control the graphics device which draws each frame
        private SpriteBatch spriteBatch;            // Part of the drawing process, used to batch textures together to improve render speed
        private List<IDraw> drawables;              // List of each token to be drawn each frame
        private IUpdate simulation;                 // Holds a reference to the game simulation to be able to call its Update method each frame
        private AssetManager assetManager;          // Stores every texture that can be used by tokens
        private ITokenManager tokenManager;         // Holds a reference to the TokenManager - for access to ChickenLeg variable
        private Camera camera;                      // Every token is drawn in a position relative to this camera
        private Screen screen;                      // Holds screen dimensions (width, height)

        /// PROPERTIES
        public IUpdate Simulation { set => simulation = value; }
        public Screen Screen { get => screen; }
        public ITokenManager TokenManager { set => tokenManager = value; }

        /// <summary>
        /// Kernel constructor - creates Game components required by MonoGame
        /// </summary>
        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);

            // Set screen dimensions
            screen = new Screen(1200, 900);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        /// <summary>
        /// Initialize method - Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic related content.
        /// Calling base.Initialize will enumerate through any components and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set display window size
            graphics.PreferredBackBufferWidth = screen.width;
            graphics.PreferredBackBufferHeight = screen.height;

            graphics.ApplyChanges();

            assetManager = new AssetManager();
            camera = new Camera(new Vector3(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 0)); // Create a new camera at the center of the viewport
            drawables = new List<IDraw>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent method - will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("Welcome to the FishORama Framework");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // Load each texture into the AssetManager as an Asset
            Asset asset;

            asset = new Asset(Content.Load<Texture2D>("Background"), new Vector2(800, 600));
            assetManager.LoadAsset("Background", asset);

            asset = new Asset(Content.Load<Texture2D>("AquariumBackground"), new Vector2(800, 600));
            assetManager.LoadAsset("AquariumBackground", asset);

            asset = new Asset(Content.Load<Texture2D>("Midground_Rocks"), new Vector2(800, 600));
            assetManager.LoadAsset("Midground_Rocks", asset);

            asset = new Asset(Content.Load<Texture2D>("ForegroundSand"), new Vector2(800, 600));
            assetManager.LoadAsset("ForegroundSand", asset);

            asset = new Asset(Content.Load<Texture2D>("Foreground_Rocks"), new Vector2(800, 600));
            assetManager.LoadAsset("Foreground_Rocks", asset);

            asset = new Asset(Content.Load<Texture2D>("OrangeFish"), new Vector2(128, 86));
            assetManager.LoadAsset("OrangeFish", asset);

            asset = new Asset(Content.Load<Texture2D>("BlueFish"), new Vector2(128, 86));
            assetManager.LoadAsset("BlueFish", asset);

            asset = new Asset(Content.Load<Texture2D>("Seahorse"), new Vector2(74, 128));
            assetManager.LoadAsset("Seahorse", asset);

            asset = new Asset(Content.Load<Texture2D>("Urchin"), new Vector2(180, 112));
            assetManager.LoadAsset("Urchin", asset);

            asset = new Asset(Content.Load<Texture2D>("Piranha1"), new Vector2(132, 128));
            assetManager.LoadAsset("Piranha1", asset);

            asset = new Asset(Content.Load<Texture2D>("Piranha2"), new Vector2(110, 128));
            assetManager.LoadAsset("Piranha2", asset);

            asset = new Asset(Content.Load<Texture2D>("AnglerFish_Lit"), new Vector2(128, 108));
            assetManager.LoadAsset("AnglerFish_Lit", asset);

            asset = new Asset(Content.Load<Texture2D>("AnglerFish_Unlit"), new Vector2(128, 108));
            assetManager.LoadAsset("AnglerFish_Unlit", asset);

            asset = new Asset(Content.Load<Texture2D>("Bubble"), new Vector2(32, 32));
            assetManager.LoadAsset("Bubble", asset);

            asset = new Asset(Content.Load<Texture2D>("ChickenLeg"), new Vector2(128, 128));
            assetManager.LoadAsset("ChickenLeg", asset);

            asset = new Asset(Content.Load<Texture2D>("DRUM_Red"), new Vector2(64, 31));
            assetManager.LoadAsset("DRUM_Red", asset);

            asset = new Asset(Content.Load<Texture2D>("DRUM_Blue"), new Vector2(64, 31));
            assetManager.LoadAsset("DRUM_Blue", asset);

            // Create a new Aquarium to act as the background for the game
            Aquarium aquarium = new Aquarium("AquariumBackground", new Vector2(0, 0), assetManager);
            InsertToken(aquarium);

            (simulation as ILoadContent).LoadContent(assetManager); // Call the kernel's LoadContent method, for it to create tokens and add them to the game
        }

        /// <summary>
        /// UnloadContent method - will be called once per game and is the place to unload game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Update method - Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // If a chicken leg is not currently placed and the left mouse button is pressed
            if (tokenManager.ChickenLeg == null && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Capture the current position of the mouse and align it to the camera
                Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
                mousePosition.X -= camera.Transform.Translation.X;
                mousePosition.Y -= camera.Transform.Translation.Y;

                // Check if click within window
                if (mousePosition.X < screen.width / 2
                    && mousePosition.X > (screen.width / 2) * -1
                    && mousePosition.Y < screen.height / 2
                    && mousePosition.Y > (screen.height / 2) * -1)
                {
                    // Place a new chicken leg at the position of the mouse
                    ChickenLeg newChickenLeg = new ChickenLeg("ChickenLeg", (int)mousePosition.X, (int)mousePosition.Y);
                    tokenManager.SetChickenLeg(newChickenLeg);
                    InsertToken(newChickenLeg);
                }
            }

            // Right click to remove chicken leg
            if (tokenManager.ChickenLeg != null && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                RemoveChickenLeg();
            }

            // Poll chicken leg if present and check remove flag - remove if true
            if (tokenManager.ChickenLeg != null)
            {
                if (tokenManager.ChickenLeg.RemoveFlag)
                {
                    RemoveChickenLeg();
                }
            }

            // Call update on Simulation to run main game loop
            simulation.Update(gameTime);

            // Update MonoGame game engine
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw method - This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Start a new batch, aligned to the camera at the center of the viewport
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                              null,
                              null,
                              null,
                              null,
                              null,
                              camera.Transform);

            // Call the draw method for each token in drawables, passing the open sprite batch
            foreach (IDraw Token in drawables)
            {
                Token.Draw(assetManager, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// InsertToken method - Insert a new token into the kernel, so that it can be drawn every frame
        /// </summary>
        /// <param name="pToken">The token to be inserted</param>
        public void InsertToken(IDraw pToken)
        {
            drawables.Add(pToken);
        }

        /// <summary>
        /// RemoveToken method - Remove an existing token from the kernel, so that its draw method won't be called on future updates
        /// </summary>
        /// <param name="pToken">The token to be removed</param>
        public void RemoveToken(IDraw pToken)
        {
            drawables.Remove(pToken);
        }

        /// <summary>
        /// RemoveChickenLeg method - Removes the chicken leg from the game, if it exists
        /// </summary>
        private void RemoveChickenLeg()
        {
            if (tokenManager.ChickenLeg != null)
            {
                RemoveToken(tokenManager.ChickenLeg);
                tokenManager.SetChickenLeg(null);
            }
        }
    }

    // Bespoke data structure to store screen dimensions
    public struct Screen
    {
        public int width, height;

        public Screen(int p1, int p2)
        {
            width = p1;
            height = p2;
        }
    }
}
