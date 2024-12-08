using System.Collections.Generic;

namespace ThanaNita.MonoGameTnt
{
    public class RegionSelector
    {
        // [row][col]
        //private TextureRegion[][] region2d;
        //private int colCount;
        private List<TextureRegion> list;

        public RegionSelector(TextureRegion[][] region2d)
        {
            //this.region2d = region2d;

            list = new List<TextureRegion>();
            for (int row = 0; row < region2d.Length; ++row)
            {
                int colCount = region2d[row].Length;
                for (int col = 0; col < colCount; ++col)
                {
                    list.Add(region2d[row][col]);
                }
            }
        }

        public TextureRegion[] All()
        {
            return list.ToArray();
        }

        public TextureRegion[] SelectAll()
        {
            return All();
        }

        public TextureRegion[] Select(int start, int count)
        {
            TextureRegion[] regions = new TextureRegion[count];
            return list.Slice(start, count).ToArray();
        }

        public TextureRegion[] Select1by1(params int[] indexList)
        {
            TextureRegion[] regions = new TextureRegion[indexList.Length];

            for (int i = 0; i < indexList.Length; ++i)
            {
                regions[i] = list[indexList[i]];
            }

            return regions;
        }

        public static TextureRegion[] SelectAll(TextureRegion[][] regions)
        {
            return new RegionSelector(regions).SelectAll();
        }
        public static TextureRegion[] Select(TextureRegion[][] regions, int start, int count)
        {
            return new RegionSelector(regions).Select(start, count);
        }
        public static TextureRegion[] Select1by1(TextureRegion[][] regions, params int[] indexList)
        {
            return new RegionSelector(regions).Select1by1(indexList);
        }
    }
}
