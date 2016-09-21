using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{

    /// <summary>
    /// Este movimento tenta imitar a velocidade (e direção do movimento) do alvo escolhido. 
    /// É semelhante ao algoritmo do movimento de “Procura” mas em vez de tentar atingir a posição do alvo,
    /// tentar-se-á atingir a sua velocidade.
    /// </summary>
    public class Velocity : Behavior
    {

        //origem do movimento
        private MovementInfo origin;

        //destino
        private MovementInfo target;

        //aceleração maxima
        private float maxAcceleration;

        //tempo para aceleração máxima
        private float timeToTarget;

        public Velocity(float maxAcceleration, float timeToTarget)
        {
            this.maxAcceleration = maxAcceleration;
            this.timeToTarget = timeToTarget;
        }

        public Steering Update(
            MovementInfo origin,
            MovementInfo target)
        {
            this.origin = origin;
            this.target = target;

            return getSteering();
        }

        protected override Steering getSteering()
        {
            Steering steering = new Steering();

            //não mexer se o alvo não se mexe
            if (target.velocity.Length() == 0)
            {
                return Steering.None();
            }

            //acelerar e tentar obter a velicidade desejada
            steering.linear = target.velocity - origin.velocity;
            steering.linear /= timeToTarget;

            //não acelerar demasiado
            if (steering.linear.Length() > maxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= maxAcceleration;
            }

            steering.angular = 0;
            
            return steering;
        }

    }
}
