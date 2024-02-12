using Formatif03B;
namespace UnitFormatif02B
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

   
        [Test]
        public void TestAjouter()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "1,1";
            calcul.ajouter(nombre);
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(2));
        }

        [Test]
        public void TestAjouterAvecSeparateur()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "1,2\n3";
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(6));
        }

        [Test]
        public void TestAjouterValidationAvecDeliminateurFin_doitRenvoy�Erreur()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "1,2,";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { calcul.ajouter(nombre); });
            Assert.That(erreur.Message, Is.EqualTo("La chaine de caract�re ne peut pas finir avec ,"));
        }
        [Test]
        public void TestAjouterAvecD�liminateur_DoitRenvoyer4()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "//;\n1;3";
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(4));
        }
        [Test]
        public void TestAjouterAvecD�liminateur_DoitRenvoyer6()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "//|\n1|2|3";
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(6));
        }
        [Test]
        public void TestAjouterAvecPlusieurD�liminateur_DoitRenvoyer7()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "//sep\n2sep5";
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(7));
        }
        [Test]
        public void TestAjouterAvecDifferentDeliminateur_DoitRenvoyerErreur()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "//|\n1|2,3";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { calcul.ajouter(nombre); });
            Assert.That(erreur.Message, Is.EqualTo("'|' attendu mais ',' trouv�"));
        }
        [Test]
        public void TestAjouterAvecNombreNegatif_DoitRenvoyerErreur()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "1,-2";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { calcul.ajouter(nombre); });

            Assert.That(erreur.Message, Is.EqualTo("Nombre(s) n�gatif(s) non autoris�(s) : -2, "));
        }
        [Test]
        public void TestAjouterAvecPlusieursNombreNegatif_DoitRenvoyerErreur()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "2,-4,-9";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { calcul.ajouter(nombre); });

            Assert.That(erreur.Message, Is.EqualTo("Nombre(s) n�gatif(s) non autoris�(s) : -4, -9, "));
        }
        [Test]
        public void TestAjouterAvecPlusieursErreur_DoitRenvoyerToutesLesErreurs()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "//|\n1|2,-3";
            var erreur = Assert.Throws<ArgumentException>(
                delegate { calcul.ajouter(nombre); });

            Assert.That(erreur.Message, Is.EqualTo("'|' attendu mais ',' trouv�\nNombre(s) n�gatif(s) non autoris�(s) : -3, "));
        }
        [Test]
        public void TestAjouterAvecNombreTropHaut_DoitRenvoyer2()
        {
            Calculatrice calcul = new Calculatrice();
            string nombre = "2;1001";
            Assert.That(calcul.ajouter(nombre), Is.EqualTo(2));
        }
    }
}