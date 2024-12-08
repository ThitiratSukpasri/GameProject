using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame01.MonoGameTnt.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class MorphActor : Actor
    {
        private VertexPositionColorTexture[] vertices; // ห้ามแก้ TextureCoordinate
        private short[] indices; // set ครั้งเดียว ห้ามแก้อีก
        private Texture2D texture; // set ครั้งเดียว ห้ามแก้อีก
        public int VertexCount { get { return vertices.Length; } }
        public Vector2 GetPosition(int index) => vertices[index].Position.ToVector2();
        public void SetPosition(int index, Vector2 position) =>
                                    vertices[index].Position = new Vector3(position, 0);
        public Color GetColor(int index) => vertices[index].Color;
        public void SetColor(int index, Color color) => vertices[index].Color = color;
        public MorphActor(
            Texture2D texture, // can be null
            Vector2 textureOrigin, // ใช้เฉพาะกรณีมี texture: Position = position; textureCoor = Origin + position
            Vector2[] positions,
            short[] indices,
            Color? color = null)
        {
            Debug.Assert(positions != null);
            Debug.Assert(indices != null);
            Debug.Assert(indices.Length % 3 == 0, "indices length should be multiple of 3.");

            color = color ?? Color.White;
            CopyPositionsAndColor(positions, color.Value);
            CopyIndices(indices);
            this.texture = texture;
            if(texture != null)
                CopyTextureCoordinates(textureOrigin, positions, texture.Size());
        }

        private void CopyTextureCoordinates(Vector2 textureOrigin, Vector2[] positions, Vector2 textureSize)
        {
            for (int i = 0; i < positions.Length; i++)
                // Normalized coordinate by divisor
                vertices[i].TextureCoordinate = (textureOrigin + positions[i]) / textureSize;
        }

        private void CopyPositionsAndColor(Vector2[] positions, Color color)
        {
            vertices = new VertexPositionColorTexture[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                vertices[i].Position = new Vector3(positions[i], 0);
                vertices[i].Color = color;
            }
        }

        private void CopyIndices(short[] indices)
        {
            this.indices = new short[indices.Length];
            Array.Copy(indices, this.indices, indices.Length);
        }

        // แบบไม่มี texture
        public MorphActor(
                        Vector2[] positions,
                        short[] indices,
                        Color? color = null)
            : this(null, Vector2.Zero, positions, indices, color)
        {
        }
        public void SetColorList(IEnumerable<Color> colors)
        {
            int i = 0;
            foreach(var color in colors)
            {
                if (i >= vertices.Length)
                    break;
                vertices[i].Color = color;
                i++;
            }
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            var combined = state.Combine(this.GetMatrix());
            target.Draw(vertices, indices, texture, combined);
        }
    }
}
