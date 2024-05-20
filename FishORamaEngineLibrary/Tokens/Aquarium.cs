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
    /// Aquarium class - A drawable Token that provides the aquarium background image
    /// </summary>
    class Aquarium : IDraw
    {
        private string textureID;       // Holds a string to identify asset used for this token
        private Vector2 position;       // Holds X and Y coordinates for token position on screen
        private Vector2 size;           // Holds the size of the image associated with this token

        /// <summary>
        /// Aquarium Constructor
        /// </summary>
        /// <param name="pTextureID">ID of the texture that should be used for this token</param>
        /// <param name="pPosition">Starting coordinates of the token</param>
        /// <param name="pAssetManager">Reference to the AssetManager, used to find the size of the token's visible texture</param>
        public Aquarium(string pTextureID, Vector2 pPosition, AssetManager pAssetManager)
        {
            textureID = pTextureID;
            position = pPosition;
            size = pAssetManager.GetAssetByID(textureID).Size;
        }

        /// <summary>
        /// Draw Method
        /// </summary>
        /// <param name="pAssetManager">Reference to the AssetManager which stores the assets required for drawing</param>
        /// <param name="pSpriteBatch">Reference to an open SpriteBatch, to add this token's texture to</param>
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
                              new Vector2(1.5f, 1.5f),
                              SpriteEffects.None,
                              1);
        }
    }
}
