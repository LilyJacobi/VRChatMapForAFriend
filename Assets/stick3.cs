
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class stick3 : UdonSharpBehaviour
{
    
    public AudioSource stick;
    void Start()
    {
        
    }
    public override void OnPickupUseDown() {
        stick.Play();
    }
}
