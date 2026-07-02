using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class EndingCredit : SingletoneBehaviour<EndingCredit>
{
	// Token: 0x06000447 RID: 1095 RVA: 0x0002E344 File Offset: 0x0002C544
	public void SetText()
	{
		this.Contents[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_2", 0, 0, 0);
		this.Contents[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_3", 0, 0, 0);
		this.Contents[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_4", 0, 0, 0);
		this.Contents[13].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_13", 0, 0, 0);
		for (int i = 5; i <= 17; i++)
		{
			this.SetLeft(this.Contents[i], i);
		}
		this.Contents[18].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_17", 0, 0, 0);
		this.Contents[18].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_17", 0, 1, 0);
		this.Contents[19].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_18", 0, 0, 0);
		this.Contents[19].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_18", 0, 1, 0);
		this.Contents[20].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_20", 0, 0, 0);
		this.Contents[20].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_20", 0, 1, 0);
		this.Contents[22].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_19", 0, 0, 0);
		this.Contents[22].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("end_19", 0, 1, 0);
		this.Contents[23].GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString("감사인사", 0, 0, 0);
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0002E608 File Offset: 0x0002C808
	public void SetLeft(GameObject TextObject, int i)
	{
		if (TextObject.transform.childCount != 3)
		{
			return;
		}
		bool flag = i % 2 == 1;
		Transform child = TextObject.transform.GetChild(0);
		Transform child2 = TextObject.transform.GetChild(1);
		Transform child3 = TextObject.transform.GetChild(2);
		string text = "end_" + i.ToString();
		child2.GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString(text, 0, 0, 0);
		child3.GetComponent<TextMeshProUGUI>().text = DBManager.instance.GetSettingString(text, 0, 1, 0);
		Vector2 size = child2.GetComponent<RectTransform>().rect.size;
		size.x = (float)(Screen.width / 2 + 250);
		if (flag)
		{
			child.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
			child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			child2.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
			child2.GetComponent<RectTransform>().sizeDelta = size;
			child2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			child3.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
			child3.GetComponent<RectTransform>().sizeDelta = size;
			child3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 120f);
			return;
		}
		child.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
		child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		child2.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
		child2.GetComponent<RectTransform>().sizeDelta = size;
		child2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		child3.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
		child3.GetComponent<RectTransform>().sizeDelta = size;
		child3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 120f);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00010C27 File Offset: 0x0000EE27
	private void Awake()
	{
		this.BlackCanvas.alpha = 0f;
		this.CreditScroll.gameObject.SetActive(false);
		this.CreditScroll.verticalScrollbar.value = 1f;
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00010C5F File Offset: 0x0000EE5F
	public void PlayEndingCredit()
	{
		base.StartCoroutine("EndingCreditRoutine");
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00010C6D File Offset: 0x0000EE6D
	private IEnumerator EndingCreditRoutine()
	{
		EndingCredit.CreditEnd = false;
		yield return TweenExtensions.WaitForCompletion(this.BlackCanvas.DOFade(1f, 1f));
		this.SetText();
		this.CreditGroup.alpha = 1f;
		yield return new WaitForSeconds(2f);
		this.CreditScroll.gameObject.SetActive(true);
		this.CreditScroll.verticalScrollbar.value = 1f;
		yield return TweenExtensions.WaitForCompletion(DOVirtual.Float(1f, 0f, this.Speed, delegate(float f)
		{
			this.CreditScroll.verticalScrollbar.value = f;
		}).SetEase(Ease.Linear).SetSpeedBased<Tweener>());
		yield return new WaitForSeconds(5f);
		yield return TweenExtensions.WaitForCompletion(this.CreditGroup.DOFade(0f, 1f));
		yield return new WaitUntil(() => !SoundManager.instance.bgmPlayer.isPlaying);
		this.CreditScroll.verticalScrollbar.value = 1f;
		this.CreditScroll.gameObject.SetActive(false);
		EndingCredit.CreditEnd = true;
		yield break;
	}

	// Token: 0x04000494 RID: 1172
	public CanvasGroup BlackCanvas;

	// Token: 0x04000495 RID: 1173
	public CanvasGroup CreditGroup;

	// Token: 0x04000496 RID: 1174
	public float Speed = 1f;

	// Token: 0x04000497 RID: 1175
	public ScrollRectNoDrag CreditScroll;

	// Token: 0x04000498 RID: 1176
	public List<GameObject> Contents;

	// Token: 0x04000499 RID: 1177
	public RectTransform image;

	// Token: 0x0400049A RID: 1178
	public static bool CreditEnd;
}
