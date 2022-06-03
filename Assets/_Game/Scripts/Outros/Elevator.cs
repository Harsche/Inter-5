using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour{
    [SerializeField] private Transform elevator;
    [SerializeField] private Vector3[] positions;
    [SerializeField] private float speed;
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Vector3[] leftDoorPositions;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private Vector3[] rightDoorPositions;

    private int positionIndex;
    private int currentFloor;
    private bool moving;
    private Sequence sequence;

    private void Awake(){
        positionIndex = 0;
        currentFloor = positionIndex;
    }

    public void UseElevator(int floor){
        if(moving) return;
        if (currentFloor == floor) return;
        moving = true;
        positionIndex = floor;
        sequence = DOTween.Sequence();
        sequence.Append(
            leftDoor.DOLocalMove(leftDoorPositions[1], 2f)
                .SetSpeedBased()
        );
        
        sequence.Join(
            rightDoor.DOLocalMove(rightDoorPositions[1], 2f)
                .SetSpeedBased()
        );
        
        sequence.Append(
            elevator.DOLocalMove(positions[positionIndex], speed)
                .SetSpeedBased()
        );
        
        sequence.Append(
            leftDoor.DOLocalMove(leftDoorPositions[0], 2f)
                .SetSpeedBased()
        );
        
        sequence.Join(
            rightDoor.DOLocalMove(rightDoorPositions[0], 2f)
                .SetSpeedBased()
        );

        sequence.OnKill(() => {
            currentFloor = positionIndex;
            moving = false;
        });
    }
}