using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    // Singleton Design Pattern
    public class TextureCache
    {
        private static TextureCacheInstance obj;
        public static TextureCacheInstance Obj
        {
            get
            {
                if(obj != null)
                    return obj;

                Debug.Assert(obj != null, "TextureCache.Init() must be called first.");
                return null;
            }
        }
        public static void Init(GraphicsDevice graphicsDevice, ContentManager content, bool directLoad = true)
        {
            obj = new TextureCacheInstance(graphicsDevice, content, directLoad);
        }
        public static Texture2D Get(string filename) // convenient method: instead of calling TextureCache.Obj.Get(...);
        {
            return Obj.Get(filename);
        }
        //public static bool DirectLoad { get => obj.DirectLoad; set => obj.DirectLoad = value; }
        //public static string DirectPath { get => obj.DirectPath; set => obj.DirectPath = value; }
        //public static void Clear()
        //{
        //    obj.Clear();
        //}
    }

    public class TextureCacheInstance
    {
        private Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();
        private GraphicsDevice device;
        private ContentManager content;
        public bool DirectLoad { get; set; }
        public string DirectPath { get; set; } = "";
        public TextureCacheInstance(GraphicsDevice graphicsDevice, ContentManager content, bool directLoad = true)
        {
            device = graphicsDevice;
            this.content = content;
            DirectLoad = directLoad;
        }

        public Texture2D Get(string filename)
        {
            Debug.Assert(device != null, "TextureCache.Init() must be called.");

            Texture2D? texture;
            
            if (cache.TryGetValue(filename, out texture)) 
                return texture;

            // https://stackoverflow.com/questions/21999303/load-images-to-content-pipeline-at-runtime
            texture = LoadTexture(filename);
            cache[filename] = texture;
            return texture;
        }

        // โดย default MonoGame จะ load แบบ Premultiplied Alpha
        // ดังนั้น DirectLoad ก็จะ load แบบ Premultiplied Alpha เช่นกัน
        private Texture2D LoadTexture(string filename)
        {
            if (DirectLoad)
                return Texture2D.FromFile(device, Path.Combine(DirectPath, filename)
                                            , DefaultColorProcessors.PremultiplyAlpha);
            else
                return content.Load<Texture2D>(Path.GetFileNameWithoutExtension(filename));
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
