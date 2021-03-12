using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkCharacter : MonoBehaviour
{
    [SerializeField] public List<Target> legs;
    private bool switcher = true;
   
    public virtual void Start()
    {
        MoveLegs(true);
    }
    public virtual void MoveNextLeg(int current)
    {

        if (legs[current].side == Side.RIGHT)
        {
            if (current + 1 > legs.Count)
                return;
            legs[current + 1].CanMove = true;
        }
        else
        {
            if (current - 1 > legs.Count)
                return;
            legs[current - 1].CanMove = true;
        }

    }
    public virtual void MoveLegs(bool side)
    {
        int a = -1;
        if (side) //0XX00XX0
        {
            legs[0].CanMove = true;
            for (int i = 1; i < legs.Count; i++)
            {
                if (a == 0 || a == 1)
                    legs[i].CanMove = true;
                else
                    legs[i].CanMove = false;
                if (i >= 2)
                {
                    if (a > 1)
                        a = -1;
                    else
                        a++;
                }

            }
        }
        else //X00XX00X
        {
            int b = 0;
            legs[0].CanMove = false;
            for (int i = 1; i < legs.Count; i++)
            {
                if (b == 0 || b == 1)
                    legs[i].CanMove = true;
                else
                    legs[i].CanMove = false;

                if (b > 2)
                    b = 0;
                else
                    b++;

            }
        }
    }

}
