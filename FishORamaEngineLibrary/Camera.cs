using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FishORamaEngineLibrary
{
    /// <summary>
    /// Camera class - manages the camera position and geometry
    /// </summary>
    public class Camera
    {
        private Matrix transform;       // Holds a variety of positional information about the camera, including scale and rotation

        public Matrix Transform { get => transform; }           // Property to access transform

        /// <summary>
        /// Camera Constructor
        /// </summary>
        /// <param name="pCameraPosition">Initial position of the camera</param>
        public Camera(Vector3 pCameraPosition)
        {
            transform = Matrix.CreateTranslation(pCameraPosition); // Create a new translation matrix from pCameraPosition
        }
    }
}
