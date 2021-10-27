
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using VRC.Udon;

public class spikes : UdonSharpBehaviour
{
    public VRC.SDKBase.VRC_Pickup pickup;
    public VRC.SDKBase.VRC_Pickup.PickupHand hand;
    public VRC.SDKBase.VRCPlayerApi player;
    public VRC.SDKBase.VRCPlayerApi.TrackingDataType  trackerEnum;
    public VRC.SDKBase.VRCPlayerApi.TrackingData trackerData;
    public Quaternion quat;
    public float modifier;
    public Vector3 currentHandRotation;
    public float  currentHandzRot;
    public float charge;
    public Slider chargeBar;
    public float baseRunSpeed;
    public float baseWalkSpeed; 
    public float baseStrafeSpeed; 
    public bool isTapping;
    public bool isInUse;
    public int[] tappingRangeLeft;
    public GameObject canvas;
    public int[] fillingRangeLeft;
    public int[] tappingRangeRight;
    public int[] fillingRangeRight;
    public bool saveTapping;
    public float savedZRotation;
    public int[] tappingRange;
    public int[] fillingRange;
    public float[] modifierValuesTap;
    public float[] modifierValuesFill;
    int handInt;
    void Start()
    {
        chargeBar.value = 0;
        canvas.SetActive(false);
        tappingRangeLeft = new int[] {270, 360};
        fillingRangeLeft = new int[] {150,270};
        tappingRangeRight= new int[] {180,270};
        fillingRangeRight = new int[] {270, 360};

    }
    void DetermineTappingAndFillingRange() {
        if(handInt == 1) {
            tappingRange = tappingRangeLeft;
            fillingRange = fillingRangeLeft;
        }
        else{
            tappingRange = tappingRangeRight;
            fillingRange = fillingRangeRight;
        }
    }
    void Update() {
        if(pickup.IsHeld) {
            hand = pickup.currentHand;
            player = pickup.currentPlayer;
            handInt = (int) hand;
            DetermineTappingAndFillingRange();
            trackerEnum = (VRC.SDKBase.VRCPlayerApi.TrackingDataType) handInt;
            trackerData = player.GetTrackingData(trackerEnum);
            quat = trackerData.rotation;
            currentHandRotation = quat.eulerAngles;
            currentHandzRot = currentHandRotation.z;
            //Debug.Log(currentHandRotation);
            if( currentHandzRot > tappingRange[0] &&  currentHandzRot <= tappingRange[1] && isInUse) {
                Tap(currentHandzRot);
            }
            else if( currentHandzRot >= fillingRange[0] &&  currentHandzRot < fillingRange[1] && isInUse) {
                Fill(currentHandzRot);
            }
            else if( savedZRotation > tappingRange[0] &&  savedZRotation <= tappingRange[1]) {
                Tap(savedZRotation);
            }
            else if( savedZRotation >= fillingRange[0] &&  savedZRotation < fillingRange[1]) {
                Fill(savedZRotation);
            }
            else if(currentHandzRot < 30 && handInt == 2){
                Fill(currentHandzRot);
            }
            else if(savedZRotation > 0 && savedZRotation < 30 && handInt == 2){
                Fill(savedZRotation);
            }
            //Debug.Log(isTapping);
            if(chargeBar.value == 0) {
                player.SetRunSpeed(baseRunSpeed );
                player.SetWalkSpeed(baseWalkSpeed );
                player.SetStrafeSpeed(baseStrafeSpeed );
            }

        }
    }
    public override void OnPickupUseDown() {
        isInUse = true;
        savedZRotation = 0;
    }
    public override void OnPickup() {
        canvas.SetActive(true);
    }
    public override void OnPickupUseUp() {
        isInUse = false;
        savedZRotation =  currentHandzRot;
    }
    public override void OnDrop() {
        
        player.SetRunSpeed(baseRunSpeed );
        player.SetWalkSpeed(baseWalkSpeed );
        player.SetStrafeSpeed(baseStrafeSpeed );
        isInUse = false;
        canvas.SetActive(false);
    }
    void DetermineFillModifier(float  rotationZ) {
        if(handInt == 2) {
           if(  rotationZ >= 270 &&   rotationZ < 285) {
                modifier = modifierValuesFill[0];
            }
            else if(  rotationZ >= 285 &&   rotationZ < 300) {
                modifier = modifierValuesFill[1];
            }
            else if(  rotationZ >= 300 &&   rotationZ < 315) {
                modifier = modifierValuesFill[2];
                 
            }
            else if(  rotationZ >= 315 &&   rotationZ < 330) {
                modifier = modifierValuesFill[3];
                 
            }
            else if(   rotationZ >= 330 &&    rotationZ < 345) {
                modifier = modifierValuesFill[4];
                 
            }
            else {
                modifier = modifierValuesFill[5];
                 
            };
        }
        else {
            if(   rotationZ >= 255 &&    rotationZ < 270) {
                modifier = modifierValuesFill[0];
            }
            else if(   rotationZ >= 240 &&    rotationZ < 255) {
                modifier = modifierValuesFill[1];
                 
            }
            else if(   rotationZ >= 225 &&    rotationZ < 240) {
                modifier = modifierValuesFill[2];
                 
            }
            else if(   rotationZ >= 210 &&    rotationZ < 225) {
                modifier = modifierValuesFill[3];
                 
            }
            else if(   rotationZ >= 195 &&    rotationZ < 210) {
                modifier = modifierValuesFill[4];
                 
            }
            else {
                modifier = modifierValuesFill[5];
                 
            }
        }
    }
    void DetermineTapModifier(float rotationZ) {
        if(handInt == 1) {
           if(   rotationZ >= 270 &&    rotationZ < 285) {
                modifier = modifierValuesTap[0];
            }
            else if(   rotationZ >= 285 &&    rotationZ < 300) {
                modifier = modifierValuesTap[1];
                 
            }
            else if(   rotationZ >= 300 &&    rotationZ < 315) {
                modifier = modifierValuesTap[2];
                 
            }
            else if(   rotationZ >= 315 &&    rotationZ < 330) {
                modifier = modifierValuesTap[3];
                 
            }
            else if(   rotationZ >= 330 &&    rotationZ < 345) {
                modifier = modifierValuesTap[4];
                 
            }
            else {
                modifier = modifierValuesTap[5];
                 
            };
        }
        else {
            if(   rotationZ >= 255 &&    rotationZ < 270) {
                modifier = modifierValuesTap[0];
                
            }
            else if(   rotationZ >= 240 &&    rotationZ < 255) {
                modifier = modifierValuesTap[1];
                 
            }
            else if(   rotationZ >= 225 &&    rotationZ < 240) {
                modifier = modifierValuesTap[2];
                 
            }
            else if(   rotationZ >= 210 &&    rotationZ < 225) {
                modifier = modifierValuesTap[3];
                 
            }
            else if(   rotationZ >= 195 &&    rotationZ < 210) {
                modifier = modifierValuesTap[4];
                 
            }
            else {
                modifier = modifierValuesTap[5];
                 
            }
        }
    }
    void Tap(float roatz) {
        DetermineTapModifier(roatz);
        if(modifier != 1) {
            chargeBar.value -= modifier;
        }
        player.SetRunSpeed(baseRunSpeed * modifier);
        player.SetWalkSpeed(baseWalkSpeed * modifier);
        player.SetStrafeSpeed(baseStrafeSpeed * modifier);
    }
    void Fill(float roatz) {
        Debug.Log(currentHandRotation);
        DetermineFillModifier(roatz);
        
        if(modifier != 1) {
            charge = 1/modifier;
            charge = charge/2;
            chargeBar.value += charge;
        }
        Debug.Log(modifier);
        player.SetRunSpeed(baseRunSpeed * modifier);
        player.SetWalkSpeed(baseWalkSpeed * modifier);
        player.SetStrafeSpeed(baseStrafeSpeed * modifier);
    }
}
