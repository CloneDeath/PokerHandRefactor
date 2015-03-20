using System.Linq;

namespace PokerHands{
	public class PairDetector{
		private HandRanker _handRanker;
		public PairDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasAPair(out int winner1){
			// One of the hands have a pair
			if (_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 2) || _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 2)){
				if (_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 2) && _handRanker.Player2Hand.ValueCountList.All(i => i.Count != 2)){
					winner1 = 1;
					return true;
				}

				if (_handRanker.Player1Hand.ValueCountList.All(i => i.Count != 2) && _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 2)){
					winner1 = 2;
					return true;
				}

				var hand1PairValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();
				var hand2PairValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();

				if (hand1PairValue > hand2PairValue){
					winner1 = 1;
					return true;
				}
				if (hand1PairValue < hand2PairValue){
					winner1 = 2;
					return true;
				}

				// Remove Pair Values 3 Cards Left
				_handRanker.Player1Hand.RemoveCardsWithCardValue(hand1PairValue);
				_handRanker.Player2Hand.RemoveCardsWithCardValue(hand2PairValue);
			}
			winner1 = 0;
			return false;
		}
	}
}