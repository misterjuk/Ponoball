using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Animator")]
    [SerializeField]
    private Animator LeftAnimator;
    [SerializeField]
    private Animator RightAnimator;

    private float _screenWidth;
    private float _screenHeight;

    void Start()
    {
        _screenWidth = Screen.currentResolution.width;
        _screenHeight = Screen.currentResolution.height;
        Debug.Log(Screen.currentResolution.width);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LeftAnimator.SetBool("Up", true);
        }
        if(Input.touchCount >= 1)
        {
            if (Input.GetTouch(0).position.x >= _screenWidth/2)
            {
                RightAnimator.SetBool("Up",true);
                Debug.Log("right animation");
            }
            else if(Input.GetTouch(0).position.x < _screenWidth / 2)
            {
                LeftAnimator.SetBool("Up", true);
                Debug.Log("left animation");
            }
        }
    }
}
