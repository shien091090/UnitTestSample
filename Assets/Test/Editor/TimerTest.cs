using Moq;
using NUnit.Framework;

public class TimerTest
{
    [Test]
    public void first_click_button()
    {
        string buttonText = string.Empty;
        string timerText = string.Empty;

        Mock<ITimerView> timerViewMock = new Mock<ITimerView>();
        timerViewMock.Setup(x => x.SetButtonText(It.IsAny<string>())).Callback((string text) =>
        {
            buttonText = text;
        });

        timerViewMock.Setup(x => x.SetTimerDisplay(It.IsAny<string>())).Callback((string text) =>
        {
            timerText = text;
        });

        TimerManager timerManager = new TimerManager(timerViewMock.Object);

        //一開始按鈕狀態為Start
        Assert.AreEqual(TimerState.Start, timerManager.CurrentTimerState);
        timerViewMock.Verify(x=>x.SetButtonText(It.IsAny<string>()), Times.Exactly(1));
        Assert.AreEqual("O", buttonText);
        timerViewMock.Verify(x=>x.SetTimerDisplay(It.IsAny<string>()), Times.Exactly(1));
        Assert.AreEqual("0.0", timerText);

        //點按鈕 
        timerManager.OnClickButton();

        //狀態變為Play
        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        timerViewMock.Verify(x=>x.SetButtonText(It.IsAny<string>()), Times.Exactly(2));
        Assert.AreEqual(">", buttonText);

        //更新Timter
        timerManager.CheckRefreshTimer(1.5f);

        //時間顯示數字增加
        Assert.AreEqual(1.5f, timerManager.Timer);
        timerViewMock.Verify(x=>x.SetTimerDisplay(It.IsAny<string>()), Times.Exactly(2));
        Assert.AreEqual("1.5", timerText);
    }

    [Test]
    public void click_button_when_playing()
    {
    }

    [Test]
    public void click_button_when_stopped()
    {
    }
}