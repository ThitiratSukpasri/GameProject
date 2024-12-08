using System.Collections.Generic;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class CollisionGroups
    {
        public int MaxGroup { get => listOfList.Length; }
        private List<CollisionObj>[] listOfList;
        public CollisionGroups(int maxGroup)
        {
            listOfList = new List<CollisionObj>[maxGroup];

            for (int i = 0; i < listOfList.Length; ++i)
            {
                listOfList[i] = new List<CollisionObj>();
            }
        }

        public List<CollisionObj> GetList(int index) 
        {
            return listOfList[index];
        }

        public void Add(CollisionObj obj)
        {
            Debug.Assert(obj.GroupCode < listOfList.Length);
            listOfList[obj.GroupCode].Add(obj);
        }

        public bool IsSingleGroup(out List<CollisionObj>? list)
        {
            int countFound = 0;
            list = null;

            for (int i = 0; i < listOfList.Length; ++i)
                if (listOfList[i].Count > 0)
                {
                    countFound++;
                    list = listOfList[i];
                }

            if (countFound == 1)
            {
                return true;
            }
            else // 0, or 2,3,4, ...
            {
                list = null;
                return false;
            }
        }
    }
}
