using UnityEngine;

namespace com.ABinder
{
    public static class GoExtention
    {
        public static string GetStringHierarchiPath(this GameObject go)
        {
            string path = go.name;
            while (go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                path = go.name + "/" + path;
            }
            return path;
        }
    }
}