using System.Linq;

namespace PokerHands{
	public class ThreeOfAKindDetector{
		private HandRanker _handRanker;
		public ThreeOfAKindDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasThreeOfAKind(out int winner1){
			// One of the Hands have 3 of a Kind
			if (_handRanker.Player1Hand.ValueCountList.Any(i => i.Count == 3) || _handRanker.Player2Hand.ValueCountList.Any(i => i.Count == 3)){
				var hand1ThreeOfAKindValue = _handRanker.Player1Hand.ValueCountList.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();
				var hand2ThreeOfAKindValue = _handRanker.Player2Hand.ValueCountList.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();

				if (hand1ThreeOfAKindValue > hand2ThreeOfAKindValue){
					winner1 = 1;
					return true;
				}

				if (hand2ThreeOfAKindValue > hand1ThreeOfAKindValue){
					winner1 = 2;
					return true;
				}
			}
			winner1 = 0;
			return false;
		}
	}
}