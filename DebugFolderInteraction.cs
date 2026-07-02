using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B4 RID: 180
public class DebugFolderInteraction : MonoBehaviour
{
	// Token: 0x0600046B RID: 1131 RVA: 0x0002F0A0 File Offset: 0x0002D2A0
	private void Start()
	{
		ColorUtility.TryParseHtmlString("#A4BED9", ref this.FolderIcon_OriginColor);
		this.horizontal_Left_origin = this.debugHorizontal.padding.left;
		this.horizontal_Spacing_origin = this.debugHorizontal.spacing;
		this.debug = GameManager.instance.gameData.Debug;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0002F0FC File Offset: 0x0002D2FC
	private void Update()
	{
		if (this.isStudy)
		{
			if (SingletoneBehaviour<IconManager>.Instance.IsWindowActive(Icon.Folder_Debug) && !this.studyAnimCompletion)
			{
				if (!this.consoleON)
				{
					this.consoleON = true;
					int childCount = this.console_Study.transform.childCount;
					this.console_Study.SetActive(true);
					for (int i = 0; i < childCount; i++)
					{
						this.console_Study.transform.GetChild(i).gameObject.SetActive(true);
					}
				}
				if (!this.debug.worldWinionEnabled && this.debug.UIWinionEnabled && this.debug.whichFolder == Winion.Debug)
				{
					this.studyAnimCompletion = true;
					if (this.debug.winionAnimator.canChangeAnimation)
					{
						this.debug.winionAnimator.PlayAnimation("LeftIdle", false);
						return;
					}
				}
			}
			else if (!SingletoneBehaviour<IconManager>.Instance.IsWindowActive(Icon.Folder_Debug) && this.studyAnimCompletion)
			{
				this.studyAnimCompletion = false;
				return;
			}
		}
		else if (this.consoleON)
		{
			this.consoleON = false;
			int childCount2 = this.console_Study.transform.childCount;
			for (int j = 0; j < childCount2; j++)
			{
				this.console_Study.transform.GetChild(j).gameObject.SetActive(false);
			}
			this.console_Study.SetActive(false);
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00010DA1 File Offset: 0x0000EFA1
	public void SetHorizontal(float spacing)
	{
		this.debugHorizontal.spacing = spacing;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00010DAF File Offset: 0x0000EFAF
	public void SetHorizontal_Left(int left)
	{
		this.debugHorizontal.padding.left = left;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00010DC2 File Offset: 0x0000EFC2
	public void ResetHorizontal()
	{
		this.debugHorizontal.padding.left = this.horizontal_Left_origin;
		this.debugHorizontal.spacing = this.horizontal_Spacing_origin;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002F25C File Offset: 0x0002D45C
	public void WinionDebugRoomSetting()
	{
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00)
		{
			if (DBManager.instance.dialogueData.curEventNum >= 0 && DBManager.instance.dialogueData.curEventNum <= 3)
			{
				SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Debug, false);
				this.FolderIcon.color = Color.white;
				this.Folder_Text.text = "-";
				this.WinionIcon.SetActive(false);
			}
			else if (DBManager.instance.dialogueData.curEventNum == 4 && this.FolderIcon.color == Color.white)
			{
				this.ResetFolder_Icon();
			}
		}
		if (this.light02.activeSelf)
		{
			this.light02.SetActive(false);
		}
		if (this.debugHorizontal.padding.left != this.horizontal_Left_origin)
		{
			this.debugHorizontal.padding.left = this.horizontal_Left_origin;
			this.debugHorizontal.spacing = this.horizontal_Spacing_origin;
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02)
		{
			if (DBManager.instance.dialogueData.curEventNum == 12)
			{
				this.debugHorizontal.padding.left = 120;
				this.debugHorizontal.spacing = 110f;
				this.fix_passOut.gameObject.SetActive(true);
				this.console.SetActive(false);
			}
			if (DBManager.instance.dialogueData.curEventNum == 13)
			{
				this.fix_passOut.gameObject.SetActive(false);
				this.console.SetActive(false);
			}
			if (DBManager.instance.dialogueData.curEventNum == 14)
			{
				this.debugHorizontal.padding.left = 120;
				this.debugHorizontal.spacing = 110f;
				this.fix_passOut.gameObject.SetActive(true);
				this.console.SetActive(true);
			}
			if (DBManager.instance.dialogueData.curEventNum >= 15 && DBManager.instance.dialogueData.curEventNum < 18)
			{
				this.fix_passOut.gameObject.SetActive(false);
				this.console.SetActive(false);
			}
			if (DBManager.instance.dialogueData.curEventNum == 18)
			{
				this.debugHorizontal.padding.left = 120;
				this.debugHorizontal.spacing = 110f;
				this.fix_passOut.gameObject.SetActive(true);
				this.console.SetActive(true);
			}
			if (DBManager.instance.dialogueData.curEventNum >= 19 && DBManager.instance.dialogueData.curEventNum < 23)
			{
				this.fix_passOut.gameObject.SetActive(false);
				this.console.SetActive(false);
			}
			if (DBManager.instance.dialogueData.curEventNum == 23)
			{
				this.debugHorizontal.padding.left = 120;
				this.debugHorizontal.spacing = 110f;
				this.fix_passOut.gameObject.SetActive(true);
				this.console.SetActive(true);
			}
			if (DBManager.instance.dialogueData.curEventNum == 24 || DBManager.instance.dialogueData.curEventNum == 25)
			{
				this.fix_passOut.gameObject.SetActive(false);
				this.console.SetActive(false);
			}
			if (DBManager.instance.dialogueData.curEventNum == 26)
			{
				this.debugHorizontal.padding.left = 120;
				this.debugHorizontal.spacing = 110f;
				this.fix_passOut.gameObject.SetActive(true);
				this.console.SetActive(true);
			}
			if (DBManager.instance.dialogueData.curEventNum == 27)
			{
				this.ResetHorizontal();
				this.fix_passOut.gameObject.SetActive(false);
				this.console.SetActive(false);
			}
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03)
		{
			this.fix_passOut.gameObject.SetActive(false);
			this.light02.SetActive(true);
			if (DBManager.instance.dialogueData.curEventNum >= 12)
			{
				this.death_Winion.gameObject.SetActive(false);
				this.flower.SetActive(true);
			}
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0002F6A4 File Offset: 0x0002D8A4
	public void ResetFolder_Icon()
	{
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Debug, true);
		this.FolderIcon.color = this.FolderIcon_OriginColor;
		this.Folder_Text.text = DBManager.instance.GetSettingString("Debug", 0, 0, 0);
		this.WinionIcon.SetActive(true);
	}

	// Token: 0x040004BF RID: 1215
	private WinionHandler debug;

	// Token: 0x040004C0 RID: 1216
	public GameObject light;

	// Token: 0x040004C1 RID: 1217
	public GameObject addLight;

	// Token: 0x040004C2 RID: 1218
	public GameObject light02;

	// Token: 0x040004C3 RID: 1219
	public GameObject fix_passOut;

	// Token: 0x040004C4 RID: 1220
	public GameObject console;

	// Token: 0x040004C5 RID: 1221
	public GameObject Blood;

	// Token: 0x040004C6 RID: 1222
	public GameObject Blood01;

	// Token: 0x040004C7 RID: 1223
	public GameObject Blood02;

	// Token: 0x040004C8 RID: 1224
	public GameObject death_Winion;

	// Token: 0x040004C9 RID: 1225
	public HorizontalLayoutGroup debugHorizontal;

	// Token: 0x040004CA RID: 1226
	private int horizontal_Left_origin;

	// Token: 0x040004CB RID: 1227
	private float horizontal_Spacing_origin;

	// Token: 0x040004CC RID: 1228
	public GameObject console_Study;

	// Token: 0x040004CD RID: 1229
	public bool consoleON;

	// Token: 0x040004CE RID: 1230
	public bool isStudy;

	// Token: 0x040004CF RID: 1231
	public bool studyAnimCompletion;

	// Token: 0x040004D0 RID: 1232
	public Color FolderIcon_OriginColor;

	// Token: 0x040004D1 RID: 1233
	public Image FolderIcon;

	// Token: 0x040004D2 RID: 1234
	public TMP_Text Folder_Text;

	// Token: 0x040004D3 RID: 1235
	public GameObject WinionIcon;

	// Token: 0x040004D4 RID: 1236
	public GameObject flower;
}
