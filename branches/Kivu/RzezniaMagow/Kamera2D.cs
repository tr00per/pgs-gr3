using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace RzezniaMagow
{
    public class Kamera2d
    {
        
        public Matrix transform; // Matrix Transform
        private Vector2 pozycja; // Camera Position
        

        public Kamera2d()
        {
            pozycja = Vector2.Zero;
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 wektor)
        {
            pozycja += wektor;
        }

        public Matrix getTransformation(GraphicsDeviceManager graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(-pozycja.X, -pozycja.Y, 0))   
                        * Matrix.CreateTranslation(new Vector3(graphicsDevice.GraphicsDevice.Viewport.Width * 0.5f, graphicsDevice.GraphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }

        // Get set position
        public Vector2 getPozycja
        {
            get { return pozycja; }
            set { pozycja = value; }
        }


    }

}
