using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootPistol : MonoBehaviour
{
    public GameObject bullet;

    public Transform spawnPosition;

    private float velocity = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(FireBullet);
    }

    private void FireBullet(ActivateEventArgs arg0)
    {
        GameObject bullet = Instantiate(this.bullet);
        bullet.transform.position = spawnPosition.position;
        bullet.GetComponent<Rigidbody>().velocity = spawnPosition.forward * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
