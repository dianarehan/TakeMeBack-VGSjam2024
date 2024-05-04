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

    GameObject movingPillar;
    private Vector3 initialPosition3;
    private Vector3 targetPosition3;
    private bool isOpening3 = false;

    GameObject movingPlatfrom;

    Vector3 target;


    public AudioClip clip;
    AudioSource audioSource;
    private void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        movingPillar = GameObject.Find("moving pillar");
        doorBeforePortal = GameObject.Find("fakewall (1)");
        doorAfterPortal = GameObject.Find("fakewall");
        movingPlatfrom = GameObject.Find("MovingPlatform");
        if (doorBeforePortal != null)
        {
            initialPosition = doorBeforePortal.transform.localPosition;
            targetPosition = initialPosition + Vector3.up * 9f; // Move 6 units up
        }
        if (doorAfterPortal != null)
        {
            initialPosition2 = doorAfterPortal.transform.localPosition;
            targetPosition2 = initialPosition2 + Vector3.up * 9f; // Move 6 units up
        }
        if (movingPillar != null)
        {
            initialPosition3 = movingPillar.transform.localPosition;
            targetPosition3 = initialPosition3 + Vector3.up * 17f; // Move 6 units up
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
            audioSource.PlayOneShot(clip,0.006f);
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
            audioSource.PlayOneShot(clip,0.006f);
            // Check if the door has reached or passed the target position
            if (Vector3.Distance(doorAfterPortal.transform.localPosition, targetPosition2) < 0.01f)
            {
                isOpening2 = false;
            }
        }
        if (isOpening3)
        {
            // Calculate the percentage of time elapsed
            float t = Mathf.Clamp01(Time.deltaTime);
            audioSource.PlayOneShot(clip,0.006f);
            // Move the door towards the target position gradually
            movingPillar.transform.localPosition = Vector3.Lerp(movingPillar.transform.localPosition, targetPosition3, t);

            // Check if the door has reached or passed the target position
            if (Vector3.Distance(movingPillar.transform.localPosition, targetPosition3) < 0.01f)
            {
                isOpening3 = false;
            }
        }

    }
    public void MovePlatfrom()
    {
        float t = Mathf.Clamp01(Time.deltaTime);
         target = new Vector3(-30, 2.5f, 0);
        movingPlatfrom.transform.localPosition = Vector3.Lerp(movingPlatfrom.transform.localPosition, target, t);

        targetPosition = target;
    }
    public Vector3 GetPlatformMovement()
    {
        // Calculate the platform's movement (target position - current position)
        return targetPosition - movingPlatfrom.transform.localPosition;
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
    public void OpenDoorFirstLevel()
    {
        if (!isOpening3)
        {
            // Reset the door position
            movingPillar.transform.localPosition = initialPosition3;

            // Start the opening coroutine
            StartCoroutine(OpenDoorCoroutine3());
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
    private IEnumerator OpenDoorCoroutine3()
    {
        isOpening3 = true;
        yield return new WaitForSeconds(3);
        isOpening3 = false;
    }
}