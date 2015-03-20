using System.Collections.Generic;
using System.Linq;

namespace PokerHands{
	public class HandRanker{
		private readonly Hand player1Hand;
		private readonly Hand player2Hand;

		public HandRanker(List<Card> hand1, List<Card> hand2){
			player1Hand = new Hand(hand1);
			player2Hand = new Hand(hand2);
		}

		public Hand Player1Hand{
			get { return player1Hand; }
		}

		public Hand Player2Hand{
			get { return player2Hand; }
		}

		public int GetWinner(){
			int winner;
			if (new StraightFlushDetector(this).DetermineIfPlayerHasStraightFlush(out winner)) return winner;
			if (new FourOfAKindDetector(this).DetermineIfPlayerHasFourOfAKind(out winner)) return winner;
			if (new FullHouseDetector(this).DetermineIfPlayerHasFullHouse(out winner)) return winner;
			if (new FlushDetector(this).DetermineIfPlayerHasFlush(out winner)) return winner;
			if (new StraightDetector(this).DetermineIfPlayerHasStraight(out winner)) return winner;
			if (new ThreeOfAKindDetector(this).DetermineIfPlayerHasThreeOfAKind(out winner)) return winner;
			if (new TwoPairDetector(this).DetermineIfPlayerHasTwoPairs(out winner)) return winner;
			if (new PairDetector(this).DetermineIfPlayerHasAPair(out winner)) return winner;
			return GetPlayerWithLargestHand(player1Hand, player2Hand);
		}

		static int GetPlayerWithLargestHand(Hand player1Hand, Hand player2Hand)
		{
			do{
				if (player1Hand.GetLargestCardValue() > player2Hand.GetLargestCardValue()) return 1;
				if (player1Hand.GetLargestCardValue() < player2Hand.GetLargestCardValue()) return 2;

				player1Hand.RemoveLargestCardValue();
				player2Hand.RemoveLargestCardValue();
			} while (player1Hand.HasCards);

			return -1;
		}
	}
}