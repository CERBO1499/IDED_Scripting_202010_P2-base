using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public  class Player : MonoBehaviour
{
    #region SingletonPlayer

    public static Player _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad((this.gameObject));
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
   
    private const int PLAYER_LIVES = 3;

    private const float PLAYER_RADIUS = 0.4F;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float hVal;

    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private Rigidbody bullet;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float bulletSpeed = 3F;

    #endregion Bullet

    #region BoundsReferences

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    #endregion BoundsReferences

    #region StatsProperties

    public int Score { get; set; }
    public static int playerLives
    {
        get => PLAYER_LIVES;
        
    }
    public int Lives { get;  set; }

    #endregion StatsProperties

    #region MovementProperties

    public bool ShouldMove
    {
        get =>
            (hVal != 0F && InsideCamera) ||
            (hVal > 0F && ReachedLeftBound) ||
            (hVal < 0F && ReachedRightBound);
    }

    private bool InsideCamera
    {
        get => !ReachedRightBound && !ReachedLeftBound;
    }

    private bool ReachedRightBound { get => referencePointComponent >= rightCameraBound; }
    private bool ReachedLeftBound { get => referencePointComponent <= leftCameraBound; }

    private bool CanShoot { get => bulletSpawnPoint != null && bullet != null; }

   

    #endregion MovementProperties

    public ICommand healtCommand;
    //public ICommand atackCommand;
    
    public Action OnPlayerDied;
   // public Action <int> OnPlayerScoreChanged;
    public Action <int> OnPlayerHit;
    
    public Action <int>OnPlayerScoreChanged;

    //private Target tgReference;

    // Start is called before the first frame update
    private void Start()
    {
        
        healtCommand = new HealtCommand(this);
        //atackCommand = new AtackCommand(this,tgReference);
        
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            0F, 0F, 0F)).x + PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            1F, 0F, 0F)).x - PLAYER_RADIUS;

        Lives = PLAYER_LIVES;
    }

   public void ReciveDamage(int delta)
    {
        Lives -= delta;
        if (Lives > 0)
        {
            if (OnPlayerHit != null)
            {
                OnPlayerHit(delta);
            }

        }
        else
        {
            if (OnPlayerDied != null)
            {
                OnPlayerDied();
            }
        }
        if (Lives <= 0)
        {
            this.enabled = false;
            gameObject.SetActive(false);
        }
        
        print("Recibiendo daño del command");
        
    }

    
    private void Update()
    {
        
        if(Lives>0)
        {
            hVal = Input.GetAxis("Horizontal");

            if (ShouldMove)
            {
                transform.Translate(transform.right * hVal * moveSpeed * Time.deltaTime);
                referencePointComponent = transform.position.x;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                && CanShoot)
            {
               /* Instantiate<Rigidbody>
                   (bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation)
                   .AddForce(transform.up * bulletSpeed, ForceMode.Impulse);*/
               
               ShootBala();
               
            }
        }
    }

    void ShootBala()
    {
        PoolBalas.Instance.Get().transform.position = bulletSpawnPoint.position;
        print("usandoPool");
    }

    public void ADDScore(int delta)
    {
        Score += delta;
        print("ADDSCORE funcionando");

        if (OnPlayerScoreChanged != null)
        {
            OnPlayerScoreChanged(delta);
        }
    }
}