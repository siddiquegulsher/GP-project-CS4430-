using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject baseWindow;
    public GameObject noWindow;
    public GameObject brokenGlass1;
    public GameObject brokenGlass2;

    private bool isFixed = true;

    private void Start()
    {
        ResetWindow();
    }

    public void ResetWindow()
    {
        baseWindow.SetActive(true);
        noWindow.SetActive(false);
        brokenGlass1.SetActive(false);
        brokenGlass2.SetActive(false);
        isFixed = true;
        Debug.Log("Window reset"); // Add this line
    }

    public void Break()
    {
        if (isFixed)
        {
            noWindow.SetActive(true);
            ToggleRandomBrokenGlass();
            isFixed = false;
            Debug.Log("Window broken"); // Add this line
        }
    }

    private void ToggleRandomBrokenGlass()
    {
        bool showGlass1 = Random.value > 0.5f;
        brokenGlass1.SetActive(showGlass1);
        brokenGlass2.SetActive(!showGlass1);
    }

    public void Fix()
    {
        if (!isFixed)
        {
            ResetWindow();
            Debug.Log("Window fixed"); // Add this line
        }
    }

    public bool IsFixed()
    {
        return isFixed;
    }
}