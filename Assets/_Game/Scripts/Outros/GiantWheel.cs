using System;
using UnityEngine;
using DG.Tweening;

public class GiantWheel : MonoBehaviour{
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float rotationDuration;
    [SerializeField] private Rigidbody player;
    [SerializeField] private Transform cabin;
    [SerializeField] private GameObject loadEndingScene;

    private bool active;
    private Vector3 lockPosition;
    private Rigidbody giantWheel;

    private void Awake(){
        giantWheel = GetComponent<Rigidbody>();
    }

    private void GiantWheelAnimation(){
        player.transform.SetParent(cabin);
        player.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        lockPosition = player.transform.localPosition;
        player.isKinematic = true;
        active = true;
        giantWheel.DORotate(endRotation, rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .OnComplete(() => loadEndingScene.SetActive(true))
            .SetLink(gameObject);
    }

    public void TriggerAnimation(){
        GiantWheelAnimation();
    }

    private void Update(){
        if(active) player.transform.localPosition = lockPosition;
    }
}