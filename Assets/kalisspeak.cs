
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class kalisspeak : UdonSharpBehaviour
{
    public VRC.SDKBase.VRC_Pickup pickup;
    public AudioSource depression;
    void Start()
    {
        pickup =  ((VRC.SDKBase.VRC_Pickup)GetComponent(typeof(VRC.SDKBase.VRC_Pickup)));
        depression = GetComponent<AudioSource>();
    }

    public override void OnPickupUseDown() {
        depression.Play();
    }
}
