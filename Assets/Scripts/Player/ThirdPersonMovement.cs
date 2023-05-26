using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float walkSpeed = 6;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] Transform cam;
    float turnSmoothVelocity;


    [SerializeField] CinemachineFreeLook freeLookCamera;
    float camYSpeed = 4f;
    float camXSpeed = 450f;

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1 && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            float targetAngle = MathF.Atan2(direction.x, direction.z) * 180 / MathF.PI + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            freeLookCamera.m_YAxis.m_MaxSpeed = 0;
            freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        else
        {
            freeLookCamera.m_YAxis.m_MaxSpeed = camYSpeed;
            freeLookCamera.m_XAxis.m_MaxSpeed = camXSpeed;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;


        }
    }
}
