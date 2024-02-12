using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectTransposer : MonoBehaviour
    {
        [SerializeField]
        private float _transposeSpeed = 1f;

        public void Transpose(GameObject GObject, Vector3 targetPosition)
        {
            StartCoroutine(TransposeCoroutine(GObject, targetPosition));
        }

        private IEnumerator TransposeCoroutine(GameObject GObject, Vector3 targetPosition)
        {
            while(GObject.transform.localPosition != targetPosition)
            {
                Debug.Log($"TranposeCoroutine: {GObject.transform.position}; {GObject.transform.localPosition}; {targetPosition}");
                float delta = _transposeSpeed * Time.deltaTime;
                GObject.transform.position = Vector3.MoveTowards(GObject.transform.position, targetPosition, delta);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
