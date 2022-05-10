using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UPQP.Features.SliceView
{
    [AddComponentMenu("UPQP/Features/SliceView/Hide GameObject")]
    public class SliceView_HideGameObject : MonoBehaviour
    {
        private static List<GameObject> objects;
        private static List<GameObject> Objects
        {
            get
            {
                if (objects == null)
                    objects = new List<GameObject>();

                return objects;
            }
        }
        private void Awake()
        {
            Objects.Add(gameObject);
        }

        private void OnDestroy()
        {
            Objects.Remove(gameObject);
        }
        public static void HideAll()
        {
            foreach (GameObject go in Objects)
            {
                if (go != null)
                    go.SetActive(false);
            }
        }

        public static void ShowAll()
        {
            foreach (GameObject go in Objects)
            {
                if (go != null)
                    go.SetActive(false);
            }
        }
    }
}