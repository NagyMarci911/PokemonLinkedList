using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LancoltLista;
using NUnit.Framework;

namespace TestLibary
{
    [TestFixture]
    public class TesterClass
    {
        PokemonLinkedList lista;
        [SetUp]
        public void Init()
        {
            lista = new PokemonLinkedList();
        }

        //Beszuras tesztek
        [TestCase]
        public void EqualsTeszt()
        {
            Pokemon pokemon1 = new Pokemon("Pikachu", Pokemontype.Electric, 31, false, 1, 20);
            Pokemon pokemon2 = new Pokemon("Pikachu2", Pokemontype.Electric, 31, false, 1, 20);
            Assert.That(!(pokemon1.Equals(pokemon2)));
        }
        public void EqualsTeszt2()
        {
           lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 31, false, 1, 20));
           
            Assert.That(lista.head.Equals(new Pokemon("Pikachu", Pokemontype.Electric, 31, false, 1, 20)));
        }
        [TestCase]
        public void UresTorzsBeszuras()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric,20 , false, 1, 40));
            Assert.That(lista.head.Equals(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40)));
        }
        [TestCase]
        public void IsmetloBeszuras()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            Assert.Throws(typeof(IlyenMarLetezikException), () => lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40)));
        }
        [TestCase]
        public void BeszurasSorrend()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 30));
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 60));
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 10));
            bool sorrend = true;
            Pokemon p = lista.head;
            while (p.Stronger!=null)
            {
                if (p.StrengthPoint > p.Stronger.StrengthPoint)
                {
                    sorrend = false;
                }
                p = p.Stronger;
            }
            Assert.That(sorrend);
        }


        // Kereses tesztek
        [TestCase]
        public void UresTorzsKereses()
        {
            Assert.Throws(typeof(NincsIlyenPokemonException), () => lista.Kereses("Pikachu"));
        }
        [TestCase]
        public void VanolyanKereses()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur", Pokemontype.Grass, 30, false, 1, 60));
            Assert.That(lista.Kereses("Diglet").Equals(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30)));
        }


        // Torles tesztek
        [TestCase]
        public void NincsOlyanTorzsTorles()
        {
            Assert.Throws(typeof(NincsIlyenPokemonException), () => lista.Torles("Pikachu"));
            
        }
        
        [TestCase]
        public void TorlesNevAlapjan()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur", Pokemontype.Grass, 30, false, 1, 60));
            lista.Torles("Pikachu");
            Assert.That(lista.head.Equals(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30)));

        }
        [TestCase]
        public void TorlesReferenciaAlapjan()
        {
            Pokemon pokemon1 = new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40);
            lista.Beszuras(pokemon1);
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur", Pokemontype.Grass, 30, false, 1, 60));
            lista.Torles(pokemon1, false);
            Assert.That(lista.head.Equals(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30)));
        }
        [TestCase]
        public void TorlesTartalomAlapjan()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur", Pokemontype.Grass, 30, false, 1, 60));
            lista.Torles(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40), true);
            Assert.That(lista.head.Equals(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30)));
        }



        // Szures tesztek

        [TestCase]
        public void nullSzures()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur", Pokemontype.Grass, 30, false, 1, 60));
            Assert.That(lista.Szures(Pokemontype.Bug).head == null);
        }

        [TestCase]
        public void typeSzures()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));
            PokemonLinkedList lista2 = lista.Szures(Pokemontype.Grass);
            int i = 0;
            Pokemon p = lista2.head;
            while (p!=null)
            {
                i++;
                p = p.Stronger;
            }
            Assert.That(i == 3);
        }

        [TestCase]
        public void nevSzures()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));
            PokemonLinkedList lista2 = lista.Szures("i");
            int i = 0;
            Pokemon p = lista2.head;
            while (p != null)
            {
                i++;
                p = p.Stronger;
            }
            Assert.That(i == 2);
        }

        [TestCase]
        public void StrengthSzures()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));
            PokemonLinkedList lista2 = lista.Szures(35,true);
            int i = 0;
            Pokemon p = lista2.head;
            while (p != null)
            {
                i++;
                p = p.Stronger;
            }
            Assert.That(i == 4);
        }



        //Bejaras tesztek
        [TestCase]
        public void Bejarasteszt()
        {
            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));
            lista.bejarasEvent += (Pokemon p) => p.Name = p.Name + "methodUsed";
            lista.Bejaras();
            bool sikerult = true;
            Pokemon n = lista.head;
            while (n!=null && sikerult)
            {
                if (!n.Name.Contains("methodUsed"))
                {
                    sikerult = false;
                }
                n = n.Stronger;
            }
            Assert.That(sikerult);
            
            
        }

        //Osszetett prog tetelek
        [TestCase]
        public void MetszetTeszt()
        {

            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));

            PokemonLinkedList lista2 = new PokemonLinkedList();
            lista2.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista2.Beszuras(new Pokemon("Eevee", Pokemontype.Normal, 30, true, 1, 40));
            lista2.Beszuras(new Pokemon("Charmander", Pokemontype.Fire, 50, true, 1, 70));
            lista2.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista2.Beszuras(new Pokemon("Diglet2", Pokemontype.Ground, 10, false, 1, 30));

            PokemonLinkedList lista3 = lista.Metszet(lista2);

            PokemonLinkedList lista4 = new PokemonLinkedList();
            lista4.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista4.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));

            Assert.That(lista4.head.Equals(lista3.head) && lista3.Kereses("Diglet").Equals(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30)));
            
        }
        [TestCase]
        public void UnioTeszt()
        {

            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));

            PokemonLinkedList lista2 = new PokemonLinkedList();
            lista2.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista2.Beszuras(new Pokemon("Eevee", Pokemontype.Normal, 30, true, 1, 40));
            lista2.Beszuras(new Pokemon("Charmander", Pokemontype.Fire, 50, true, 1, 70));
            lista2.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista2.Beszuras(new Pokemon("Diglet2", Pokemontype.Ground, 10, false, 1, 30));

            PokemonLinkedList lista3 = lista.Unio(lista2);

            int i = 0;
            Pokemon p = lista3.head;
            while (p!=null)
            {
                i++;
                p = p.Stronger;
            }
            Assert.That(i == 8);

        }
        [TestCase]
        public void KulonbsegTeszt()
        {

            lista.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista.Beszuras(new Pokemon("Balbasaur1", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista.Beszuras(new Pokemon("Balbasaur2", Pokemontype.Grass, 30, false, 1, 60));
            lista.Beszuras(new Pokemon("Balbasaur3", Pokemontype.Grass, 30, false, 1, 60));

            PokemonLinkedList lista2 = new PokemonLinkedList();
            lista2.Beszuras(new Pokemon("Diglet", Pokemontype.Ground, 10, false, 1, 30));
            lista2.Beszuras(new Pokemon("Eevee", Pokemontype.Normal, 30, true, 1, 40));
            lista2.Beszuras(new Pokemon("Charmander", Pokemontype.Fire, 50, true, 1, 70));
            lista2.Beszuras(new Pokemon("Pikachu", Pokemontype.Electric, 20, false, 1, 40));
            lista2.Beszuras(new Pokemon("Diglet2", Pokemontype.Ground, 10, false, 1, 30));

            PokemonLinkedList lista3 = lista.Kulonbseg(lista2);

            int i = 0;
            Pokemon p = lista3.head;
            while (p != null)
            {
                i++;
                p = p.Stronger;
            }
            Assert.That(i == 3);


        }






    }
}
