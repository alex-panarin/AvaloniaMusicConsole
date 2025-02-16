using AvaloniaMusicConsole.Data;
using AvaloniaMusicConsole.Repositories;
using System.Diagnostics;

namespace AvaloniaMusicConsole.Tests
{
    public class ModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(@"E:\Music")]
        [TestCase(@"E:\Music\ZZtop")]
        public async Task GetModelsFromContent(string path)
        {

            var provider = new LocalContentProvider();
            var repository = new DataRepository(provider);

            await foreach(var model in repository.GetModels(path))
            {
                Assert.NotNull(model);

                Debug.WriteLine(model);
                //Assert.Pass(model.ToString());
            }
            Assert.Pass();
        }
    }
}