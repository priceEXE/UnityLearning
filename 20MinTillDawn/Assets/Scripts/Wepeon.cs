using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepeon : MonoBehaviour
{
    private int damage;
    Vector3 mouseWorldPosition;
    public int getDamage()
    {
        return damage;
    }
    private void FollowMousePo()
    {
        mouseWorldPosition = Input.mousePosition;
        mouseWorldPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition);
        transform.right = mouseWorldPosition - transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePo();
    }
}
