using System.Collections.Generic;
using UnityEngine;

public class ClickToAppear : MonoBehaviour
{
    [SerializeField] private Material normalMaterial = null;
    [SerializeField] private Material transparentMaterial = null;
    private readonly LayerMask _normalLayerMask = 8;
    private readonly LayerMask _transparentLayerMask = 0;

    private List<BoxCollider2D> _colliders;
    private List<SpriteRenderer> _renderers;
    private bool isClickable = true;

    private void Awake()
    {
        if (!isClickable) return;
        
        _colliders = new List<BoxCollider2D>(GetComponents<BoxCollider2D>());
        _renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        _colliders.ForEach(coll =>
        {
            if (!coll.isTrigger) coll.enabled = false;
        });
        _renderers.ForEach(item => item.material = transparentMaterial);
        gameObject.layer = _transparentLayerMask;
    }

    private void OnMouseDown()
    {
        if (!isClickable) return;
        
        _colliders.ForEach(item => item.enabled = true);
        _renderers.ForEach(item => item.material = normalMaterial);
        gameObject.layer = _normalLayerMask;
    }
}
