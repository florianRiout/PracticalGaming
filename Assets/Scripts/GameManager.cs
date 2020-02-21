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

    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 mousePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Ray my_ray = MainCamera.ScreenPointToRay(mousePosition);
            Debug.DrawRay(my_ray.origin, 100*my_ray.direction);
            if (Physics.Raycast(my_ray, out RaycastHit info_on_hit))
            {
                print(info_on_hit.collider.name);
                //if (info_on_hit.collider.name)
            }
        }
    }
}
