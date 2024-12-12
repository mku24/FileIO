// 현재는 몬스터의 파티가 무조건 3마리로 구현되어 있습니다.
// 우리가 배운 List를 사용해서 여러분들이 원하는 만큼의 최대 마리수를 정하여
// 몬스터 파티의 몬스터 수가 랜덤하게 등장하도록 바꿔 보세요

using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace MiniBattleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int finalScore = 0;
            int slavedMonsterCount = 0;

            
            StreamReader charData = new StreamReader("CharData.json");
            string jsonString = charData.ReadToEnd();
            CharaterData jsonChar = JsonSerializer.Deserialize<CharaterData>(jsonString);



            Player player = new Player("용사 최진영", 10, 200);
            
            List<Monster> monsters = new List<Monster>(); // 현재 크기가 0

            // Battle
            while(true)
            {
                if (player.isAlive == false)
                {
                    // 점수 표시
                    Console.WriteLine("{0}(은)는 죽었습니다. 당신의 업적은 기억될 것입니다.", player.name);
                    Console.WriteLine("{0}(이)가 쓰러트린 몬스터는 모두 {1}마리 입니다.", player.name, slavedMonsterCount);
                    Console.WriteLine("최종점수 : {0}", finalScore);

                    // 게임 종료
                    return;
                }

                Console.WriteLine("===============================================================");

                Random random = new Random(); 

                player.Heal();

                int spawnCount = random.Next(1, 5);
                monsters.Clear();
                // 몬스터 파티 생성
                for (int i = 0; i < spawnCount; ++i)
                {
                    int strength = random.Next(3, 5);
                    int health = random.Next(20, 30);

                    Monster monster = new Monster("몬스터" + (slavedMonsterCount + 1 + i), (byte)strength, health);
                    monsters.Add(monster);
                }

                Console.WriteLine("{0}(은)는 새로운 몬스터 파티와 마주쳤습니다", player.name);
                // 플레이어와 몬스터 정보 표시
                player.ShowInfo();
                player.ChargeSkill();

                for (int i = 0; i < monsters.Count; ++i)
                {
                    monsters[i].ShowInfo();
                }
                
                while (CheckMonstersAlive(monsters) && player.isAlive)
                {
                    // 입력대기 처리
                    Console.Write("{0}(이)가 공격할 차례입니다.", player.name);
                    
                    Console.ReadLine();
                    Console.WriteLine();

                    string input = null;
                    int inputButffer = 1;
                    // 플레이어 공격
                    if (player.canUseSkill)
                    {
                        while (true)
                        {
                            Console.WriteLine("공격 방법을 정해주세요.");
                            Console.Write("1. 일반공격\t2.회전베기");
                            input = Console.ReadLine();

                            if (int.TryParse(input, out inputButffer) == false)
                            {
                                Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (inputButffer == 2)
                    { 
                        player.SpinningSlash(monsters);
                    }
                    else
                    {
                        while (true)
                        {
                            if (monsters.Count > 1)
                            {
                                Console.WriteLine("공격을 할 몬스터를 정해주세요 1~{0} : ", monsters.Count);
                                input = Console.ReadLine();
                                if (input != null && int.TryParse(input, out inputButffer))
                                {
                                    if (inputButffer < 1 || inputButffer > monsters.Count)
                                    {
                                        Console.WriteLine("알맞은 범위의 숫자를 입력하세요");
                                        continue;
                                    }

                                    player.Attack(monsters[inputButffer - 1]);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                                }
                            }
                            else
                            {
                                player.Attack(monsters[0]);
                                break;
                            }
                        }   
                    }

                    // 공격후 플레이어와 몬스터의 정보 표시
                    player.ShowInfo();
                    for (int i = 0; i < monsters.Count; ++i)
                    {
                        monsters[i].ShowInfo();
                        if (monsters[i].isAlive == true && monsters[i].hp <= 0)
                        {
                            monsters[i].Die();
                            Console.WriteLine("{0}을 쓰러트렸습니다.", monsters[i].name);
                            Console.WriteLine("100점의 점수를 얻었습니다.");
                            finalScore += 100;
                            slavedMonsterCount++;
                        }
                    }

                    // 입력대기
                    for(int i = 0;i < monsters.Count; ++i)
                    {
                        if (monsters[i].isAlive == false)
                        {
                            continue;
                        }

                        Console.Write("{0}(이)가 공격할 차례입니다. 엔터키를 누르시면 전투를 진행합니다.", monsters[i].name);
                        Console.ReadLine();
                        Console.WriteLine();

                        // 몬스터 공격
                        monsters[i].Attack(player);

                        player.ShowInfo();
                        if (player.hp <= 0)
                        {
                            player.Die();
                            break;
                        }
                    }
                }
            }
        }

        static bool CheckMonstersAlive(List<Monster> monsters)
        {
            for(int i = 0; i < monsters.Count; ++i)
            {
                if(monsters[i].isAlive)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
