using JetBrains.Annotations;
using UnityEngine;

namespace World
{
    public struct Segment
    {
        public DepartmentScriptableObject Department;
        [CanBeNull] public GameObject TopWall, LeftWall, RightWall, BottomWall;

        public Segment(DepartmentScriptableObject department, [CanBeNull] GameObject topWall,
            [CanBeNull] GameObject leftWall, [CanBeNull] GameObject rightWall, [CanBeNull] GameObject bottomWall)
        {
            Department = department;
            TopWall = topWall;
            LeftWall = leftWall;
            RightWall = rightWall;
            BottomWall = bottomWall;
        }
    }
}