using System.Collections.Generic;
using System.Linq;

namespace PokerHands{
	public class HandRanker{
		private readonly Hand player1Hand;
		private readonly Hand player2Hand;
		public bool isHand1AStraight;
		public bool isHand2AStraight;
		private int lowestHand1Value;
		private int lowestHand2Value;
		private readonly PairDetector _pairDetector;
		private readonly TwoPairDetector _twoPairDetector;
		private readonly ThreeOfAKindDetector _threeOfAKindDetector;
		private readonly StraightDetector _straightDetector;
		private readonly FlushDetector _flushDetector;
		private readonly FullHouseDetector _fullHouseDetector;
		private readonly FourOfAKindDetector _fourOfAKindDetector;
		private readonly StraightFlushDetector _straightFlushDetector;

		public HandRanker(List<Card> hand1, List<Card> hand2){
			player1Hand = new Hand(hand1);
			player2Hand = new Hand(hand2);
			_pairDetector = new PairDetector(this);
			_twoPairDetector = new TwoPairDetector(this);
			_threeOfAKindDetector = new ThreeOfAKindDetector(this);
			_straightDetector = new StraightDetector(this);
			_flushDetector = new FlushDetector(this);
			_fullHouseDetector = new FullHouseDetector(this);
			_fourOfAKindDetector = new FourOfAKindDetector(this);
			_straightFlushDetector = new StraightFlushDetector(this);
		}

		public Hand Player1Hand{
			get { return player1Hand; }
		}

		public Hand Player2Hand{
			get { return player2Hand; }
		}

		public int GetWinner(){
			CheckIfHand1IsAStraight();
			CheckIfHand2IsAStraight();

			int winner;
			if (_straightFlushDetector.DetermineIfPlayerHasStraightFlush(out winner)) return winner;
			if (_fourOfAKindDetector.DetermineIfPlayerHasFourOfAKind(out winner)) return winner;
			if (_fullHouseDetector.DetermineIfPlayerHasFullHouse(out winner)) return winner;
			if (_flushDetector.DetermineIfPlayerHasFlush(out winner)) return winner;
			if (_straightDetector.DetermineIfPlayerHasStraight(out winner)) return winner;
			if (_threeOfAKindDetector.DetermineIfPlayerHasThreeOfAKind(out winner)) return winner;
			if (_twoPairDetector.DetermineIfPlayerHasTwoPairs(out winner)) return winner;
			if (_pairDetector.DetermineIfPlayerHasAPair(out winner)) return winner;
			return GetPlayerWithLargestHand(player1Hand, player2Hand);
		}

		public void CheckIfHand2IsAStraight()
		{
			lowestHand2Value = player2Hand.ValueCountList.OrderBy(i => (int) i.Value).Select(i => (int) i.Value).FirstOrDefault();

			isHand2AStraight = player2Hand.ValueCountList.Count == 5;

			if (player2Hand.ValueCountList.All(i => i.Count > 0 && i.Count < 2)){
				if (player2Hand.ValueCountList.Any(i => i.Value == CardValue.Ace)){
					for (var i = 0; i < 4; i++){
						if (player2Hand.ValueCountList.Any(v => (int) v.Value == (i))) ;
						else{
							isHand2AStraight = false;

							break;
						}
					}
				}
				else{
					foreach (var value in player2Hand.ValueCountList){
						if (player2Hand.ValueCountList.Any(v => (int) v.Value == (lowestHand2Value)))
							lowestHand2Value = lowestHand2Value + 1;
						else{
							isHand2AStraight = false;

							break;
						}
					}
				}
			}
		}

		public void CheckIfHand1IsAStraight()
		{
			lowestHand1Value = player1Hand.ValueCountList.OrderBy(i => (int) i.Value).Select(i => (int) i.Value).FirstOrDefault();
			isHand1AStraight = player1Hand.ValueCountList.Count == 5;

			if (isHand1AStraight){
				if (player1Hand.ValueCountList.Any(i => i.Value == CardValue.Ace)){
					for (var i = 0; i < 4; i++){
						if (player1Hand.ValueCountList.Any(v => (int) v.Value == (i))) ;
						else{
							isHand1AStraight = false;

							break;
						}
					}
				}
				else{
					for (var i = 0; i < player1Hand.ValueCountList.Count; i++){
						if (player1Hand.ValueCountList.Any(v => (int) v.Value == (lowestHand1Value)))
							lowestHand1Value = lowestHand1Value + 1;
						else{
							isHand1AStraight = false;

							break;
						}
					}
				}
			}
		}

		public static int GetPlayerWithLargestHand(Hand player1Hand, Hand player2Hand)
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