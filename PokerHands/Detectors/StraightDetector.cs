using System.Linq;

namespace PokerHands
{
	public class StraightDetector{
		private HandRanker _handRanker;
		public StraightDetector(HandRanker handRanker){
			_handRanker = handRanker;
		}

		public bool DetermineIfPlayerHasStraight(out int winner1){
			// One of the hands is a straight
			if (_handRanker.Player1Hand.IsAStraight || _handRanker.Player2Hand.IsAStraight){
				if (_handRanker.Player1Hand.IsAStraight && !_handRanker.Player2Hand.IsAStraight){
					winner1 = 1;
					return true;
				}

				if (!_handRanker.Player1Hand.IsAStraight){
					winner1 = 2;
					return true;
				}

				// Both are Straights
				var hand1HighCard = _handRanker.Player1Hand.ValueCountList.OrderByDescending(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();
				var hand2HighCard = _handRanker.Player2Hand.ValueCountList.OrderByDescending(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();

				if (hand1HighCard == 12){
					var lowCard = _handRanker.Player1Hand.ValueCountList.OrderBy(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();

					if (lowCard == 0) hand1HighCard = 3;
				}

				if (hand2HighCard == 12){
					var lowCard = _handRanker.Player2Hand.ValueCountList.OrderBy(i => i.Value).Select(i => (int) i.Value).FirstOrDefault();

					if (lowCard == 0) hand2HighCard = 3;
				}

				if (hand1HighCard > hand2HighCard){
					winner1 = 1;
					return true;
				}

				if (hand1HighCard < hand2HighCard){
					winner1 = 2;
					return true;
				}

				{
					winner1 = -1;
					return true;
				}
			}
			winner1 = 0;
			return false;
		}
	}
}