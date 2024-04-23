using UnityEngine;
using System.Collections.Generic;


public class Cache
{
    public Cache()
    {
        m_Scripts.Clear();
    }
    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if (!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------


    private static Dictionary<Collider2D, EnemyController> m_Scripts = new Dictionary<Collider2D, EnemyController>();

    public static EnemyController GetScript(Collider2D key)
    {
        if (!m_Scripts.ContainsKey(key))
        {
            //Debug.Log(3);
            EnemyController burger = key.GetComponent<EnemyController>();

            if (burger != null)
            {
                //Debug.Log(1);
                m_Scripts.Add(key, burger);
            }
            else
            {
                return null;
            }
        }
        //Debug.Log(2);
        return m_Scripts[key];
    }
}
