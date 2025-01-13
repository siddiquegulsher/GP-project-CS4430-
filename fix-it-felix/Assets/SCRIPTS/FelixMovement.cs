using UnityEngine;

public class FelixMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float interactionRange = 2f;
    private Rigidbody2D body;
    private Animator anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(12, 12, 12);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-12, 12, 12);

        if (Input.GetKeyDown(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);

        // Run animation control
        anim.SetBool("run", horizontalInput != 0);

        if (Input.GetKeyDown(KeyCode.F))
            TriggerFixing();

        CheckWinCondition();
    }
private void TriggerFixing()
{
    WindowManager closestWindow = FindClosestWindowManager();

    if (closestWindow != null && !closestWindow.IsFixed())
    {
        anim.SetTrigger("fixing"); // Change this line
        closestWindow.Fix();
        Invoke(nameof(StopFixingAnimation), 2f);
    }
}

private void StopFixingAnimation()
{
    anim.ResetTrigger("fixing"); // Change this line
}

    private WindowManager FindClosestWindowManager()
    {
        WindowManager[] windows = FindObjectsOfType<WindowManager>();
        WindowManager closestWindow = null;
        float closestDistance = Mathf.Infinity;

        foreach (WindowManager window in windows)
        {
            float distance = Vector2.Distance(transform.position, window.transform.position);

            if (distance < closestDistance && distance <= interactionRange)
            {
                closestDistance = distance;
                closestWindow = window;
            }
        }

        return closestWindow;
    }

    private void CheckWinCondition()
    {
        WindowManager[] windows = FindObjectsOfType<WindowManager>();
        bool allFixed = true;

        foreach (WindowManager window in windows)
        {
            if (!window.IsFixed())
            {
                allFixed = false;
                break;
            }
        }

        if (allFixed)
        {
            Debug.Log("Felix wins! All windows are fixed!");
        }
    }
}

