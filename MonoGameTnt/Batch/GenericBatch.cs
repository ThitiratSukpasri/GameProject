using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThanaNita.MonoGameTnt
{
    // ดูเหมือน Unsafe ไม่ได้ช่วยให้เร็วขึ้นเท่าไร ตอนนี้เลยไม่ใช้ Unsafe แล้ว
    // Reduce GPU Draw Call. Trade-off with the need to copy vertices & indices arrays.
    public class GenericBatch : DrawTarget, GraphicsDeviceConfig.ConfigListener
    {
        private DrawTargetSimple target;
        private Texture2D texture;
        private VertexPositionColorTexture[] vertices;
        private short[] indices;
        private int verticesCount;
        private int indicesCount;
        private int batchCount;
        public int BatchCount { get { return  batchCount; } }

        private int MaxVertices = 20000;
        private int MaxIndices = 25000;

        public const int DefaultMaxVertices = 20000;
        public const int DefaultMaxIndices = 25000;

        public GenericBatch(DrawTargetSimple target) : this(target, DefaultMaxVertices, DefaultMaxIndices)
        {
        }
        public GenericBatch(DrawTargetSimple target, int MaxVertices, int MaxIndices)
        {
            this.target = target;
            verticesCount = 0;
            indicesCount = 0;
            this.MaxVertices = MaxVertices;
            this.MaxIndices = MaxIndices;
            vertices = new VertexPositionColorTexture[MaxVertices];
            indices = new short[MaxIndices];
        }

        public void Begin()
        {
            batchCount = 0;
        }

        public void End()
        {
            Flush();
        }

        public void Draw(VertexPositionColorTexture[] vertices, int verticesCount,
            short[] indices, int indicesCount, Texture2D texture)
        {
            Draw(vertices, verticesCount, indices, indicesCount, texture, DrawState.Identity);
        }
        public void Draw(VertexPositionColorTexture[] vertices, int verticesCount,
                    short[] indices, int indicesCount, Texture2D texture, DrawState state)
        {
            if (this.texture != texture || this.verticesCount+verticesCount > MaxVertices 
                || this.indicesCount + indicesCount > MaxIndices)
                Flush();
            this.texture = texture;

            Copy(vertices, this.vertices, this.verticesCount, verticesCount, state);
            CopyAndIncrease(indices, this.indices, destStart: this.indicesCount, indicesCount, (short)this.verticesCount);
            this.verticesCount += verticesCount;
            this.indicesCount += indicesCount;
        }

        // srcStart always be zero
        private static void Copy(VertexPositionColorTexture[] src, 
            VertexPositionColorTexture[] dest, int destStart, int count, DrawState state)
        {
            if (destStart + count > dest.Length)
                throw new Exception("Too large Vertices Array Argument");

            for (int i = 0; i < count; ++i)
            {
                dest[destStart + i].TextureCoordinate = src[i].TextureCoordinate;
                dest[destStart + i].Position = state.WorldMatrix.Transform(src[i].Position);
                dest[destStart + i].Color = TransformColor(src[i].Color, state.ColorF);
            }
            // NOTE: Or Use the unsafe code block below instead of this for loop

            /*unsafe
            {
                fixed (VertexPositionColorTexture* destArray = dest, srcArray = src) 
                {
                    VertexPositionColorTexture* pDest = destArray + destStart;
                    VertexPositionColorTexture* pSrc = srcArray;
                    for (int i = 0; i < count; ++i)
                    {
                        *pDest = *pSrc;
                        pDest++;
                        pSrc++;
                    }
                }
            }*/
        }

        private static Color TransformColor(Color color, ColorF worldColorF)
        {
            return ((ColorF)color * worldColorF).ToPremultiplyAlpha().ToColor();
        }

        // srcStart always be zero
        private static void CopyAndIncrease(short[] src, short[] dest, int destStart, int count, int verticesCount)
        {
            if (destStart + count > dest.Length)
                throw new Exception("Too large Indices Array Argument");

            for (int i = 0; i < count; ++i)
                dest[destStart + i] = (short)(src[i] + verticesCount);
            // NOTE: Or Use the unsafe code block below instead of this for loop

            /*unsafe
            {
                fixed (short* destArray = dest, srcArray = src)
                {
                    short* pDest = destArray + destStart;
                    short* pSrc = srcArray;
                    for (int i = 0; i < count; ++i)
                    {
                        *pDest = (short)(*pSrc + verticesCount);
                        pDest++;
                        pSrc++;
                    }
                }
            }*/
        }

        public void Flush()
        {
            if (verticesCount == 0 && indicesCount == 0)
                return;

            target.Draw(vertices, verticesCount, indices, indicesCount, texture);
            verticesCount = 0;
            indicesCount = 0;

            batchCount++;
        }

        public void ConfigChanged()
        {
            Flush();
        }
    }
}
