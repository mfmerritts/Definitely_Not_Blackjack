using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3450
{
    public class BlackjackTable
    {
        public int game_Id;
        public int round_Count;
        public List<Card> deck;
        public Player[] table;
        public Dealer dealer;
        public int min_Bet;

        public BlackjackTable()
        {
            game_Id = 666;
            round_Count = 0;
            deck = new List<Card>();
            table = new Player[5];
            dealer = new Dealer("Dealer", 888);
            min_Bet = 10;      
        }

        public void create_Deck(int x)
        {
            //int[] scores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            char[] ranks = { 'a', '2', '3', '4', '5', '6', '7', '8', '9', '1', 'j', 'q', 'k' };
            string[] suits = { "heart", "diamond", "club", "spade" };

            for (int j = 0; j < x; j++)
            {
                for (int i = 0; i < 4; i++)
                {

                    for (int y = 0; y < 13; y++)
                    {
                        Card card_To_Add = new Card(ranks[y], suits[i]);
                        deck.Add(card_To_Add);
                    }
                }
            }
        }

        public void print_Deck()
        {
            //while(deck.Count != 0)
            //{
            //    Card test = deck[deck.Count - 1];
            //    deck.RemoveAt(deck.Count() - 1);
            //    Console.WriteLine(test.get_Rank() + " " + test.get_Suit() + " " + test.get_Score());  
            //}

            for (int x = 0; x < deck.Count(); x++)
            {
                Console.WriteLine(deck[x].get_Rank() + ":" + deck[x].get_Suit());
            }
        }

        public void add_Player(Player player)
        {
            Boolean found = false;
            for(int i = 0; i < 5; i++)
            {
                if (table[i] == null && found == false)
                {
                    table[i] = player;
                    found = true;                   
                }
            }

            if (found == false)
            {
                Console.WriteLine("Sorry " + player.get_Name() + ", this table is full.");
            }
        }

        public void remove_Player(Player player)
        {
            Boolean found_Player = false;
            for(int i = 0; i < 5; i++)
            {
                if (table[i] != null)
                {
                    if (table[i].get_Name() == player.get_Name())
                    {
                        table[i] = null;
                        found_Player = true;
                    }
                }                            
            }
            if (!found_Player) { Console.WriteLine(player.get_Name() + " was not found in the table."); }
        }

        public void print_Table()
        {
            for(int i = 0; i < table.Count(); i++)
            {
                if (table[i] != null)
                {
                    Console.WriteLine(table[i].get_Name() + " is sitting at seat " + (i + 1));
                }
                
                if(table[i] == null)
                {
                    Console.WriteLine("empty");
                }                
            }
        }

        public void shuffle_deck()
        {
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        public void deal_Card_To_Person(int x)
        {
            if (x == 5)
            {
                if (deck.Count() > 0)
                {
                    Card tmp = deck[deck.Count() - 1];
                    dealer.hand.Add(tmp);
                    deck.RemoveAt(deck.Count() - 1);
                }

            }
            else if (x < 5 && x >= 0)
            {
                if (deck.Count() > 0)
                {
                    Card tmp = deck[deck.Count() - 1];
                    table[x].hand.Add(tmp);
                    deck.RemoveAt(deck.Count() - 1);
                }
            }
            else
            {
                Console.WriteLine("Invalid arguments for function deal_Card_To_Player");
            }
        }

        public int get_Round_Count()
        {
            return round_Count;
        }

        public void increment_Count()
        {
            round_Count++;
        }

        public void reset_Count()
        {
            round_Count = 0;
        }

        public int get_Game_Id()
        {
            return game_Id;
        }

        public void set_Game_Id(int x)
        {
            game_Id = x;
        }

        public void start_Round()
        {
            increment_Count();

            for(int i = 0; i < table.Count(); i++)
            {
                if (table[i] != null)
                {
                    if (table[i].get_Bet() >= min_Bet) // this is for testing purpose only. Min bet will be changed in final version
                    {
                        //Sets player to active, deals cards and prints the intial hand.
                        table[i].activate();
                        //deal_Card_To_Person(i);
                        //deal_Card_To_Person(i);
                        table[i].print_Hand();
                        Console.WriteLine("total is " + table[i].sum_Hand());

                        //Asks the player if they wish to surrender
                        string ans1;
                        Boolean surrendered = false;
                        Console.WriteLine("Would you like to surrender?");
                        ans1 = Console.ReadLine();

                        switch(ans1)
                        {
                            case "yes":
                                table[i].deactivate();
                                table[i].give_Money(table[i].get_Bet() / 2);
                                surrendered = true;
                                break;
                            case "no":
                                break;
                        }                                      

                        //Continues here if player did not surrender
                        if (!surrendered)
                        {

                            //Asks player if they want to split hand
                            Boolean has_split = false;
                            if (table[i].hand[0].get_Rank() == table[i].hand[1].get_Rank())
                            {                                
                                Console.WriteLine("Would you like to split");
                                string ans3 = Console.ReadLine();
                                if(ans3 == "yes")
                                {
                                    has_split = true;
                                    table[i].check_Split_Hand();
                                    table[i].deal_To_Split(this);
                                    this.deal_Card_To_Person(i);
                                    Console.WriteLine("New Hand:");
                                    table[i].print_Hand();
                                    Console.WriteLine("total is " + table[i].sum_Hand());
                                    Console.WriteLine("Split Hand:");
                                    table[i].print_Split_Hand();
                                    Console.WriteLine("total is " + table[i].sum_Split());
                                    Console.WriteLine("The following questions are regaurding the main hand:");
                                }
                            }

                            //Asks player to double down
                            string ans2;
                            Console.WriteLine("Would you like to doubledown?");
                            ans2 = Console.ReadLine();

                            switch (ans2)
                            {
                                case "yes":
                                    table[i].double_Down();                                    
                                    break;
                                case "no":
                                    break;
                            }

                            //Here is where the player can choose to hit or stay
                            Console.WriteLine("Ok " + table[i].get_Name() + ", what would you like to do?(hit or stay)");
                            string ans;
                            Boolean finish_Turn = false;

                            while (!finish_Turn)
                            {
                                ans = Console.ReadLine();
                                Console.WriteLine(table[i].get_Name() + " has entered " + ans);

                                switch (ans)
                                {
                                    case "hit":
                                        deal_Card_To_Person(i);
                                        if (table[i].sum_Hand() > 21)
                                        {
                                            for (int y = 0; y < table[i].hand.Count(); y++)
                                            {
                                                if (table[i].hand[y].get_Rank() == 'a' && table[i].sum_Hand() > 21)
                                                {
                                                    table[i].hand[y].change_Ace();
                                                }
                                            }
                                        }
                                        if (table[i].sum_Hand() > 21)
                                        {
                                            finish_Turn = true;
                                            table[i].deactivate();
                                            Console.WriteLine("Sorry " + table[i].get_Name() + ", you have bust");
                                        }
                                        table[i].print_Hand();
                                        Console.WriteLine("total is " + table[i].sum_Hand());
                                        break;
                                    case "stay":
                                        finish_Turn = true;
                                        break;
                                    default:
                                        Console.WriteLine("Please try again");
                                        break;
                                }
                            }

                            //Here is where the player makes choices for split hand
                            //Asks player to double down
                            string ans4;
                            Console.WriteLine("Would you like to doubledown the split hand?");
                            ans4 = Console.ReadLine();

                            switch (ans4)
                            {
                                case "yes":                                    
                                    break;
                                case "no":
                                    break;
                            }

                            //Here is where the player can choose to hit or stay
                            Console.WriteLine("Ok " + table[i].get_Name() + ", what would you like to do with split hand?(hit or stay)");
                            string ans5;
                            Boolean finish_Turn_Split = false;
                            while (!finish_Turn_Split)
                            {
                                ans5 = Console.ReadLine();
                                Console.WriteLine(table[i].get_Name() + " has entered " + ans5);

                                switch (ans5)
                                {
                                    case "hit":
                                        table[i].deal_To_Split(this);
                                        if (table[i].sum_Split() > 21)
                                        {
                                            for (int y = 0; y < table[i].second_Hand.Count(); y++)
                                            {
                                                if (table[i].second_Hand[y].get_Rank() == 'a' && table[i].sum_Split() > 21)
                                                {
                                                    table[i].second_Hand[y].change_Ace();
                                                }
                                            }
                                        }
                                        if (table[i].sum_Split() > 21)
                                        {
                                            finish_Turn_Split = true;
                                            //table[i].deactivate();
                                            Console.WriteLine("Sorry " + table[i].get_Name() + ", your split had has bust");
                                        }
                                        table[i].print_Split_Hand();
                                        Console.WriteLine("total is " + table[i].sum_Split());
                                        break;
                                    case "stay":
                                        finish_Turn_Split = true;
                                        break;
                                    default:
                                        Console.WriteLine("Please try again");
                                        break;
                                }
                            }

                            //end of split hand
                        }                       
                    }
                    else
                    {
                        Console.WriteLine("Sorry " + table[i].get_Name() + ", you have not put up a large enough bet.");
                    }
                }
            }

            this.dealer.start_Turn();
        }

        public void check_Players_Vs_Dealer()
        {
            for(int i = 0; i < table.Count(); i++)
            {
                if (table[i] != null)
                {
                    if (table[i].get_Active() == true)
                    {                        
                        if (table[i].sum_Hand() > dealer.sum_Hand() || dealer.sum_Hand() > 21)
                        {
                            Console.WriteLine(table[i].get_Name() + " has won!");
                            table[i].give_Money(table[i].get_Bet() * 2);
                        }
                        else if (table[i].sum_Hand() < dealer.sum_Hand())
                        {
                            Console.WriteLine(table[i].get_Name() + " has lost!");
                        }
                        else
                        {
                            Console.WriteLine(table[i].get_Name() + " tied with dealer");
                            table[i].give_Money(table[i].get_Bet());
                        }
                    }
                }
            }
        }

        public void clean_Up()
        {
            for(int i = 0; i < table.Count(); i++)
            {
                if(table[i] != null)
                {
                    table[i].take_Cards();
                    table[i].set_Bet(0);

                    if(round_Count == 5)
                    {
                        deck.Clear();
                        this.create_Deck(1);
                        this.shuffle_deck();
                    }
                }
            }
        }
    }
}
