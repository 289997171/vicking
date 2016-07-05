
using System.Collections.Generic;

public interface OnPersonDie
{
    void onPersonDie(Person person, Person killer, params object[] args);
}

public class PersonDieScript : NormalSingleton<PersonDieScript>, IManager, OnPersonDie
{

    private List<OnPersonDie> onPersonDies = new List<OnPersonDie>();

    public bool Init()
    {
        onPersonDies.Add(new PlayerDie1());
        onPersonDies.Add(new PlayerDie2());
        onPersonDies.Add(new MonsterDie1());
        onPersonDies.Add(new MonsterDie2());
        return true;
    }

    public void onPersonDie(Person person, Person killer, params object[] args)
    {
        foreach (OnPersonDie onPersonDie in onPersonDies)
        {
            onPersonDie.onPersonDie(person, killer, args);
        }
    }
}


 class PlayerDie1 : OnPersonDie
{
    public void onPersonDie(Person person, Person killer, params object[] args)
    {
        if (person.personType != Person.PERSON_PLAYER)
        {
            return;
        }

        Player player = person as Player;


        // PlayerManager.Instance.xxxx
    }
}

 class PlayerDie2 : OnPersonDie
{
     public void onPersonDie(Person person, Person killer, params object[] args)
     {
     }
}

class MonsterDie1 : OnPersonDie
{
    public void onPersonDie(Person person, Person killer, params object[] args)
    {
    }
}

class MonsterDie2 : OnPersonDie
{
    public void onPersonDie(Person person, Person killer, params object[] args)
    {
    }
}