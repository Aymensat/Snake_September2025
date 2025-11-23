    using Unity.VisualScripting;
    using UnityEngine;

    public class AI_manager : MonoBehaviour
    {

        public SnakeController cntrl;

        private MyDirection prevDir;



        private void OnEnable()
        {
            this.enabled = SettingsManager.Instance.isAi;
            cntrl.afterMove += Decide; 

        }

        private void OnDisable()
        {
            cntrl.afterMove -= Decide;
        }

        private void Decide()


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
