using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{
    /// <summary>
    /// Comportamento identico ao Seek. A unica diferença é que se afasta do alvo. Diferente na linha 37
    /// </summary>
    public class Flee : Behavior
    {
        //origem do movimento
        private MovementInfo origin;

        //destino do movimento
        private MovementInfo target;

        //aceleração máxima, por segundo
        private float maxAcceleration;

        public void Update(
            MovementInfo origin,
            MovementInfo target,
            float maxAcceleration)
        {
            this.origin = origin;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
        }

        protected override Steering getSteering()
        {
            Steering steering = new Steering();

            //calcular vetor na direção desejada. A unica diferença para o Seek é que se afasta do target.
            steering.linear = origin.position - target.position;

            //calcular aceleração máxima permitida
            steering.linear.Normalize();
            steering.linear *= maxAcceleration;

            //não adicionar qualquer rotação
            steering.angular = 0;

            return steering;
        }
    }
}
