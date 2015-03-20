using System.Linq;

namespace PokerHands{
	public class ThreeOfAKindDetector{
		private HandRanker _handRanker;
		public ThreeOfAKindDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasThreeOfAKind(out int winner){
			if (_handRanker.Player1Hand.HasThreeOfAKind || _handRanker.Player2Hand.HasThreeOfAKind){
				var hand1ThreeOfAKindValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();
				var hand2ThreeOfAKindValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();

				if (hand1ThreeOfAKindValue > hand2ThreeOfAKindValue){
					winner = 1;
					return true;
				}

				if (hand2ThreeOfAKindValue > hand1ThreeOfAKindValue){
					winner = 2;
					return true;
				}
			}
			winner = 0;
			return false;
		}
	}
}