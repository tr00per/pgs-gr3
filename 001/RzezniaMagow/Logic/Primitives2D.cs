using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RzezniaMagow
{
    public static class Primitives2D
    {
        public static Texture2D dotTexture { get; set; }

        public static void DrawLine(Vector2 start, Vector2 end, SpriteBatch SpriteBatch)
        {
            Vector2 StartToEndVector = end - start;
            int Times = (int)StartToEndVector.Length();
            StartToEndVector.Normalize();
            for (int i = 0; i < Times; i++)
                SpriteBatch.Draw(dotTexture, start + i * StartToEndVector, Color.White);
        }

        public static void DrawRectangle(Vector2 position, int Width, int Height,SpriteBatch SpriteBatch)
        {
            DrawLine(position, new Vector2() { X = position.X + Width, Y = position.Y }, SpriteBatch);
            DrawLine(new Vector2() { X = position.X + Width, Y = position.Y }, new Vector2() { X = position.X + Width, Y = position.Y+Height }, SpriteBatch);
            DrawLine(new Vector2() { X = position.X + Width, Y = position.Y+Height }, new Vector2() { X = position.X, Y = position.Y + Height }, SpriteBatch);
            DrawLine( new Vector2() { X = position.X, Y = position.Y + Height },position, SpriteBatch);
        }

        public static void DrawRectangle(List<Vector2> Points, SpriteBatch SpriteBatch)
        {

            DrawLine(Points[0], Points[1], SpriteBatch);
            DrawLine(Points[1], Points[2], SpriteBatch);
            DrawLine(Points[2], Points[3], SpriteBatch);
            DrawLine(Points[3], Points[0], SpriteBatch);
        }
        public static void DrawTriangle(List<Vector2> Points, SpriteBatch SpriteBatch)
        {
            DrawLine(Points[0], Points[1], SpriteBatch);
            DrawLine(Points[1], Points[2], SpriteBatch);
            DrawLine(Points[2], Points[0], SpriteBatch);
            
        }

        public static void DrawCircle(Vector2 centre, int radius, SpriteBatch SpriteBatch)
        {
            float ThetaStep = 0.1F;
            float Theta = 0;

            Vector2 PointToDraw;
            while (Theta < Math.PI*2)
            {
                PointToDraw = new Vector2() { X = (float)(radius * Math.Cos(Theta)), Y = (float)(radius * Math.Sin(Theta))}+centre;
                SpriteBatch.Draw(dotTexture, PointToDraw, Color.White);
                Theta += ThetaStep;
            }
        }


    }
}
