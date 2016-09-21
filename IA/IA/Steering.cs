using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    /// <summary>
    /// Para permitir a junção dos vários comportamentos calculados,cada algoritmo irá indicar como
    /// o personagem deve alterar o seu movimento de modo a atingir o comportamento desejado. Essa 
    /// informação será dada por base em dois valores de aceleração: um linear, que indica a direção
    /// e velocidade a que o personagem se deve movimentar; e um angular, que indica a direção e 
    /// velocidade a que o personagem deve rodar sobre si mesmo
    /// </summary>
    public class Steering
    {
        public Vector3 linear; //linear steering
        public float angular; //angular steering

        /// <summary>
        /// Operator overload para permitir somar objetos do tipo steering, somando as suas propriedades
        /// </summary>
        public static Steering operator +(Steering s1, Steering s2)
        {
            Steering newSteering = new Steering();
            newSteering.linear = s1.linear + s2.linear;
            newSteering.angular = s1.angular + s2.angular;
            return newSteering;
        }

        public static Steering None()
        {
            Steering nullSteering = new Steering();
            nullSteering.linear = Vector3.Zero;
            nullSteering.angular = 0;
            return nullSteering;
        }
    }
}
