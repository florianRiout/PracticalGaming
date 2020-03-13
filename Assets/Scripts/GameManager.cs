using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Player Player { get; set; }
    public static GameObject[] Enemies { get; set; }
    public static GameObject[] Menus { get; set; }
    public static Button Revive { get; set; }
    public static GameObject Death { get; set; }
    public static GameObject Target { get; set; }
    public static Camera MainCamera { get; set; }
    public static Enemy SelectedEnemy { get; set; }
    public static bool isTap { get; set; }

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        isTap = true;
        timer = 0;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Menus = GameObject.FindGameObjectsWithTag("Menu");
        for(int i = 0; i < Menus.Length; i++)
        {
            Menus[i].gameObject.SetActive(false);
            if (Menus[i].name.Equals("Death"))
            {
                Death = Menus[i];
                Revive = Menus[i].GetComponentInChildren<Button>();
            }
        }
        Revive.onClick.AddListener(Player.Respawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                isTap = false;
            }
            Ray my_ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(my_ray.origin, 50 * my_ray.direction);
            if (isTap)
            {
                if (Physics.Raycast(my_ray, out RaycastHit info_on_hit))
                {
                    if (SelectedEnemy != info_on_hit.collider.transform.GetComponentInParent<Enemy>() && info_on_hit.collider.transform.GetComponentInParent<Enemy>() != null)
                    {
                        if (SelectedEnemy != null)
                            SelectedEnemy.Unselect();
                        SelectedEnemy = info_on_hit.collider.transform.GetComponentInParent<Enemy>();
                        SelectedEnemy.Select();
                    }
                    else if (info_on_hit.collider.transform.GetComponentInParent<Enemy>() == null)
                    {
                        if (SelectedEnemy != null)
                        {
                            SelectedEnemy.Unselect();
                            SelectedEnemy = null;
                        }
                    }
                }
                else
                {
                    if (SelectedEnemy != null)
                    {
                        SelectedEnemy.Unselect();
                        SelectedEnemy = null;
                    }
                }
            }
                
        }
        else
        {
            timer = 0;
            isTap = true;
        }
    }
}
