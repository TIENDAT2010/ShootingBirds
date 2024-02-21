using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fireRate;
    private float m_curfireRate;

    public GameObject viewFinder;
    GameObject m_viewFinderClone;

    private bool m_isShooting;

    private void Awake()
    {
        m_curfireRate = fireRate;
    }

    private void Start()
    {
        if (viewFinder)
        {
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }    
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !m_isShooting)
        {
            Shoot(mousePos);
        }    

        if (m_isShooting)
        {
            m_curfireRate -= Time.deltaTime;

            if (m_curfireRate <= 0)
            {
                m_isShooting = false;

                m_curfireRate = fireRate;
            }

            GameGUIManager.Ins.UpdateFireRate(m_curfireRate / fireRate);
        }


        if (m_viewFinderClone)
        {
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }    
    }
    private void Shoot(Vector3 mousePos)
    {
        m_isShooting = true;

        Vector3 shootDir = Camera.main.transform.position - mousePos;

        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position,(Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if(bird)
                    {
                        bird.Die();
                    }    
                }    
            }
        }

        CineController.Ins.ShakeTrigger();

        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }    
}
