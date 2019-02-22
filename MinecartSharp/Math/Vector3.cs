using System;
using System.Collections.Generic;
using System.Text;

namespace MinecartSharp.Math
{
    class Vector3
    {
        public Vector3()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public int ToLocation()
        {
            return (((int)X & 0x3FFFFFF) << 38) | (((int)Y & 0xFFF) << 26) | ((int)Z & 0x3FFFFFF);
        }
    }
}
