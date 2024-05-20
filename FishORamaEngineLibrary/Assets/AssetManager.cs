using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishORamaEngineLibrary
{
    /// <summary>
    /// AssetManager class - Asset manager to store a dictionary of drawable assets injected into the game at run time
    /// </summary>
    public class AssetManager : IGetAsset
    {
        private Dictionary<string, Asset> assets;       // A collection of key/value pairs representing each asset ID and the asset associated with it

        /// <summary>
        /// AssetManager Constructor
        /// </summary>
        public AssetManager()
        {
            assets = new Dictionary<string, Asset>();
        }

        /// <summary>
        /// LoadAsset method - Load a new asset into the manager's list
        /// </summary>
        /// <param name="pKey">ID to associate with the asset</param>
        /// <param name="pAsset">Asset object to be added to the list</param>
        public void LoadAsset(string pKey, Asset pAsset)
        {
            assets.Add(pKey, pAsset);
        }

        /// <summary>
        /// GetAssetByID method - Get an asset from the manager's list
        /// </summary>
        /// <param name="pKey">ID of the asset to retrieve</param>
        /// <returns>The asset object associated with the given ID</returns>
        public Asset GetAssetByID(string pKey)
        {
            return assets[pKey];
        }
    }
}
