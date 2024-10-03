using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepeon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    Vector3 mouseWorldPosition;
    private void FollowMousePo()
    {
        mouseWorldPosition = Input.mousePosition;
        mouseWorldPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition);
        if((mouseWorldPosition - transform.position).x < 0) spriteRenderer.flipY = true;
        else spriteRenderer.flipY = false;
        transform.right = mouseWorldPosition - transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePo();
    }
}
