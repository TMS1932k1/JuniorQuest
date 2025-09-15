using UnityEngine;

public class Gollux_SkillRockDrop : MonoBehaviour
{
    [SerializeField] float heightDis;
    [SerializeField] ObjectPool<Gollux_Rock> objectPool;


    private Entity_Stat stat;
    private Gollux gollux;


    private float damage;


    private void Awake()
    {
        stat = GetComponentInParent<Entity_Stat>();
        gollux = GetComponentInParent<Gollux>();
    }

    public void Perform()
    {
        // Get pool object
        Gollux_Rock rock = objectPool.GetObject();

        // Set trans and damage
        damage = stat.GetDamageWithCrit(out bool isCrit);
        Vector3 pos = new Vector3(gollux.detectTarget.transform.position.x, transform.position.y + heightDis);
        rock.SetDetails(pos, damage);
    }
}
