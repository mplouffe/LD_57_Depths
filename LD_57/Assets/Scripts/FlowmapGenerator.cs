using UnityEngine;
using System.IO;

public class FlowmapGenerator : MonoBehaviour
{
    [ContextMenu("Generate Flowmap")]
    void Generate()
    {
        int size = 128;
        Texture2D flowmap = new Texture2D(size, size, TextureFormat.RGB24, false);

        Color flowColor = new Color(0.5f, 0.0f, 0.0f); // Red=0.5, Green=0, Blue=0

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                flowmap.SetPixel(x, y, flowColor);
            }
        }

        flowmap.Apply();

        byte[] bytes = flowmap.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/SimpleFlowmap.png", bytes);

        Debug.Log("Flowmap generated at: " + Application.dataPath + "/SimpleFlowmap.png");
    }
}