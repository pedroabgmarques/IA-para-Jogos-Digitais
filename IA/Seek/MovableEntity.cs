using IA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seek
{

    /// <summary>
    /// Class documentation
    /// </summary>
    public class MovableEntity
    {
        protected MovementInfo movementInfo;
        protected Steering steering;
        protected float maxSpeed;

        protected Texture2D textura;
        protected Vector2 positionV2;
        protected Vector2 origin;

        public MovementInfo getMovementInfo()
        {
            return movementInfo;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            positionV2.X = movementInfo.position.X;
            positionV2.Y = movementInfo.position.Z;
            spriteBatch.Draw(textura, positionV2, null, Color.White, movementInfo.orientation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
