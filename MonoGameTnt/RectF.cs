using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace ThanaNita.MonoGameTnt
{
    public readonly struct RectF : IEquatable<RectF>
    {
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public float XMax { get => X + Width; }
        public float YMax { get => Y + Height; }

        public Vector2 Position { get => new Vector2(X, Y); }
        public Vector2 Size {  get => new Vector2(Width, Height); }
        public Vector2 CenterPoint { get => Position + Size / 2; }
        public Vector2 MaxPoint { get => Position + Size; }

        // Static
        public static RectF Zero = new RectF();

        public RectF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public RectF(Vector2 position, Vector2 size)
            : this(position.X, position.Y, size.X, size.Y)
        {
        }

        public bool Contains(Vector2 point)
        {
            return this.X <= point.X && point.X < XMax 
                && this.Y <= point.Y && point.Y < YMax;
        }

        public bool IsOverlap(RectF other)
        {
            return X < other.XMax && XMax > other.X
                && Y < other.YMax && YMax > other.Y;
        }
        public RectF CalcOverlapRect(RectF b)
        {
            return CalcOverlapRect(this, b);
        }
        public static RectF CalcOverlapRect(RectF a, RectF b)
        {
            float x = MathF.Max(a.X, b.X);
            float y = MathF.Max(a.Y, b.Y);
            float xMax = MathF.Min(a.X + a.Width, b.X + b.Width);
            float yMax = MathF.Min(a.Y + a.Height, b.Y + b.Height);

            float width = xMax - x;
            float height = yMax - y;
            if (width < 0 || height < 0)
                return new RectF();

            return new RectF(x, y, width, height);
        }
        public bool Intersects(RectF rectB, out RectF overlapRect)
        {
            if (IsOverlap(rectB))
            {
                overlapRect = CalcOverlapRect(rectB);
                return true;
            }
            else
            {
                overlapRect = new RectF();
                return false;
            }
        }

        public RectF CreateExpand(float delta)
        {
            return new RectF(X-delta, Y-delta, Width + 2*delta, Height + 2*delta);
        }
        public RectF CreateAdjusted(float widthRatio, float heightRatio)
        {
            var newRect = new RectF(
                X + Width * (1 - widthRatio) / 2,
                Y + Height * (1 - heightRatio) / 2,
                Width * widthRatio,
                Height * heightRatio
            );
            return newRect;
        }

        public static explicit operator RectF(RectangleF rect)
        {
            return new RectF(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static explicit operator RectangleF(RectF rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static explicit operator RectF(Rectangle rect)
        {
            return new RectF(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static explicit operator Rectangle(RectF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        public override string ToString()
        {
            return $"{{X: {X}, Y: {Y}, Width: {Width}, Height: {Height}}}";
        }

        public bool Equals(RectF other)
        {
            return X == other.X && Y == other.Y 
                && Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RectF))
                return false;

            return Equals((RectF)obj);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(X);
            hash.Add(Y);
            hash.Add(Width);
            hash.Add(Height);
            return hash.ToHashCode();
        }
    }
}
