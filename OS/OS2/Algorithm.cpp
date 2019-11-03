#include <iostream>
#include <thread>
#include <map>
#include <string>
#include <vector>
#include <set>

// to denote which thread will enter next 
int favouredthread = 1;
// flags to indicate if each thread is in 
// queue to enter its critical section 
bool thread1wantstoenter = false;
bool thread2wantstoenter = false;
bool canBeChange = false;
bool wantInputValue = true;

//CommonResource
std::map<int, int> mapOfCoin;
std::map<int, int> result;
int inputValue;
std::set<int> wantedCoin;

void CheckMoney()
{
    int bufString;
    do {

        thread1wantstoenter = true;

        // entry section 
        // wait until thread2 wants to enter 
        // its critical section 
        while (thread2wantstoenter == true) {

            // if 2nd thread is more favored 
            if (favouredthread == 2) {

                // gives access to other thread 
                thread1wantstoenter = false;

                // wait until this thread is favored 
                while (favouredthread == 2)
                {
                    //std::cout << "CheckMoney wait\n";
                }


                thread1wantstoenter = true;
            }
        }

        if (wantInputValue)
        {
            std::cout << "\nwe have:\n";
            for (auto it : mapOfCoin)
            {
                std::cout << it.first << ": " << it.second << "\n";
            }
            std::cout << "\ninput your coin: ";
            std::cin >> inputValue;
            std::cout << "\nyou coin: " << inputValue;
            std::cout << "\ninput wanted coin: ";
            while (std::cin >> bufString)
            {
                wantedCoin.insert(bufString);
            }
            std::cout << "\nyou wanted coin: ";
            for (auto it : wantedCoin)
            {
                std::cout << it << " ";
            }
            wantInputValue = false;
            canBeChange = true;
        }

        // critical section 
        // favor the 2nd thread 
        favouredthread = 2;
        // exit section 
        // indicate thread1 has completed 
        // its critical section 
        thread1wantstoenter = false;

        

    } while (true);
}

void CoinChange()
{

    do {

        thread2wantstoenter = true;

        // entry section 
        // wait until thread1 wants to enter 
        // its critical section 
        while (thread1wantstoenter == true) {

            // if 1st thread is more favored 
            if (favouredthread == 1) {

                // gives access to other thread 
                thread2wantstoenter = false;

                // wait until this thread is favored 
                while (favouredthread == 1)
                {
                    //std::cout << "CoinChange wait\n";
                }

                thread2wantstoenter = true;
            }
        }
        if (canBeChange)
        {
            ++mapOfCoin[inputValue];
            
        }
        // critical section 

        // favour the 1st thread 
        favouredthread = 1;

        // exit section 
        // indicate thread2 has completed 
        // its critical section 
        thread2wantstoenter = false;

        // remainder section 

    } while (true);
}


void Algorithm()
{
    mapOfCoin[1] = 10;
    mapOfCoin[2] = 10;
    mapOfCoin[5] = 10;
    mapOfCoin[10] = 10;
    mapOfCoin[25] = 10;
    mapOfCoin[50] = 10;
    mapOfCoin[100] = 0;
    std::thread first(CheckMoney);
    std::thread second(CoinChange);
    std::cout << "START\n";

    first.join();
    second.join();

    std::cout << "END\n";
}
