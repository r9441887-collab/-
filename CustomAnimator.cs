using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000101 RID: 257
public class CustomAnimator : MonoBehaviour
{
	// Token: 0x06000635 RID: 1589 RVA: 0x00012031 File Offset: 0x00010231
	private void Update()
	{
		if (this.PlayAnimationTestKey && Input.GetKeyDown(116))
		{
			this.PlayAnimation(this.playFirstAnimationName, false);
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x000374F0 File Offset: 0x000356F0
	private void Awake()
	{
		if (this.spriteRenderer == null)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (this.spriteRenderer == null && this.image != null)
		{
			this.forUIAnimator = true;
			return;
		}
		if (this.spriteRenderer != null && this.image != null)
		{
			this.useBoth = true;
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00037578 File Offset: 0x00035778
	private void OnEnable()
	{
		base.StartCoroutine("PlayAnimationCoroutine");
		if (this.playFirstAnimation)
		{
			if (this.playFirstAnimationIndex != -1)
			{
				this.PlayAnimation(this.playFirstAnimationIndex);
			}
			else
			{
				this.PlayAnimation(this.playFirstAnimationName, false);
			}
		}
		if (this.PlayRelay && this.currentAnimationIndex != -1)
		{
			this.PlayAnimation(this.currentAnimationIndex);
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00012051 File Offset: 0x00010251
	private IEnumerator PlayAnimationCoroutine()
	{
		for (;;)
		{
			if (this.customAnimations == null)
			{
				yield return null;
			}
			else
			{
				this.PlayNextFrame();
				if (this.isEndFrame)
				{
					this.isEnd = true;
					Action endFrameAction = this.EndFrameAction;
					if (endFrameAction != null)
					{
						endFrameAction();
					}
					if (!this.isLoop)
					{
						this.isLoop = true;
						this.EndAnimationEvent();
					}
				}
				else
				{
					this.isEnd = false;
				}
				float waitTime = 0f;
				while (waitTime < this.currentFrameSpeed && !this.interrupt)
				{
					waitTime += Time.deltaTime * this.adjustSpeed;
					yield return null;
				}
				if (this.interrupt)
				{
					if (this.playInterruptAction)
					{
						this.interruptAction.PlayAction();
						this.playInterruptAction = false;
					}
					this.interrupt = false;
				}
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00012060 File Offset: 0x00010260
	public void PlayAnimation(string AnimationName, int AnimationIndex)
	{
		if (AnimationIndex != -1 && this.customAnimations.animations[AnimationIndex].Key.Equals(AnimationName))
		{
			this.SetCurrentState(0, AnimationIndex, this.customAnimations.animations[AnimationIndex].frameSpeed);
			return;
		}
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x000375DC File Offset: 0x000357DC
	public virtual void PlayAnimation(string AnimationName, bool IndexFollowing = false)
	{
		int num = 0;
		if (IndexFollowing)
		{
			num = this.currentSpriteIndex;
		}
		foreach (var <>f__AnonymousType in this.customAnimations.animations.Select((CustomAnimation value, int index) => new { value, index }))
		{
			CustomAnimation value2 = <>f__AnonymousType.value;
			if (value2.Key.Equals(AnimationName))
			{
				this.SetCurrentState(num, <>f__AnonymousType.index, value2.frameSpeed);
				break;
			}
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0001209B File Offset: 0x0001029B
	public void PlayAnimation(int AnimationIndex)
	{
		if (this.customAnimations.animations.Length <= AnimationIndex)
		{
			return;
		}
		this.SetCurrentState(0, AnimationIndex, this.customAnimations.animations[AnimationIndex].frameSpeed);
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x000120C8 File Offset: 0x000102C8
	private void SetCurrentState(int spriteIndex, int animationIndex, float frameSpeed)
	{
		if (this.canChangeAnimation)
		{
			this.interrupt = true;
			this.currentSpriteIndex = spriteIndex;
			this.currentAnimationIndex = animationIndex;
			this.currentFrameSpeed = frameSpeed;
			this.currentAnimationName = this.customAnimations.animations[animationIndex].Key;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00037684 File Offset: 0x00035884
	private void PlayNextFrame()
	{
		if (this.forUIAnimator || this.useBoth)
		{
			this.image.sprite = this.customAnimations.animations[this.currentAnimationIndex].sprites[this.currentSpriteIndex];
		}
		if (!this.forUIAnimator || this.useBoth)
		{
			this.spriteRenderer.sprite = this.customAnimations.animations[this.currentAnimationIndex].sprites[this.currentSpriteIndex];
		}
		this.currentSpriteIndex++;
		this.isEndFrame = false;
		if (this.currentSpriteIndex >= this.customAnimations.animations[this.currentAnimationIndex].sprites.Length)
		{
			this.currentSpriteIndex = 0;
			this.isEndFrame = true;
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void EndAnimationEvent()
	{
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00012106 File Offset: 0x00010306
	public void SetLoop(bool loop)
	{
		this.isLoop = loop;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0001210F File Offset: 0x0001030F
	public void Interrupting()
	{
		this.interrupt = true;
		this.playInterruptAction = true;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00037748 File Offset: 0x00035948
	public void AlwaysLook(CustomAnimator.LookWay way)
	{
		switch (way)
		{
		case CustomAnimator.LookWay.None:
			this.ignoreFlip = false;
			return;
		case CustomAnimator.LookWay.Left:
			this.ignoreFlip = true;
			this.spriteRenderer.flipX = false;
			this.isRight = false;
			return;
		case CustomAnimator.LookWay.Right:
			this.ignoreFlip = true;
			this.spriteRenderer.flipX = true;
			this.isRight = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x000377A8 File Offset: 0x000359A8
	public void FlipSprite(bool value)
	{
		if (this.ignoreFlip)
		{
			return;
		}
		if (this.spriteRenderer != null)
		{
			this.spriteRenderer.flipX = value;
			this.isRight = !value;
		}
		if (this.image != null)
		{
			Vector3 localScale = this.image.transform.localScale;
			float num = Mathf.Abs(localScale.x);
			localScale.x = (value ? (-num) : num);
			this.isRight = value;
			this.image.transform.localScale = localScale;
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001211F File Offset: 0x0001031F
	public virtual void SetAnimationCanChange(bool value)
	{
		this.canChangeAnimation = value;
	}

	// Token: 0x040006C9 RID: 1737
	public SpriteRenderer spriteRenderer;

	// Token: 0x040006CA RID: 1738
	public Image image;

	// Token: 0x040006CB RID: 1739
	public string currentAnimationName = "";

	// Token: 0x040006CC RID: 1740
	public int currentAnimationIndex;

	// Token: 0x040006CD RID: 1741
	public int currentSpriteIndex;

	// Token: 0x040006CE RID: 1742
	public float currentFrameSpeed;

	// Token: 0x040006CF RID: 1743
	public bool interrupt;

	// Token: 0x040006D0 RID: 1744
	public bool canInterrupt;

	// Token: 0x040006D1 RID: 1745
	public bool playInterruptAction;

	// Token: 0x040006D2 RID: 1746
	public CustomAnimations customAnimations;

	// Token: 0x040006D3 RID: 1747
	public InterruptAction interruptAction;

	// Token: 0x040006D4 RID: 1748
	public bool playFirstAnimation;

	// Token: 0x040006D5 RID: 1749
	public int playFirstAnimationIndex = -1;

	// Token: 0x040006D6 RID: 1750
	public string playFirstAnimationName;

	// Token: 0x040006D7 RID: 1751
	public bool isLoop;

	// Token: 0x040006D8 RID: 1752
	public bool isEndFrame;

	// Token: 0x040006D9 RID: 1753
	public bool forUIAnimator;

	// Token: 0x040006DA RID: 1754
	public bool canChangeAnimation = true;

	// Token: 0x040006DB RID: 1755
	public float adjustSpeed = 1f;

	// Token: 0x040006DC RID: 1756
	public bool isPlay;

	// Token: 0x040006DD RID: 1757
	public bool isEnd;

	// Token: 0x040006DE RID: 1758
	public bool useBoth;

	// Token: 0x040006DF RID: 1759
	public bool PlayRelay;

	// Token: 0x040006E0 RID: 1760
	public bool PlayAnimationTestKey;

	// Token: 0x040006E1 RID: 1761
	public Action EndFrameAction;

	// Token: 0x040006E2 RID: 1762
	public bool ignoreFlip;

	// Token: 0x040006E3 RID: 1763
	public bool isRight;

	// Token: 0x02000102 RID: 258
	public enum LookWay
	{
		// Token: 0x040006E5 RID: 1765
		None,
		// Token: 0x040006E6 RID: 1766
		Left,
		// Token: 0x040006E7 RID: 1767
		Right
	}
}
