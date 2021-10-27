
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class door : UdonSharpBehaviour
{
    public Animator anim;
    public GameObject text;
    void Start()
    {
        text.SetActive(false);
        
        //anim = GetComponent<Animator>();
        anim.applyRootMotion = false;
    }
    public override void Interact() {
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
        text.SetActive(true);
    }
    public void open() {
        anim.Play("set90");
    }
}
