using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UBHG.LevelProgress {
    public sealed class CreateObjects : LevelActivity {
        [Header("Parameters")]
        [SerializeField] private bool activateOnly;

        [SerializeField] private List<CreationParameters> objects;

        protected override IEnumerator ActivityAction() {
            foreach (var (obj, position) in objects) {
                if (activateOnly) {
                    obj.transform.position = position;
                    obj.SetActive(true);
                } else {
                    Instantiate(obj, position, Quaternion.identity);
                }
            }

            yield return null;
        }

        [Serializable]
        private struct CreationParameters {
            public GameObject prefab;
            public Vector2 spawnPosition;

            public void Deconstruct(out GameObject prefab_, out Vector2 spawnPosition_) =>
                (prefab_, spawnPosition_) = (prefab, spawnPosition);
        }
    }
}