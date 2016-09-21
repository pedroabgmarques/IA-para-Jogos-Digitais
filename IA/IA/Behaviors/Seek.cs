using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{

    /// <summary>
    /// Com este comportamento pretende-se que o personagem atinja determinada posição. Será calculada a 
    /// direção em que se deve movimentar, e retornar um aceleração nessa direção. Será só tida em conta a 
    /// aceleração máxima permitida.
    /// Note-se que não há qualquer preocupação com uma aceleração ou desaceleração progressiva ao 
    /// aproximar-se do destino.
    /// </summary>
    public class Seek : Behavior
    {
        //origem do movimento
        private MovementInfo origin;

        //destino do movimento
        private MovementInfo target;

        //aceleração máxima, por segundo
        private float maxAcceleration;

        public Steering Update(
            MovementInfo origin,
            MovementInfo target,
            float maxAcceleration)
        {
            this.origin = origin;
            this.target = target;
            this.maxAcceleration = maxAcceleration;

            return getSteering();
        }

        protected override Steering getSteering()
        {
            Steering steering = new Steering();

            //calcular vetor na direção desejada
            steering.linear = target.position - origin.position;

            //calcular aceleração máxima permitida
            steering.linear.Normalize();
            steering.linear *= maxAcceleration;

            //não adicionar qualquer rotação
            steering.angular = 0;

            return steering;
        }
    }
}
