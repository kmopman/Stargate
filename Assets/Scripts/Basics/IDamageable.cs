using UnityEngine;

public interface IDamageable// a interface, every script that implements this interface is forced to have this method(TakeDamg) including it.
{
    void TakeDamg(float damage);//how much damage
}
