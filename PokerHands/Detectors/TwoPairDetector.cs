using System.Linq;

namespace PokerHands{
	public class TwoPairDetector{
		private HandRanker _handRanker;
		public TwoPairDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasTwoPairs(out int winner1){
			// One of the hands have 2 pairs
			if (_handRanker.Player1Hand.ValueCountList.Count(i => i.Count == 2) == 2 || _handRanker.Player2Hand.ValueCountList.Count(i => i.Count == 2) == 2){
				if (_handRanker.Player1Hand.ValueCountList.Count(i => i.Count == 2) == 2 && _handRanker.Player2Hand.ValueCountList.Count(i => i.Count == 2) != 2){
					winner1 = 1;
					return true;
				}

				if (_handRanker.Player2Hand.ValueCountList.Count(i => i.Count == 2) == 2 && _handRanker.Player1Hand.ValueCountList.Count(i => i.Count == 2) != 2){
					winner1 = 2;
					return true;
				}

				// Both Have 2 Pair find Highest Pair Value
				var hand1HighestPairValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 2)
					.OrderByDescending(i => i.Value)
					.Select(i => i.Value)
					.FirstOrDefault();
				var hand2HighestPairValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 2)
					.OrderByDescending(i => i.Value)
					.Select(i => i.Value)
					.FirstOrDefault();

				if (hand1HighestPairValue > hand2HighestPairValue){
					winner1 = 1;
					return true;
				}

				if (hand1HighestPairValue < hand2HighestPairValue){
					winner1 = 2;
					return true;
				}

				// Same Highest Pair must compare next
				_handRanker.Player1Hand.RemoveCardsWithCardValue(hand1HighestPairValue);
				_handRanker.Player2Hand.RemoveCardsWithCardValue(hand2HighestPairValue);
			}
			winner1 = 0;
			return false;
		}
	}
}