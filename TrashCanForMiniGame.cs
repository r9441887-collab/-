using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000169 RID: 361
public class TrashCanForMiniGame : SingletoneBehaviour<TrashCanForMiniGame>
{
	// Token: 0x06000861 RID: 2145 RVA: 0x000431C0 File Offset: 0x000413C0
	public void AddTrash(GameObject trash)
	{
		if (!this.trashCan.activeSelf)
		{
			this.TranscanRect.SetParent(SingletoneBehaviour<SpawnNewWindow>.Instance.GetRandomTransform());
			this.lastParent = this.TranscanRect.parent.GetComponent<RectTransform>();
			this.TranscanRect.localPosition = Vector3.zero;
			this.TranscanRect.SetParent(this.DragableParent);
			this.trashCan.transform.localScale = Vector3.one;
			this.trashCan.SetActive(true);
		}
		this.trashList.RemoveAll((GameObject obj) => obj == null);
		this.trashList.Add(trash);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0001380F File Offset: 0x00011A0F
	public void SetBackgroundImage(bool value)
	{
		this.backgroundImage.enabled = value;
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00043280 File Offset: 0x00041480
	public void RemoveFile(RectTransform _target = null, bool value = true)
	{
		if (_target == null && this.targetRect != null)
		{
			_target = this.targetRect;
		}
		this.SetBackgroundImage(false);
		if (_target != null)
		{
			this.trashList.Remove(_target.gameObject);
		}
		this.trashList.RemoveAll((GameObject obj) => obj == null);
		if (this.trashList.Count == 0)
		{
			this.trashCan.SetActive(false);
		}
		if (_target != null)
		{
			_target.GetComponent<PurseByBeat>().RemoveSelf(value);
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00043324 File Offset: 0x00041524
	private void Update()
	{
		if (this.targetRect != null)
		{
			if (Vector3.Distance(this.targetRect.localPosition, SingletoneBehaviour<TrashCanForMiniGame>.Instance.TranscanRect.localPosition) < 130f)
			{
				this.SetBackgroundImage(true);
				if (this._checkRemove)
				{
					this.RemoveFile(this.targetRect, true);
					this._checkRemove = false;
					return;
				}
			}
			else
			{
				this.SetBackgroundImage(false);
				if (this._checkRemove)
				{
					this.targetRect.GetComponent<DropTo>().SetOriginPosition();
					this._checkRemove = false;
					this.targetRect = null;
					return;
				}
			}
		}
		else
		{
			this.SetBackgroundImage(false);
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0001381D File Offset: 0x00011A1D
	public void CheckRemove()
	{
		this._checkRemove = true;
	}

	// Token: 0x0400094A RID: 2378
	public GameObject trashCan;

	// Token: 0x0400094B RID: 2379
	public List<GameObject> trashList = new List<GameObject>();

	// Token: 0x0400094C RID: 2380
	public RectTransform TranscanRect;

	// Token: 0x0400094D RID: 2381
	public Image backgroundImage;

	// Token: 0x0400094E RID: 2382
	public RectTransform DragableParent;

	// Token: 0x0400094F RID: 2383
	public RectTransform lastParent;

	// Token: 0x04000950 RID: 2384
	public RectTransform targetRect;

	// Token: 0x04000951 RID: 2385
	private bool _checkRemove;
}
