
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class stortime : UdonSharpBehaviour
{
    public AudioSource line1;
    public UdonBehaviour storyTime;
    void Start()
    {
        
    }
    public void Story() {
        Debug.Log("Got here");
        if(!line1.isPlaying) {
            if(line1.time == 0){
                line1.Play();
            }
            else {
                line1.UnPause();
            }
            
        }
        else{
            line1.Pause();
        }

    }
    public override void Interact() {
        storyTime.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Story");
        
    }
}
