using System.Collections.Generic;
using UnityEngine;

public class ClickToAppear : MonoBehaviour
{
    [SerializeField] private Material normalMaterial = null;
    [SerializeField] private Material transparentMaterial = null;
    private LayerMask normalLayerMask = 8;
    private LayerMask transparentLayerMask = 0;

    private List<BoxCollider2D> colliders;
    private List<SpriteRenderer> renderers;
    private bool isClickable = true;

    private void Awake()
    {
        if (isClickable)
        {
            colliders = new List<BoxCollider2D>(GetComponents<BoxCollider2D>());
            renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

            colliders.ForEach(coll =>
            {
                if (!coll.isTrigger) coll.enabled = false;
            });
            renderers.ForEach(renderer => renderer.material = transparentMaterial);
            gameObject.layer = transparentLayerMask;
        }
    }

    private void OnMouseDown()
    {
        if (isClickable)
        {
            colliders.ForEach(collider => collider.enabled = true);
            renderers.ForEach(renderer => renderer.material = normalMaterial);
            gameObject.layer = normalLayerMask;
        }
    }
}
