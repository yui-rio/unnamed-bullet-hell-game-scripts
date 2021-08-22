using System.Collections;
using System.Collections.Generic;
using UBHG.Common;
using UnityEngine;

namespace UBHG.LevelProgress {
    public sealed class WaitUntilEntitiesKilled : LevelActivity {
        [SerializeField] private List<Health> entities;

        private int entitiesAlive;

        protected override IEnumerator ActivityAction() {
            entitiesAlive = entities.Count;

            foreach (var entity in entities) {
                entity.Killed += () => entitiesAlive -= 1;
            }

            yield return new WaitUntil(() => 0 == entitiesAlive);
        }
    }
}