using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScottEwing.MovingObjects.MovingPhysicsPlatform2D {
    public class LeverController2D : MonoBehaviour {
        public GameObject m_Handle;
        
        [SerializeField] static float m_OnRotation = -30f;          // the angle of the lever on/off position
        static float m_OffRotation = -m_OnRotation;

        [SerializeField] float m_RotateSpeed = 1000;
        [SerializeField] bool m_IsLeverOn = true;
        [SerializeField] bool m_ShouldOnlyActivateOnce = false;     // should the lever only be activatable once
        bool m_HasBeenActivated;                                    // has the lever already been activated

        [HideInInspector] public float leverState;      // 1 when lever is on -1 when lever is off. Can be passed into Move To Final Position Coroutine
        Quaternion m_OnQuaternion;
        Quaternion m_OffQuaternion;

        bool shouldCheckForLeverInput1 = false;
        bool shouldCheckForLeverInput2 = false;
        bool shouldCheckForLeverInput3 = false;

        public GameObject moveStuff;
        MoveStuff moveStuff2;
        bool isHeDone1 = false;
        bool isHeDone2 = false;
        bool isHeDone3 = false;
        public bool ShouldOnlyActivateOnce {
            get { return m_ShouldOnlyActivateOnce; }
        }
        public bool HasBeenActivated {
            get { return m_HasBeenActivated; }
        }

        private void Awake() {
            Vector3 startRotation = transform.rotation.eulerAngles;
            Vector3 onRotationEuler = startRotation + new Vector3(0, 0, m_OnRotation);
            Vector3 offRotationEuler = startRotation + new Vector3(0, 0, -m_OnRotation);
            // add the on/ off rotation to the initial rotation of the lever to get the correct Quaternion
            m_OnQuaternion = Quaternion.Euler(onRotationEuler);
            m_OffQuaternion = Quaternion.Euler(offRotationEuler);

            if (m_IsLeverOn) {
                m_Handle.transform.rotation = m_OnQuaternion;
                leverState = 1f;
            }
            else {
                m_Handle.transform.rotation = m_OffQuaternion;
                leverState = -1f;
            }
        }

        private void Start() {
            if(moveStuff != null) 
            moveStuff2 =moveStuff.GetComponent<MoveStuff>();
           /* if (m_IsLeverOn)
                moveStuff.OpenDoorBeforePortal();*/
            //else
                //physicsPlatform.enabled = false;    // Stops the platform from moving
        }

        

        // Move the lever to a position between the off and on rotation based on player input (between -1 and 1).
        // Use this method if the player is actively holding the lever and has control over it.
        public void MoveLever(float xInput) {
            if (m_ShouldOnlyActivateOnce && m_HasBeenActivated) {
                return;
            }
            float t = xInput / 2 + 0.5f;     // this line converts the xInput which will be between -1 and 1 into the appropriate number between 0 and 1 which can be used in the lerp method.
            Quaternion targetRotation = Quaternion.Lerp(m_OffQuaternion, m_OnQuaternion, t);
            m_Handle.transform.rotation = Quaternion.RotateTowards(m_Handle.transform.rotation, targetRotation, Time.deltaTime * m_RotateSpeed);
        }

        // This moves the lever to its final position after the hand has released the lever
        public IEnumerator MoveLeverToFinalPosition(float xInput) {
            if (m_ShouldOnlyActivateOnce && m_HasBeenActivated) {
                yield break;
            }
            bool keepLooping = true;
            Quaternion targetRotation = m_OnQuaternion;     // on by default
            if (xInput < 0)
                targetRotation = m_OffQuaternion;           //Set to off if input is less than 0        

            while (keepLooping)                             // loops until the lever is at the target position 
            {
                m_Handle.transform.rotation = Quaternion.RotateTowards(m_Handle.transform.rotation, targetRotation, Time.deltaTime * m_RotateSpeed);
                if (m_Handle.transform.rotation == targetRotation)
                    keepLooping = false;
                yield return null;
            }
            
            m_HasBeenActivated = true;
            SetLeverState();
        }

        private void SetLeverState() {
            if (m_Handle.transform.rotation == m_OnQuaternion)
                leverState = 1f;
            else if (m_Handle.transform.rotation == m_OffQuaternion)
                leverState = -1f;
        }

        


        // === Activating the Lever START ===
        private void Update() {
            // If the lever is touching the player and the submit key is pressed then activate the lever
            if (shouldCheckForLeverInput1 && Input.GetButtonDown("Submit")&&!isHeDone1) {
                StartCoroutine(MoveLeverToFinalPosition(-leverState));      // pass in - lever state to switch it to other position
                moveStuff2.OpenDoorBeforePortal();
                isHeDone1 = true;
            }
            if (shouldCheckForLeverInput2 && Input.GetButtonDown("Submit") && !isHeDone2)
            {
                StartCoroutine(MoveLeverToFinalPosition(-leverState));      // pass in - lever state to switch it to other position
                moveStuff2.OpenDoorAfterPortal();
                isHeDone2 = true;
            }
            if (shouldCheckForLeverInput3 && Input.GetButtonDown("Submit") && !isHeDone3)
            {
                StartCoroutine(MoveLeverToFinalPosition(-leverState));      // pass in - lever state to switch it to other position
                moveStuff2.OpenDoorFirstLevel();
                isHeDone3 = true;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")&&this.gameObject.CompareTag("lever1")) {
                shouldCheckForLeverInput1 = true;
            }
            if (collision.CompareTag("Player") && this.gameObject.CompareTag("lever2"))
            {
                shouldCheckForLeverInput2 = true;
            }
            if (collision.CompareTag("Player") && this.gameObject.CompareTag("lever3"))
            {
                shouldCheckForLeverInput3 = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                shouldCheckForLeverInput1 = false;
            }
            if (collision.CompareTag("Player") && this.gameObject.CompareTag("lever2"))
            {
                shouldCheckForLeverInput2 = true;
            }
        }
        // === Activating the Lever END ===
    }
}
