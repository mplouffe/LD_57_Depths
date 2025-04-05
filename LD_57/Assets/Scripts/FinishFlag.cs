using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class FinishFlag : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelManager.Instance.EndOfLevelReached();
            }
        }
    }
}
