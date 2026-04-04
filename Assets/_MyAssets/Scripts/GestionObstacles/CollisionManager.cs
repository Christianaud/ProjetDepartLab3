using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static event EventHandler<OnCollisionOccuredEventArgs> OnCollisionOccured;  // Event qui se d?clenche lors d'une collision
    public class OnCollisionOccuredEventArgs : EventArgs
    {
        public int CollisionValue; // Un EventArgs nomm? CollisionValue
    }

    [SerializeField] private Material _hitMaterial = default(Material);
    [SerializeField] private int _collisionValue = 1;

    private bool _isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isHit)
        {
            //Debug.Log("Collision détectée");
            if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            {
                meshRenderer.material = _hitMaterial;
            }
            else
            {
                MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (var m in meshes)
                {
                    m.material = _hitMaterial;
                }
            }

            OnCollisionOccured?.Invoke(this, new OnCollisionOccuredEventArgs
            {
                CollisionValue = _collisionValue
            });
            //Debug.Log("Event envoyé");
            _isHit = true;
        }
    }
}

