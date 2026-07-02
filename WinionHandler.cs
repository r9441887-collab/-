using System;
using System.Collections;
using DG.Tweening;
using project.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011A RID: 282
public class WinionHandler : MonoBehaviour
{
	// Token: 0x060006B7 RID: 1719 RVA: 0x0003C254 File Offset: 0x0003A454
	public void SetSortOrder(bool active, int order = 0)
	{
		if (this.winionSort == null)
		{
			SpriteRenderer spriteRenderer = this.winionAnimator.spriteRenderer;
			this.winionSort = ((spriteRenderer != null) ? spriteRenderer.GetComponent<WinionSpriteSort>() : null);
		}
		if (this.winionSort != null)
		{
			this.winionSort.enabled = !active;
			if (this.winionAnimator.spriteRenderer != null)
			{
				this.winionAnimator.spriteRenderer.sortingOrder = order;
			}
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
	public void Awake()
	{
		this.whichFolder = Winion.None;
		this.SetHandler<WinionAnimator>(ref this.winionAnimator);
		this.SetHandler<WinionMouseEvent>(ref this.winionMouseEvent);
		this.SetHandler<WinionDragAndDrop>(ref this.winionDragAndDrop);
		this.SetHandler<WinionInterruptAction>(ref this.winionInterruptAction);
		this.SetHandler<WinionMovement>(ref this.winionMovement);
		this.SetHandler<WinionStatus>(ref this.winionStatus);
		this.SetHandler<WinionFeed>(ref this.winionFeed);
		this.SetHandler<WinionBehaviour>(ref this.winionBehaviour);
		this.SetHandler<WinionLookAt>(ref this.winionLookAt);
		this.SetActiveWorldWinion(true);
		this.SetActiveUIWinion(false);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x000125F1 File Offset: 0x000107F1
	public void SetHandler<T>(ref T scriptType) where T : IHandler
	{
		if (scriptType == null)
		{
			scriptType = base.GetComponent<T>();
		}
		scriptType.winionHandler = this;
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00012619 File Offset: 0x00010819
	public void ChangeCharacterState(CharacterState state)
	{
		this.Log("ChangeCharacterState : " + state.ToString(), false);
		this.characterState = state;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0003C360 File Offset: 0x0003A560
	public void SetIdleByWinionStatus()
	{
		if (!this.winionLookAt.IsWinionLookTarget())
		{
			if (DBManager.instance.dialogueData.curDialogue_ing || DBManager.instance.dialogueData.runNextEvent)
			{
				if (WinionHandler.SetAutoCheckIdle && this.SetAutoChackIdle_Personal)
				{
					base.StartCoroutine("CheckCanIdle");
					return;
				}
			}
			else
			{
				if (this.winionStatus.winionInfo.battery < 20)
				{
					this.winionAnimator.PlayAnimation("Down", false);
					this.winionMovement.SetMoveSpeed(MoveSpeed.Slow, false);
					this.ChangeCharacterState(CharacterState.FrontIdle);
					return;
				}
				if (this.winionStatus.winionInfo.memory > 80)
				{
					this.winionAnimator.PlayAnimation("Angry", false);
					this.winionMovement.SetMoveSpeed(MoveSpeed.Fast, false);
					this.ChangeCharacterState(CharacterState.FrontIdle);
					return;
				}
				if (!this.winionLookAt.IsWinionLookTarget() && this.characterState != CharacterState.Charging)
				{
					this.winionAnimator.PlayAnimation("FrontIdle", false);
					this.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
					this.ChangeCharacterState(CharacterState.FrontIdle);
				}
			}
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00012640 File Offset: 0x00010840
	private IEnumerator CheckCanIdle()
	{
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
		{
			yield return null;
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			if (!this.winionLookAt.IsWinionLookTarget() && this.CanInterruptState(2))
			{
				this.winionAnimator.PlayAnimation("FrontIdle", false);
				this.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
				this.ChangeCharacterState(CharacterState.FrontIdle);
			}
		}
		else if (!this.winionLookAt.IsWinionLookTarget() && this.characterState != CharacterState.Charging)
		{
			this.winionAnimator.PlayAnimation("FrontIdle", false);
			this.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
			this.ChangeCharacterState(CharacterState.FrontIdle);
		}
		yield break;
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0001264F File Offset: 0x0001084F
	public bool CanChangeAnimation()
	{
		return this.characterState != CharacterState.Feed;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0003C474 File Offset: 0x0003A674
	public bool CanInterruptState(int level = 0)
	{
		bool flag = true;
		switch (level)
		{
		case 0:
			flag = flag && this.characterState != CharacterState.Feed && this.characterState != CharacterState.PickUp && this.characterState != CharacterState.Sleeping && this.characterState != CharacterState.ShakeHand && this.characterState != CharacterState.Pooping && this.characterState != CharacterState.Hanging && this.characterState != CharacterState.Charging;
			break;
		case 1:
			flag = flag && this.characterState != CharacterState.PickUp && this.characterState != CharacterState.Sleeping && this.characterState != CharacterState.ShakeHand && this.characterState != CharacterState.Pooping && this.characterState != CharacterState.Hanging && this.characterState != CharacterState.Charging;
			break;
		case 2:
			flag = flag && this.characterState < CharacterState.Hanging;
			break;
		}
		return flag;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0001265D File Offset: 0x0001085D
	public void SetActiveFalseWinion()
	{
		this.SetActiveWorldWinion(false);
		this.SetActiveUIWinion(false);
		this.ReturnDialogueEmotion(true);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0003C544 File Offset: 0x0003A744
	public void SetActiveWorldWinion(bool value)
	{
		this.winionMovement.SetActiveMovement(value, false, true);
		this.winionAnimator.spriteRenderer.enabled = value;
		this.worldWinionEnabled = value;
		BoxCollider2D component = base.GetComponent<BoxCollider2D>();
		if (component != null)
		{
			component.enabled = value;
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00012674 File Offset: 0x00010874
	public void SetActiveUIWinion(bool value)
	{
		if (this.winionAnimator.useBoth)
		{
			this.winionAnimator.image.enabled = value;
		}
		this.UIWinionEnabled = value;
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0003C590 File Offset: 0x0003A790
	public void HaveDialogueEmotion(bool isSmallTalk = false)
	{
		bool flag = false;
		if (!this.worldWinionEnabled && this.UIWinionEnabled && this.uiWinionSpeechBubblePos == null)
		{
			UIWinion _uiWinion = this.uiWinion.GetComponent<UIWinion>();
			_uiWinion.OnEnableAction = delegate
			{
				if (!this.blockDialogue)
				{
					this.HaveDialogueEmotion(isSmallTalk);
				}
				_uiWinion.OnEnableAction = null;
			};
			flag = true;
			if (this.uiWinion.activeSelf && !isSmallTalk && this.whichFolder < Winion.BatteryCenter)
			{
				ScreenCanvas.Instance.Switch_WinionRoomBubble(true, this.whichFolder);
			}
		}
		if (!flag && !this.blockDialogue)
		{
			string text;
			if (!isSmallTalk)
			{
				text = "{느낌표}";
			}
			else
			{
				text = "{나 할말있어}";
			}
			if (text == "{느낌표}")
			{
				if (this.emotionHaveEvent_obj != null && this.haveEventEmotion)
				{
					return;
				}
				if (this.emotionSmallTalk_obj != null && this.haveSmallTalkEmotion)
				{
					this.ReturnDialogueEmotion(true);
				}
				this.haveEventEmotion = true;
				string text2 = text;
				this.emotion = DBManager.instance.ReplaceIdToEmotionBubble(text2);
				DBManager.instance.dialogueController.Animation_ByEmotion(text2, this);
				this.emotionHaveEvent_obj = DBManager.instance.Get_Emotion_SpeechBubble(this.emotion, this);
				this.emotionHaveEvent_obj.gameObject.SetActive(true);
				SpeechBubbleInfo component = this.emotionHaveEvent_obj.GetComponent<SpeechBubbleInfo>();
				Transform speechBubblePos;
				if (!this.worldWinionEnabled && this.UIWinionEnabled)
				{
					speechBubblePos = this.uiWinionSpeechBubblePos;
					if (this.whichFolder < Winion.BatteryCenter)
					{
						ScreenCanvas.Instance.Switch_WinionRoomBubble(true, this.whichFolder);
					}
				}
				else
				{
					speechBubblePos = this.winionStatus.speechBubblePos;
				}
				component.StartSpeechBubble(speechBubblePos);
				if (this.uiwinionColorChange && !this.worldWinionEnabled && this.UIWinionEnabled)
				{
					int winionType = (int)this.winionStatus.winionInfo.winionType;
					UIWinion component2 = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType].GetComponent<UIWinion>();
					Color color = component2.winionImg.color;
					if (this.changeUIGradient)
					{
						color = component2.winionGradient.color1;
					}
					this.emotionHaveEvent_obj.GetComponent<Image>().color = color;
					return;
				}
			}
			else
			{
				if (this.emotionSmallTalk_obj != null && this.haveSmallTalkEmotion)
				{
					return;
				}
				this.haveSmallTalkEmotion = true;
				string text3 = text;
				this.smallTalkEmotion = DBManager.instance.ReplaceIdToEmotionBubble(text3);
				DBManager.instance.dialogueController.Animation_ByEmotion(text3, this);
				this.emotionSmallTalk_obj = DBManager.instance.Get_Emotion_SpeechBubble(this.smallTalkEmotion, this);
				this.emotionSmallTalk_obj.gameObject.SetActive(true);
				SpeechBubbleInfo component3 = this.emotionSmallTalk_obj.GetComponent<SpeechBubbleInfo>();
				Transform speechBubblePos2;
				if (!this.worldWinionEnabled && this.UIWinionEnabled)
				{
					speechBubblePos2 = this.uiWinionSpeechBubblePos;
				}
				else
				{
					speechBubblePos2 = this.winionStatus.speechBubblePos;
				}
				component3.StartSpeechBubble(speechBubblePos2);
				if (this.uiwinionColorChange && !this.worldWinionEnabled && this.UIWinionEnabled)
				{
					int winionType2 = (int)this.winionStatus.winionInfo.winionType;
					UIWinion component4 = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType2].GetComponent<UIWinion>();
					Color color2 = component4.winionImg.color;
					if (this.changeUIGradient)
					{
						color2 = component4.winionGradient.color1;
					}
					this.emotionSmallTalk_obj.GetComponent<Image>().color = color2;
				}
			}
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0003C8EC File Offset: 0x0003AAEC
	public void ReturnDialogueEmotion(bool isSmallTalk = false)
	{
		string text;
		if (!isSmallTalk)
		{
			text = "{느낌표}";
		}
		else
		{
			text = "{나 할말있어}";
		}
		if (text == "{느낌표}")
		{
			if (this.haveEventEmotion)
			{
				this.emotionHaveEvent_obj.GetComponent<Image>().color = Color.white;
				this.emotionHaveEvent_obj.GetComponent<SpeechBubbleInfo>().StopSpeechBubble();
				DBManager.instance.Return_Emotion_SpeechBubble(this.emotionHaveEvent_obj, this.emotion);
				ScreenCanvas.Instance.Switch_WinionRoomBubble(false, this.whichFolder);
				this.haveEventEmotion = false;
				this.emotionHaveEvent_obj = null;
				this.emotion = DBManager.EmotionSpeechBubble.None;
				return;
			}
		}
		else if (this.haveSmallTalkEmotion)
		{
			this.emotionSmallTalk_obj.GetComponent<Image>().color = Color.white;
			this.emotionSmallTalk_obj.GetComponent<SpeechBubbleInfo>().StopSpeechBubble();
			DBManager.instance.Return_Emotion_SpeechBubble(this.emotionSmallTalk_obj, this.smallTalkEmotion);
			this.haveSmallTalkEmotion = false;
			this.emotionSmallTalk_obj = null;
			this.smallTalkEmotion = DBManager.EmotionSpeechBubble.None;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0003C9E4 File Offset: 0x0003ABE4
	public void SetOutline(bool value)
	{
		GameObject gameObject = this.winionAnimator.spriteRenderer.gameObject;
		if (value)
		{
			this.winionAnimator.spriteRenderer.material.SetFloat("_OutlineEnabled", 1f);
			gameObject.transform.localScale = new Vector3(1.006f, 1.06f, 1f);
			return;
		}
		this.winionAnimator.spriteRenderer.material.SetFloat("_OutlineEnabled", 0f);
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0003CA74 File Offset: 0x0003AC74
	public void Adjust_AlphaValue(float target, float duration = 0f)
	{
		float a = this.winionAnimator.spriteRenderer.color.a;
		Color curColor = this.winionAnimator.spriteRenderer.color;
		DOVirtual.Float(a, target, duration, delegate(float alphaValue)
		{
			curColor.a = alphaValue;
			this.winionAnimator.spriteRenderer.color = curColor;
		}).OnComplete(delegate
		{
			this.finishAlphaValue = true;
		});
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
	public void SetScale(float _scale)
	{
		Vector3 vector;
		vector..ctor(_scale, _scale, _scale);
		base.transform.localScale = vector;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
	public void ResetScale(float _scale = 2f)
	{
		Vector3 vector;
		vector..ctor(_scale, _scale, _scale);
		base.transform.localScale = vector;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0001269B File Offset: 0x0001089B
	public Winion GetWinionType()
	{
		return this.winionStatus.winionInfo.winionType;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x000126AD File Offset: 0x000108AD
	public void Log(string log, bool force = false)
	{
		if (this.DebugLog || force)
		{
			Debug.Log(base.gameObject.name + " : " + log);
		}
	}

	// Token: 0x04000773 RID: 1907
	public bool DebugLog;

	// Token: 0x04000774 RID: 1908
	public CharacterState characterState = CharacterState.None;

	// Token: 0x04000775 RID: 1909
	public WinionAnimator winionAnimator;

	// Token: 0x04000776 RID: 1910
	public WinionMouseEvent winionMouseEvent;

	// Token: 0x04000777 RID: 1911
	public WinionDragAndDrop winionDragAndDrop;

	// Token: 0x04000778 RID: 1912
	public WinionInterruptAction winionInterruptAction;

	// Token: 0x04000779 RID: 1913
	public WinionMovement winionMovement;

	// Token: 0x0400077A RID: 1914
	public WinionStatus winionStatus;

	// Token: 0x0400077B RID: 1915
	public WinionFeed winionFeed;

	// Token: 0x0400077C RID: 1916
	public WinionBehaviour winionBehaviour;

	// Token: 0x0400077D RID: 1917
	public WinionLookAt winionLookAt;

	// Token: 0x0400077E RID: 1918
	public bool optionOpen;

	// Token: 0x0400077F RID: 1919
	public GameObject winionFaceUI;

	// Token: 0x04000780 RID: 1920
	public bool haveEvent;

	// Token: 0x04000781 RID: 1921
	public bool automaticEvent;

	// Token: 0x04000782 RID: 1922
	public bool doubleLock;

	// Token: 0x04000783 RID: 1923
	public bool blockDialogue;

	// Token: 0x04000784 RID: 1924
	public bool have_smallTalk_Events;

	// Token: 0x04000785 RID: 1925
	public bool dialogue_ing;

	// Token: 0x04000786 RID: 1926
	public GameObject uiWinion;

	// Token: 0x04000787 RID: 1927
	public Transform uiWinionSpeechBubblePos;

	// Token: 0x04000788 RID: 1928
	public Transform uiWinionSpeechBubblePos_Left;

	// Token: 0x04000789 RID: 1929
	public Transform uiWinionSpeechBubblePos_Right;

	// Token: 0x0400078A RID: 1930
	public bool uiwinionColorChange;

	// Token: 0x0400078B RID: 1931
	public bool changeUIGradient;

	// Token: 0x0400078C RID: 1932
	public bool specialBubblePos;

	// Token: 0x0400078D RID: 1933
	public bool winionDialogue_upUI;

	// Token: 0x0400078E RID: 1934
	public bool worldWinionEnabled;

	// Token: 0x0400078F RID: 1935
	public bool UIWinionEnabled;

	// Token: 0x04000790 RID: 1936
	public Winion whichFolder = Winion.None;

	// Token: 0x04000791 RID: 1937
	public GameObject Debug_HaveSomethingToSay_temp;

	// Token: 0x04000792 RID: 1938
	public Transform BowatchWinionBubblePos;

	// Token: 0x04000793 RID: 1939
	private WinionSpriteSort winionSort;

	// Token: 0x04000794 RID: 1940
	public static bool SetAutoCheckIdle = true;

	// Token: 0x04000795 RID: 1941
	public bool SetAutoChackIdle_Personal = true;

	// Token: 0x04000796 RID: 1942
	public bool haveEventEmotion;

	// Token: 0x04000797 RID: 1943
	public GameObject emotionHaveEvent_obj;

	// Token: 0x04000798 RID: 1944
	private DBManager.EmotionSpeechBubble emotion;

	// Token: 0x04000799 RID: 1945
	public bool haveSmallTalkEmotion;

	// Token: 0x0400079A RID: 1946
	public GameObject emotionSmallTalk_obj;

	// Token: 0x0400079B RID: 1947
	private DBManager.EmotionSpeechBubble smallTalkEmotion;

	// Token: 0x0400079C RID: 1948
	public bool finishAlphaValue;
}
