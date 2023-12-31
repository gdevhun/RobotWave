using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    
    public static ExplosionManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject explosionPrefab2;

    private List<GameObject> explosionPool= new List<GameObject>();
    private List<GameObject> explosionPool2= new List<GameObject>();

    void Start()
    {
        for(int i=0; i<30; i++)
		{
            GameObject exgo = Instantiate(explosionPrefab);
            exgo.transform.parent = this.transform;
            exgo.SetActive(false);
            explosionPool.Add(exgo);
		}

        for (int i = 0; i < 100; i++)
        {
            GameObject exgo = Instantiate(explosionPrefab2);
            exgo.transform.parent = this.transform;
            exgo.SetActive(false);
            explosionPool.Add(exgo);
        }
    }

    public GameObject GetAvailableExgo2(Vector3 vec, Quaternion quat)
    {   //�� ȭ�� �ǰ� ���ϸ��̼� ����Ʈ
        for (int i = 0; i < explosionPool2.Count; i++)
        {
            if (!explosionPool2[i].gameObject.activeInHierarchy)
            {
                explosionPool2[i].transform.SetPositionAndRotation(vec, quat);
                explosionPool2[i].SetActive(true);
                StartCoroutine(DisabledExgo(explosionPool2[i]));
                return explosionPool2[i];
            }

        }
        // ��� ������ Ȱ��ȭ�Ǿ� �ִٸ� ���ο� ������ �����Ͽ� ��ȯ
        GameObject exgo = Instantiate(explosionPrefab2, vec, quat);
        exgo.SetActive(true);
        explosionPool2.Add(exgo);
        StartCoroutine(DisabledExgo2(exgo));
        return exgo;
    }

        public GameObject GetAvailableExgo(Vector3 vec, Quaternion quat)
    {   //�� �Ѿ� �ǰ� ���ϸ��̼� ����Ʈ
        for (int i = 0; i < explosionPool.Count; i++)
        {
            if (!explosionPool[i].gameObject.activeInHierarchy)
            {
                explosionPool[i].transform.SetPositionAndRotation(vec, quat);
                explosionPool[i].SetActive(true);
                StartCoroutine(DisabledExgo(explosionPool[i]));
                return explosionPool[i];
            }

        }
        // ��� ������ Ȱ��ȭ�Ǿ� �ִٸ� ���ο� ������ �����Ͽ� ��ȯ
        GameObject exgo = Instantiate(explosionPrefab,vec,quat);
        exgo.SetActive(true);
        explosionPool.Add(exgo);
        StartCoroutine(DisabledExgo(exgo));
        return exgo;
    }
	
    IEnumerator DisabledExgo(GameObject go)
	{
        yield return new WaitForSeconds(1f);
        go.SetActive(false);
    }

    IEnumerator DisabledExgo2(GameObject go)
    {
        yield return new WaitForSeconds(0.2f);
        go.SetActive(false);
    }
}
