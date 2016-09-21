using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{

    /// <summary>
    /// Com este algoritmo pretende-se que determinado personagem imite a orientação de um outro
    /// objeto (objeto alvo). Para este algoritmo será definida uma velocidade máxima para a rotação,
    /// uma área de abrandamento, e uma área de chegada. Esta área é, na verdade, uma margem
    /// de erro. Qualquer orientação que esteja dentro dessa margem de erro em relação à orientação
    /// desejada será considerada final.
    /// </summary>
    public class Align : Behavior
    {

        //origem do movimento
        private MovementInfo origin;

        //destino
        private MovementInfo target;

        //aceleração angular máxima
        private float maxAngAccel;

        //velocidade de rotação máxima
        private float maxRotation;

        //angulo para desaceleração
        private float slowRadius;

        //margem de erro
        private float targetRadius;

        //tempo para aceleração máxima
        private float timeToTarget;

        public Align(float maxAngAccel, float maxRotation, float slowRadius, float targetRadius, float timeToTarget)
        {
            this.maxAngAccel = maxAngAccel;
            this.maxRotation = maxRotation;
            this.slowRadius = slowRadius;
            this.targetRadius = targetRadius;
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

            //calcular distancia da rotacao
            float rotation = origin.orientation - target.orientation;
            //normalizar rotação entre -PI e PI
            rotation = Utils.normAngle(rotation);
            //calcular distancia (angulo) absoluta
            float rotationSize = Math.Abs(rotation);

            float targetRotation = 0;

            //já chegamos?
            if (rotationSize < targetRadius)
            {
                return Steering.None();
            }

            //estamos muito longe?
            if (rotationSize > slowRadius)
            {
                targetRotation = maxRotation;
            }
            else
            {
                targetRotation = maxRotation * rotationSize / slowRadius;
            }

            //rotação de destino combina velocidade com direção
            targetRotation *= Utils.signal(rotation);

            //aceleração necessária para obter rotação desejada
            steering.angular = targetRotation - rotation;
            steering.angular /= timeToTarget;

            //mas não fazer a rotação demasiado rápida
            float angularAcceleration = Math.Abs(steering.angular);

            if (angularAcceleration > maxAngAccel)
            {
                steering.angular /= angularAcceleration;
                steering.angular *= maxAngAccel;
            }

            steering.linear = Vector3.Zero;
            return steering;
        }

    }
}
