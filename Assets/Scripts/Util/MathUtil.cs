using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Util
{
    public static class MathUtil
    {
        private static readonly string[] Abbreviations =
            {"k", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH", "XX", "YY", "ZZ"};

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

        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public static string Abbreviate(int num)
        {
            if (num < 1000)
                return num.ToString();
            else
                for (var i = 0; i < Abbreviations.Length - 1; i++)
                {
                    // 0 -> k
                    // 1 -> M

                    if (num < Mathf.Pow(10, 3 * (i + 1) + 1))
                        return $"{num / Mathf.Pow(10f, 3 * (i + 1)):F1}{Abbreviations[i]}";

                    if (num < Mathf.Pow(10, 3 * (i + 1) + 3))
                    {
                    }

                    return $"{num / ((int) Mathf.Pow(10, 3 * (i + 1))):D}{Abbreviations[i]}".Replace(".0", "");
                }

            return "BIG";
        }
    }
}