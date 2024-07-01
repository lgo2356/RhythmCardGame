using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    /**
     * 몬스터가 사용한 글로벌 방해 요소를 관리한다.
     * 플레이어가 사용한 글로벌 이로운 효과를 관리한다.
     * 턴을 관리한다.
     */
    public class BattleManager : MonoBehaviour
    {
        private Button m_TurnButton;
        private Button m_CardButton;
        private PlayerCharacter m_Player;
        private MonsterCharacter m_Monster;

        public bool IsPlayerTurn 
        { 
            get; 
            private set;
        }

        public void InjectUI(Button turnButton, Button cardButton)
        {
            if (turnButton == null)
                throw new System.Exception("턴 버튼이 없습니다.");

            if (cardButton == null)
                throw new System.Exception("카드 버튼이 없습니다.");

            m_TurnButton = turnButton;
            m_CardButton = cardButton;
        }

        public void InjectCharacter(PlayerCharacter player, MonsterCharacter monster)
        {
            if (player == null)
                throw new System.Exception("플레이어가 없습니다.");

            if (monster == null)
                throw new System.Exception("몬스터가 없습니다.");

            m_Player = player;
            m_Monster = monster;
        }

        public bool GetFirstTurn()
        {
            IsPlayerTurn = true;

            return IsPlayerTurn;
        }

        public void NextTurn()
        {
            IsPlayerTurn = !IsPlayerTurn;

            if (IsPlayerTurn)
            {
                SetPlayerTurn();
            }
            else
            {
                SetMonsterTurn();
            }
        }

        private void SetPlayerTurn()
        {
            Debug.Log("Player Turn");

            m_TurnButton.gameObject.SetActive(true);
            m_CardButton.gameObject.SetActive(true);
        }

        private void SetMonsterTurn()
        {
            Debug.Log("Monster Turn");

            m_TurnButton.gameObject.SetActive(false);
            m_CardButton.gameObject.SetActive(false);

            StartCoroutine(MonsterTurnCoroutine());
        }

        private IEnumerator MonsterTurnCoroutine()
        {
            yield return new WaitForSeconds(1.0f);

            //TODO : 몬스터 AI 구현
            /**
             * 몬스터 행동패턴
             * 1. 플레이어 공격하기
             * 2. 플레이어 디버프 걸기
             * 3. 리듬 디버프(노트 속도 느리게/빠르게 하기, 노트 투명하게 하기) 걸기
             * 1번은 무조건 행동하고, 마나가 있으면 2, 3번 둘 중에 하나를 선택해서 사용한다.
             */
            m_Monster.Attack(m_Player, m_Monster.Stat.AttackDamage);

            yield return new WaitForSeconds(1.5f);

            NextTurn();

            //m_Monster.SetOnQuitTurnListener(() => NextTurn());
            //m_Monster.GetTurn();
        }
    }
}
