/*
 * Utilidades para a IA de cálculo de movimentos
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    /// <summary>
    /// Classe estática que disponibiliza métodos utilitários
    /// </summary>
    public static class Utils
    {

        //Converte um angulo em radianos para um vector
        public static Vector3 orientationToVector(float orientation)
        {
            return new Vector3((float)Math.Sin(orientation), 0f, -(float)Math.Cos(orientation));
        }

        //Normaliza um angulo para um valor entre -pi e pi
        public static float normAngle(float angle)
        {
            while (angle > Math.PI)
            {
                angle -= MathHelper.TwoPi;
            }
            while (angle < -Math.PI)
            {
                angle += MathHelper.TwoPi;
            }
            return angle;
        }

        //devolve o sinal de um float
        public static float signal(float valor)
        {
            if (valor < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

    }
}
