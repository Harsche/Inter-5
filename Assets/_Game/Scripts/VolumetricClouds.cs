using System;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class VolumetricClouds : MonoBehaviour{
    [SerializeField] private bool drawClouds = true;
    [SerializeField] private float cloudHeight;
    [SerializeField] private int horizontalStackSize = 20;
    [SerializeField] private int layer;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Mesh quadMesh;
    [SerializeField] private Material cloudMaterial;
    private Camera currentCamera;
    private float offset;
    private Matrix4x4 matrix;
    private Transform myTransform;
    private static readonly int Property = Shader.PropertyToID("Y_Position");
    private static readonly int Property1 = Shader.PropertyToID("Cloud_Height");

    private void OnValidate(){
        EditorApplication.playModeStateChanged += change => {
            switch (change){
                case PlayModeStateChange.EnteredPlayMode:
                    currentCamera = myCamera;
                    return;
                case PlayModeStateChange.ExitingPlayMode:{
                    if (Camera.current != null)
                        currentCamera = Camera.current;
                    return;
                }
            }
        };
    }

    private void Awake(){
        myTransform = transform;
    }

    private void Update(){
        if (!drawClouds) return;
        Vector3 position = myTransform.position;


        cloudMaterial.SetFloat(Property, position.y);
        cloudMaterial.SetFloat(Property1, cloudHeight);

        offset = cloudHeight / horizontalStackSize / 2f;
        Vector3 startPosition = position + (Vector3.up * (offset * horizontalStackSize / 2f));
        for (int i = 0; i < horizontalStackSize; i++){
            matrix = Matrix4x4.TRS(startPosition - (Vector3.up * (offset * i)), myTransform.rotation,
                myTransform.localScale);
            Graphics.DrawMesh(
                quadMesh,
                matrix,
                cloudMaterial,
                layer,
                currentCamera,
                0,
                null,
                false,
                false,
                false
            );
        }
    }
}