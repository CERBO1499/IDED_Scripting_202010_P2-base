using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    private const float TIME_TO_DESTROY = 10F;

    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    public int currentHp { get; set; }


    

    public ICommand atackCommand;
    public ICommand healthCommand;
    private void Start()
    {
        currentHP = maxHP;
        
        //Destroy(gameObject, TIME_TO_DESTROY);
        atackCommand = new AtackCommand(this);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            this.gameObject.SetActive(false);
            PoolBalas.Instance.Return(collision.other.gameObject);
            //Destroy(collision.gameObject);
            
            atackCommand.Execute();

            if (currentHP <= 0)
            {
              
               gameObject.SetActive(false);
              
                //Destroy(gameObject);
            }
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            

            if (Player._instance != null)
            {
                Player._instance.healtCommand.Execute();
                //player.Lives -= 1;

                if (Player._instance.Lives <= 0 && Player._instance.OnPlayerDied != null)
                {
                    Player._instance.OnPlayerDied();
                }
            }
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    
    public void RecibeDamageEnemy(int delta)
    {
        currentHp -= delta;
        print("USANDO EL MAKE DAMAGE");
        
        
        if (Player._instance != null)
        {
            Player._instance.ADDScore(scoreAdd);
        }

            
    }
    
    
    
   
}