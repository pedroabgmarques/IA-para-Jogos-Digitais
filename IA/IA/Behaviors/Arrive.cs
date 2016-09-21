using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{
    /// <summary>
    /// Com este comportamento o objeto dirige se para determinado alvo, 
    /// desacelarando quando se encontra a uma distancia predefinida, 
    /// e para quando atinge a sua posição.
    /// </summary>
    public class Arrive : Behavior
    {
        //origem do movimento
        MovementInfo origin;
        
        //destino
        MovementInfo target;

        //acelaracao maxima por segundo
        float maxAcceleration;

        //raio de chegada.
        float targetRadius;

        //raio de abrandamento
        float slowRadius;

        //tempo para a aceleracao maxima
        float timeToTarget;

        //distancia ao alvo
        float distance;

        //velocidade desejada
        float targetSpeed;

        //velocidade maxima
        float maxSpeed;

        Vector3 targetVelocity;

        public Arrive(float slowRadius, float targetRadius, float timeToTarget)
        {
            this.slowRadius = slowRadius;
            this.targetRadius = targetRadius;
            this.timeToTarget = timeToTarget;
        }

        public Steering Update(
            MovementInfo origin,
            MovementInfo target,
            float maxAcceleration,
            float maxSpeed)
        {
            this.origin = origin;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
            this.maxSpeed = maxSpeed;

            return getSteering();
        }

        protected override Steering getSteering()
        {
            Steering steering = new Steering();

            //direcao do movimento
            steering.linear = target.position - origin.position;

            //distancia ao alvo
            distance = steering.linear.Length();

            //verificar distancia ao alvo
            if(distance < targetRadius)
            {
                //chegamos
                steering.linear = Vector3.Zero;
            }
            //verificar se estamos perto.
            if(distance > slowRadius)
            {
                //ainda nao entramos na zona de abrandamento
                targetSpeed = maxSpeed;
            }
            else
            {
                targetSpeed = maxSpeed * distance / slowRadius;
            }

            //calcular vetor direcao com velocidade desejada
            targetVelocity = steering.linear;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            //quanto devemos acelerar para atingir velocidade desejada?
            if (steering.linear != Vector3.Zero)
            {
                steering.linear = targetVelocity - origin.velocity;
                steering.linear /= timeToTarget;
            }

            //garantir que nao acelera mais do que o maximo
            if(steering.linear.Length() > maxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= maxAcceleration;
            }

            steering.angular = 0f;

            Console.WriteLine(steering.linear.ToString());

            return steering;

        }
    }
}
