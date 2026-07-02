using System;
using project.Scripts.CharacterScripts;

// Token: 0x0200010B RID: 267
public class WinionAnimator : CustomAnimator, IHandler
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000667 RID: 1639 RVA: 0x000122CB File Offset: 0x000104CB
	// (set) Token: 0x06000668 RID: 1640 RVA: 0x000122D3 File Offset: 0x000104D3
	public WinionHandler winionHandler { get; set; }

	// Token: 0x06000669 RID: 1641 RVA: 0x00037EE4 File Offset: 0x000360E4
	public void InitBool()
	{
		this.winionEmptiness = false;
		this.FixEmptiness02 = false;
		this.fix_Glass = false;
		this.fix_blood = false;
		this.debug_bright = false;
		this.isFlush = false;
		if (!this.notInit)
		{
			this.Grid_emptyness02 = false;
			this.Bo_emptyness02 = false;
		}
		this.Debug_Fear = false;
		this.preAnimation = "";
		this.curAnimationName = "";
		this.FaceName = "";
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x000122DC File Offset: 0x000104DC
	public override void SetAnimationCanChange(bool value)
	{
		this.canChangeAnimation = value;
		this.winionHandler.ChangeCharacterState(CharacterState.None);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00037F5C File Offset: 0x0003615C
	public override void PlayAnimation(string AnimationName, bool IndexFollowing = false)
	{
		if (!this.canChangeAnimation)
		{
			return;
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == Winion.Ion)
		{
			if (this.winionHandler.winionStatus.isBizit && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_Bizit";
			}
			if (this.winionHandler.winionStatus.isRescueTeam && (AnimationName == "FrontIdle" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_RescueTeam";
			}
			AnimationName = this.IONAnimationCheck(AnimationName);
			if (this.ion_Hurt && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_Hurt";
			}
			if (this.winionHandler.whichFolder == Winion.TrashCan)
			{
				if (this.winionEmptiness)
				{
					if (AnimationName == "BackIdle")
					{
						this.preAnimation = AnimationName;
						AnimationName = "BackIdle_TrashCan";
					}
					if (AnimationName == "FrontIdle" || AnimationName == "LeftIdle")
					{
						this.preAnimation = AnimationName;
						AnimationName += "_Emptiness_TrashCan";
					}
					if (AnimationName == "Depression_Emptyness")
					{
						this.preAnimation = AnimationName;
						AnimationName += "_TrashCan";
					}
				}
				if (AnimationName == "Smile" || AnimationName == "SmileBlushFree")
				{
					AnimationName = "Smile_TrashCan";
				}
				if (AnimationName == "Smile_Left" || AnimationName == "SmileBlushFree_Left")
				{
					AnimationName = "Smile_Left_TrashCan";
				}
				if (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk")
				{
					this.preAnimation = AnimationName;
					AnimationName += "_TrashCan";
				}
			}
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == Winion.Bo)
		{
			if (this.winionHandler.winionStatus.isWatchWinion && (AnimationName == "FrontIdle" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_WatchWinion";
			}
			AnimationName = this.BoAnimationCheck(AnimationName);
			if (this.winionHandler.whichFolder == Winion.TrashCan)
			{
				if (AnimationName == "Sad_Left")
				{
					AnimationName = "LeftIdle";
				}
				if (this.Bo_emptyness02 && (AnimationName == "LeftWalk" || AnimationName == "LeftIdle"))
				{
					this.preAnimation = AnimationName;
					AnimationName += "_Emptiness_TrashCan";
				}
				if (AnimationName == "Smile_Left")
				{
					AnimationName = "Smile_Left_TrashCan";
				}
				if (AnimationName == "LeftIdle_Whiteness" || AnimationName == "LeftIdle_Whiteness_cry")
				{
					AnimationName = "LeftIdle_Emptiness_TrashCan";
				}
				if (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk")
				{
					this.preAnimation = AnimationName;
					AnimationName += "_TrashCan";
				}
			}
			if (this.Bo_emptyness02 && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_Whiteness";
			}
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == Winion.Fix)
		{
			if (this.winionHandler.winionStatus.isFriend01 && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_Friend01";
			}
			if (this.winionHandler.winionStatus.isWinion02 && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_Winion02";
			}
			if (this.winionHandler.winionStatus.isRescueTeam && (AnimationName == "FrontIdle" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_RescueTeam";
			}
			if (this.winionHandler.winionStatus.isSystemWinion && AnimationName == "FrontIdle")
			{
				AnimationName += "_SystemWinion";
			}
			AnimationName = this.FixAnimationCheck(AnimationName);
			if (this.fix_Glass && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_glass";
			}
			if (this.fix_blood && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_Eat_ION";
			}
			if (this.FixEmptiness02 && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_madness01";
			}
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == Winion.Debug)
		{
			if ((this.winionHandler.winionStatus.isFriend02 || this.winionHandler.winionStatus.isWinion01) && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_Friend02";
			}
			if (this.winionHandler.winionStatus.isRescueTeam && (AnimationName == "FrontIdle" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				AnimationName += "_RescueTeam";
			}
			AnimationName = this.DebugAnimationCheck(AnimationName);
			if (this.debug_bright && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_bright";
			}
			if (this.Debug_Fear && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_Fear";
			}
			if (this.Debug_Angry && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				if (AnimationName == "FrontWalk")
				{
					this.preAnimation = AnimationName;
					AnimationName = "FrontIdle_Angry";
				}
				else
				{
					this.preAnimation = AnimationName;
					AnimationName += "_Angry";
				}
			}
			if (this.isDebug_0)
			{
				DBManager.instance.dialogueData.curEvent.Chapter02_Event22_Debug02(AnimationName);
			}
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == Winion.Grid)
		{
			AnimationName = this.GridAnimationCheck(AnimationName);
			if (this.winionHandler.whichFolder == Winion.TrashCan)
			{
				if (this.Grid_emptyness02 && (AnimationName == "LeftWalk" || AnimationName == "LeftIdle"))
				{
					this.preAnimation = AnimationName;
					AnimationName += "_Emptiness_TrashCan";
				}
				if (AnimationName == "LeftIdle_WhitenessDownEye" || AnimationName == "LeftIdle_Whiteness_Cry" || AnimationName == "LeftIdle_Whiteness")
				{
					AnimationName = "LeftIdle_Emptiness_TrashCan";
				}
				if (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk")
				{
					this.preAnimation = AnimationName;
					AnimationName += "_TrashCan";
				}
			}
			if (this.Grid_emptyness02 && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk" || AnimationName == "BackWalk"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_Whiteness";
			}
			if (this.isFlush && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle"))
			{
				this.preAnimation = AnimationName;
				AnimationName += "_Flush";
			}
		}
		if (this.winionEmptiness && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "LeftWalk" || AnimationName == "FrontWalk"))
		{
			this.preAnimation = AnimationName;
			AnimationName += "_Emptiness";
		}
		if (this.winionHandler.whichFolder == Winion.TrashCan && (AnimationName == "FrontIdle" || AnimationName == "LeftIdle" || AnimationName == "BackIdle" || AnimationName == "LeftWalk"))
		{
			this.preAnimation = AnimationName;
			AnimationName += "_TrashCan";
		}
		base.PlayAnimation(AnimationName, IndexFollowing);
		this.curAnimationName = AnimationName;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00038A70 File Offset: 0x00036C70
	public override void EndAnimationEvent()
	{
		if (this.noAction)
		{
			this.winionHandler.SetIdleByWinionStatus();
			this.winionHandler.winionMovement.canInterrupt = true;
			this.winionHandler.winionMovement.waitAndPlay = true;
			return;
		}
		Action animationEndAction = this.AnimationEndAction;
		if (animationEndAction != null)
		{
			animationEndAction();
		}
		this.noAction = true;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00038ACC File Offset: 0x00036CCC
	public void SetAction(Action action)
	{
		this.noAction = false;
		this.AnimationEndAction = delegate
		{
			Action action2 = action;
			if (action2 != null)
			{
				action2();
			}
			this.AnimationEndAction = null;
		};
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void GetNowAnimation()
	{
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x000122F1 File Offset: 0x000104F1
	public void PlayCurAnimation()
	{
		if (this.openFaceWindow)
		{
			if (this.preAnimation != "")
			{
				this.PlayAnimation(this.preAnimation, false);
				return;
			}
			this.PlayAnimation(this.currentAnimationName, false);
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00012328 File Offset: 0x00010528
	public void ResetCurAnimation()
	{
		if (this.preAnimation != "")
		{
			this.PlayAnimation(this.preAnimation, false);
			this.preAnimation = "";
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00038B08 File Offset: 0x00036D08
	public string IONAnimationCheck(string animationName)
	{
		this.preAnimation = "";
		if (this.openFaceWindow && (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle" || animationName == "LeftWalk" || animationName == "FrontWalk" || animationName == "BackWalk" || animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit" || animationName == "BackIdle_Bizit"))
		{
			string faceName = this.FaceName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(faceName);
			if (num <= 1941629204U)
			{
				if (num <= 1090515110U)
				{
					if (num <= 413253463U)
					{
						if (num <= 293654250U)
						{
							if (num != 34415898U)
							{
								if (num != 293654250U)
								{
									return animationName;
								}
								if (!(faceName == "[아이온-머리채]"))
								{
									return animationName;
								}
								return animationName;
							}
							else if (!(faceName == "[아이온-맞음2]"))
							{
								return animationName;
							}
						}
						else if (num != 382595016U)
						{
							if (num != 401476537U)
							{
								if (num != 413253463U)
								{
									return animationName;
								}
								if (!(faceName == "[아이온- 공허 걱정 손모음]"))
								{
									return animationName;
								}
								goto IL_096B;
							}
							else
							{
								if (!(faceName == "[아이온- 공허 눈물]"))
								{
									return animationName;
								}
								goto IL_096B;
							}
						}
						else
						{
							if (!(faceName == "[아이온-다침]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else if (num <= 779379525U)
					{
						if (num != 490532168U)
						{
							if (num != 779379525U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온-머리뜯김]"))
							{
								return animationName;
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온-울음]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "Cry";
							}
							if (animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								animationName = "Cry_Back";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "Cry_Left";
							}
							return animationName;
						}
					}
					else if (num != 911609960U)
					{
						if (num != 955039230U)
						{
							if (num != 1090515110U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온-눈찡긋]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "Wink";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "Wink_Left";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온-다침_공포]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[비지트- 눈깔음]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit")
						{
							this.preAnimation = animationName;
							return animationName + "_DownEye";
						}
						return animationName;
					}
				}
				else if (num <= 1447747712U)
				{
					if (num <= 1259945317U)
					{
						if (num != 1122863828U)
						{
							if (num != 1259945317U)
							{
								return animationName;
							}
							if (!(faceName == "[비지트-눈물 광기]"))
							{
								return animationName;
							}
							goto IL_0B81;
						}
						else
						{
							if (!(faceName == "[비지트-눈 감음]"))
							{
								return animationName;
							}
							goto IL_0B81;
						}
					}
					else if (num != 1263751283U)
					{
						if (num != 1411817176U)
						{
							if (num != 1447747712U)
							{
								return animationName;
							}
							if (!(faceName == "[비지트-눈물고임]"))
							{
								return animationName;
							}
							goto IL_0A76;
						}
						else
						{
							if (!(faceName == "[아이온-안타까움]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "Sad";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "Sad_Left";
							}
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[비지트-눈물고임_2]"))
						{
							return animationName;
						}
						goto IL_0B81;
					}
				}
				else if (num <= 1766801077U)
				{
					if (num != 1558529031U)
					{
						if (num != 1722276922U)
						{
							if (num != 1766801077U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온-맞음2공포]"))
							{
								return animationName;
							}
							goto IL_08D4;
						}
						else if (!(faceName == "[아이온-맞음]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[비지트-무표정]"))
						{
							return animationName;
						}
						return animationName;
					}
				}
				else if (num != 1810660204U)
				{
					if (num != 1918296435U)
					{
						if (num != 1941629204U)
						{
							return animationName;
						}
						if (!(faceName == "[아이온-화냄]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "Angry";
						}
						if (animationName == "BackIdle")
						{
							this.preAnimation = animationName;
							animationName = "Angry_Back";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return "Angry_Left";
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[비지트-광기]"))
						{
							return animationName;
						}
						goto IL_0B81;
					}
				}
				else
				{
					if (!(faceName == "[비지트-화남]"))
					{
						return animationName;
					}
					if (animationName == "LeftIdle_Bizit")
					{
						this.preAnimation = animationName;
						return animationName + "_Angry";
					}
					return animationName;
				}
				if (animationName == "FrontIdle")
				{
					this.preAnimation = animationName;
					animationName = "BeHit01_Front";
				}
				if (animationName == "BackIdle")
				{
					this.preAnimation = animationName;
					animationName = "BeHit01_Back";
				}
				if (animationName == "LeftIdle")
				{
					this.preAnimation = animationName;
					return "BeHit01_Left";
				}
				return animationName;
			}
			else
			{
				if (num <= 2900794582U)
				{
					if (num <= 2241088671U)
					{
						if (num <= 2108498250U)
						{
							if (num != 1966585292U)
							{
								if (num != 2108498250U)
								{
									return animationName;
								}
								if (!(faceName == "[아이온-충격]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									animationName = "shock";
								}
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									return "shock_Left";
								}
								return animationName;
							}
							else
							{
								if (!(faceName == "[아이온-다침_무표정]"))
								{
									return animationName;
								}
								return animationName;
							}
						}
						else if (num != 2133805773U)
						{
							if (num != 2228638371U)
							{
								if (num != 2241088671U)
								{
									return animationName;
								}
								if (!(faceName == "[아이온-맞음2_눈물]"))
								{
									return animationName;
								}
								goto IL_08D4;
							}
							else
							{
								if (!(faceName == "[아이온- 공허 걱정]"))
								{
									return animationName;
								}
								goto IL_096B;
							}
						}
						else
						{
							if (!(faceName == "[비지트-웃음]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit")
							{
								this.preAnimation = animationName;
								return animationName + "_Smile";
							}
							return animationName;
						}
					}
					else if (num <= 2455554206U)
					{
						if (num != 2313267977U)
						{
							if (num != 2455554206U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온-호기심]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								return "Chorong";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온-웃음]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "Smile";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "Smile_Left";
							}
							return animationName;
						}
					}
					else if (num != 2497620790U)
					{
						if (num != 2500731148U)
						{
							if (num != 2900794582U)
							{
								return animationName;
							}
							if (!(faceName == "[비지트-아쉬움]"))
							{
								return animationName;
							}
							goto IL_0A76;
						}
						else
						{
							if (!(faceName == "[비지트-시선회피]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle_Bizit")
							{
								this.preAnimation = animationName;
								return animationName + "_AvoidanceOfGaze";
							}
							return animationName;
						}
					}
					else if (!(faceName == "[비지트-충격]"))
					{
						return animationName;
					}
				}
				else if (num <= 3426673660U)
				{
					if (num <= 3275600228U)
					{
						if (num != 3215730581U)
						{
							if (num != 3275600228U)
							{
								return animationName;
							}
							if (!(faceName == "[비지트-귀여운충격]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit")
							{
								this.preAnimation = animationName;
								return animationName + "_Shock";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온-다침_눈물]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else if (num != 3328517432U)
					{
						if (num != 3377504123U)
						{
							if (num != 3426673660U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온- 공허 무표정 손모음]"))
							{
								return animationName;
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온-무표정]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[아이온- 공허 무표정]"))
						{
							return animationName;
						}
						return animationName;
					}
				}
				else if (num <= 3825581691U)
				{
					if (num != 3461521562U)
					{
						if (num != 3673560469U)
						{
							if (num != 3825581691U)
							{
								return animationName;
							}
							if (!(faceName == "[아이온-하얗게 질림]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "FrontWalk")
							{
								this.preAnimation = animationName;
								animationName = "FrontIdle_Whiteness";
							}
							if (animationName == "BackIdle" || animationName == "BackWalk")
							{
								this.preAnimation = animationName;
								animationName = "BackIdle_Whiteness";
							}
							if (animationName == "LeftIdle" || animationName == "LeftWalk")
							{
								this.preAnimation = animationName;
								return "LeftIdle_Whiteness";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[아이온- 공허 눈물 손모음]"))
							{
								return animationName;
							}
							goto IL_096B;
						}
					}
					else
					{
						if (!(faceName == "[아이온-공허웃음]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "SmileBlushFree";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return "SmileBlushFree_Left";
						}
						return animationName;
					}
				}
				else if (num != 3975164550U)
				{
					if (num != 4088207036U)
					{
						if (num != 4111464026U)
						{
							return animationName;
						}
						if (!(faceName == "[비지트-호기심]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit")
						{
							this.preAnimation = animationName;
							return animationName + "_Chorong";
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[비지트-눈물줄줄]"))
						{
							return animationName;
						}
						goto IL_0B81;
					}
				}
				else if (!(faceName == "[비지트-경악]"))
				{
					return animationName;
				}
				if (animationName == "LeftIdle_Bizit")
				{
					this.preAnimation = animationName;
					return animationName + "_Shock02";
				}
				return animationName;
			}
			IL_08D4:
			if (animationName == "FrontIdle")
			{
				this.preAnimation = animationName;
				animationName = "BeHit02_Front";
			}
			if (animationName == "BackIdle")
			{
				this.preAnimation = animationName;
				animationName = "BeHit02_Back";
			}
			if (animationName == "LeftIdle")
			{
				this.preAnimation = animationName;
				return "BeHit02_Left";
			}
			return animationName;
			IL_096B:
			if (animationName == "FrontIdle")
			{
				this.preAnimation = animationName;
				return "Depression_Emptyness";
			}
			return animationName;
			IL_0A76:
			if (animationName == "FrontIdle_Bizit" || animationName == "LeftIdle_Bizit")
			{
				this.preAnimation = animationName;
				return animationName + "_Sad";
			}
			return animationName;
			IL_0B81:
			if (animationName == "BackIdle_Bizit")
			{
				this.preAnimation = animationName;
				animationName += "_LastBack";
			}
		}
		return animationName;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x000396B8 File Offset: 0x000378B8
	public string FixAnimationCheck(string animationName)
	{
		this.preAnimation = "";
		if (this.openFaceWindow && (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle" || animationName == "LeftWalk" || animationName == "FrontWalk" || animationName == "BackWalk"))
		{
			string faceName = this.FaceName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(faceName);
			if (num <= 1826730914U)
			{
				if (num <= 1023016835U)
				{
					if (num <= 25227239U)
					{
						if (num != 19178671U)
						{
							if (num != 25227239U)
							{
								return animationName;
							}
							if (!(faceName == "[픽스-충격]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "Shock";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "Shock_Left";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[픽스-웃음홍조]"))
							{
								return animationName;
							}
							goto IL_03B8;
						}
					}
					else if (num != 729890205U)
					{
						if (num != 940363561U)
						{
							if (num != 1023016835U)
							{
								return animationName;
							}
							if (!(faceName == "[픽스-걱정]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "worry";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return "worry_Left";
							}
							return animationName;
						}
						else if (!(faceName == "[픽스-유리조각_광기 눈깔 축소]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[픽스-혼란눈물]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return "ConfusedTears";
						}
						return animationName;
					}
				}
				else if (num <= 1587265944U)
				{
					if (num != 1551991155U)
					{
						if (num != 1585988062U)
						{
							if (num != 1587265944U)
							{
								return animationName;
							}
							if (!(faceName == "[픽스-웃음]"))
							{
								return animationName;
							}
							goto IL_03B8;
						}
						else if (!(faceName == "[픽스-유리조각_광기 눈깔 애니]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[픽스-충격_경악_눈물]"))
						{
							return animationName;
						}
						goto IL_06A1;
					}
				}
				else if (num != 1593001395U)
				{
					if (num != 1680544369U)
					{
						if (num != 1826730914U)
						{
							return animationName;
						}
						if (!(faceName == "[픽스-유리조각_광기]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[픽스-아련]"))
						{
							return animationName;
						}
						return animationName;
					}
				}
				else
				{
					if (!(faceName == "[픽스-화남2]"))
					{
						return animationName;
					}
					goto IL_03F6;
				}
				if (animationName == "FrontIdle" || animationName == "BackIdle" || animationName == "LeftIdle" || animationName == "FrontWalk" || animationName == "BackWalk" || animationName == "LeftWalk")
				{
					this.preAnimation = animationName;
					return animationName + "_glass";
				}
				return animationName;
			}
			else
			{
				if (num <= 3215172010U)
				{
					if (num <= 2508628781U)
					{
						if (num != 1830238168U)
						{
							if (num != 2330105825U)
							{
								if (num != 2508628781U)
								{
									return animationName;
								}
								if (!(faceName == "[픽스-괴로운 눈물]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									return "Sad_Madness";
								}
								return animationName;
							}
							else
							{
								if (!(faceName == "[픽스-화남]"))
								{
									return animationName;
								}
								goto IL_03F6;
							}
						}
						else
						{
							if (!(faceName == "[픽스-무표정]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else if (num != 2653462287U)
					{
						if (num != 3135601170U)
						{
							if (num != 3215172010U)
							{
								return animationName;
							}
							if (!(faceName == "[픽스-광기2]"))
							{
								return animationName;
							}
						}
						else
						{
							if (!(faceName == "[픽스-폭소]"))
							{
								return animationName;
							}
							goto IL_03B8;
						}
					}
					else
					{
						if (!(faceName == "[픽스- 눈 아래 생각]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "Sad";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return "Sad_Left";
						}
						return animationName;
					}
				}
				else if (num <= 3575106182U)
				{
					if (num != 3483643722U)
					{
						if (num != 3508044758U)
						{
							if (num != 3575106182U)
							{
								return animationName;
							}
							if (!(faceName == "[픽스-악마]"))
							{
								return animationName;
							}
						}
						else
						{
							if (!(faceName == "[픽스-충격_경악]"))
							{
								return animationName;
							}
							goto IL_06A1;
						}
					}
					else
					{
						if (!(faceName == "[픽스-아이온먹음]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle" || animationName == "BackIdle" || animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_Eat_ION";
						}
						return animationName;
					}
				}
				else if (num != 3785066250U)
				{
					if (num != 4105975413U)
					{
						if (num != 4137184139U)
						{
							return animationName;
						}
						if (!(faceName == "[픽스-머리 복잡]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return "ComplicatedHead";
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[픽스-아픔]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return "Pain";
						}
						return animationName;
					}
				}
				else
				{
					if (!(faceName == "[픽스-광기]"))
					{
						return animationName;
					}
					if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "FrontWalk" || animationName == "LeftWalk")
					{
						this.preAnimation = animationName;
						return animationName + "_Emptiness";
					}
					return animationName;
				}
				if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "FrontWalk" || animationName == "LeftWalk")
				{
					this.preAnimation = animationName;
					return animationName + "_madness01";
				}
				return animationName;
			}
			IL_03B8:
			if (animationName == "FrontIdle")
			{
				this.preAnimation = animationName;
				animationName = "Smile";
			}
			if (animationName == "LeftIdle")
			{
				this.preAnimation = animationName;
				return "Smile_Left";
			}
			return animationName;
			IL_03F6:
			if (animationName == "FrontIdle")
			{
				this.preAnimation = animationName;
				animationName = "Angry";
			}
			if (animationName == "BackIdle")
			{
				this.preAnimation = animationName;
				animationName = "Angry_Back";
			}
			if (animationName == "LeftIdle")
			{
				this.preAnimation = animationName;
				return "Angry_Left";
			}
			return animationName;
			IL_06A1:
			if (animationName == "FrontIdle" || animationName == "BackIdle" || animationName == "LeftIdle")
			{
				this.preAnimation = animationName;
				animationName += "_superShock";
			}
		}
		return animationName;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00039DDC File Offset: 0x00037FDC
	public string DebugAnimationCheck(string animationName)
	{
		this.preAnimation = "";
		if (this.openFaceWindow && (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle" || animationName == "LeftWalk" || animationName == "FrontWalk" || animationName == "BackWalk"))
		{
			string faceName = this.FaceName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(faceName);
			if (num <= 1807888044U)
			{
				if (num <= 980048825U)
				{
					if (num <= 179188796U)
					{
						if (num != 29045129U)
						{
							if (num != 50041931U)
							{
								if (num != 179188796U)
								{
									return animationName;
								}
								if (!(faceName == "[디버그-작은 공포]"))
								{
									return animationName;
								}
							}
							else
							{
								if (!(faceName == "[디버그-무표정]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "FrontWalk" || animationName == "LeftWalk")
								{
									this.preAnimation = animationName;
									return animationName + "_bright";
								}
								return animationName;
							}
						}
						else
						{
							if (!(faceName == "[디버그-다침_화냄]"))
							{
								return animationName;
							}
							goto IL_0738;
						}
					}
					else if (num <= 770370611U)
					{
						if (num != 362710620U)
						{
							if (num != 770370611U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-귀찢어짐_화남]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								return animationName + "rippedEar_Angry";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[디버그-귀찢어지는중_눈물]"))
							{
								return animationName;
							}
							goto IL_07DF;
						}
					}
					else if (num != 845263039U)
					{
						if (num != 980048825U)
						{
							return animationName;
						}
						if (!(faceName == "[디버그-다침_눈아래]"))
						{
							return animationName;
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return "LeftIdle_HurtDownEye";
						}
						return animationName;
					}
					else if (!(faceName == "[디버그-큰 공포]"))
					{
						return animationName;
					}
					if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
					{
						this.preAnimation = animationName;
						return animationName + "_Fear";
					}
					return animationName;
				}
				else if (num <= 1115247974U)
				{
					if (num != 985399581U)
					{
						if (num != 1029823732U)
						{
							if (num != 1115247974U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-귀찢어짐]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								return animationName + "_Blood";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[디버그-눈물고임-공허]"))
							{
								return animationName;
							}
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[디버그-자책]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_BlameOneself";
						}
						return animationName;
					}
				}
				else
				{
					if (num <= 1193873594U)
					{
						if (num != 1163357271U)
						{
							if (num != 1193873594U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-소리치며 화냄]"))
							{
								return animationName;
							}
						}
						else
						{
							if (!(faceName == "[디버그-귀찢어지는중]"))
							{
								return animationName;
							}
							goto IL_07DF;
						}
					}
					else if (num != 1432812521U)
					{
						if (num != 1807888044U)
						{
							return animationName;
						}
						if (!(faceName == "[디버그-눈물고이고 화냄]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[디버그-눈을 내리깐 무표정]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle" || animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_DownEye";
						}
						return animationName;
					}
					if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
					{
						this.preAnimation = animationName;
						return animationName + "_Angry";
					}
					return animationName;
				}
				IL_07DF:
				if (animationName == "FrontIdle")
				{
					this.preAnimation = animationName;
					animationName = "pullEar";
				}
				if (animationName == "BackIdle" || animationName == "DebugEar")
				{
					this.preAnimation = animationName;
					return "DebugEar_Blood01";
				}
				return animationName;
			}
			else
			{
				if (num <= 2697521330U)
				{
					if (num <= 2252194834U)
					{
						if (num != 1839456261U)
						{
							if (num != 1967260210U)
							{
								if (num != 2252194834U)
								{
									return animationName;
								}
								if (!(faceName == "[디버그-웃음홍조]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									animationName = "Smile_Blush";
								}
								if (animationName == "LeftIdle")
								{
									this.preAnimation = animationName;
									return "Smile_Left";
								}
								return animationName;
							}
							else
							{
								if (!(faceName == "[디버그-눈 아래_공허]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle" || animationName == "LeftIdle")
								{
									this.preAnimation = animationName;
									return animationName + "_EmptyDownEye";
								}
								return animationName;
							}
						}
						else if (!(faceName == "[디버그-다침_눈물]"))
						{
							return animationName;
						}
					}
					else if (num <= 2362946667U)
					{
						if (num != 2362791098U)
						{
							if (num != 2362946667U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-다침_화냄2]"))
							{
								return animationName;
							}
							goto IL_0738;
						}
						else
						{
							if (!(faceName == "[디버그-경악]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								return animationName + "_Astonishment";
							}
							return animationName;
						}
					}
					else if (num != 2611077145U)
					{
						if (num != 2697521330U)
						{
							return animationName;
						}
						if (!(faceName == "[디버그-눈물_공허]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_EmptyCry";
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[디버그-웃음]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "FrontIdle_LastSmile";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return "Smile_Left";
						}
						return animationName;
					}
				}
				else
				{
					if (num <= 3465276120U)
					{
						if (num <= 3256114506U)
						{
							if (num != 3198536696U)
							{
								if (num != 3256114506U)
								{
									return animationName;
								}
								if (!(faceName == "[디버그-다침_큰공포]"))
								{
									return animationName;
								}
								if (animationName == "LeftIdle")
								{
									this.preAnimation = animationName;
									return "LeftIdle_Hurt_Fear";
								}
								return animationName;
							}
							else
							{
								if (!(faceName == "[디버그-울음]"))
								{
									return animationName;
								}
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									animationName = "Cry";
								}
								if (animationName == "LeftIdle" || animationName == "BackIdle")
								{
									this.preAnimation = animationName;
									return animationName + "_cry";
								}
								return animationName;
							}
						}
						else if (num != 3370643275U)
						{
							if (num != 3465276120U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-다침]"))
							{
								return animationName;
							}
							goto IL_0715;
						}
						else if (!(faceName == "[디버그-귀잡아당김_눈물]"))
						{
							return animationName;
						}
					}
					else if (num <= 3916677614U)
					{
						if (num != 3777758687U)
						{
							if (num != 3916677614U)
							{
								return animationName;
							}
							if (!(faceName == "[디버그-귀잡아당김]"))
							{
								return animationName;
							}
						}
						else
						{
							if (!(faceName == "[디버그-귀찢어짐_아래바라봄]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName += "DebugEar_Blood01";
							}
							if (animationName == "LeftIdle" || animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								return animationName + "_Blood";
							}
							return animationName;
						}
					}
					else if (num != 3926063555U)
					{
						if (num != 3994281028U)
						{
							return animationName;
						}
						if (!(faceName == "[디버그-눈물고임]"))
						{
							return animationName;
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[디버그-무표정_공허]"))
						{
							return animationName;
						}
						return animationName;
					}
					if (animationName == "FrontIdle")
					{
						this.preAnimation = animationName;
						animationName = "pullEar";
					}
					if (animationName == "BackIdle")
					{
						this.preAnimation = animationName;
						return "DebugEar";
					}
					return animationName;
				}
				IL_0715:
				if (animationName == "LeftIdle")
				{
					this.preAnimation = animationName;
					return "LeftIdle_Hurt";
				}
				return animationName;
			}
			IL_0738:
			if (animationName == "LeftIdle")
			{
				this.preAnimation = animationName;
				animationName = "LeftIdle_HurtAngry";
			}
		}
		return animationName;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0003A73C File Offset: 0x0003893C
	public string GridAnimationCheck(string animationName)
	{
		this.preAnimation = "";
		if (this.openFaceWindow && (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle" || animationName == "LeftWalk" || animationName == "FrontWalk" || animationName == "BackWalk"))
		{
			string faceName = this.FaceName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(faceName);
			if (num <= 1002855899U)
			{
				if (num <= 276674932U)
				{
					if (num != 11985199U)
					{
						if (num != 249653631U)
						{
							if (num != 276674932U)
							{
								return animationName;
							}
							if (!(faceName == "[그리드-아픔]"))
							{
								return animationName;
							}
							return animationName;
						}
						else if (!(faceName == "[그리드-하얗게 질리고 눈물]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[그리드-붉은얼굴]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle" || animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_RedFace";
						}
						return animationName;
					}
				}
				else if (num != 772946923U)
				{
					if (num != 917050587U)
					{
						if (num != 1002855899U)
						{
							return animationName;
						}
						if (!(faceName == "[그리드-하얗게 질림]"))
						{
							return animationName;
						}
					}
					else
					{
						if (!(faceName == "[그리드-무표정]"))
						{
							return animationName;
						}
						return animationName;
					}
				}
				else
				{
					if (!(faceName == "[그리드-하얗게 질리고 눈물흘림]"))
					{
						return animationName;
					}
					if (animationName == "FrontIdle")
					{
						this.preAnimation = animationName;
						animationName += "_Whiteness_Cry";
					}
					if (animationName == "LeftIdle" || animationName == "BackIdle")
					{
						this.preAnimation = animationName;
						return animationName + "_Whiteness";
					}
					return animationName;
				}
				if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
				{
					this.preAnimation = animationName;
					animationName += "_Whiteness";
				}
			}
			else
			{
				if (num <= 2337514679U)
				{
					if (num != 1040031103U)
					{
						if (num != 1604861699U)
						{
							if (num != 2337514679U)
							{
								return animationName;
							}
							if (!(faceName == "[그리드-하얗게 질리고 눈깔기]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								animationName += "_WhitenessDownEye";
							}
							if (animationName == "BackIdle")
							{
								this.preAnimation = animationName;
								return animationName + "_Whiteness";
							}
							return animationName;
						}
						else
						{
							if (!(faceName == "[그리드-홍조]"))
							{
								return animationName;
							}
							if (animationName == "FrontIdle" || animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								return animationName + "_Flush";
							}
							return animationName;
						}
					}
					else if (!(faceName == "[그리드-홍조놀람]"))
					{
						return animationName;
					}
				}
				else if (num != 2395291488U)
				{
					if (num != 3425339906U)
					{
						if (num != 4136517539U)
						{
							return animationName;
						}
						if (!(faceName == "[그리드-찡그림]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_Grimace";
						}
						return animationName;
					}
					else
					{
						if (!(faceName == "[그리드-눈깔고 생각]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							return animationName + "_DownEye";
						}
						return animationName;
					}
				}
				else if (!(faceName == "[그리드-홍조정면]"))
				{
					return animationName;
				}
				if (animationName == "FrontIdle" || animationName == "LeftIdle")
				{
					this.preAnimation = animationName;
					animationName += "_Flush";
				}
			}
		}
		return animationName;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0003AB30 File Offset: 0x00038D30
	public string BoAnimationCheck(string animationName)
	{
		this.preAnimation = "";
		if (this.openFaceWindow && (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle" || animationName == "LeftWalk" || animationName == "FrontWalk" || animationName == "BackWalk" || animationName == "FrontIdle_WatchWinion"))
		{
			string faceName = this.FaceName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(faceName);
			if (num <= 1872271854U)
			{
				if (num <= 821222068U)
				{
					if (num != 139354055U)
					{
						if (num != 528643784U)
						{
							if (num != 821222068U)
							{
								return animationName;
							}
							if (!(faceName == "[보-안타까움]"))
							{
								return animationName;
							}
						}
						else if (!(faceName == "[보-눈물고임]"))
						{
							return animationName;
						}
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "Sad";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							animationName = "Sad_Left";
						}
					}
					else if (faceName == "[보-하얗게 질림]")
					{
						if (animationName == "FrontIdle" || animationName == "LeftIdle" || animationName == "BackIdle")
						{
							this.preAnimation = animationName;
							animationName += "_Whiteness";
						}
					}
				}
				else if (num <= 1253372810U)
				{
					if (num != 1234029278U)
					{
						if (num == 1253372810U)
						{
							if (faceName == "[감시위니언-맞음]")
							{
								if (animationName == "FrontIdle_WatchWinion")
								{
									this.preAnimation = animationName;
									animationName += "_GetDamage";
								}
							}
						}
					}
					else if (faceName == "[보-충격]")
					{
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "Shock";
						}
						if (animationName == "LeftIdle")
						{
							this.preAnimation = animationName;
							animationName = "Shock_Left";
						}
					}
				}
				else if (num != 1714410639U)
				{
					if (num == 1872271854U)
					{
						if (faceName == "[보-뺨 맞음]")
						{
							if (animationName == "FrontIdle")
							{
								this.preAnimation = animationName;
								animationName = "SlapFace";
							}
							if (animationName == "LeftIdle")
							{
								this.preAnimation = animationName;
								animationName = "Sad_Left";
							}
						}
					}
				}
				else if (faceName == "[감시위니언-비웃음]")
				{
					if (animationName == "FrontIdle_WatchWinion")
					{
						this.preAnimation = animationName;
						animationName += "_Smile";
					}
				}
			}
			else if (num <= 3164493252U)
			{
				if (num != 2187869541U)
				{
					if (num != 3082489000U)
					{
						if (num == 3164493252U)
						{
							if (faceName == "[보-울음]")
							{
								if (animationName == "FrontIdle")
								{
									this.preAnimation = animationName;
									animationName = "Cry";
								}
								if (animationName == "LeftIdle" || animationName == "BackIdle")
								{
									this.preAnimation = animationName;
									animationName += "_Cry";
								}
							}
						}
					}
					else if (faceName == "[보-화냄]")
					{
						if (animationName == "FrontIdle")
						{
							this.preAnimation = animationName;
							animationName = "Angry";
						}
						if (animationName == "LeftIdle" || animationName == "BackIdle")
						{
							this.preAnimation = animationName;
							animationName += "_Angry";
						}
					}
				}
				else if (faceName == "[보-웃음]")
				{
					if (animationName == "FrontIdle")
					{
						this.preAnimation = animationName;
						animationName = "Smile";
					}
					if (animationName == "LeftIdle")
					{
						this.preAnimation = animationName;
						animationName = "Smile_Left";
					}
				}
			}
			else if (num <= 3712276943U)
			{
				if (num != 3587854379U)
				{
					if (num == 3712276943U)
					{
						if (!(faceName == "[보-먹음]"))
						{
						}
					}
				}
				else if (!(faceName == "[감시위니언-무표정]"))
				{
				}
			}
			else if (num != 3848150700U)
			{
				if (num == 3891343567U && !(faceName == "[보-무표정]"))
				{
				}
			}
			else if (faceName == "[보-하얗게 질림_눈물]")
			{
				if (animationName == "FrontIdle")
				{
					this.preAnimation = animationName;
					animationName += "_Whiteness_cry";
				}
				if (animationName == "LeftIdle" || animationName == "BackIdle")
				{
					this.preAnimation = animationName;
					animationName += "_Whiteness";
				}
			}
		}
		return animationName;
	}

	// Token: 0x04000712 RID: 1810
	public Action AnimationEndAction;

	// Token: 0x04000713 RID: 1811
	public bool noAction = true;

	// Token: 0x04000714 RID: 1812
	public bool openFaceWindow;

	// Token: 0x04000715 RID: 1813
	public string FaceName = "";

	// Token: 0x04000716 RID: 1814
	public bool winionEmptiness;

	// Token: 0x04000717 RID: 1815
	public bool FixEmptiness02;

	// Token: 0x04000718 RID: 1816
	public bool fix_Glass;

	// Token: 0x04000719 RID: 1817
	public bool fix_blood;

	// Token: 0x0400071A RID: 1818
	public bool debug_bright;

	// Token: 0x0400071B RID: 1819
	public bool Grid_emptyness02;

	// Token: 0x0400071C RID: 1820
	public bool Bo_emptyness02;

	// Token: 0x0400071D RID: 1821
	public bool Debug_Fear;

	// Token: 0x0400071E RID: 1822
	public bool Debug_Angry;

	// Token: 0x0400071F RID: 1823
	public bool ion_Hurt;

	// Token: 0x04000720 RID: 1824
	public bool isDebug_0;

	// Token: 0x04000721 RID: 1825
	public bool notInit;

	// Token: 0x04000722 RID: 1826
	public bool isFlush;

	// Token: 0x04000723 RID: 1827
	public string curAnimationName = "";

	// Token: 0x04000724 RID: 1828
	public string preAnimation = "";
}
