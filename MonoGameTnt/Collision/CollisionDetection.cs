using System.Collections.Generic;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class CollisionDetection
    {
        public bool FirstContactDetectionEnable { get; set; } = true;
        public int MaxGroup { get; } = 10;
        private CollisionGroups groups;
        private List<TwoGroups> detectors = new List<TwoGroups>();
        public void AddDetector(int groupCode1, int groupCode2)
        {
            detectors.Add(new TwoGroups(groupCode1, groupCode2));
        }
        public CollisionDetection()
        {
            groups = new CollisionGroups(MaxGroup);
        }
        public void DetectAndResolve(Actor root)
        {
            groups = new CollisionGroups(MaxGroup);
            FindAllCollisionObjs(root);
            FirstContactInit();
            DetectAndResolveAllGroups();
        }

        // หาเจอแล้วค่อยๆ เติมเข้า list
        private void FindAllCollisionObjs(Actor node)
        {
            Debug.Assert(node != null);

            for (int i = 0; i < node.ChildCount; ++i)
            {
                var collisionObj = node.GetChild(i) as CollisionObj;
                if (collisionObj != null)
                {
                    groups.Add(collisionObj);
                    break; // ถือว่าวัตถุหนึ่งมี collisionObj ได้แค่ตัวเดียว
                }
            }

            for (int i = 0; i < node.ChildCount; ++i)
            {
                var child = node.GetChild(i);
                FindAllCollisionObjs(child);
            }
        }

        private void FirstContactInit()
        {
            if (!FirstContactDetectionEnable)
                return;

            for (int i = 0; i < groups.MaxGroup; i++)
            {
                var list = groups.GetList(i);
                for (int j = 0; j < list.Count; ++j)
                {
                    var obj = list[j];
                    obj.LastObjList = obj.CurrentObjList; // move Cur list into Last List
                    obj.CurrentObjList = new List<CollisionObj>(); // new Cur List
                }
            }
        }

        private void DetectAndResolveAllGroups()
        {
            List<CollisionObj>? list;
            if(detectors.Count == 0 && groups.IsSingleGroup(out list))
                DetectAndResolveSameList(list!);
            else
            {
                for (int i = 0; i < detectors.Count; i++)
                {
                    var list1 = groups.GetList(detectors[i].GroupCode1);
                    var list2 = groups.GetList(detectors[i].GroupCode2);
                    DetectAndResolveTwoLists(list1, list2);
                }
            }
        }

        private void DetectAndResolveTwoLists(List<CollisionObj> list1, List<CollisionObj> list2)
        {
            if(list1 == list2)
                DetectAndResolveSameList(list1);
            else
                DetectAndResolveDistinctLists(list1, list2);
        }

        private void DetectAndResolveSameList(List<CollisionObj> list)
        {
            for (int i = 0; i < list.Count; i++)
                for (int j = i + 1; j < list.Count; j++)
                    DetectAndResolve(list[i], list[j]);
        }
        private void DetectAndResolveDistinctLists(List<CollisionObj> list1, List<CollisionObj> list2)
        {
            for (int i = 0; i < list1.Count; i++)
                for (int j = 0; j < list2.Count; j++)
                    DetectAndResolve(list1[i], list2[j]);
        }

        private void DetectAndResolve(CollisionObj objA, CollisionObj objB)
        {
            if (objA == objB)
                return;

            CollideData collideData;
            if (objA.Shape.IsCollide(objB.Shape, out collideData))
                InvokeCollideBoth(objA, objB, collideData);
        }
        private void InvokeCollideBoth(CollisionObj objA, CollisionObj objB, CollideData collideData)
        {
            if(FirstContactDetectionEnable)
            {
                collideData.FirstContact = !objB.LastObjList.Contains(objA);
                objA.CurrentObjList.Add(objB);
                objB.CurrentObjList.Add(objA);
            }

            objA.InvokeCollide(objB, collideData);
            objB.InvokeCollide(objA, collideData);
        }


    }

    struct TwoGroups
    {
        public int GroupCode1;
        public int GroupCode2;

        public TwoGroups(int groupCode1, int groupCode2)
        {
            GroupCode1 = groupCode1;
            GroupCode2 = groupCode2;
        }
    }
}
