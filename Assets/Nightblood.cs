
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Nightblood : UdonSharpBehaviour
{
    public Collider col;
    public Rigidbody rig;
    public float timer;
    public GameObject prefabPe;
    public AudioSource destroy;
    public AudioSource iDontSuppose;
    public AudioSource hellow;
    public float timerMax;
    public GameObject hoe;
    public UdonBehaviour target;
    void Start()
    {
        timer = 0;
    }
    
    public override void OnPickup() {
        if(!iDontSuppose.isPlaying && !destroy.isPlaying && timer == 0) {
            iDontSuppose.Play();
            timer = timerMax;
        }
        //col = gameObject.GetComponentInChildren<MeshCollider>();
        rig.isKinematic = true;
    }
    public override void OnDrop() {
        //col = gameObject.GetComponentInChildren<MeshCollider>();
        //rig.detectCollisions = true;
        rig.isKinematic = false;

    }
    public override void OnPickupUseDown() {
        if(!iDontSuppose.isPlaying && !destroy.isPlaying) {
            destroy.Play();
        }
    }
    void Update() {
        if(timer > 0) {
            timer -= 1;
        }
    }
    public void killDaHoe() {
        if(hoe != null){
            GameObject pe = VRCInstantiate(prefabPe);
            pe.transform.position = hoe.transform.position;
            Destroy(hoe);
            Destroy(pe, 2);
        }
    }
    void OnCollisionEnter(Collision col) {
        Debug.Log(col.collider.gameObject.name);
        if(col.collider.gameObject.name.Contains("Moash")) {
            hoe = col.collider.gameObject;
            target.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"killDaHoe");
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
   }

}