using UnityEngine;
using System.Collections;
public class bullet : MonoBehaviour
{
    public float lifeTime;
    private float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}