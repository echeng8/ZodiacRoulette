using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] float followSpeed = 8f;
    [SerializeField] float distanceFromCamera = 5f;
    [SerializeField] float releaseDelayTime = .5f;

    ParticleSystem[] particles;
    bool clicked;

    private void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].Play();
            }
        }
        else if(Input.GetMouseButtonUp(0) && clicked)
        {
            clicked = false;
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Stop();
            }
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 position = Vector3.Lerp(transform.position, mouseWorldPosition, 1.0f - Mathf.Exp(-followSpeed * Time.deltaTime));
        transform.position = position;
    }

}
