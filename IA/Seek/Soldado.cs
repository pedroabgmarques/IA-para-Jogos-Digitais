using IA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seek
{

    /// <summary>
    /// Descreve um soldado (player)
    /// </summary>
    public class Soldado : MovableEntity
    {

        private Texture2D textura;
        private Vector2 positionV2;
        private Vector2 origin;

        public Soldado(ContentManager content, GraphicsDevice graphics, float maxSpeed)
        {
            positionV2 = Vector2.Zero;
            LoadContent(content);

            origin = new Vector2(textura.Width / 2f, textura.Height / 2f);

            movementInfo.position = new Vector3(
                graphics.Viewport.Width / 2f - textura.Width / 2f, 
                0f, 
                graphics.Viewport.Height / 2f - textura.Height / 2f);
            movementInfo.orientation = 0f;
            movementInfo.velocity = Vector3.Zero;
            movementInfo.rotation = 0f;
            this.maxSpeed = maxSpeed;
            steering = new Steering();
        }

        public void Update(GameTime gameTime)
        {

            //atualizar posição e orientação
            movementInfo.position += movementInfo.velocity * gameTime.ElapsedGameTime.Milliseconds;
            movementInfo.orientation += movementInfo.rotation * gameTime.ElapsedGameTime.Milliseconds;

            //aplicar atrito
            movementInfo.velocity *= 0.95f;
            movementInfo.rotation *= 0.95f;

            //calcular novo movimento
            steering = Steering.None();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //Frente
                steering.linear = Vector3.Normalize(Utils.orientationToVector(movementInfo.orientation));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //Trás
                steering.linear = -Vector3.Normalize(Utils.orientationToVector(movementInfo.orientation));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //Virar à esquerda
                steering.angular = -0.0001f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //Virar à direita
                steering.angular = 0.0001f;
            }

            movementInfo.velocity += steering.linear;
            movementInfo.rotation += steering.angular;

            //garantir velocidade máxima
            if (movementInfo.velocity.Length() > maxSpeed)
            {
                movementInfo.velocity.Normalize();
                movementInfo.velocity *= maxSpeed;
            }

            
        }

        private void LoadContent(ContentManager content)
        {
            textura = content.Load<Texture2D>("soldado");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            positionV2.X = movementInfo.position.X;
            positionV2.Y = movementInfo.position.Z;
            spriteBatch.Draw(textura, positionV2, null, Color.White, movementInfo.orientation, origin, 1f, SpriteEffects.None, 0f);
        }

    }
}
