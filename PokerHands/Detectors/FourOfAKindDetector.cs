using System.Linq;

namespace PokerHands
{
	public class FourOfAKindDetector{
		private HandRanker _handRanker;
		public FourOfAKindDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasFourOfAKind(out int winner)
		{
			if (_handRanker.Player1Hand.HasFourOfAKind && !_handRanker.Player2Hand.HasFourOfAKind)
			{
				winner = 1;
				return true;
			}

			if (_handRanker.Player2Hand.HasFourOfAKind && !_handRanker.Player1Hand.HasFourOfAKind)
			{
				winner = 2;
				return true;
			}

			if (_handRanker.Player1Hand.HasFourOfAKind || _handRanker.Player2Hand.HasFourOfAKind)
			{
				var hand1FourOfAKindValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 4).Select(i => (int) i.Value).FirstOrDefault();
				var hand2FourOfAKindValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 4).Select(i => (int) i.Value).FirstOrDefault();

				if (hand1FourOfAKindValue > hand2FourOfAKindValue){
					winner = 1;
					return true;
				}

				if (hand2FourOfAKindValue > hand1FourOfAKindValue){
					winner = 2;
					return true;
				}
			}
			winner = 0;
			return false;
		}
	}
}