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
        
        public Matrix _transform; // Matrix Transform
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

        public Matrix get_transformation(GraphicsDeviceManager graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(pozycja.X, pozycja.Y, 0))   *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.GraphicsDevice.Viewport.Width * 0.5f, graphicsDevice.GraphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }

        // Get set position
        public Vector2 Pozycja
        {
            get { return pozycja; }
            set { pozycja = value; }
        }



//        Camera2d cam = new Camera2d();
//cam.Pos = new Vector2(500.0f,200.0f);
//// cam.Zoom = 2.0f // Example of Zoom in
//// cam.Zoom = 0.5f // Example of Zoom out
 
//spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
//                        SpriteSortMode.Immediate,
//                        SaveStateMode.SaveState,
//                        cam.get_transformation(device /*Send the variable that has your graphic device here*/));
 
//// Draw Everything
//// You can draw everything in their positions since the cam matrix has already done the maths for you 
 
//spriteBatch.End(); // Call Sprite Batch End


    }

}
