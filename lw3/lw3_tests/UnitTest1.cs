using Microsoft.VisualStudio.TestTools.UnitTesting;
using lw3;

namespace lw3_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNewTv_WithTurnedOn_TurnStatusIsTrueChannelIsOne()
        {
            CTVSet tv = new CTVSet();

            tv.TurnOn();

            Assert.IsTrue(tv.GetTurnStatus());
            Assert.AreEqual(1, tv.GetChannel());
        }

        [TestMethod]
        public void CreateNewTv_WithTurnedOnAndTurnedOff_TurnStatusIsFalseChannelIsZero()
        {
            CTVSet tv = new CTVSet();

            tv.TurnOn();
            tv.TurnOff();

            Assert.IsFalse(tv.GetTurnStatus());
            Assert.AreEqual(0, tv.GetChannel());
        }

        [TestMethod]
        public void SelectChannel_WithOffTv_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();
            int channel = 3;

            tv.SelectChannel(channel);

            Assert.AreEqual(0, tv.GetChannel());
        }

        [TestMethod]
        public void SelectChannel_OutOfRange_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            int channelOutOfRange = 0;

            tv.TurnOn();
            tv.SelectChannel(channelOutOfRange);

            Assert.AreEqual(1, tv.GetChannel());
        }

        [TestMethod]
        public void SelectChannel_InRange_ChannelIsSelectedValue()
        {
            CTVSet tv = new CTVSet();
            int channelInRange = 50;
            int expectedChannel = 50;

            tv.TurnOn();
            tv.SelectChannel(channelInRange);

            Assert.AreEqual(expectedChannel, tv.GetChannel());
        }

        [TestMethod]
        public void SelectChannel_BorderValue_ChannelIsBorderValue()
        {
            CTVSet tv = new CTVSet();
            int channelBorderValue = 1;
            int expectedChannel = 1;

            tv.TurnOn();
            tv.SelectChannel(channelBorderValue);

            Assert.AreEqual(expectedChannel, tv.GetChannel());
        }

        [TestMethod]
        public void SelectChannel_SaveSelectedChannelAfterOnAgain_ChannelIsSelectedChannelBeforeOff()
        {
            CTVSet tv = new CTVSet();
            int channel = 4;
            int expectedChannel = 4;

            tv.TurnOn();
            tv.SelectChannel(channel);
            tv.TurnOff();
            tv.TurnOn();

            Assert.AreEqual(expectedChannel, tv.GetChannel());
        }

        [TestMethod]
        public void SelectPreviousChannel_WithOffTv_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();

            tv.SelectPreviousChannel();

            Assert.AreEqual(0, tv.GetChannel());
        }

        [TestMethod]
        public void SelectPreviousChannel_WithoutSelectedChannel_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();

            tv.TurnOn();
            tv.SelectPreviousChannel();

            Assert.AreEqual(1, tv.GetChannel());
        }

        [TestMethod]
        public void SelectPreviousChannel_WithSelectedChannelOnce_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            int channel = 4;

            tv.TurnOn();
            tv.SelectChannel(channel);
            tv.SelectPreviousChannel();

            Assert.AreEqual(1, tv.GetChannel());
        }
        
        [TestMethod]
        public void SelectPreviousChannel_WithSeveralSelectedChannels_ChannelIsPrevious()
        {
            CTVSet tv = new CTVSet();
            int channel1 = 4;
            int channel2 = 60;

            tv.TurnOn();
            tv.SelectChannel(channel1);
            tv.SelectChannel(channel2);
            tv.SelectPreviousChannel();

            Assert.AreEqual(channel1, tv.GetChannel());
        }

        [TestMethod]
        public void SelectPreviousChannel_SavePreviousChannelAfterOnAgain_ChannelIsPreviousBeforeOff()
        {
            CTVSet tv = new CTVSet();
            int channel1 = 4;
            int channel2 = 60;

            tv.TurnOn();
            tv.SelectChannel(channel1);
            tv.SelectChannel(channel2);
            tv.TurnOff();
            tv.TurnOn();
            tv.SelectPreviousChannel();

            Assert.AreEqual(channel1, tv.GetChannel());
        }
    }
}
