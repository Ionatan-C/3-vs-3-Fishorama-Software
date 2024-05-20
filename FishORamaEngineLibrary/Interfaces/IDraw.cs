using Microsoft.Xna.Framework.Graphics;

namespace FishORamaEngineLibrary
{
    public interface IDraw
    {
        void Draw(IGetAsset pAssets, SpriteBatch pSpriteBatch);
    }
}
