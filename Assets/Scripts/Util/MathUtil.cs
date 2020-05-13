using Unity.Mathematics;
using UnityEngine;

namespace Util
{
    public static class MathUtil
    {

        public static float Wrap(float x, float min, float max)
        {
            while (x > max) x -= max + 1;
            while (x < min) x += max + 1;

            return x;
        }

        public static int Wrap(int x, int min, int max)
        {
            return (int) Wrap((float) x, min, max);
        }
        
    }
}