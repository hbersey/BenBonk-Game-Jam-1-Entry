using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Util
{
    public static class MathUtil
    {

        private static readonly string[] units = {"", "k", "m", "b", "t", "q", "Q", "s", "S"};

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
            if (num < 1)
            {
                return "0";
            }

            var n = (int) Mathf.Log(num, 1000);
            var m = num / Mathf.Pow(1000, n);
            var unit = "";
            
            if (n < units.Length)
            {
                unit = units[n];
            }
            else
            {
                var unitInt = n - units.Length;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + 'A').ToString() + Convert.ToChar(secondUnit + 'A');
            }
            
            return (Mathf.Floor(m * 100) / 100).ToString("0.##") + unit;

        }
    }
}