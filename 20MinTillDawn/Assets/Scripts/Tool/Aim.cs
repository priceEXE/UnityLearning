using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    Vector3 mouseWorldPosition;
    private void FoLLowMouse()
    {
        mouseWorldPosition = Input.mousePosition;
        mouseWorldPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition);
        transform.position = mouseWorldPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FoLLowMouse();
    }
}
