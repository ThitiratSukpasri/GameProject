using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    // มีสองวิธี optimize
    // 1. update matrix เฉพาะเมื่อเรียก getmatrix ครั้งแรก หลังมีการเปลี่ยนแปลง; ไม่ต้อง update บ่อย แต่ตอน get จะเกิดการ copy matrix
    // 2. update matrix ทุกครั้งที่ position, rotation, scale เปลี่ยน   (บ่อยหน่อย) ; แต่ตอนใช้ matrix สามารถอ้าง protected field matrix ตรงๆ ได้
    public class Transformable
    {
        private Vector2 position;
        private float rotation = 0; // in Degree
        private Vector2 scale = Vector2.One;
        private Vector2 origin;
        private bool needUpdateMatrix = true;
        private bool needUpdateReverse = true;
        private Matrix3 matrix;
        //private Matrix reverseMatrix;

        public Transformable(Vector2 position, float rotation, Vector2 scale, Vector2 origin)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.origin = origin;
        }
        public Transformable(Vector2 position)
        {
            this.position = position;
        }

        public Transformable()
        {
            matrix = Matrix3.Identity;
            needUpdateMatrix = false;
        }

        public Vector2 Position 
        {
            get => position;
            set
            {
                position = value;
                needUpdateMatrix = true;
                needUpdateReverse = true;
            }
        }
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                needUpdateMatrix = true;
                needUpdateReverse = true;
            }
        }

        public Vector2 Scale
        {
            get => scale;
            set
            {
                scale = value;
                needUpdateMatrix = true;
                needUpdateReverse = true;
            }
        }
        public Vector2 Origin
        {
            get => origin;
            set
            {
                origin = value;
                needUpdateMatrix = true;
                needUpdateReverse = true;
            }
        }

        public Matrix3 GetMatrix()
        {
            UpdateMatrix();
            return matrix;
        }

        private void UpdateMatrix()
        {
            if (!needUpdateMatrix)
                return;

            needUpdateMatrix = false;

            // Adapted from SFML Library
            float angle = -rotation * MathF.PI / 180.0F;
            float cosine = MathF.Cos(angle);
            float sine = MathF.Sin(angle);
            float sxc = scale.X * cosine;
            float syc = scale.Y * cosine;
            float sxs = scale.X * sine;
            float sys = scale.Y * sine;
            float tx = -origin.X * sxc - origin.Y * sys + position.X;
            float ty = origin.X * sxs - origin.Y * syc + position.Y;

            matrix = new Matrix3(sxc, sys, tx,
                                -sxs, syc, ty,
                                0.0F, 0.0F, 1.0F);
        }

        // Matrix ใน MonoGame เป็น row-major order ตามปกติ https://en.wikipedia.org/wiki/Row-_and_column-major_order
        // การคูณก็คูณแบบปกติของ matrix
        // เพียงแต่การตีความ Transform Matrix มองแบบ transpose
        //   เช่น - Matrix.CreateTranslation()
        //       - BasicEffect.View = ...
        // ผลก็คือ - เวลาเราสร้าง 4x4 matrix เราเลยต้อง transpose ให้ด้วย ดังโค้ดด้านบน
        //       - เวลาคูณ transform matrix ต้องสลับลำดับกัน
        // เดาว่าเพราะ OpenGL ต้องการรับในแบบ col-major order
        //
        // เราไม่สามารถมองว่า Matrix class ใน MonoGame เป็น column-major order ได้เพราะ ถ้าเช่นนั้น
        // .Multiply() ต้องเขียนใหม่; this[row, col] ต้องสลับ arg; constructor(row1, row2, ...) ต้องเป็น col1, col2, ...
        public Vector3 Transform(Vector3 inputVector)
        {
            UpdateMatrix();
            return matrix.Transform(inputVector);
        }

        public Vector2 Transform(Vector2 inputVector)
        {
            UpdateMatrix();
            return matrix.Transform(inputVector);
        }
    }
}
