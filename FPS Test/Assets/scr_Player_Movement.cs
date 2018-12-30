using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player_Movement : MonoBehaviour {

    CharacterController charControl;

    public float moveSpeed;

    private bool isJumping;
    [SerializeField] private AnimationCurve jumpFallOff;
    public float jumpMultiplier;

    private float diagonalSpeedModifier = 1f;

    void Awake()
    {

        charControl = GetComponent<CharacterController>();

    }

    void Update()
    {

        MovePlayer();

        Mathf.Clamp(charControl.velocity.x, 0f, 0.1f);

        Mathf.Clamp(charControl.velocity.z, 0f, 0.1f);

    }

    void MovePlayer()
    {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if(horiz != 0 && vert != 0)
        {

            diagonalSpeedModifier = 0.714f;

        }

        else
        {

            diagonalSpeedModifier = 1f;

        }

        horiz *= moveSpeed;
        vert *= moveSpeed;

        Vector3 moveDirSide = transform.right * horiz * diagonalSpeedModifier;
        Vector3 moveDirForward = transform.forward * vert * diagonalSpeedModifier;

        //SimpleMove function actually applies Time.deltaTime in the background, but I put it anyway as it is good practice.
        charControl.SimpleMove(moveDirSide + moveDirForward);

        JumpInput();

    }

    void JumpInput()
    {
    
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
    
            isJumping = true;
    
            StartCoroutine(JumpEvent());
    
        }
    
    }

    IEnumerator JumpEvent()
    {
    
        charControl.slopeLimit = 90f;
    
        float timeInAir = 0f;
    
        do
        {
    
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
    
            charControl.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
    
            timeInAir += Time.deltaTime;
    
            yield return null;
    
        } while (!charControl.isGrounded && charControl.collisionFlags != CollisionFlags.Above);
    
        charControl.slopeLimit = 45f;
    
        isJumping = false;
    
    }

}
