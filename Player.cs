using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    public class Player : Person
    {
        public int money;
        public int bet;
        public Boolean active;
        public List<Card> second_Hand;

        public Player (string name, int id) : base(name, id)
        {
            money = 1000;
            active = false;
            second_Hand = new List<Card>();
            bet = 0;
        }

        public int get_Money()
        {
            return money;
        }
        public void give_Money(int more)
        {
            money += more;
            Console.WriteLine(this.get_Name() + " was given " + more + " dollars and now has " + this.get_Money());
        }

        public void take_Money(int take)
        {
            if (take > money)
            {
                Console.WriteLine(this.get_Name() + " does not have " + take + " dollars");
            }
            else
            {
                money -= take;
                Console.WriteLine(this.get_Name() + " lost " + take + " dollars and now has " + this.get_Money());
            }
        }

        public Boolean get_Active()
        {
            return active;
        }

        public void activate()
        {
            active = true;
        }

        public void deactivate()
        {
            active = false;
        }

        public void check_Split_Hand()
        {
            if (this.hand.Count() > 0)
            {
                if(this.get_Bet() > money)
                {
                    Console.WriteLine("Sorry " + this.get_Name() + ", you do not have enough money to split.");
                    return;
                }

                if (hand[0].get_Rank() == hand[1].get_Rank())
                {
                    second_Hand.Add(hand[1]);
                    hand.RemoveAt(1);
                }
                else
                {
                    Console.WriteLine("You cannot split this hand");
                }
            }
            else
            {
                Console.WriteLine(this.get_Name() + " does not have any cards to split");
            }
        }  
                      
        public void set_Bet(int x)
        {
            if (x > money)
            {
                Console.WriteLine(this.get_Name() + " does not have enough money to bet");
            }
            else
            {
                money = money - x;
                bet = x;
            }
        } 

        public void double_Down()
        {
            if (bet <= money)
            {
                Console.WriteLine(this.get_Name() + " has doubled the bet from " + bet + " to " + (bet * 2));
                money = money - bet;
                bet = bet * 2;              
            }
            else
            {
                Console.WriteLine("Sorry " + this.get_Name() + ", you do not haven enough to doubledown.");
            }
        }
        
        public int get_Bet()
        {
            return bet;
        }

        public void deal_To_Split(BlackjackTable bjt)
        {
            if (bjt.deck.Count() > 0)
            {
                Card tmp = bjt.deck[bjt.deck.Count() - 1];
                this.second_Hand.Add(tmp);
                bjt.deck.RemoveAt(bjt.deck.Count() - 1);
            }
        }

        public void print_Split_Hand()
        {
            if (this.second_Hand.Count() > 0)
            {
                for (int i = 0; i < this.second_Hand.Count(); i++)
                {
                    Console.WriteLine(second_Hand[i].get_Rank() + " : " + second_Hand[i].get_Suit());
                }
            }
            else
            {
                Console.WriteLine(this.get_Name() + " has no cards in split hand");
            }
        }

        public int sum_Split()
        {
            int sum = 0;
            if(second_Hand.Count() > 0)
            {
                for(int i = 0; i < second_Hand.Count(); i++)
                {
                    sum = second_Hand[i].get_Score() + sum;
                }
            }
            return sum;
        }
    }
}
