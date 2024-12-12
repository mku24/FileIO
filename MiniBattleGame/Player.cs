using System;

public class Player : Character
{
    private bool _canUseSKill = true;

    private int maxHP;
    public bool canUseSkill
    {
        get
        {
            return _canUseSKill;
        }
    }

    public Player(string name, byte strength, int hp) : base(name, strength, hp)
    {
        maxHP = hp;
    }

    // 정보 표시 가상 함수 재정의
    public override void ShowInfo()
    {
        Console.WriteLine("-------------- 플레이어 상태 --------------");
        Console.WriteLine("이름 : {0}", name);
        Console.WriteLine("힘   : {0}", strength);
        Console.WriteLine("체력 : {0}", _hp);
        Console.WriteLine("-------------------------------------------");
    }

    public override void GetDamage(int dmg)
    {
        base.GetDamage(dmg);
    }

    // 플레이어의 공격 루틴
    public override void Attack(Character targetCharacter)
    {
        // 기본 데미지는 힘 수치 대로
        int dmg = strength;

        // 두배 대미지 확률 결정할 주사위 생성
        Random dice = new Random();

        // 주사위 굴림
        int result = dice.Next(0, 9);

        // 0이 나오면
        if (result == 0)
        {
            // 두배의 대미지
            dmg *= 2;

            // 회심의 일격 화면 출력
            Console.WriteLine("{0} 회심의 일격!!!!", name);
        }

        // 대미지 수치 표시
        Console.WriteLine("{0}(이)가 {1}에게 공격해 {2} 대미지를 입혔습니다.", name, targetCharacter.name, dmg);

        // 타겟 캐릭터 대미지 실제 처리
        targetCharacter.GetDamage(dmg);

        // 타겟 캐릭터가 죽었다면 2번째 공격 확률 계산 할 필요 없이 바로 메서드 종료
        if (targetCharacter.isAlive == false)
        {
            return;
        }

        // 두번째 공격 확률 계산을 위한 주사위 굴림
        result = dice.Next(0, 9);
        if (result == 0)
        {
            // 이하 위의 대미지 계산과 동일 루틴
            dmg = strength;
            result = dice.Next(0, 9);
            if (result == 0)
            {
                dmg *= 2;
                Console.WriteLine("{0} 회심의 일격!!!!", name);
            }
            Console.WriteLine("{0}(이)가 빈틈을 발견해 추가로 공격기회를 얻었습니다.");
            Console.WriteLine("{0}(이)가 {1}에게 공격해 {2} 대미지를 입혔습니다.", name, targetCharacter.name, dmg);
            targetCharacter.GetDamage(dmg);
        }
    }

    public void SpinningSlash(List<Monster> targetCharaters)
    {
        _canUseSKill = false;
        int iDmg = 0;
        for (int i = 0; i < targetCharaters.Count; ++i)
        {
            float dmg = strength * 1.5f;
            iDmg = Convert.ToInt32(dmg);
            targetCharaters[i].GetDamage(iDmg);
        }
        Console.WriteLine("{0}(이)가 모든 몬스터에게 공격해 {1} 대미지를 입혔습니다.", name, iDmg);
        Console.WriteLine();
    }

    public void ChargeSkill()
    {
        Console.WriteLine("회전 베기 스킬이 사용 가능하게 되었습니다.");
        _canUseSKill = true;
    }
    public void Heal()
    {
        if (_hp != maxHP)
        {
            int healPoint = Convert.ToInt32(maxHP * 0.5f);
            Console.WriteLine("++++{0}(은)는 {1}의 체력을 회복했습니다.++++", name, healPoint);
            _hp += Convert.ToInt32(maxHP * 0.5f);
            if (_hp > maxHP)
            {
                _hp = maxHP;
            }
        }
    }
}