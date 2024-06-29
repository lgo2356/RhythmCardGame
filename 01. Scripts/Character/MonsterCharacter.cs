using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public abstract class MonsterCharacter : Character
    {
        private Action m_OnQuitTurnAction;

        public void GetTurn()
        {
            StartCoroutine(GetTurnCoroutine());
        }

        public void SetOnQuitTurnListener(Action callback)
        {
            m_OnQuitTurnAction = callback;
        }

        private IEnumerator GetTurnCoroutine()
        {
            yield return new WaitForSeconds(1.0f);

            QuitTurn();
        }

        private void QuitTurn()
        {
            Debug.Log("Monster Quit Turn");

            m_OnQuitTurnAction?.Invoke();
        }
    }
}
