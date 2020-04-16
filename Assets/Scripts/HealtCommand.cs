using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtCommand :ICommand
{
    private Player player;

    public HealtCommand(Player p)
    {
        player = p;
    }
    
    public void Execute()
    {
        player.ReciveDamage(1);
        
    }
}
