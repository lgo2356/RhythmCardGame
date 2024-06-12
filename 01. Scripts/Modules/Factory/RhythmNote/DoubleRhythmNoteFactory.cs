using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class DoubleRhythmNoteFactory : RhythmNoteFactory
    {
        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            IProduct product = base.GetProduct(pos, parent);

            return product;

            //GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmNotePrefab;
            //RhythmNote note = Instantiate(prefab, parent)
            //    .GetOrAddComponent<RhythmNote>();

            //note.transform.position = pos;
            //note.StartMove();

            //return note;
        }

        public override void GenerateRhythmNote(double tempo, RhythmNoteConfig config, Transform parent)
        {
            StartCoroutine(RhythmNoteCoroutine(tempo, config, parent));
        }

        protected override IEnumerator RhythmNoteCoroutine(double meter, RhythmNoteConfig config, Transform parent)
        {
            double timer = meter;
            int tempoValue = 2;
            int count = 2;

            while (count > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= meter)
                {
                    IProduct product = GetProduct(new Vector3(25f, 1110f, 0), parent);
                    product.SetConfig(config);

                    timer -= meter;
                    count--;
                }

                yield return null;
            }
        }
    }
}
