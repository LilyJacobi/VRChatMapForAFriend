using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class buttoncontroller : UdonSharpBehaviour
{
    [Tooltip("Game object to toggle")]
    public GameObject toggleObject;

    [Tooltip("More game objects to toggle")]
    public GameObject[] toggleObjects;

    [Tooltip("Prefab to spawn")]
    public GameObject prefabMoash;
    [Tooltip("place to spawn")]
    public Transform moashTransform;

    [Space(10)]
    [Tooltip("Target script")]
    public UdonBehaviour target;
    public bool isNetwork;
    [Tooltip("Toggle boolean property on the target script")]
    public string toggleProperty;
    
    [Tooltip("Calling method (custom event) on the target script")]
    public string callMethod;
    public UdonBehaviour button;
    public string eventName;
    [Space(10)]
    [Tooltip("0 - Off, 1 - On, else Toggle")]
    public int operation = -1;
    
    [Space(10)]
    public AudioSource pressAudio;
    public AudioSource[] music = new AudioSource[2];
public int musicInt;
    VRCPlayerApi _localPlayer;

    void Start()
    {
        _localPlayer = Networking.LocalPlayer;
    }

    void OnMouseDown()
    {
        if (_localPlayer == null)
            spawnMoash();
    }

    public override void Interact()
    {
        if(isNetwork) {
            button.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, eventName);
        }
        else {
            button.SendCustomEvent(eventName       );
        }
    }
    
    public void toggleMusic() {
        if(!music[musicInt].isPlaying) {
            if(music[musicInt].time == 0){
                music[musicInt].Play();
            }
            else {
                music[musicInt].UnPause();
            }
            
        }
        else{
            music[musicInt].Pause();
        }
    }
    public void swapMusicInClass() {
        target.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"swapMusic");
    }
    public void swapMusic() {
        if(musicInt == 1) {
            musicInt = 0;
        }
        else{
            musicInt = 1;
        }
        foreach(AudioSource musicFile in music) {
            if(musicFile.isPlaying){
                musicFile.Stop();
            }
        }
    }
    public void DoInteract()
    {
        PlayAudio(pressAudio);

        if (toggleObject != null) {
            if (operation == 0)
                toggleObject.SetActive(false);
            else if (operation == 1)
                toggleObject.SetActive(true);
            else
                toggleObject.SetActive(!toggleObject.activeSelf);
        }

        if (toggleObjects != null && toggleObjects.Length > 0) {
            foreach (var obj in toggleObjects) {
                if (obj != null) {
                    if (operation == 0)
                        obj.SetActive(false);
                    else if (operation == 1)
                        obj.SetActive(true);
                    else
                        obj.SetActive(!obj.activeSelf);
                }
            }
        }

        if (target != null) {
            if (toggleProperty != null && toggleProperty.Length > 0) {
                if (operation == 0)
                    target.SetProgramVariable(toggleProperty, false);
                else if (operation == 1)
                    target.SetProgramVariable(toggleProperty, true);
                else {
                    bool active = !(bool)target.GetProgramVariable(toggleProperty);
                    target.SetProgramVariable(toggleProperty, active);
                }
            }

            if (callMethod != null && callMethod.Length > 0)
                target.SendCustomEvent(callMethod);
        }
    }
    public void spawnMoash() {
        GameObject mo = VRCInstantiate(prefabMoash);
        mo.transform.position = moashTransform.position;    
        }
    void PlayAudio(AudioSource audio)
    {
        if (audio != null)
            audio.Play();
    }
}
