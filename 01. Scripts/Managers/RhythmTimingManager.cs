using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingManager : MonoBehaviour
{
    [SerializeField] Transform m_NoteHitZoneTransform;
    [SerializeField] RectTransform[] m_NoteHitBoxRects;

    public List<GameObject> RhythmNoteInstanceList = new();

    Vector2[] m_NoteHitBoxs;

    public void CheckHitTiming()
    {
        for (int i = 0; i < RhythmNoteInstanceList.Count; i++)
        {
            float notePosX = RhythmNoteInstanceList[i].transform.localPosition.x;

            for (int j = 0; j < m_NoteHitBoxs.Length; j++)
            {
                if (m_NoteHitBoxs[j].x <= notePosX && notePosX <= m_NoteHitBoxs[j].y)
                {
                    Debug.Log("Hit Note!" + j);
                    return;
                }
            }
        }

        Debug.Log("Break!");
    }

    void Awake()
    {
        m_NoteHitBoxs = new Vector2[m_NoteHitBoxRects.Length];

        for (int i = 0; i < m_NoteHitBoxs.Length; i++)
        {
            m_NoteHitBoxs[i]
                .Set(m_NoteHitZoneTransform.localPosition.x - m_NoteHitBoxRects[i].rect.width / 2,
                    m_NoteHitZoneTransform.localPosition.x + m_NoteHitBoxRects[i].rect.width / 2);
        }
    }

    void Start()
    {
        
    }
}
