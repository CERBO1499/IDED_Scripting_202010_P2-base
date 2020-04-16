using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackCommand : ICommand
{
    //private Player _player;
    private Target _target;

    public AtackCommand( Target tg)
    {
       // _player = p;
        _target = tg;
    }
    public void Execute()
    {
        _target.RecibeDamageEnemy(1);
        
        //_player.MakeDamage(1);
    }
    
    
    
}
