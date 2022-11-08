using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class screenBounce : MonoBehaviour
{
    public Camera mainCamera;
    BoxCollider2D boxCollider;

    public UnityEvent<Collider2D> ExitTriggerFired;

    [SerializeField]
    private float teleportoffset = 0.2f;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        updateBoundSize();
    }

    public void updateBoundSize()
    {
        float Ysize = mainCamera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(Ysize * mainCamera.aspect, Ysize);
        boxCollider.size = boxColliderSize;
    }

    public bool ImOutOfBounds(Vector3 worldPosition)
    {
        return
            Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) ||
            Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);
    }

    public Vector2 calculatePosition(Vector2 worldPosition)
    {
        bool Xbound = Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x);
        bool Ybound = Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);

        Vector2 signVector = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (Xbound && Ybound)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1) + Vector2.Scale(new Vector2(teleportoffset, teleportoffset), signVector);
        }else if (Xbound)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) + new Vector2(teleportoffset * signVector.x, teleportoffset);
        }
        else if (Ybound)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1) + new Vector2(teleportoffset, teleportoffset * signVector.y);
        }
        else
        {
            return worldPosition;
        }
    }
}
