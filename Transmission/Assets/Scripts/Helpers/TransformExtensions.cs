using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            while (transform.childCount > 0)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}
