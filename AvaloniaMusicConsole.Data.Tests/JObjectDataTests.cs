using AvaloniaMusicConsole.Data.Contents;
using System.Diagnostics;

namespace AvaloniaMusicConsole.Data.Tests
{
    public class JObjectDataTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(@"E:\Music")]
        public async Task  FindAndViewDirectory(string url)
        {
            var content = new DirectoryContent(url);

            var data = await content.GetValue();
            
            Assert.That(data, Is.Not.Null);

            Debug.WriteLine(data);
        }

        [Test]
        [TestCase(@"E:\Music\ZZTop")]
        [TestCase(@"E:\Music\ZZTop\1983_Eliminator")]
        public async Task FindAndViewDirectoryValues(string url)
        {
            var content = new DirectoryContent(url);

            await foreach (var value in content.GetValues())
            {
                Assert.That(value, Is.Not.Null);
                Debug.WriteLine(value);
            }
        }
    }
}