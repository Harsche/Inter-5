using UnityEngine;
using DG.Tweening;

public class InteractionIcon : MonoBehaviour{
    [SerializeField] private float yOffset;
    [SerializeField] private float duration = 0.25f;
    private Vector3 startPosition;
    private Material myMaterial;

    private static readonly int Alpha = Shader.PropertyToID("Alpha");
    private Sequence sequence;
    
    private void Awake(){
        myMaterial = GetComponent<MeshRenderer>().material;
        startPosition = transform.localPosition;
        myMaterial.SetFloat(Alpha, 0f);
    }

    public void Toggle(bool value){
        sequence = DOTween.Sequence();
        transform.localPosition = startPosition;
        if (value){
            transform.Translate(0f, yOffset, 0f);
            myMaterial.SetFloat(Alpha, 0f);
            sequence.Append(transform.DOLocalMoveY(-yOffset, duration).SetRelative());
            sequence.Join(myMaterial.DOFloat(1f, Alpha, duration));
            return;
        }
        myMaterial.SetFloat(Alpha, 1f);
        sequence.Append(transform.DOLocalMoveY(yOffset, duration).SetRelative());
        sequence.Join(myMaterial.DOFloat(0f, Alpha, duration));
    }
}