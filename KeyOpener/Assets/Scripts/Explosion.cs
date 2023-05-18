using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Rigidbody[] objectsToExplode;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    public void Explode()
    {
        foreach (Rigidbody objectToExplode in objectsToExplode)
        {
            objectToExplode.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
