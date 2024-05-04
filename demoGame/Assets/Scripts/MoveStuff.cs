using System.Collections;
using UnityEngine;

public class MoveStuff : MonoBehaviour
{
     GameObject doorBeforePortal;
     float openDuration = 2f; // Duration in seconds for the door to fully open

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;

    GameObject doorAfterPortal;
    float openDuration2 = 2f; // Duration in seconds for the door to fully open

    private Vector3 initialPosition2;
    private Vector3 targetPosition2;
    private bool isOpening2 = false;


    GameObject movingPlatfrom;
    private void Start()
    {
        doorBeforePortal = GameObject.Find("fakewall (1)");
        doorAfterPortal = GameObject.Find("fakewall");
        movingPlatfrom = GameObject.Find("MovingPlatform");
        if (doorAfterPortal != null)
        {
            initialPosition = doorBeforePortal.transform.localPosition;
            targetPosition = initialPosition + Vector3.up * 9f; // Move 6 units up
        }
        if (doorAfterPortal != null)
        {
            initialPosition2 = doorAfterPortal.transform.localPosition;
            targetPosition2 = initialPosition2 + Vector3.up * 9f; // Move 6 units up
        }
    }

    private void Update()
    {
        if (isOpening)
        {
            // Calculate the percentage of time elapsed
            float t = Mathf.Clamp01(Time.deltaTime / openDuration);

            // Move the door towards the target position gradually
            doorBeforePortal.transform.localPosition = Vector3.Lerp(doorBeforePortal.transform.localPosition, targetPosition, t);

            // Check if the door has reached or passed the target position
            if (Vector3.Distance(doorBeforePortal.transform.localPosition, targetPosition) < 0.01f)
            {
                isOpening = false;
            }
        }


        if (isOpening2)
        {
            // Calculate the percentage of time elapsed
            float t = Mathf.Clamp01(Time.deltaTime / openDuration2);

            // Move the door towards the target position gradually
            doorAfterPortal.transform.localPosition = Vector3.Lerp(doorAfterPortal.transform.localPosition, targetPosition2, t);

            // Check if the door has reached or passed the target position
            if (Vector3.Distance(doorAfterPortal.transform.localPosition, targetPosition2) < 0.01f)
            {
                isOpening2 = false;
            }
        }

    }
    public void MovePlatfrom()
    {
        float t = Mathf.Clamp01(Time.deltaTime);
        Vector3 target = new Vector3(-30, 2.5f, 0);
        movingPlatfrom.transform.localPosition = Vector3.Lerp(movingPlatfrom.transform.localPosition, target, t);

        
    }
    public void OpenDoorBeforePortal()
    {
        if (!isOpening)
        {
            // Reset the door position
            doorBeforePortal.transform.localPosition = initialPosition;

            // Start the opening coroutine
            StartCoroutine(OpenDoorCoroutine());
        }
    }
    public void OpenDoorAfterPortal()
    {
        if (!isOpening2)
        {
            // Reset the door position
            doorAfterPortal.transform.localPosition = initialPosition2;

            // Start the opening coroutine
            StartCoroutine(OpenDoorCoroutine2());
        }
    }
    private IEnumerator OpenDoorCoroutine()
    {
        isOpening = true;
        yield return new WaitForSeconds(openDuration);
        isOpening = false;
    }

    private IEnumerator OpenDoorCoroutine2()
    {
        isOpening2 = true;
        yield return new WaitForSeconds(openDuration2);
        isOpening2 = false;
    }

}