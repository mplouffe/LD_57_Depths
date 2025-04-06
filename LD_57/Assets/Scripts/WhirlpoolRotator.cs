using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolRotator : MonoBehaviour
{
    [SerializeField]
    private float m_rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, m_rotationSpeed * Time.deltaTime);
    }
}
