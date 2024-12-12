using System;

public class CharaterData
{
    public string Name { get; set; }
    public byte strength { get; set; }
    public int hp { get; set; }
}

public class Character
{
    public string name;
    public byte strength;
    protected int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
    }

    public bool isAlive = true;     // 살아있는지 죽었는지 판단하기 위한 멤버변수

    public Character(string name, byte strength, int hp)
    {
        this.name = name;
        this.strength = strength;
        this._hp = hp;
    }

    // 공격 루틴을 구현할 가상 함수
    public virtual void Attack(Character targetCharacter)
    {
    }

    // 정포 표시를 위한 가상 함수
    public virtual void ShowInfo()
    {
    }

    // 캐릭터의 대미지 계산
    public virtual void GetDamage(int dmg)
    {
        _hp -= dmg;
        if (_hp <= 0)
        {
            _hp = 0;
        }
    }

    // 죽는 상태 처리
    public void Die()
    {
        isAlive = false;
    }
}