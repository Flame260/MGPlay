using UnityEngine;
using TMPro;
using DG.Tweening;


public class GamePlayManager : MonoBehaviour
{
    public enum MoveType{
        MoveDistance,
        MoveToTarget,
        MoveToClick
    }
    
    public MoveType moveType;
    public float MoveDistance=2f;
    public float moveTime=0.5f

    public Transform targetPosition;

    public Camera mainCamera;

    public TextMeshProUGUI scoreText;
    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       UpdateScore(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){

        }
    }

    void UpdateScore(){
        scoreText.text = score.ToString();
    }
    void MovetoPosition(){

    }
    
    void Move(){
        if(moveType == MoveType.MoveToTarget){
            Vector3 newPosition=transform.position+transform.forward*MoveDistance;
            transform.DOMove(newPosition=moveTime)
        }
    }

    public void MoveToClickPosition(Vector3 screenPosition){

    }
    
}
