using UnityEngine;

public class InputReader : MonoBehaviour
{
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float JumpInput => Input.GetAxis("Jump");
}
