
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class hat2 : UdonSharpBehaviour
{   
    
    public VRC.SDKBase.VRC_Pickup pickup;
    public VRC.SDKBase.VRCPlayerApi.TrackingDataType track;
    public Rigidbody rig;
    public VRC.SDKBase.VRCPlayerApi.TrackingData trackingData;
    public HumanBodyBones head;
    public bool shouldBeStuck;
    VRCPlayerApi player;
    public float yShift;
    void Start()
    {
        
    }
    void Update() {
        if(shouldBeStuck) {
            //trackingData = player.GetTrackingData((track));
            rig.isKinematic = true;
            Vector3 bonePos = player.GetBonePosition(head);
            Quaternion boneRotation = player.GetBoneRotation(head);
            bonePos = bonePos + new Vector3(0,yShift,0);
            gameObject.transform.position = bonePos;
            gameObject.transform.rotation = boneRotation;
            
            gameObject.transform.Rotate(-90,0,0, Space.Self);
            //gameObject.transform.Rotate(-90,0,0, Space.Self);
            /*gameObject.transform.position = trackingData.position;
            gameObject.transform.rotation = trackingData.rotation;
            Debug.Log(gameObject.transform.position);*/
        }
    }
    public override void Interact() {
        shouldBeStuck =false;
        pickup.pickupable = true ;
    }
    public override void OnPickupUseDown() {
        player = pickup.currentPlayer;
        pickup.Drop();
        pickup.pickupable = false;
        shouldBeStuck =true;
    }
    
}
