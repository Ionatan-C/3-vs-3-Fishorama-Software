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
    /// Asset class - which stores the texture assets loaded into the game engine
    /// </summary>
    public class Asset
    {
        private Texture2D texture;      // Stores the image with which to draw this asset
        private Vector2 size;           // The size of the image, in pixels

        public Texture2D Texture { get => texture; set => texture = value; }    // Property to access texture
        public Vector2 Size { get => size; set => size = value; }               // Property to access size

        /// <summary>
        /// Asset Constructor
        /// </summary>
        /// <param name="pTexture">Asset's image</param>
        /// <param name="pSize">Size of the given image</param>
        public Asset(Texture2D pTexture, Vector2 pSize)
        {
            texture = pTexture;
            size = pSize;
        }
    }
}
