using lvl_0;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{

    [Serializable]
    public class DirectorEvent
    {
        public GameEvent EventType;

        public GameStartEvent GameStartEvent;

        public float Wait;
    }

    [Serializable]
    public enum GameEvent
    {
        Wait,
        GameStart,
        GameOver,
    }
}
