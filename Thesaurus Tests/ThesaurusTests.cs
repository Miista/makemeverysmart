using System.Linq;
using NUnit.Framework;
using Thesaurus;

namespace Thesaurus_Tests
{
    [TestFixture]
    public class ThesaurusTests
    {
        [Test]
        public void GetUsages_ValidWord_CorrectCount()
        {
            var thesaurus = new Thesaurus.Thesaurus();
            var usages = thesaurus.GetUsages( "smart" );

            Assert.AreEqual( 4, usages.Count );
        }

        [Test]
        public void GetUsages_ValidWord_CorrectUsages()
        {
            var thesaurus = new Thesaurus.Thesaurus();
            var usages = thesaurus.GetUsages( "smart" );

            Assert.IsTrue( usages.Any( w => w.Text.ToLower() == "intelligent" ) );
            Assert.IsTrue( usages.Any( w => w.Text.ToLower() == "stylish, fashionable" ) );
            Assert.IsTrue( usages.Any( w => w.Text.ToLower() == "brisk, lively" ) );
            Assert.IsTrue( usages.Any( w => w.Text.ToLower() == "hurt, pain" ) );
        }

        [Test]
        public void GetUsages_ValidWord_CorrectWordType()
        {
            var thesaurus = new Thesaurus.Thesaurus();
            var usages = thesaurus.GetUsages( "smart" );

            Assert.AreEqual( WordType.Adjective, usages[0].Type );
            Assert.AreEqual( WordType.Adjective, usages[1].Type );
            Assert.AreEqual( WordType.Adjective, usages[2].Type );
            Assert.AreEqual( WordType.Verb, usages[3].Type );
        }

        [Test]
        public void GetUsages_ValidWord_CorrectSynonymCount()
        {
            var thesaurus = new Thesaurus.Thesaurus();
            var usages = thesaurus.GetUsages( "smart" );

            Assert.AreEqual( 40, usages[0].Synonyms.Count );
            Assert.AreEqual( 21, usages[1].Synonyms.Count );
            Assert.AreEqual( 17, usages[2].Synonyms.Count );
            Assert.AreEqual( 10, usages[3].Synonyms.Count );
        }

        [Test]
        public void GetUsages_ValidWord_CorrectComplexity()
        {
            var thesaurus = new Thesaurus.Thesaurus();
            var usages = thesaurus.GetUsages("smart");

            Assert.AreEqual( 40, usages[0].Synonyms.Count );
            Assert.AreEqual( 18,
                usages[0].Synonyms.Count( w => w.Complexity == WordComplexity.LowComplexity ),
                "Incorrect number of synonyms with low complexity" );
            Assert.AreEqual( 17,
                usages[0].Synonyms.Count( w => w.Complexity == WordComplexity.MediumComplexity ),
                "Incorrect number of synonyms with medium complexity" );
            Assert.AreEqual( 5,
                usages[0].Synonyms.Count( w => w.Complexity == WordComplexity.HighComplexity ),
                "Incorrect number of synonyms with high complexity" );
        }
    }
}
