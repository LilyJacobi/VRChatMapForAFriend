
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PushButton_Mirror1 : UdonSharpBehaviour
{
    public UdonBehaviour thing;
    void Start()
    {
        
    }
    public override void Interact() {
        thing.SendCustomEvent("SetButtonsModePhysical");
    }
}
