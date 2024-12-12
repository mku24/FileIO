using System;

public class Monster : Character
{
    public Monster(string name, byte strength, int hp) : base(name, strength, hp)
    {
    }

    // 몬스터의 공격 루틴
    public override void Attack(Character targetCharacter)
    {
        // 20퍼센트 확률로 회심의 일격 대미지 루틴
        int dmg = strength;
        Random dice = new Random();
        int result = dice.Next(0, 9);
        if (result >= 8)                // 확률만 플레이어와 다름
        {
            dmg *= 2;
            Console.WriteLine("{0} 회심의 일격!!!!", name);
        }
        Console.WriteLine("{0}(이)가 {1}에게 공격해 {2} 대미지를 입혔습니다.", name, targetCharacter.name, dmg);
        targetCharacter.GetDamage(dmg);

        // 몬스터는 두번째 공격이 없음
    }

    // 정보 표시 가상 함수 재정의
    public override void ShowInfo()
    {
        Console.WriteLine("--------------- 몬스터 상태 ---------------");

        if (hp > 0)
        {
            Console.WriteLine("이름 : {0}", name);
            Console.WriteLine("힘   : {0}", strength);
            Console.WriteLine("체력 : {0}", _hp);
        }
        else
        {
            Console.WriteLine("{0}는 죽었습니다.", name);
        }
        Console.WriteLine("-------------------------------------------");
    }
}
