using System;
using System.Collections;
using UBHG.Extensions;
using UnityEngine;

namespace UBHG.Utility {
    [RequireComponent(typeof(AudioSource))]
    public sealed class Spawner : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float spawnEffectDuration;

        [SerializeField] private bool activateOnly;

        [Header("Prefabs")]
        [SerializeField] private ParticleSystem spawnEffect;

        [Header("Assets")]
        [SerializeField] private AudioClip spawnSound;

        private AudioSource audioSource;

        private void Awake() => audioSource = GetComponent<AudioSource>();

        internal void Spawn<T>(T prefab, Vector2 position, Action<T> onEntityCreated) where T : MonoBehaviour {
            Instantiate(spawnEffect, position, Quaternion.identity);

            StartCoroutine(
                activateOnly
                    ? ActivateLater(prefab, position, onEntityCreated)
                    : InstantiateLater(prefab, position, onEntityCreated));
        }

        private IEnumerator ActivateLater<T>(T entity, Vector2 position, Action<T> onCreated) where T : MonoBehaviour {
            yield return new WaitForSeconds(spawnEffectDuration);
            entity.transform.position = position;
            entity.gameObject.SetActive(true);

            audioSource.PlayWithRandomPitch(spawnSound);
            onCreated(entity);
        }

        private IEnumerator InstantiateLater<T>(T prefab, Vector2 position, Action<T> onCreated)
            where T : MonoBehaviour {
            yield return new WaitForSeconds(spawnEffectDuration);
            var entity = Instantiate(prefab, position, Quaternion.identity);
            if (false == entity.gameObject.activeSelf) {
                entity.gameObject.SetActive(true);
            }

            audioSource.PlayWithRandomPitch(spawnSound);
            onCreated(entity);
        }
    }
}