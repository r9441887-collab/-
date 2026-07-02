using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class LoadingBar : SingletoneBehaviour<LoadingBar>
{
	// Token: 0x06001BD8 RID: 7128 RVA: 0x000CF8B8 File Offset: 0x000CDAB8
	private void Awake()
	{
		this.children.Clear();
		int childCount = this.processBar.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.processBar.GetChild(i).gameObject;
			gameObject.SetActive(false);
			this.children.Add(gameObject);
		}
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000CF910 File Offset: 0x000CDB10
	public void PlayLoading(bool LoadingAutoEnd = true, float _speed = 1f)
	{
		if (this.playingLoading)
		{
			return;
		}
		this.playingLoading = true;
		this.loadingSpeed = _speed;
		this.LoadingActionEnd = LoadingAutoEnd;
		this.dotCount = 1;
		this.LoadingWindow.SetActive(true);
		this.dotRoutine = base.StartCoroutine("LoadingTextRoutine");
		this.barRoutine = base.StartCoroutine("LoadingProcessRoutine");
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x0001A197 File Offset: 0x00018397
	public void EndMemoryRoutine()
	{
		this.LoadingActionEnd = false;
		this.loadingSpeed = 10f;
		this.waitIndex = 20;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000CF970 File Offset: 0x000CDB70
	public void PlayLoadingWithInterrupt(float _speed = 1f)
	{
		if (this.playingLoading)
		{
			return;
		}
		this.interrupt = true;
		this.playingLoading = true;
		this.loadingSpeed = _speed;
		this.LoadingActionEnd = true;
		this.dotCount = 1;
		this.LoadingWindow.SetActive(true);
		this.dotRoutine = base.StartCoroutine("LoadingTextRoutine");
		this.barRoutine = base.StartCoroutine("LoadingProcessRoutine");
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x0001A1B3 File Offset: 0x000183B3
	public void SetPosition(Vector2 position_)
	{
		base.GetComponent<RectTransform>().localPosition = position_;
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x0001A1C6 File Offset: 0x000183C6
	private IEnumerator LoadingTextRoutine()
	{
		this.LoadingText.text = "Loading";
		while (base.gameObject.activeSelf)
		{
			int num = this.dotCount;
			this.dotCount = num + 1;
			if (num >= 3)
			{
				this.LoadingText.text = "Loading";
				this.dotCount = 0;
			}
			yield return new WaitForSeconds(0.4f);
			TextMeshProUGUI loadingText = this.LoadingText;
			loadingText.text += ".";
		}
		yield break;
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x0001A1D5 File Offset: 0x000183D5
	private IEnumerator LoadingProcessRoutine()
	{
		this.ReadyLoadingEnd = false;
		int childs = this.processBar.childCount;
		this.waitIndex = Random.Range(5, childs - 3);
		Action action = this.loadingAction;
		if (action != null)
		{
			action();
		}
		int num2;
		for (int i = 0; i < childs; i = num2 + 1)
		{
			if (this.interrupt && i == 3)
			{
				Action action2 = this.interruptAction;
				if (action2 != null)
				{
					action2();
				}
				this.interrupt = false;
				this.interruptAction = null;
			}
			if (!this.LoadingActionEnd && i == this.waitIndex)
			{
				this.ReadyLoadingEnd = true;
				yield return new WaitUntil(() => this.LoadingActionEnd);
			}
			this.children[i].SetActive(true);
			float num = Random.Range(0.01f, 0.3f) / this.loadingSpeed;
			yield return new WaitForSeconds(num);
			num2 = i;
		}
		yield return new WaitForSeconds(0.2f);
		this.EndLoading_SetDefault();
		yield break;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x0001A1E4 File Offset: 0x000183E4
	public void LoadingEnd()
	{
		this.LoadingActionEnd = true;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x0001A1ED File Offset: 0x000183ED
	public bool GetLoadingEnd()
	{
		return !this.playingLoading;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000CF9D8 File Offset: 0x000CDBD8
	private void EndLoading_SetDefault()
	{
		int childCount = this.processBar.childCount;
		base.transform.GetChild(0).gameObject.SetActive(false);
		for (int i = 0; i < childCount; i++)
		{
			this.children[i].SetActive(false);
		}
		this.loadingSpeed = 1f;
		base.StopCoroutine(this.dotRoutine);
		base.StopCoroutine(this.barRoutine);
		this.position = Vector2.one;
		this.playingLoading = false;
		this.ReadyLoadingEnd = true;
	}

	// Token: 0x04001935 RID: 6453
	public GameObject LoadingWindow;

	// Token: 0x04001936 RID: 6454
	public TextMeshProUGUI LoadingText;

	// Token: 0x04001937 RID: 6455
	public Transform processBar;

	// Token: 0x04001938 RID: 6456
	public float loadingSpeed = 1f;

	// Token: 0x04001939 RID: 6457
	public bool LoadingActionEnd;

	// Token: 0x0400193A RID: 6458
	private int dotCount = 1;

	// Token: 0x0400193B RID: 6459
	private bool playingLoading;

	// Token: 0x0400193C RID: 6460
	private Coroutine dotRoutine;

	// Token: 0x0400193D RID: 6461
	private Coroutine barRoutine;

	// Token: 0x0400193E RID: 6462
	private List<GameObject> children = new List<GameObject>();

	// Token: 0x0400193F RID: 6463
	public Action loadingAction;

	// Token: 0x04001940 RID: 6464
	private Vector2 position = Vector2.one;

	// Token: 0x04001941 RID: 6465
	public bool interrupt;

	// Token: 0x04001942 RID: 6466
	public Action interruptAction;

	// Token: 0x04001943 RID: 6467
	public int waitIndex;

	// Token: 0x04001944 RID: 6468
	public bool ReadyLoadingEnd;
}
