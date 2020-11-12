using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Com.TheFallenGames.OSA.Demos.Common
{
	/// <summary>
	/// Used for more control than <see cref="Com.TheFallenGames.OSA.Util.ExpandCollapseOnClick"/> offers.
	/// Holds all the required data for animating an item's size. The animation is done manually, using an MonoBehaviour's Update
	/// </summary>
	public class ExpandCollapseAnimationState
	{
		public readonly float expandingStartTime;
		public float initialExpandedAmount;
		public float targetExpandedAmount;
		public float duration;
		public int itemIndex;
		bool _ForcefullyFinished;
		readonly bool _UseUnscaledTime;


		float CurrentAnimationElapsedTime01
		{
			get
			{
				if (_ForcefullyFinished)
					return 1f;

				// Prevent div by zero. Also, no duration means there's no animation over time
				if (duration == 0f)
					return 1f;

				var elapsed01 = (Time - expandingStartTime) / duration;

				if (elapsed01 >= 1f)
					elapsed01 = 1f;

				return elapsed01;
			}
		}

		float CurrentAnimationElapsedTimeSmooth01
		{
			get
			{
				var t = CurrentAnimationElapsedTime01;
				if (t == 1f)
					return t;

				return Mathf.Sqrt(t); // fast-in, slow-out effect
			}
		}

		float Time { get { return _UseUnscaledTime ? UnityEngine.Time.unscaledTime : UnityEngine.Time.time; } }

		/// <summary>
		/// 0 = just getting started, 1 = done
		/// </summary>
		public float CurrentExpandedAmount { get { return Mathf.Lerp(initialExpandedAmount, targetExpandedAmount, CurrentAnimationElapsedTimeSmooth01); } }

		public bool IsDone { get { return CurrentAnimationElapsedTime01 == 1f; } }


		public ExpandCollapseAnimationState(bool useUnscaledTime)
		{
			_UseUnscaledTime = useUnscaledTime;
			expandingStartTime = Time;
		}


		public void ForceFinish()
		{
			_ForcefullyFinished = true;
		}
	}
}