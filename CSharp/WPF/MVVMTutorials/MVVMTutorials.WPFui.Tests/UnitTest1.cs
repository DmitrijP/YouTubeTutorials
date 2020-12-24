using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MVVMTutorials.WPFui.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var m = new Messenger();
            var vm1 = new VM1(m);
            var vm3 = new VM3(m);

            vm1.SendMessageToVM3();
        }
    }
}
