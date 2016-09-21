using IA;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seek
{

    /// <summary>
    /// Define uma personagem controlada por IA
    /// </summary>
    public class NPC
    {
        public MovementInfo movementInfo;

        //teste
        private MovementInfo target;

        //variavel reutilizada
        private Steering steering;

        private float maxSpeed;

        //Behaviors
        IA.Behaviors.Seek seek;

        public NPC(float maxSpeed)
        {
            seek = new IA.Behaviors.Seek();
            this.maxSpeed = maxSpeed;

            //teste
            target.position = new Vector3(100, 0, 100);
            target.velocity = new Vector3(0.1f, 0f, 0.2f);
            target.orientation = 0.2f;
            target.rotation = 0.01f;
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
            seek.Update(movementInfo, target, 0.1f);
            steering = seek.getSteering();

            movementInfo.velocity += steering.linear;
            movementInfo.rotation += steering.angular;

            //garantir velocidade máxima
            if (movementInfo.velocity.Length() > maxSpeed)
            {
                movementInfo.velocity.Normalize();
                movementInfo.velocity *= maxSpeed;
            }

        }
    }
}
