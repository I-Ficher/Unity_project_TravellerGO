using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OverrideOrb : MonoBehaviour
{
    [SerializeField] public static float throwSpeed = 30.0f;
    [SerializeField] private float collisionStallTime = 2.0f;
    [SerializeField] private float stallTime = 5.0f;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip succesSound;
    [SerializeField] private AudioClip throwSound;

    private float lastX;
    private float lastY;
    private bool released;
    private bool holding;
    private bool trackingCollision=false;
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private InputStatus inputStatus;


    private enum InputStatus
    {
        Grabbing,holding,released,None
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(rigidbody);
        Assert.IsNotNull(dropSound);
        Assert.IsNotNull(succesSound);
        Assert.IsNotNull(throwSound);
    }

    private void Update()
    {
        if (released)
        {
            return;
        }
        if (holding)
        {
            followInput();
        }
        UpdateInputStatus();

        switch (inputStatus)
        {
            case InputStatus.Grabbing:
                Grab();
                break;
            case InputStatus.holding:
                Drag();
                break;
            case InputStatus.released:
                Relese();
                break;
            case InputStatus.None:
                return;
            default:
                return;
        }
    }

    private void UpdateInputStatus()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            inputStatus = InputStatus.Grabbing;
        }
        else if (Input.GetMouseButton(0))
        {
            inputStatus = InputStatus.holding;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            inputStatus = InputStatus.released;
        }
        else
        {
            inputStatus = InputStatus.None;
        }
#endif
#if NOT_UNITY_ANDROID

        if (Input.GetTouch(0).phase==TouchPhase.Began)
        {
            inputStatus = InputStatus.Grabbing;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            inputStatus = InputStatus.released;
        }
        else if (Input.touchCount==1)
        {
            inputStatus = InputStatus.holding;
        }
        else
        {
            inputStatus = InputStatus.None;
        }
#endif
    }

    private void followInput()
    {
        Vector3 inputpos = GetInputPosition();
        inputpos.z = Camera.main.nearClipPlane * 7.5f;
        Vector3 pos = Camera.main.ScreenToWorldPoint(inputpos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 50.0f * Time.deltaTime);
    }

    private void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetInputPosition());
        RaycastHit point;
        if (Physics.Raycast(ray, out point, 100.0f) && point.transform == transform)
        {
            holding = true;
            transform.parent = null;
        }
    }

    private void Drag()
    {
        lastX = GetInputPosition().x;
        lastY = GetInputPosition().y;
    }

    private void Relese()
    {
        if (lastY < GetInputPosition().y)
        {
            Throw(GetInputPosition());
        }
    }

    private void Throw(Vector2 targetPos)
    {
        rigidbody.useGravity = true;
        trackingCollision = true;

        float yDiff = (targetPos.y - lastY) / Screen.height * 100;
        float speed = throwSpeed * yDiff;
        float x = (targetPos.x / Screen.width) - (lastX / Screen.width);
        x = Mathf.Abs(GetInputPosition().x - lastX) / Screen.width * 100 * x;
        Vector3 direction = new Vector3(x, 0.0f, 1.0f);
        direction = Camera.main.transform.TransformDirection(direction);
        rigidbody.AddForce((direction * speed / 2.0f) + Vector3.up * speed);
        audioSource.PlayOneShot(throwSound);
        released = true;
        holding = false;
        Invoke("PowerDown", stallTime);
    }

    private Vector2 GetInputPosition()
    {
        Vector2 result = new Vector2();
#if UNITY_EDITOR
        result = Input.mousePosition;
#endif
#if NOT_UNITY_ANDROID
        result = Input.GetTouch(0).position;
#endif
        return result;
    }

    private void PowerDown()
    {
        CaptureSceneManager manager = FindObjectOfType<CaptureSceneManager>();
        if (manager != null)
        {
            manager.OrbDestroy();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!trackingCollision)
        {
            return;
        }
        trackingCollision = false;
        if (other.gameObject.CompareTag(EnemyConstant.TAG_Enemy))
        {
            audioSource.PlayOneShot(succesSound);

        }
        else
        {
            audioSource.PlayOneShot(dropSound);
        }

        Invoke("PowerDown",collisionStallTime);
    }
}
