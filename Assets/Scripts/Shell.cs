using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        float power = Random.Range(90, 120);

        rigid.AddForce(transform.right * power);

        Destroy(gameObject, 10);
    }
}
