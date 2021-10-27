
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class killthisguy : UdonSharpBehaviour
{
    public GameObject prefabPe;
    public UdonBehaviour bitch;
    void Start()
    {
        UdonBehaviour behaviour = (UdonBehaviour)gameObject.GetComponent(typeof(UdonBehaviour));
        Debug.Log(behaviour);
    }
    public void killDaHoe() {
        GameObject pe = VRCInstantiate(prefabPe);
        pe.transform.position = gameObject.transform.position;
        Destroy(gameObject);

    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.name.Contains("Nightblood")) {
            killDaHoe();
        }
    }
}
