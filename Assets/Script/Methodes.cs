using UnityEngine;

public static class Methodes 
{
    #region Methodes

    // return vector3 with  Random Position inside screen
    public static Vector3 RandomPositionInScreen(int dir)
    {
        float x = Random.Range(ScreenUtils.ScreenLeft + 1, ScreenUtils.ScreenRight - .99f);
        float y = Random.Range(ScreenUtils.ScreenBottom + 1, ScreenUtils.ScreenTop - 1f);
        if (dir == 0) // upper
        {
            y = ScreenUtils.ScreenTop;
        }
        else if (dir == 1) // lower
        {
            y = ScreenUtils.ScreenBottom;
        }
        else if (dir == 2) // right
        {
            x = ScreenUtils.ScreenRight;
        }
        else if (dir == 3) // left
        {
            x = ScreenUtils.ScreenLeft;
        }
        // else return position inside screen  
        return new Vector3(x, y, 0);
    }
    
    // return direction

    public static Vector2 Direction(float direction)
    {
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == 0) // up
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == 1) // left
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == 2) // down
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else // right
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }
        // else return position inside screen
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    #endregion       
}
