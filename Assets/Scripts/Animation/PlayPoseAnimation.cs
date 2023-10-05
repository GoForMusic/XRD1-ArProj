using System;
using UnityEngine;

public class PlayPoseAnimation : MonoBehaviour
{
    public Animator anim;

    public enum AnimationPose
    {
        Idle,
        Sitting,
        TwoHandSwordCombo2,
        Learning,
        StandingYell,
        Talking,
        Talking2,
        Walking,
        WorkingOnDevice,
        SittingDrinking,
        SwordAndShieldSlash,
        TwoHandSwordCombo,
        GreatSwordIdle,
        ReachOut,
        OpeningALid,
        PickingUp,
        Talking3,
        Talking4,
        Salute,
        TwistDance
    };
    public AnimationPose myCard;
    
    // Update is called once per frame
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(myCard.ToString(),  -1, 0f);
    }
}
