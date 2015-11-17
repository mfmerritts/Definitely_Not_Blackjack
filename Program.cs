using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    public class Program
    {
        public static void Main()
        {            
            //This game is complete other than:
            //finding auto winners (i.e. people with blackjacks)
            //Dealing with split hands(currently the game can split cards and dectect when to split, still needs to be check against dealer though)

            BlackjackTable test = new BlackjackTable();

            //test for start_Round()
            test.create_Deck(1);
            test.shuffle_deck();

            Player p1 = new Player("Marshall", 001);
            p1.hand.Add(new Card('5', "diamond"));
            p1.hand.Add(new Card('5', "diamond"));
            //Player p2 = new Player("Patrick", 002);
            //Player p3 = new Player("SpongeBob", 003);

            test.add_Player(p1);
            //test.add_Player(p2);
            //test.add_Player(p3);

            p1.set_Bet(10);
            //p2.set_Bet(3);
            //p3.set_Bet(100);

            test.start_Round();

            test.dealer.auto_Deal(test);
            test.dealer.print_Hand();
            Console.WriteLine("Dealer has a total of " + test.dealer.sum_Hand());

            test.check_Players_Vs_Dealer();

            test.clean_Up();

            Console.WriteLine("\nTest clean_Up()");
            for(int i=0; i < test.table.Count(); i++)
            {
                if (test.table[i] != null)
                {
                    test.table[i].print_Hand();
                    Console.WriteLine(test.table[i].get_Name() + "'s current bet is " + test.table[i].get_Bet());
                }
            }

            ////test to create deck and shuffle
            //test.create_Deck(1);
            //test.shuffle_deck();
            //test.print_Deck();

            ////test to create players
            //Player p1 = new Player("Marshall", 001);
            //Player p2 = new Player("Ethan", 002);
            //Player p3 = new Player("Logan", 003);
            //Player p4 = new Player("Nick", 004);
            //Player p5 = new Player("Scott", 005);
            //Player p6 = new Player("Roman", 006);
            //Player p7 = new Player("LeBron", 007);

            ////test to fill table with players
            //test.print_Table();
            //test.add_Player(p1);
            //test.add_Player(p2);
            //test.add_Player(p3);
            //test.add_Player(p4);
            //test.add_Player(p5);
            //test.add_Player(p6);

            ////test to try and add player to full table
            //test.add_Player(p7);

            ////test for removing player
            //test.print_Table();
            //test.remove_Player(p1);
            //test.print_Table();
            //test.remove_Player(p7);
            //test.add_Player(p7);
            //test.print_Table();
            //test.remove_Player(p7);
            //test.print_Table();

            ////tests to see if players get dealt cards and sums up total of cards
            //test.deal_Card_To_Person(0);
            //test.deal_Card_To_Person(0);
            //p1.print_Hand();
            //Console.WriteLine(p1.get_Name() + " has a total score of " + p1.sum_Hand()); 

            ////tests for spliting hand and dealing to split hand
            //Console.WriteLine();
            //Console.WriteLine("Player hand:");
            //p1.hand.Add(new Card('k', "diamond"));
            //p1.hand.Add(new Card('k', "spade"));
            //p1.print_Hand();
            //p1.check_Split_Hand();
            //p1.deal_To_Split(0, test);
            //test.deal_Card_To_Person(0);
            //Console.WriteLine();
            //Console.WriteLine("Player hand after split:");
            //p1.print_Hand();
            //Console.WriteLine();
            //Console.WriteLine("Player split hand:");
            //p1.print_Split_Hand();

        }
    }
}
