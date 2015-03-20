using System.Linq;

namespace PokerHands{
	public class PairDetector{
		private Hand Player1Hand { get; set; }
		private Hand Player2Hand { get; set; }

		public PairDetector(HandRanker handRanker){
			Player1Hand = handRanker.Player1Hand;
			Player2Hand = handRanker.Player2Hand;
		}

		public bool DetermineIfPlayerHasAPair(out int winner){
			// One of the hands have a pair
			if (EitherPlayerHasPair){
				if (Player1Hand.HasPair && !Player2Hand.HasPair)
				{
					winner = 1;
					return true;
				}

				if (!Player1Hand.HasPair && Player2Hand.HasPair){
					winner = 2;
					return true;
				}

				var hand1PairValue = Player1Hand.ValueCountList.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();
				var hand2PairValue = Player2Hand.ValueCountList.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();

				if (hand1PairValue > hand2PairValue){
					winner = 1;
					return true;
				}
				if (hand1PairValue < hand2PairValue){
					winner = 2;
					return true;
				}

				// Remove Pair Values 3 Cards Left
				Player1Hand.RemoveCardsWithCardValue(hand1PairValue);
				Player2Hand.RemoveCardsWithCardValue(hand2PairValue);
			}
			winner = 0;
			return false;
		}

		private bool EitherPlayerHasPair{
			get { return Player1Hand.HasPair || Player2Hand.HasPair; }
		}
	}
}