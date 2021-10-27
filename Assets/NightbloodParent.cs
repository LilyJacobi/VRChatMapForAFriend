
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class NightbloodParent : UdonSharpBehaviour
{
    public Collider col;
    public Rigidbody rig;
    public GameObject prefabPe;
    public AudioSource destroy;
    public AudioSource iDontSuppose;
    public AudioSource hellow;
    void Start()
    {
        
    }
    
    public override void OnPickup() {
        if(!iDontSuppose.isPlaying && !destroy.isPlaying) {
            iDontSuppose.Play();
        }
        Debug.Log("Picked up NB");
        //col = gameObject.GetComponentInChildren<MeshCollider>();
        rig.isKinematic = true;
    }
    public override void OnDrop() {
        Debug.Log("Picked up NB");
        //col = gameObject.GetComponentInChildren<MeshCollider>();
        //rig.detectCollisions = true;
        rig.isKinematic = false;

    }
    public override void OnPickupUseDown() {
        Debug.Log("Picked up NB");
        if(!iDontSuppose.isPlaying && !destroy.isPlaying) {
            destroy.Play();
        }
    }
    /*void OnCollisionEnter(Collision col) {
        Debug.Log(col.collider.gameObject.name);
        if(col.collider.gameObject.name.Contains("Moash")) {
            GameObject pe = VRCInstantiate(prefabPe);
            pe.transform.position = col.collider.gameObject.transform.position;
            Destroy(col.collider.gameObject);
            Destroy(pe, 2);
        }
    }
    void OnTriggerEnter(Collider col) {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name.Contains("Moash")) {
            GameObject pe = VRCInstantiate(prefabPe);
            pe.transform.position = col.gameObject.transform.position;
            Destroy(col.gameObject);
            Destroy(pe, 2);
        }
   }*/

}