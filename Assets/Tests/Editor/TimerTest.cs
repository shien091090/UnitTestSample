using Moq;
using NUnit.Framework;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    private string timerText;
    private string buttonStateText;
    private Mock<ITimerView> timerViewMock;
    private TimerManager timerManager;

    [SetUp]
    public void Setup()
    {
        Init();
    }

    [Test]
    public void click_button_not_yet()
    {
        Assert.AreEqual(TimerState.Start, timerManager.CurrentTimerState);
        Assert.AreEqual("0.0", timerText);
        Assert.AreEqual("O", buttonStateText);
        timerViewMock.Verify(x => x.SetTimerText(It.IsAny<string>()), Times.Exactly(1));
        timerViewMock.Verify(x => x.SetButtonStateText(It.IsAny<string>()), Times.Exactly(1));
    }

    [Test]
    public void first_click_button()
    {
        timerManager.OnClickButton();

        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        Assert.AreEqual(">", buttonStateText);
        timerViewMock.Verify(x => x.SetTimerText(It.IsAny<string>()), Times.Exactly(1));
        timerViewMock.Verify(x => x.SetButtonStateText(It.IsAny<string>()), Times.Exactly(2));
        timerViewMock.Verify(x => x.StartUpdateTimer(), Times.Exactly(1));
        timerViewMock.Verify(x => x.StopUpdateTimer(), Times.Exactly(0));
    }

    private void Init()
    {
        timerText = string.Empty;
        buttonStateText = string.Empty;
        timerViewMock = new Mock<ITimerView>();
        timerViewMock.Setup(x => x.SetTimerText(It.IsAny<string>())).Callback((string text) =>
        {
            timerText = text;
        });

        timerViewMock.Setup(x => x.SetButtonStateText(It.IsAny<string>())).Callback((string text) =>
        {
            buttonStateText = text;
        });

        timerViewMock.Setup(x => x.StartUpdateTimer()).Callback(() =>
        {
        });

        timerViewMock.Setup(x => x.StopUpdateTimer()).Callback(() =>
        {
        });

        timerManager = new TimerManager(timerViewMock.Object);
    }
}