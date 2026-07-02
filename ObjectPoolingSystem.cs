using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B6 RID: 950
[Serializable]
public class ObjectPoolingSystem
{
	// Token: 0x06001C1F RID: 7199 RVA: 0x0001A4C2 File Offset: 0x000186C2
	public void InitPooling()
	{
		this.gameObjectPrefabs = new Dictionary<string, GameObject>();
		this.gameObjectPools = new Dictionary<string, List<GameObject>>();
		this.gameObjectPrefabs.Clear();
		this.gameObjectPools.Clear();
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x000D0328 File Offset: 0x000CE528
	public GameObject GetGameObjectPrefab(string gameObjectName, string resourcePos, Transform parent = null, bool isEffect = false)
	{
		GameObject curGameObject = null;
		if (this.gameObjectPrefabs.ContainsKey(gameObjectName))
		{
			curGameObject = this.gameObjectPrefabs[gameObjectName];
		}
		else
		{
			curGameObject = Resources.Load<GameObject>(resourcePos + gameObjectName);
			if (curGameObject != null)
			{
				this.gameObjectPrefabs.Add(gameObjectName, curGameObject);
			}
		}
		if (curGameObject != null)
		{
			if (this.gameObjectPools.ContainsKey(gameObjectName))
			{
				if (this.gameObjectPools[gameObjectName].Count > 0)
				{
					curGameObject = this.gameObjectPools[gameObjectName][0];
					this.gameObjectPools[gameObjectName].RemoveAt(0);
				}
				else
				{
					curGameObject = Object.Instantiate<GameObject>(curGameObject);
				}
			}
			else
			{
				this.gameObjectPools.Add(gameObjectName, new List<GameObject>());
				curGameObject = Object.Instantiate<GameObject>(curGameObject);
			}
			Transform objParent = null;
			if (DBManager.instance.is3DScene)
			{
				objParent = ((parent == null) ? DBManager.instance.transform : parent);
				curGameObject.gameObject.transform.SetParent(objParent);
			}
			else
			{
				objParent = ((parent == null) ? GameManager.instance.transform : parent);
				curGameObject.gameObject.transform.SetParent(objParent);
			}
			if (isEffect)
			{
				Effect component = curGameObject.GetComponent<Effect>();
				component.ShowEffect();
				component.callBack = delegate
				{
					this.AddGameObjectPool(gameObjectName, curGameObject, objParent);
				};
			}
		}
		return curGameObject;
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x000D0514 File Offset: 0x000CE714
	public void AddGameObjectPool(string gameObjectName, GameObject gameObj, Transform parent = null)
	{
		if (this.gameObjectPools[gameObjectName].Count >= this.gameObjectPoolCount)
		{
			for (int i = 0; i < this.gameObjectPools[gameObjectName].Count; i++)
			{
			}
			Object.Destroy(gameObj);
			return;
		}
		if (DBManager.instance.is3DScene)
		{
			Transform transform = ((parent == null) ? DBManager.instance.transform : parent);
			gameObj.transform.SetParent(transform);
			this.gameObjectPools[gameObjectName].Add(gameObj);
			gameObj.SetActive(false);
			return;
		}
		Transform transform2 = ((parent == null) ? GameManager.instance.transform : parent);
		gameObj.transform.SetParent(transform2);
		this.gameObjectPools[gameObjectName].Add(gameObj);
		gameObj.SetActive(false);
	}

	// Token: 0x04001981 RID: 6529
	public int gameObjectPoolCount = 15;

	// Token: 0x04001982 RID: 6530
	public Dictionary<string, GameObject> gameObjectPrefabs;

	// Token: 0x04001983 RID: 6531
	public Dictionary<string, List<GameObject>> gameObjectPools;
}
