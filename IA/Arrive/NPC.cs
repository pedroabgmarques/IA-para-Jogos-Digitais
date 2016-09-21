using IA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrive
{

    /// <summary>
    /// Define uma personagem controlada por IA
    /// </summary>
    public class NPC : MovableEntity
    {

        private MovableEntity target;

        //Behaviors
        IA.Behaviors.Arrive arrive;

        public NPC(Random rnd, ContentManager content, GraphicsDevice graphics, MovableEntity target, float maxSpeed)
        {

            LoadContent(content);

            //Behaviors
            arrive = new IA.Behaviors.Arrive(100f, 50f, 2f);

            //Inicializações
            movementInfo.position = new Vector3(
                rnd.Next(0, graphics.Viewport.Width - textura.Width),
                0f,
                rnd.Next(0, graphics.Viewport.Height - textura.Height));
            movementInfo.orientation = 0f;
            movementInfo.velocity = Vector3.Zero;
            movementInfo.rotation = 0f;

            origin = new Vector2(textura.Width / 2f, textura.Height / 2f);
            this.maxSpeed = maxSpeed;
            this.target = target;

            steering = new Steering();
        }

        private void LoadContent(ContentManager content)
        {
            textura = content.Load<Texture2D>("soldadoNPC");
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            steering = Steering.None();

            //atualizar posição e orientação
            movementInfo.position += movementInfo.velocity * gameTime.ElapsedGameTime.Milliseconds;
            movementInfo.orientation += movementInfo.rotation * gameTime.ElapsedGameTime.Milliseconds;

            //aplicar atrito
            movementInfo.velocity *= 0.95f;
            movementInfo.rotation *= 0.95f;

            //calcular novo movimento
            steering = arrive.Update(movementInfo, target.getMovementInfo(), 0.1f, 0.1f);

            movementInfo.velocity += steering.linear;
            movementInfo.rotation += steering.angular;

            //garantir velocidade máxima
            if (movementInfo.velocity.Length() > maxSpeed)
            {
                movementInfo.velocity.Normalize();
                movementInfo.velocity *= maxSpeed;
            }

            base.Update(graphics);

        }
    }
}
