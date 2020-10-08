using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orginPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float y = Random.Range(5, 6) * magnitude;

            transform.localPosition = new Vector3(orginPos.x, y, orginPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = orginPos;
    }
}
