using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FishORamaEngineLibrary
{
    /// <summary>
    /// ChickenLeg class - for representation of fish food in scene
    /// </summary>
    public class ChickenLeg : IDraw, IToken
    {
        // CLASS VARIABLES
        private string textureID;       // Holds a string to identify asset used for this token
        private Vector2 position;       // Holds X and Y coordinates for token position on screen
        private bool removeFlag;        // Flag  - remove chickenLeg of true (polled by kernel update)

        /// PROPERTIES
        public string TextureID { get => textureID; }           // Property to access textureID
        public Vector2 Position { get => new Vector2(position.X, position.Y * -1); }    // Return Position with Y reversed to cater for reversed ycoordinate system
        public bool RemoveFlag { get => removeFlag; }           // Property to access removeFlag

        /// <summary>
        /// Constructor - sets token texture and position
        /// </summary>
        /// <param name="pTextureID"> String with texture name to associate preloaded asset</param>
        /// <param name="pPosition"> Vector2 with initial 2D position in scene</param>
        public ChickenLeg(string pTextureID, int pXpos, int pYpos)
        {
            textureID = pTextureID;
            position = new Vector2(pXpos, pYpos);
            removeFlag = false;
        }

        /// <summary>
        /// Remove Method - sets removeFlag to true (for polling by kernel to remove chickenleg from scene during main update loop)
        /// </summary>
        public void Remove()
        {
            removeFlag = true;
        }

        /// <summary>
        /// Draw Method
        /// </summary>
        /// <param name="pAssetManager"> Reference to the AssetManager which stores the assets required for drawing</param>
        /// <param name="pSpriteBatch"> Reference to an open SpriteBatch, to add this token's texture to</param>
        public void Draw(IGetAsset pAssetManager, SpriteBatch pSpriteBatch)
        {
            Asset currentAsset = pAssetManager.GetAssetByID(textureID); // Get this token's asset from the AssetManager

            // Draw an image centered at the token's position, using the associated texture
            pSpriteBatch.Draw(currentAsset.Texture,
                              position,
                              null,
                              Color.White,
                              0f,
                              new Vector2(currentAsset.Size.X / 2, currentAsset.Size.Y / 2),
                              new Vector2(1, 1),
                              SpriteEffects.None,
                              1);
        }
    }
}
