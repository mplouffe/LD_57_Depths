using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupsManager : MonoBehaviour
{
    public static PopupsManager Instance;

    [SerializeField]
    private CanvasGroup m_pausedCanvasGroup;

    [SerializeField]
    private CanvasGroup m_gameOverCanvasGroup;

    [SerializeField]
    private CanvasGroup m_getReadyCanvasGroup;

    [SerializeField]
    private CanvasGroup m_escapedCanvasGroup;

    [SerializeField]
    private CanvasGroup m_levelStartGroup;

    [SerializeField]
    private TextMeshProUGUI m_levelStartText;

    [SerializeField]
    private CanvasGroup m_levelEndingGroup;

    [SerializeField]
    private CanvasGroup m_pollenatingGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;

        m_pausedCanvasGroup.alpha = 0;
        m_gameOverCanvasGroup.alpha = 0;
        m_getReadyCanvasGroup.alpha = 0;
        m_escapedCanvasGroup.alpha = 0;
    }

    public void Pause(bool pause)
    {
        m_pausedCanvasGroup.alpha = pause ? 1 : 0;
    }

    public void GameOver(bool gameOver)
    {
        m_gameOverCanvasGroup.alpha = gameOver ? 1 : 0;
    }

    public void GetReady(bool getReady)
    {
        m_getReadyCanvasGroup.alpha = getReady ? 1 : 0;
    }

    public void Escaped(bool escaped)
    {
        m_escapedCanvasGroup.alpha = escaped ? 1 : 0;
    }

    public void LevelStart(bool levelStarting, int level = 0)
    {
        m_levelStartGroup.alpha = levelStarting ? 1 : 0;
        if (level > 0)
        {
            m_levelStartText.text = $"Level {level}";
        }
        else
        {
            Debug.Log("called with >= 0");
        }
    }

    public void LevelEnd(bool levelEnding)
    {
        m_levelEndingGroup.alpha = levelEnding ? 1 : 0;
    }

    public void Pollenating(bool pollenating)
    {
        m_pollenatingGroup.alpha = pollenating ? 1 : 0;
    }
}
