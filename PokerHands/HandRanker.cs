using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    public class HandRanker
    {
        public int RankHands(IList<Card> hand1, IList<Card> hand2){
	        Hand player1Hand = new Hand(hand1);
	        Hand player2Hand = new Hand(hand2);

			List<ValueCount> hand1ByValues = player1Hand.GetValueCounts;
	        List<ValueCount> hand2ByValues = player2Hand.GetValueCounts;

            int lowestHand1Value = hand1ByValues.OrderBy(i => (int)i.Value).Select(i => (int)i.Value).FirstOrDefault();

            bool isHand1AStraight = hand1ByValues.Count == 5;

            if (hand1ByValues.Count == 5)
            {
                if (hand1ByValues.Any(i => i.Value == CardValue.Ace))
                {
                    for (var i = 0; i < 4; i++)
                    {
                        if (hand1ByValues.Any(v => (int)v.Value == (i))) ;
                        else
                        {
                            isHand1AStraight = false;

                            break;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < hand1ByValues.Count; i++)
                    {
                        if (hand1ByValues.Any(v => (int)v.Value == (lowestHand1Value))) lowestHand1Value = lowestHand1Value + 1;
                        else
                        {
                            isHand1AStraight = false;

                            break;
                        }
                    }
                }
            }

            var lowestHand2Value = hand2ByValues.OrderBy(i => (int)i.Value).Select(i => (int)i.Value).FirstOrDefault();

            var isHand2AStraight = hand2ByValues.Count == 5;

            if (hand2ByValues.All(i => i.Count > 0 && i.Count < 2))
            {
                if (hand2ByValues.Any(i => i.Value == CardValue.Ace))
                {
                    for (var i = 0; i < 4; i++)
                    {
                        if (hand2ByValues.Any(v => (int)v.Value == (i))) ;
                        else
                        {
                            isHand2AStraight = false;

                            break;
                        }
                    }
                }
                else
                {
                    foreach (var value in hand2ByValues)
                    {
                        if (hand2ByValues.Any(v => (int)v.Value == (lowestHand2Value))) lowestHand2Value = lowestHand2Value + 1;
                        else
                        {
                            isHand2AStraight = false;

                            break;
                        }
                    }
                }
            }

	        var hand1Flush = player1Hand.HasFlush;
	        var hand2Flush = player2Hand.HasFlush;

            if ((hand1Flush && isHand1AStraight) || (hand2Flush && isHand2AStraight))
            {
                if (!(hand1Flush && isHand1AStraight)) return 2;

                if (!(hand2Flush && isHand2AStraight)) return 1;

                var hand1MaxValue = hand1ByValues.OrderByDescending(i => (int)i.Value).Select(i => (int)i.Value).FirstOrDefault();
                var hand2MaxValue = hand2ByValues.OrderByDescending(i => (int)i.Value).Select(i => (int)i.Value).FirstOrDefault();

                if (hand1MaxValue == 12)
                {
                    var lowCard = hand1ByValues.OrderBy(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();

                    if (lowCard == 0) hand1MaxValue = 3;
                }

                if (hand2MaxValue == 12)
                {
                    var lowCard = hand2ByValues.OrderBy(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();

                    if (lowCard == 0) hand2MaxValue = 3;
                }

                if (hand1MaxValue > hand2MaxValue) return 1;

                if (hand1MaxValue < hand2MaxValue) return 2;
            }

            if (hand1ByValues.Any(i => i.Count == 4) || hand2ByValues.Any(i => i.Count == 4))
            {
                if (hand1ByValues.Any(i => i.Count == 4) && hand2ByValues.All(i => i.Count != 4)) return 1;

                if (hand2ByValues.Any(i => i.Count == 4) && hand1ByValues.All(i => i.Count != 4)) return 2;

                var hand1FourOfAKindValue = hand1ByValues.Where(i => i.Count == 4).Select(i => (int)i.Value).FirstOrDefault();
                var hand2FourOfAKindValue = hand2ByValues.Where(i => i.Count == 4).Select(i => (int)i.Value).FirstOrDefault();

                if (hand1FourOfAKindValue > hand2FourOfAKindValue) return 1;

                if (hand2FourOfAKindValue > hand1FourOfAKindValue) return 2;
            }

            if ((hand1ByValues.Any(i => i.Count == 3) && hand1ByValues.Any(i => i.Count == 2)) || (hand2ByValues.Any(i => i.Count == 3) && hand2ByValues.Any(i => i.Count == 2)))
            {
                // Full house
                if ((hand1ByValues.Any(i => i.Count == 3) && hand1ByValues.Any(i => i.Count == 2)) && !(hand2ByValues.Any(i => i.Count == 3) && hand2ByValues.Any(i => i.Count == 2))) return 1;

                if (!(hand1ByValues.Any(i => i.Count == 3) && hand1ByValues.Any(i => i.Count == 2))) return 2;
            }

            if (hand1Flush || hand2Flush)
            {
                if (hand1Flush && !hand2Flush) return 1;

                if (!hand1Flush) return 2;
            }

            // One of the hands is a straight
            if (isHand1AStraight || isHand2AStraight)
            {
                if (isHand1AStraight && !isHand2AStraight) return 1;

                if (!isHand1AStraight) return 2;

                // Both are Straights
                var hand1HighCard = hand1ByValues.OrderByDescending(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();
                var hand2HighCard = hand2ByValues.OrderByDescending(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();

                if (hand1HighCard == 12)
                {
                    var lowCard = hand1ByValues.OrderBy(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();

                    if (lowCard == 0) hand1HighCard = 3;
                }

                if (hand2HighCard == 12)
                {
                    var lowCard = hand2ByValues.OrderBy(i => i.Value).Select(i => (int)i.Value).FirstOrDefault();

                    if (lowCard == 0) hand2HighCard = 3;
                }

                if (hand1HighCard > hand2HighCard) return 1;

                if (hand1HighCard < hand2HighCard) return 2;

                return -1;
            }

            // One of the Hands have 3 of a Kind
            if (hand1ByValues.Any(i => i.Count == 3) || hand2ByValues.Any(i => i.Count == 3))
            {
                var hand1ThreeOfAKindValue = hand1ByValues.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();
                var hand2ThreeOfAKindValue = hand2ByValues.Where(i => i.Count == 3).Select(i => i.Value).FirstOrDefault();

                if (hand1ThreeOfAKindValue > hand2ThreeOfAKindValue) return 1;

                if (hand2ThreeOfAKindValue > hand1ThreeOfAKindValue) return 2;
            }

            // One of the hands have 2 pairs
            if (hand1ByValues.Count(i => i.Count == 2) == 2 || hand2ByValues.Count(i => i.Count == 2) == 2)
            {
                if (hand1ByValues.Count(i => i.Count == 2) == 2 && hand2ByValues.Count(i => i.Count == 2) != 2) return 1;

                if (hand2ByValues.Count(i => i.Count == 2) == 2 && hand1ByValues.Count(i => i.Count == 2) != 2) return 2;

                // Both Have 2 Pair find Highest Pair Value
                var hand1HighestPairValue = hand1ByValues.Where(i => i.Count == 2).OrderByDescending(i => i.Value).Select(i => i.Value).FirstOrDefault();
                var hand2HighestPairValue = hand2ByValues.Where(i => i.Count == 2).OrderByDescending(i => i.Value).Select(i => i.Value).FirstOrDefault();

                if (hand1HighestPairValue > hand2HighestPairValue) return 1;

                if (hand1HighestPairValue < hand2HighestPairValue) return 2;

                // Same Highest Pair must compare next
                hand1ByValues = hand1ByValues.Where(i => i.Value != hand1HighestPairValue).ToList();
                hand2ByValues = hand2ByValues.Where(i => i.Value != hand2HighestPairValue).ToList();
            }

            // One of the hands have a pair
            if (hand1ByValues.Any(i => i.Count == 2) || hand2ByValues.Any(i => i.Count == 2))
            {
                if (hand1ByValues.Any(i => i.Count == 2) && hand2ByValues.All(i => i.Count != 2)) return 1;

                if (hand1ByValues.All(i => i.Count != 2) && hand2ByValues.Any(i => i.Count == 2)) return 2;

                var hand1PairValue = hand1ByValues.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();
                var hand2PairValue = hand2ByValues.Where(i => i.Count == 2).Select(i => i.Value).FirstOrDefault();

                if (hand1PairValue > hand2PairValue) return 1;
                if (hand1PairValue < hand2PairValue) return 2;

                // Remove Pair Values 3 Cards Left
	            player1Hand.RemovePairValue3CardsLeft(hand1PairValue);
				player2Hand.RemovePairValue3CardsLeft(hand2PairValue);
            }

			return GetPlayerWithLargestHand(player1Hand, player2Hand);
        }



		private static int GetPlayerWithLargestHand(Hand player1Hand, Hand player2Hand)
        {
            do
            {
				if (player1Hand.GetLargestCardValue() > player2Hand.GetLargestCardValue()) return 1;
				if (player1Hand.GetLargestCardValue() < player2Hand.GetLargestCardValue()) return 2;

	            player1Hand.RemoveLargestCardValue();
				player2Hand.RemoveLargestCardValue();

            } while (player1Hand.HasCards);

            return -1;
        }

        
    }
}