using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart {complete, bottom, top};
    [SerializeField] LadderPart part = LadderPart.complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controls>())
        {
            Player_Controls player = collision.GetComponent<Player_Controls>();
            switch (part)
            {
                case LadderPart.complete:
                    player.canClimb = true;
                    player.ladder = this;
                    break;
                case LadderPart.bottom:
                    player.bottom_L = true;
                    break;
                case LadderPart.top:
                    player.top_L = true;
                    break;
                default:
                    break;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controls>())
        {
            Player_Controls player = collision.GetComponent<Player_Controls>();
            switch (part)
            {
                case LadderPart.complete:
                    player.canClimb = false;
                    break;
                case LadderPart.bottom:
                    player.bottom_L = false;
                    break;
                case LadderPart.top:
                    player.top_L = false;
                    break;
                default:
                    break;
            }
        }
    }
}
