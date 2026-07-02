using System;
using Coffee.UIEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000111 RID: 273
public class WinionFace : MonoBehaviour
{
	// Token: 0x0600068E RID: 1678 RVA: 0x0003B934 File Offset: 0x00039B34
	private void Start()
	{
		if (base.gameObject.name == "Fix Face")
		{
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01 && DBManager.instance.dialogueData.curEventNum >= 7)
			{
				if (!DBManager.instance.winionFaceInfo.fixReSetAnim)
				{
					this.faceImage.sprite = DBManager.instance.winionFaceInfo.Fix_Setting_Sprite("[픽스-무표정]");
					GameManager.instance.gameData.Fix.winionAnimator.FaceName = "[픽스-무표정]";
					this.SetUIGradient("#8C3939", "#390000");
					return;
				}
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum < 13 && !DBManager.instance.winionFaceInfo.fixReSetAnim)
			{
				this.faceImage.sprite = DBManager.instance.winionFaceInfo.Fix_Setting_Sprite("[픽스-무표정]");
				GameManager.instance.gameData.Fix.winionAnimator.FaceName = "[픽스-무표정]";
				this.SetUIGradient("#8C3939", "#390000");
			}
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0003BA64 File Offset: 0x00039C64
	public void SetUIGradient(string color01, string color02)
	{
		Color color3;
		if (color01 != this.preColor_1 && ColorUtility.TryParseHtmlString(color01, ref color3))
		{
			this.preColor_1 = color01;
			this.UIGradient.color1 = color3;
		}
		if (color02 != this.preColor_2 && ColorUtility.TryParseHtmlString(color02, ref color3))
		{
			this.preColor_2 = color02;
			this.UIGradient.color2 = color3;
		}
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0001241F File Offset: 0x0001061F
	public void SetFace(Sprite sprite)
	{
		this.FaceTextCheckION();
		this.FaceTextCheckBo();
		this.faceImage.sprite = sprite;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0003BAC8 File Offset: 0x00039CC8
	public void FaceTextCheckION()
	{
		if (base.gameObject.name == "I-on Face")
		{
			if (this.bizitFace == "")
			{
				this.originText = DBManager.instance.GetSettingString("Ion", 0, 2, 0);
				this.bizitFace = DBManager.instance.GetSettingString("Bizit", 0, 1, 0);
				this.IONFace = DBManager.instance.GetSettingString("Ion", 0, 2, 0);
			}
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && this.originText == this.IONFace && GameManager.instance.gameData.ION.winionStatus.isBizit && this.faceText.text != this.bizitFace)
			{
				Color color;
				if (ColorUtility.TryParseHtmlString("#8CAFFE", ref color))
				{
					this.titleUIGadient.color1 = color;
				}
				this.faceText.text = this.bizitFace;
			}
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0003BBCC File Offset: 0x00039DCC
	public void FaceTextCheckBo()
	{
		if (base.gameObject.name == "Bo Face")
		{
			if (this.WatchWinionFace == "")
			{
				this.originText = DBManager.instance.GetSettingString("Bo", 0, 2, 0);
				this.BoFace = DBManager.instance.GetSettingString("Bo", 0, 2, 0);
				this.WatchWinionFace = DBManager.instance.GetSettingString("감시위니언", 0, 0, 0);
			}
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && this.originText == this.BoFace)
			{
				if (GameManager.instance.gameData.Bo.winionStatus.isWatchWinion)
				{
					if (this.faceText.text != this.WatchWinionFace)
					{
						Color color;
						if (ColorUtility.TryParseHtmlString("#000000", ref color))
						{
							this.titleUIGadient.color1 = color;
						}
						if (ColorUtility.TryParseHtmlString("#322222", ref color))
						{
							this.Bo_titleIcon.GetComponent<Image>().color = color;
						}
						this.faceText.text = this.WatchWinionFace;
						return;
					}
				}
				else if (this.faceText.text == this.WatchWinionFace)
				{
					Color color2;
					if (ColorUtility.TryParseHtmlString("#FECEFD", ref color2))
					{
						this.titleUIGadient.color1 = color2;
					}
					if (ColorUtility.TryParseHtmlString("#5D5D5D", ref color2))
					{
						this.Bo_titleIcon.GetComponent<Image>().color = color2;
					}
					this.faceText.text = this.BoFace;
				}
			}
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0003BD5C File Offset: 0x00039F5C
	public void SettingUnderGrident(string text = "")
	{
		Color color;
		if (ColorUtility.TryParseHtmlString(text, ref color))
		{
			this.titleUIGadient.color2 = color;
		}
	}

	// Token: 0x04000748 RID: 1864
	public TMP_Text faceText;

	// Token: 0x04000749 RID: 1865
	public string originText;

	// Token: 0x0400074A RID: 1866
	public Image faceImage;

	// Token: 0x0400074B RID: 1867
	public Animator animator;

	// Token: 0x0400074C RID: 1868
	public UIGradient UIGradient;

	// Token: 0x0400074D RID: 1869
	private string preColor_1 = "";

	// Token: 0x0400074E RID: 1870
	private string preColor_2 = "";

	// Token: 0x0400074F RID: 1871
	public UIGradient titleUIGadient;

	// Token: 0x04000750 RID: 1872
	public string bizitFace = "";

	// Token: 0x04000751 RID: 1873
	public string IONFace = "";

	// Token: 0x04000752 RID: 1874
	public string BoFace = "";

	// Token: 0x04000753 RID: 1875
	public string WatchWinionFace = "";

	// Token: 0x04000754 RID: 1876
	public GameObject Bo_titleIcon;
}
