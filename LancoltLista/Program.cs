using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancoltLista
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    public enum Pokemontype
    {
        Normal,Fire, Water, Grass, Electric, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dark, Dragon, Steel, Fairy
    }
    public class Pokemon
    {
        public string Name { get; set; }
        public Pokemontype  Type { get; set; }
        public int Health { get; set; }
        public bool Rare { get; set; }
        public int  Generation { get; set; }
        public int StrengthPoint { get; set; }
        public Pokemon Stronger { get; set; }

        public Pokemon(string name, Pokemontype type, int health,bool rare,int generation,int strengthPoint)
        {
            Name = name;
            Type = type;
            Health = health;
            Rare = rare;
            Generation = generation;
            StrengthPoint = strengthPoint;
        }
        public override bool Equals(object obj)
        {
            Pokemon ez = this;
            Pokemon masik = (Pokemon)obj;
            return ez.GetHashCode() == masik.GetHashCode();
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Type.GetHashCode() + Health.GetHashCode() + Rare.GetHashCode() + Generation.GetHashCode() + StrengthPoint.GetHashCode();
        }

    }
    public class PokemonLinkedList
    {
        public Pokemon head;
        public void Beszuras(Pokemon pokemon)
        {        
            if (head == null)
            {
                pokemon.Stronger = null;
                head = pokemon;
            }else if (head.StrengthPoint > pokemon.StrengthPoint)
            {
                pokemon.Stronger = head;
                head = pokemon;
            }
            else
            {
                Pokemon p = head;
                Pokemon e = null;
                while (p != null && p.StrengthPoint <= pokemon.StrengthPoint)
                {
                    if (p.StrengthPoint == pokemon.StrengthPoint)
                    {
                        if (p.Equals(pokemon))
                        {
                            throw new IlyenMarLetezikException();
                        }
                    }
                    e = p;
                    p = p.Stronger;
                }             
                if (p == null)
                {
                    pokemon.Stronger = null;
                    e.Stronger = pokemon;
                }
                else
                {
                    pokemon.Stronger = p;
                    e.Stronger = pokemon;
                }
            }
        }

        public Pokemon Kereses(string nev)
        {
            Pokemon p = head;
            while (p != null && p.Name != nev)
            {
                p = p.Stronger;
            }
            if (p!= null)
            {
                return p;
            }
            else
            {
                throw new NincsIlyenPokemonException();
            }
        }

        public void Torles(Pokemon pokemon, bool tartalomszerint)
        {
            Pokemon p = head;
            Pokemon e = null;
            if (!tartalomszerint)
            {
                while (p != null && pokemon != p)
                {
                    e = p;
                    p = p.Stronger;
                }
                if (p != null)
                {
                    if (e == null)
                    {
                        head = p.Stronger;
                    }
                    else
                    {
                        e.Stronger = p.Stronger;
                    }
                }
                else
                {
                    throw new NincsIlyenPokemonException();
                }
            }
            else
            {
                while (p != null && !pokemon.Equals(p))
                {
                    e = p;
                    p = p.Stronger;
                }
                if (p != null)
                {
                    if (e == null)
                    {
                        head = p.Stronger;
                    }
                    else
                    {
                        e.Stronger = p.Stronger;
                    }
                }
                else
                {
                    throw new NincsIlyenPokemonException();
                }
            }
           
        }
        public void Torles(string nev)
        {
            Pokemon e = null;
            Pokemon p = head;
            while (p != null && p.Name != nev)
            {
                e = p;
                p = p.Stronger;
            }
            if (p!=null)
            {
                if (e==null)
                {
                    head = p.Stronger;
                }
                else
                {
                    e.Stronger = p.Stronger;
                }
            }
            else
            {
                throw new NincsIlyenPokemonException();
            }
        }

        public PokemonLinkedList Szures(string nevTartalmaz)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            while (p!=null)
            {
                if (p.Name.Contains(nevTartalmaz))
                {
                    Pokemon masolatPokemon = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(masolatPokemon);
                }
                p = p.Stronger;
            }
            return outp;
        }
        public PokemonLinkedList Szures(int strengthPoint, bool higher)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            while (p != null)
            {
                if (higher && p.StrengthPoint > strengthPoint)
                {
                    Pokemon masolatPokemon = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(masolatPokemon);
                }
                else if (!higher && p.StrengthPoint < strengthPoint)
                {
                    Pokemon masolatPokemon = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(masolatPokemon);
                }
                p = p.Stronger;
            }
            return outp;
        }
        public PokemonLinkedList Szures(Pokemontype type)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            while (p != null)
            {
                if (p.Type == type)
                {
                    Pokemon masolatPokemon = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(masolatPokemon);
                }
                p = p.Stronger;
            }
            return outp;
        }

        public event PokemonLinkedListEvent bejarasEvent;
        public void Bejaras()
        {
            Pokemon p = this.head;
            while (p!=null)
            {
                bejarasEvent?.Invoke(p);
                p = p.Stronger;
            }


        }

        public PokemonLinkedList Metszet(PokemonLinkedList masikLista)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            Pokemon m = masikLista.head;
            while (p!= null && m!= null)
            {
                if (p.Equals(m))
                {
                    Pokemon pokemoncopy = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(pokemoncopy);
                    p = p.Stronger;
                    m = m.Stronger;
                }
                else if (p.StrengthPoint == m.StrengthPoint || p.StrengthPoint > m.StrengthPoint)
                {
                    if (p.StrengthPoint == m.StrengthPoint && p.Stronger.StrengthPoint == m.StrengthPoint)
                    {
                        p = p.Stronger;
                    }
                    else
                    {
                        m = m.Stronger;
                    }

                }
                else
                {
                    p = p.Stronger;
                }
            }
            return outp;
        }

        public PokemonLinkedList Unio(PokemonLinkedList masikLista)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            Pokemon m = masikLista.head;
            while (p!=null)
            {
                try
                {
                    Pokemon pokemoncopy = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(pokemoncopy);
                    p = p.Stronger;                 
                }
                catch (IlyenMarLetezikException e)
                {

                }
                
            }
            while (m!=null)
            {
                try
                {
                    Pokemon pokemoncopy = new Pokemon(m.Name, m.Type, m.Health, m.Rare, m.Generation, m.StrengthPoint);
                    outp.Beszuras(pokemoncopy);
                }
                catch (IlyenMarLetezikException e)
                {

                }
                m = m.Stronger;
            }
            return outp;
        }

        public PokemonLinkedList Kulonbseg(PokemonLinkedList masikLista)
        {
            PokemonLinkedList outp = new PokemonLinkedList();
            Pokemon p = this.head;
            Pokemon m = masikLista.head;
            if (m == null)
            {
                return this;
            }
            else
            {
                while (p!=null && m!=null)
                {
                    if (p.Equals(m))
                    {
                        p = p.Stronger;
                        m = m.Stronger;
                    }
                    else if (p.StrengthPoint < m.StrengthPoint)
                    {

                        Pokemon pokemoncopy = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                        outp.Beszuras(pokemoncopy);
                        p = p.Stronger;
                    }
                    else if (p.StrengthPoint == m.StrengthPoint)
                    {
                        m = m.Stronger;
                    }
                    else
                    {
                        m = m.Stronger;
                    }
                }
                while (p!=null)
                {
                    Pokemon pokemoncopy = new Pokemon(p.Name, p.Type, p.Health, p.Rare, p.Generation, p.StrengthPoint);
                    outp.Beszuras(pokemoncopy);
                    p = p.Stronger;
                }
            }
            return outp;

        }

    }
    public delegate void PokemonLinkedListEvent(Pokemon pokemon);

    public class IlyenMarLetezikException : Exception
    {
        public IlyenMarLetezikException(): base("[HIBA] Ilyen mar letezik!")
        {
                
        }
    }
    public class NincsIlyenPokemonException : Exception
    {
        public NincsIlyenPokemonException():base("A keresett pokemon nem talalhato")
        {

        }
    }

}
