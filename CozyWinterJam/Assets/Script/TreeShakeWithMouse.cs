using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeShakeWithMouse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform tree;
    private string[] treeShakeSfx = { "treeshake1", "treeshake2", "treeshake3" };
    public void Start()
    {
        tree = GetComponent<Transform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.PlayGlobalSFX(treeShakeSfx[UnityEngine.Random.Range(0, treeShakeSfx.Length)]);
        Shake();
    }
    
    private void Shake()
    {
        StartCoroutine(ShakeCoroutine(3));
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
