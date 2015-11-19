using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    class Hand
    {
        public List<Card> cards_in_hand;
        public int totalScore;
        public int bet;

        public Hand(int x, List<Card> y)
        {
            bet = x;
            cards_in_hand = y;
            calc_totalScore();
        }

        public int get_totalScore()
        {
            return totalScore;
        }

        public int get_bet()
        {
            return bet;
        }

        public void set_bet(int x)
        {
            bet = x;
        }

        public void calc_totalScore()
        {
            int total = 0;
            cards_in_hand.ForEach(delegate (Card x)
            {
                total += x.get_Score();
            });
            totalScore = total;
        }

        public List<Card> get_Hand()
        {
            return cards_in_hand;
        }

        public void add_card(Card x)
        {
            cards_in_hand.Add(deal_Card_To_Person());
        }

        public void remove_card()
        {
            cards_in_hand.RemoveAt(cards_in_hand.Count - 1);
        }

    }
}
