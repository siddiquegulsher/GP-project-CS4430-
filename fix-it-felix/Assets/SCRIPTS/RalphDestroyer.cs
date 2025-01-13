using UnityEngine;
using System.Collections;

public class RalphDestroyer : MonoBehaviour
{
    [SerializeField] private float minBreakInterval = 1f;
    [SerializeField] private float maxBreakInterval = 3f;

    private void Start()
    {
        StartCoroutine(BreakWindowsRoutine());
    }

    private IEnumerator BreakWindowsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBreakInterval, maxBreakInterval));
            BreakRandomWindow();
        }
    }

    private void BreakRandomWindow()
    {
        WindowManager[] windows = FindObjectsOfType<WindowManager>();
        
        if (windows.Length > 0)
        {
            int randomIndex = Random.Range(0, windows.Length);
            if (windows[randomIndex].IsFixed()) // Only break if it's not already broken
            {
                windows[randomIndex].Break();
                Debug.Log("Window broken!"); // Add this line for debugging
            }
        }
    }
}