using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RzezniaMagow
{
    public static class Rotations
    {
        public static Vector2 RotatePoint(Vector2 PointToRotate, Vector2 OriginOfRotation, float ThetaInRads)
        {
            Vector2 RotationVector = PointToRotate - OriginOfRotation;
            Vector2 RotatedVector = new Vector2()
            {
                X = (float)(RotationVector.X * Math.Cos(ThetaInRads) - RotationVector.Y * Math.Sin(ThetaInRads)),
                Y = (float)(RotationVector.X * Math.Sin(ThetaInRads) + RotationVector.Y * Math.Cos(ThetaInRads))
            };

            return OriginOfRotation + RotatedVector;
        }
    }
}
