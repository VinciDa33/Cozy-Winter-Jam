using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShake : MonoBehaviour
{
    private Transform tree;
    private string[] treeShakeSfx = { "treeshake1", "treeshake2", "treeshake3" };

    void Start()
    {
        tree = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TreeShake");
        if (!other.gameObject.transform.parent.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.tag + " is not player " + other.gameObject.name);
            Debug.Log("Not player");
            return;
        }

        int currentSize = other.gameObject.transform.parent.GetComponent<PlayerInterface>().getSize();
        SoundManager.instance.PlayPanSFX(treeShakeSfx[UnityEngine.Random.Range(0, treeShakeSfx.Length)]);
        Shake(currentSize);
    }

    private void Shake(int playerSize)
    {
        StartCoroutine(ShakeCoroutine(playerSize));
    }

    private IEnumerator ShakeCoroutine(int playerSize)
    {
        float duration = 0.3f;
        float magnitude = 1.0f;

        Quaternion originalRotation = tree.rotation;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float z = UnityEngine.Random.Range(-1f * playerSize, 1f * playerSize) * magnitude;
            tree.rotation = Quaternion.Euler(0, 0, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        tree.rotation = originalRotation;
    }
}