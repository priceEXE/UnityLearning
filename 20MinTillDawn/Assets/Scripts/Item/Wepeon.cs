using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepeon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public AudioClip shootAudio;//射击音效

    public AudioClip reloadAudio;//换弹音效
    private AudioSource audioSource;
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
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    public void AudioSHoot()
    {
        audioSource.clip = shootAudio;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void AudioReload()
    {
        audioSource.clip = reloadAudio;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void AudioStop()
    {
        audioSource.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        FollowMousePo();
    }
}
