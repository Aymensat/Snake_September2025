using Unity.VisualScripting;
using UnityEngine;

public class AI_manager : MonoBehaviour
{

    public SnakeController cntrl;

    private MyDirection prevDir;



    private void OnEnable()
    {
        this.enabled = SettingsManager.Instance.isAi;
    }

    private void Update()


    {
        Debug.Log("testing " + (cntrl.timer * cntrl.speed - cntrl.timeUntilMove / 2));

        if (Mathf.Abs(cntrl.timer * cntrl.speed - cntrl.timeUntilMove / 2) < (0.45)) 
        {
            Debug.Log("forca ??"); 
            if ((cntrl.movingDirection == MyDirection.right) && (cntrl.snakeArray[^1].x == cntrl.gridWidth - 1))
            {
                cntrl.inputDirection = MyDirection.up;
                prevDir = MyDirection.right;
            }
        


            if((cntrl.movingDirection == MyDirection.up || cntrl.movingDirection == MyDirection.down) && prevDir == MyDirection.right)
            {
                cntrl.inputDirection = MyDirection.left;
            }



            if((cntrl.movingDirection == MyDirection.up  || cntrl.movingDirection == MyDirection.down) && prevDir == MyDirection.left)
            {
                cntrl.inputDirection=MyDirection.right;
            }
            

            if ((cntrl.movingDirection == MyDirection.left) && (cntrl.snakeArray[^1].x == 0))
            {
                cntrl.inputDirection = MyDirection.up;
                prevDir = MyDirection.left;
            }

        }

    }

}
