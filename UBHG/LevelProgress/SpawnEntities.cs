using System;
using System.Collections;
using System.Collections.Generic;
using UBHG.Common;
using UBHG.Utility;
using UnityEngine;

namespace UBHG.LevelProgress {
    public sealed class SpawnEntities : LevelActivity {
        [SerializeField] private List<SpawnParameters> entities;

        [Header("Parameters")]
        [SerializeField] private bool waitUntilKilled = true;

        [Header("Dependencies")]
        [SerializeField] private Spawner spawner;

        private int entitiesAlive;

        protected override IEnumerator ActivityAction() {
            entitiesAlive = waitUntilKilled ? entities.Count : 0;

            var onEntityCreated = waitUntilKilled
                ? (Action<Health>) (entity => entity.Killed += () => entitiesAlive -= 1)
                : _ => { };

            foreach (var (prefab, position) in entities) {
                spawner.Spawn(prefab, position, onEntityCreated);
            }

            yield return new WaitUntil(() => 0 == entitiesAlive);
        }

        [Serializable]
        private struct SpawnParameters {
            public Health prefab;
            public Vector2 spawnPosition;

            public void Deconstruct(out Health prefab_, out Vector2 spawnPosition_) =>
                (prefab_, spawnPosition_) = (prefab, spawnPosition);
        }
    }
}