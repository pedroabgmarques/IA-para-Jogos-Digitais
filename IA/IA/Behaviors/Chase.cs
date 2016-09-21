using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.Behaviors
{

    /// <summary>
    /// No jogo da apanhada terá mais sucesso o personagem que, com base na velocidade e direção do jogador, 
    /// consiga prever para onde o jogador se vai movimentar e, então, calcule uma rota que o intersecte.
    /// </summary>
    public class Chase : IA.Behaviors.Seek
    {

        //como este algoritmo irá alterar o alvo, definindo-o como sendo a 
        //posição de interseção das rotas, será necessário armazenar a
        //posição original do alvo.
        MovementInfo original_target;

        private float maxAcceleration;

        //Daqui a quantos segundos se pretende a interseção
        float maxPrediction;

        public Chase(float maxPrediction, float maxAcceleration)
        {
            this.maxPrediction = maxPrediction;
            this.maxAcceleration = maxAcceleration;
        }

        public Steering Update(
            MovementInfo origin,
            MovementInfo target)
        {
            this.origin = origin;
            this.target = target;

            return getSteering();
        }

        protected Steering getSteering()
        {
            //guardar o alvo original
            original_target = target;

            //calcular distancia ao alvo real
            Vector3 direction = original_target.position - origin.position;
            float distance = direction.Length();

            //calcular a nossa velocidade atual
            float speed = origin.velocity.Length();

            float prediction = 0;
            //conseguimos intersetar o alvo no tempo previsto?
            if (speed <= distance / maxPrediction)
            {
                //não conseguimos intersetar, usar tempo maximo
                prediction = maxPrediction;
            }
            else
            {
                //conseguimos intersetar, calcular quando
                prediction = distance / speed;
            }

            //alterar o alvo, colocando-o na posicao de interseção
            target.position += original_target.velocity * prediction;

            return base.Update(origin, target, maxAcceleration);
        }

    }
}
