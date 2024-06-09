using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    private void InstantiateCharacter()
    {
        GameObject playerPrefab = BattleSceneGameManager.Instance.SceneData.PlayerCharacterPrefab;
        GameObject monsterPrefab = BattleSceneGameManager.Instance.SceneData.MonsterCharacterPrefab;

        Debug.Log("playerPrefab: " + playerPrefab.name);
        Debug.Log("monsterPrefab: " + monsterPrefab.name);

        //TODO : Character Factory »ý¼º

        //ResourceManager.Instance.InstantiateAsync("Assets/04. Prefabs/Character/PlayerCharacter.prefab", transform, (obj) =>
        //{
        //    PlayerCharacter playerCharacter = obj.GetComponent<PlayerCharacter>();
        //    playerCharacter.Load(CharacterType.Slime);
        //});

        //ResourceManager.Instance.InstantiateAsync("Assets/04. Prefabs/Character/MonsterCharacter.prefab", transform, (obj) =>
        //{
        //    MonsterCharacter monsterCharacter = obj.GetComponent<MonsterCharacter>();
        //    monsterCharacter.Load(CharacterType.Cat);
        //});
    }

    private void Start()
    {
        //InstantiateCharacter();
    }
}
