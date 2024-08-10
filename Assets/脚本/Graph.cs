using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform pointPrefab;
    [Range(10,100)]
    public int resolution = 10;

    void Awake() {
        Vector3 position;
        float step = 2f / resolution;
        position.z = 0;
        Vector3 scale = Vector3.one * step;
         for(int i=0;i<resolution;i++)
         {
            Transform point = Instantiate(pointPrefab);
            position.x = (i+0.5f) * step - 1f;
            position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;

         }
    }
}
