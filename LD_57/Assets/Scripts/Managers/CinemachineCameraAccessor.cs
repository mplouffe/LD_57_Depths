using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class CinemachineCameraAccessor : SingletonBase<CinemachineCameraAccessor>
    {
        [SerializeField]
        private CinemachineVirtualCamera m_cinemachineCamera;

        private void OnEnable()
        {
            LevelManager.Instance.GameSceneLoaded();
        }

        public CinemachineVirtualCamera GetCamera()
        {
            return m_cinemachineCamera;
        }
    }
}
