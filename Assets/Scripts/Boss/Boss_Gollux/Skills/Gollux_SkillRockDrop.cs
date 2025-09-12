using UnityEngine;

public class Gollux_SkillRockDrop : MonoBehaviour
{
    [SerializeField] float heightDis;
    [SerializeField] ObjectPool<Gollux_Rock> objectPool;


    private Entity_Stat stat;
    private Boss boss;


    private float damage;


    private void Awake()
    {
        stat = GetComponentInParent<Entity_Stat>();
        boss = GetComponentInParent<Boss>();
    }

    public void Perform()
    {
        // Get pool object
        Gollux_Rock rock = objectPool.GetObject();

        // Set trans and damage
        damage = stat.GetDamageWithCrit(out bool isCrit);
        Vector3 pos = boss.detectTarget.transform.position + new Vector3(0, heightDis);
        rock.SetDetails(pos, damage);
    }
}
