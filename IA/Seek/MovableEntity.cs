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

        private float xAnterior = 0, zAnterior = 0;

        public MovementInfo getMovementInfo()
        {
            return movementInfo;
        }

        protected void Update(GraphicsDevice graphics)
        {
            //Limitar os bichos aos limites do ecrã
            if (movementInfo.position.X - textura.Width / 2f < 0)
            {
                movementInfo.position.X = xAnterior;
            }
            if (movementInfo.position.X + textura.Width / 2f > graphics.Viewport.Width)
            {
                movementInfo.position.X = xAnterior;
            }
            if (movementInfo.position.Z - textura.Width / 2f < 0)
            {
                movementInfo.position.Z = zAnterior;
            }
            if (movementInfo.position.Z + textura.Width / 2f > graphics.Viewport.Height)
            {
                movementInfo.position.Z = zAnterior;
            }

            xAnterior = movementInfo.position.X;
            zAnterior = movementInfo.position.Z;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            positionV2.X = movementInfo.position.X;
            positionV2.Y = movementInfo.position.Z;
            spriteBatch.Draw(textura, positionV2, null, Color.White, movementInfo.orientation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
