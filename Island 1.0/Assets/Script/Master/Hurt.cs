using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hurt : MonoBehaviour
{

    public void IceEffect()
    {
        for (int i = 0; i < EnemyCreate.Instance.DragonIces.Count; i++)
        {
            if (EnemyCreate.Instance.DragonIces[i].isHurtRange)
            {
                PlayerHp.Instance.GetHp(EnemyCreate.Instance.DragonIces[i].Damage);
            }
        }
    }
    public void FireHurtEffect()
    {
        for (int i = 0; i < EnemyCreate.Instance.DragonFires.Count; i++)
        {
            if (EnemyCreate.Instance.DragonFires[i].isHurtRange)
            {
                PlayerHp.Instance.GetHp(EnemyCreate.Instance.DragonFires[i].Damage);
            }
        }
        
    }

    public void AttachHurtEffect()
    {
        for (int i = 0; i < EnemyCreate.Instance.Dragons.Count; i++)
        {
            PlayerHp.Instance.GetHp(EnemyCreate.Instance.Dragons[i].Damage);
        }
    }
    public void AttachHurtEffect2()
    {
        for (int i = 0; i < EnemyCreate.Instance.Orcs.Count; i++)
        {
            PlayerHp.Instance.GetHp(EnemyCreate.Instance.Orcs[i].Damage);
        }
    }

    public void HealEffect()
    {

        for (int i = 0; i < EnemyCreate.Instance.Orcs.Count; i++)
        {
            if ((EnemyCreate.Instance.Orcs[i].currentHp <= EnemyCreate.Instance.Orcs[i].maxHp) && EnemyCreate.Instance.Orcs[i].Eheal)
            {
                EnemyCreate.Instance.Orcs[i].currentHp += AllMaster.Instance.GhostDamage;
                EnemyCreate.Instance.Orcs[i].HpSlider.value = EnemyCreate.Instance.Orcs[i].currentHp;
                if (EnemyCreate.Instance.Orcs[i].currentHp > EnemyCreate.Instance.Orcs[i].maxHp)
                    EnemyCreate.Instance.Orcs[i].currentHp = EnemyCreate.Instance.Orcs[i].maxHp;
            }
        }

        for (int i = 0; i < EnemyCreate.Instance.DragonIces.Count; i++)
        {
            if ((EnemyCreate.Instance.DragonIces[i].currentHp <= EnemyCreate.Instance.DragonIces[i].maxHp) && EnemyCreate.Instance.DragonIces[i].Eheal)
            {
                EnemyCreate.Instance.DragonIces[i].currentHp += AllMaster.Instance.GhostDamage;
                EnemyCreate.Instance.DragonIces[i].HpSlider.value = EnemyCreate.Instance.DragonIces[i].currentHp;
                if (EnemyCreate.Instance.DragonIces[i].currentHp > EnemyCreate.Instance.DragonIces[i].maxHp)
                    EnemyCreate.Instance.DragonIces[i].currentHp = EnemyCreate.Instance.DragonIces[i].maxHp;
            }
        }

        for (int i = 0; i < EnemyCreate.Instance.Dragons.Count; i++)
        {
            if ((EnemyCreate.Instance.Dragons[i].currentHp <= EnemyCreate.Instance.Dragons[i].maxHp) && EnemyCreate.Instance.Dragons[i].Eheal)
            {
                EnemyCreate.Instance.Dragons[i].currentHp += AllMaster.Instance.GhostDamage;
                EnemyCreate.Instance.Dragons[i].HpSlider.value = EnemyCreate.Instance.Dragons[i].currentHp;
                if (EnemyCreate.Instance.Dragons[i].currentHp > EnemyCreate.Instance.Dragons[i].maxHp)
                    EnemyCreate.Instance.Dragons[i].currentHp = EnemyCreate.Instance.Dragons[i].maxHp;
            }
        }

        for (int i = 0; i < EnemyCreate.Instance.Ghosts.Count; i++)
        {
            if ((EnemyCreate.Instance.Ghosts[i].currentHp <= EnemyCreate.Instance.Ghosts[i].maxHp) && EnemyCreate.Instance.Ghosts[i].Eheal)
            {
                EnemyCreate.Instance.Ghosts[i].currentHp += AllMaster.Instance.GhostDamage;
                EnemyCreate.Instance.Ghosts[i].HpSlider.value = EnemyCreate.Instance.Ghosts[i].currentHp;
                if (EnemyCreate.Instance.Ghosts[i].currentHp > EnemyCreate.Instance.Ghosts[i].maxHp)
                    EnemyCreate.Instance.Ghosts[i].currentHp = EnemyCreate.Instance.Ghosts[i].maxHp;
            }
        }

        for (int i = 0; i < EnemyCreate.Instance.DragonFires.Count; i++)
        {
            if ((EnemyCreate.Instance.DragonFires[i].currentHp <= EnemyCreate.Instance.DragonFires[i].maxHp) && EnemyCreate.Instance.DragonFires[i].Eheal)
            {
                EnemyCreate.Instance.DragonFires[i].currentHp += AllMaster.Instance.GhostDamage;
                EnemyCreate.Instance.DragonFires[i].HpSlider.value = EnemyCreate.Instance.DragonFires[i].currentHp;
                if (EnemyCreate.Instance.DragonFires[i].currentHp > EnemyCreate.Instance.DragonFires[i].maxHp)
                    EnemyCreate.Instance.DragonFires[i].currentHp = EnemyCreate.Instance.DragonFires[i].maxHp;
            }
        }
    }

    }

