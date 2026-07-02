using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F2 RID: 242
public class MyPcWindowResolution : SingletoneBehaviour<MyPcWindowResolution>
{
	// Token: 0x060005FF RID: 1535 RVA: 0x00011D05 File Offset: 0x0000FF05
	private void Start()
	{
		this.curChapter = MyPcWindowResolution.chapter;
		base.StartCoroutine("TurnOnRoutine");
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00011D1E File Offset: 0x0000FF1E
	private IEnumerator TurnOnRoutine()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlackPanel.alpha = 1f;
		DBManager.instance.Start3DParser();
		yield return new WaitForEndOfFrame();
		int num = 0;
		GameManager.Chapter chapter = GameManager.Chapter.None;
		switch (MyPcWindowResolution.chapter)
		{
		case HorrorChapter.Chapter0:
			chapter = GameManager.Chapter.chapter02;
			num = 30;
			break;
		case HorrorChapter.Chapter3:
			chapter = GameManager.Chapter.chapter03;
			num = 40;
			break;
		case HorrorChapter.Chapter4:
			chapter = GameManager.Chapter.chapter03;
			num = 50;
			break;
		}
		BackLog_Event backLog_Event = new BackLog_Event();
		backLog_Event.chapter = chapter;
		backLog_Event.eventID = num;
		backLog_Event.BackLog_Bundle_List = new List<BackLog_Bundle>();
		bool flag = false;
		if (DBManager.backLog_Events.Count != 0 && num == DBManager.backLog_Events[DBManager.backLog_Events.Count - 1].eventID && chapter == DBManager.backLog_Events[DBManager.backLog_Events.Count - 1].chapter)
		{
			DBManager.backLog_Events[DBManager.backLog_Events.Count - 1] = backLog_Event;
			flag = true;
		}
		if (!flag)
		{
			DBManager.backLog_Events.Add(backLog_Event);
		}
		SingletoneBehaviour<CommandLineController>.Instance.ClearConsole();
		SingletoneBehaviour<HorrorVolumeManager>.Instance.StartTurnOnTween();
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlackPanel.DOFade(0f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.SetChapter(MyPcWindowResolution.chapter);
		this.Dot.SetActive(true);
		this.FaceObject.SetActive(true);
		this.GlitchText.SetActive(true);
		DOVirtual.Float(0f, 1f, 1f, delegate(float value)
		{
			this.RenderImage.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)Mathf.Lerp(0f, 255f, value));
		});
		MyPcWindowResolution.VolumeTweenEnd = false;
		yield return new WaitUntil(() => MyPcWindowResolution.VolumeTweenEnd);
		yield break;
	}

	// Token: 0x04000673 RID: 1651
	public static HorrorChapter chapter;

	// Token: 0x04000674 RID: 1652
	public bool startChapter;

	// Token: 0x04000675 RID: 1653
	public HorrorChapter curChapter;

	// Token: 0x04000676 RID: 1654
	public GameObject NoSignal;

	// Token: 0x04000677 RID: 1655
	public RawImage RenderImage;

	// Token: 0x04000678 RID: 1656
	public GameObject Dot;

	// Token: 0x04000679 RID: 1657
	public MeshRenderer FaceImage;

	// Token: 0x0400067A RID: 1658
	public GameObject FaceObject;

	// Token: 0x0400067B RID: 1659
	public GameObject GlitchText;

	// Token: 0x0400067C RID: 1660
	public Transform bubbleParent;

	// Token: 0x0400067D RID: 1661
	public Transform faceBubblePos;

	// Token: 0x0400067E RID: 1662
	public static bool VolumeTweenEnd;
}
