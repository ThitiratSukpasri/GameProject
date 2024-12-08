using Microsoft.Xna.Framework;
using System;
using System.Text;

namespace ThanaNita.MonoGameTnt
{
    // Design Decision: Created as a "class" not "struct" because class can be returned and passed more effeciently for large data type.
    //   see: https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct
    // Immutable Type: its internal values will not be changed after constructed.
    public class Matrix3 : IEquatable<Matrix3>
    {
        private float m11, m12, m13;
        private float m21, m22, m23;
        private float m31, m32, m33;

        public float M11 { get => m11; }
        public float M12 { get => m12; }
        public float M13 { get => m13; }
        public float M21 { get => m21; }
        public float M22 { get => m22; }
        public float M23 { get => m23; }
        public float M31 { get => m31; }
        public float M32 { get => m32; }
        public float M33 { get => m33; }

        public Matrix3( float m11, float m12, float m13,
                        float m21, float m22, float m23,
                        float m31, float m32, float m33)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }
        
        public static Matrix3 operator -(Matrix3 m) // Unary Minus
        {
            return new Matrix3(
                -m.m11, -m.m12, -m.m13,
                -m.m21, -m.m22, -m.m23,
                -m.m31, -m.m32, -m.m33
                );
        }
        public static Matrix3 operator +(Matrix3 l, Matrix3 r)
        {
            return new Matrix3(
                l.m11 + r.m11, l.m12 + r.m12, l.m13 + r.m13,
                l.m21 + r.m21, l.m22 + r.m22, l.m23 + r.m23,
                l.m31 + r.m31, l.m32 + r.m32, l.m33 + r.m33
                );
        }
        public static Matrix3 operator -(Matrix3 l, Matrix3 r)
        {
            return new Matrix3(
                l.m11 - r.m11, l.m12 - r.m12, l.m13 - r.m13,
                l.m21 - r.m21, l.m22 - r.m22, l.m23 - r.m23,
                l.m31 - r.m31, l.m32 - r.m32, l.m33 - r.m33
                );
        }

        public static Matrix3 operator *(Matrix3 m, float val) 
        {
            return new Matrix3(
                m.m11 * val, m.m12 * val, m.m13 * val,
                m.m21 * val, m.m22 * val, m.m23 * val,
                m.m31 * val, m.m32 * val, m.m33 * val
                );
        }
        public static Matrix3 operator *(float val, Matrix3 m)
        {
            return m * val;
        }
        public static Matrix3 operator /(Matrix3 m, float val)
        {
            return new Matrix3(
                m.m11 / val, m.m12 / val, m.m13 / val,
                m.m21 / val, m.m22 / val, m.m23 / val,
                m.m31 / val, m.m32 / val, m.m33 / val
                );
        }

        // Matrix * Matrix
        public static Matrix3 operator *(Matrix3 l, Matrix3 r)
        {
            return new Matrix3(
                l.m11 * r.m11 + l.m12 * r.m21 + l.m13 * r.m31,
                l.m11 * r.m12 + l.m12 * r.m22 + l.m13 * r.m32,
                l.m11 * r.m13 + l.m12 * r.m23 + l.m13 * r.m33,

                l.m21 * r.m11 + l.m22 * r.m21 + l.m23 * r.m31,
                l.m21 * r.m12 + l.m22 * r.m22 + l.m23 * r.m32,
                l.m21 * r.m13 + l.m22 * r.m23 + l.m23 * r.m33,

                l.m31 * r.m11 + l.m32 * r.m21 + l.m33 * r.m31,
                l.m31 * r.m12 + l.m32 * r.m22 + l.m33 * r.m32,
                l.m31 * r.m13 + l.m32 * r.m23 + l.m33 * r.m33
                );
        }
        public Matrix3 Combine(Matrix3 other)
        {
            return this * other;
        }

        // transform vector2
        public static Vector2 operator *(Matrix3 m, Vector2 v)
        {
            return new Vector2(
                m.m11 * v.X + m.m12 * v.Y + m.m13 * 1,
                m.m21 * v.X + m.m22 * v.Y + m.m23 * 1
                );
        }
        public Vector2 Transform(Vector2 v)
        {
            return this * v;
        }
        public Vector3 Transform(Vector3 v)
        {
            return new Vector3(this.Transform(new Vector2(v.X, v.Y)), 0);
        }

        // convert to 4-dimentional and transpose Matrix
        public Matrix ToXnaMatrix()
        {
            // Swap Rows and Columns. (Transposing)
            return new Matrix(m11, m21, 0, m31,
                              m12, m22, 0, m32,
                                0, 0, 1, 0,
                              m13, m23, 0, m33);
        }

        public static Matrix3 Identity;
        public static Matrix3 Zero;

        static Matrix3()
        {
            Identity = new Matrix3(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
                );
            Zero = new Matrix3(
                0, 0, 0,
                0, 0, 0,
                0, 0, 0
                );
        }

        public static Matrix3 CreateScale(float scaleX, float scaleY)
        {
            return CreateScale(new Vector2(scaleX, scaleY));
        }
        public static Matrix3 CreateScale(Vector2 scale)
        {
            return new Matrix3(
                scale.X, 0, 0,
                0, scale.Y, 0,
                0,      0,  1
                );
        }
        public static Matrix3 CreateTranslation(Vector2 t) { return CreateTranslation(t.X, t.Y); }
        public static Matrix3 CreateTranslation(float tx, float ty)
        {
            return new Matrix3(
                1, 0, tx,
                0, 1, ty,
                0, 0, 1
                );
        }
        public static Matrix3 CreateRotation(float radian)
        {
            float cos = MathF.Cos(radian);
            float sin = MathF.Sin(radian);

            return new Matrix3(
                cos, sin, 0,
                -sin, cos, 0,
                0,   0,   1
                );
        }
        public static bool operator ==(Matrix3 left, Matrix3 right)
        {
            if (ReferenceEquals(left, null))
                if (ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);    
        }
        public static bool operator !=(Matrix3 left, Matrix3 right)
        {
            return !(left == right);
        }

        public bool Equals(Matrix3 other)
        {
            if (other == null)
                return false;

            return  m11 == other.m11 && m12 == other.m12 && m13 == other.m13 &&
                    m21 == other.m21 && m22 == other.m22 && m23 == other.m23 &&
                    m31 == other.m31 && m32 == other.m32 && m33 == other.m33;
        }

        public override bool Equals(object obj)
        {
            Matrix3 other = obj as Matrix3;
            if(other == null) 
                return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(m11);
            hash.Add(m12);
            hash.Add(m13);
            hash.Add(m21);
            hash.Add(m22);
            hash.Add(m23);
            hash.Add(m31);
            hash.Add(m32);
            hash.Add(m33);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[[{m11,11},{m12,11},{m13,11}],\n");
            sb.Append($" [{m21,11},{m22,11},{m23,11}],\n");
            sb.Append($" [{m31,11},{m32,11},{m33,11}]]");
            return sb.ToString();
        }

        // กรณีมี Rotation ค่าที่ได้จะ undefined
        public RectF TransformRectAABB(RectF rect)
        {
            Vector2 minPoint = Transform(rect.Position);
            Vector2 maxPoint = Transform(rect.MaxPoint);
            return new RectF(minPoint, maxPoint-minPoint);
        }

        /**Invert matrix
         *   https://www.youtube.com/watch?app=desktop&v=lauh7KjEfXk
         *   1. หา det A
         *   2. หา cofactor matrix
         *   3. Adjoint A = Transpose of cofactor matrix
         *   4. A inverse = (1/Det A) * (Adjoint A)
         */

        public float Determinant()
        {
            var a = +m11 * Determinant2(m22, m23, m32, m33);
            var b = -m12 * Determinant2(m21, m23, m31, m33);
            var c = + m13 * Determinant2(m21, m22, m31, m32);
            return a + b + c;
        }

        // https://www.youtube.com/watch?app=desktop&v=lauh7KjEfXk
        public Matrix3 GetInverse()
        {
            // 1. find det
            float det = Determinant();
            if (det == 0)
                return Matrix3.Identity;

            // 2. find cofactor matrix
            float c11 = Determinant2(m22, m23, m32, m33);
            float c12 = -Determinant2(m21, m23, m31, m33);
            float c13 = Determinant2(m21, m22, m31, m32);

            float c21 = -Determinant2(m12, m13, m32, m33);
            float c22 = Determinant2(m11, m13, m31, m33);
            float c23 = -Determinant2(m11, m12, m31, m32);

            float c31 = Determinant2(m12, m13, m22, m23);
            float c32 = -Determinant2(m11, m13, m21, m23);
            float c33 = Determinant2(m11, m12, m21, m22);

            // 3-4. inverse = transpose(cofactor matrix) * (1/det)
            float inv = 1 / det;
            return new Matrix3(
                inv * c11, inv * c21, inv * c31,
                inv * c12, inv * c22, inv * c32,
                inv * c13, inv * c23, inv * c33
                );
        }

        // a b
        // c d
        private static float Determinant2(float a, float b, float c, float d)
        {
            return a * d - b * c;
        }
    }
}
