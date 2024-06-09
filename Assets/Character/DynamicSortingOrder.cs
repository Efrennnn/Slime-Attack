using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Set sorting order based on the negative Y position
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}
