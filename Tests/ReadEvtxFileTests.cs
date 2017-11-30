using EvtxImporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ReadEvtxFileTests
    {
        [TestMethod]
        public void TestFileRead()
        {
            var filePath = @"C:\Data\Event Logs\20171002.evtx";

            var model = new ReadEvtxFile(filePath).Go();

            Assert.IsTrue(model.Count > 0);
        }
    }
}
