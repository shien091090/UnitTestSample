using Moq;
using NUnit.Framework;

public class TimerTest
{
    private string buttonText;
    private string timerText;
    private TimerManager timerManager;
    private Mock<ITimerView> timerViewMock;

    [SetUp]
    public void Setup()
    {
        Init();
    }

    [Test]
    public void click_button_not_yet()
    {
        //一開始按鈕狀態為Start
        Assert.AreEqual(TimerState.Start, timerManager.CurrentTimerState);
        timerViewMock.Verify(x => x.SetButtonText(It.IsAny<string>()), Times.Exactly(1));
        Assert.AreEqual("O", buttonText);
        timerViewMock.Verify(x => x.SetTimerDisplay(It.IsAny<string>()), Times.Exactly(1));
        Assert.AreEqual("0.0", timerText);
    }

    [Test]
    public void update_timer()
    {
        timerManager.OnClickButton();

        //更新Timter
        timerManager.CheckRefreshTimer(1.5f);

        //時間顯示數字增加
        Assert.AreEqual(1.5f, timerManager.Timer);
        timerViewMock.Verify(x => x.SetTimerDisplay(It.IsAny<string>()), Times.Exactly(2));
        Assert.AreEqual("1.5", timerText);
    }

    [Test]
    public void first_click_button()
    {
        //點按鈕 
        timerManager.OnClickButton();

        //狀態變為Play
        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        timerViewMock.Verify(x => x.SetButtonText(It.IsAny<string>()), Times.Exactly(2));
        Assert.AreEqual(">", buttonText);
    }

    [Test]
    public void click_button_when_playing()
    {
        //點按鈕兩次
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        
        //狀態變為Stop
        Assert.AreEqual(TimerState.Stop, timerManager.CurrentTimerState);
        timerViewMock.Verify(x => x.SetButtonText(It.IsAny<string>()), Times.Exactly(3));
        Assert.AreEqual("=", buttonText);
    }

    [Test]
    public void click_button_when_stopped()
    {
        //點按鈕三次
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        
        //狀態變為Play
        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        timerViewMock.Verify(x => x.SetButtonText(It.IsAny<string>()), Times.Exactly(4));
        Assert.AreEqual(">", buttonText);
    }

    private void Init()
    {
        buttonText = string.Empty;
        timerText = string.Empty;

        timerViewMock = new Mock<ITimerView>();
        timerViewMock.Setup(x => x.SetButtonText(It.IsAny<string>())).Callback((string text) =>
        {
            buttonText = text;
        });

        timerViewMock.Setup(x => x.SetTimerDisplay(It.IsAny<string>())).Callback((string text) =>
        {
            timerText = text;
        });

        timerManager = new TimerManager(timerViewMock.Object);
    }
}