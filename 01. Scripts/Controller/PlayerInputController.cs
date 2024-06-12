using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    RhythmTimingManager m_TimingManager;

    void OnHitRhythmNote()
    {
        Debug.Log("OnHitRhythmNote()");

        m_TimingManager.CheckHitTiming();
    }

    void Awake()
    {
        m_TimingManager = FindObjectOfType<RhythmTimingManager>();
    }

    void Update()
    {
        
    }
}
