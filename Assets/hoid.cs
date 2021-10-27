
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class hoid : UdonSharpBehaviour
{
    public AudioSource line1;
    public AudioSource line2;
    public AudioSource line3;
    public AudioSource line4;
    public int lineToPlay;
    void Start()
    {
        
    }
    public override void OnPickupUseDown() {
        AudioSource[] lines =  new AudioSource[4];
        lines[0] = (line1);
        lines[1] = (line2);
        lines[2] = (line3);
        lines[3] = (line4);
        foreach(AudioSource a in lines) {
            if(a.isPlaying) {
                a.Stop();
            }
        }
        //int lineToPlay = Random.Range(0,4);
        lines[lineToPlay].Play();
        lineToPlay+=1;
        lineToPlay = lineToPlay%4;
    }
}
