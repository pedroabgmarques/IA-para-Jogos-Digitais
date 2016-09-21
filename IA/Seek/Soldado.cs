using IA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seek
{

    /// <summary>
    /// Descreve um soldado (player)
    /// </summary>
    public class Soldado
    {

        private Texture2D textura;
        private MovementInfo movementInfo;
        private Vector2 positionV2;

        public Soldado(ContentManager content, GraphicsDevice graphics)
        {
            positionV2 = Vector2.Zero;
            LoadContent(content);

            movementInfo.position = new Vector3(
                graphics.Viewport.Width / 2f - textura.Width / 2f, 
                0f, 
                graphics.Viewport.Height / 2f - textura.Height / 2f);
            movementInfo.orientation = 0f;
            movementInfo.velocity = Vector3.Zero;
            movementInfo.rotation = 0f;
        }

        private void LoadContent(ContentManager content)
        {
            textura = content.Load<Texture2D>("soldado");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            positionV2.X = movementInfo.position.X;
            positionV2.Y = movementInfo.position.Z;
            spriteBatch.Draw(textura, positionV2, Color.White);
        }

    }
}
