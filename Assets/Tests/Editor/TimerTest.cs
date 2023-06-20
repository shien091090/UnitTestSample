using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

public class TimerTest
{
    private TimerModel timerModel;

    [SetUp]
    public void Setup()
    {
        timerModel = new TimerModel();
    }

    [Test]
    public void click_button_not_yet()
    {
        //Arrange 準備
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshMainButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerModel.Init();
        //Assert 驗證
        Assert.AreEqual(TimerState.Start, timerModel.CurrentTimerState);
        onRefreshButtonStateEvent.Received(1).Invoke("O");
    }

    [Test]
    public void first_click_button()
    {
        //Arrange 準備
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshMainButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerModel.Init();
        timerModel.OnClickMainButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Play, timerModel.CurrentTimerState);
        onRefreshButtonStateEvent.Received(2).Invoke(Arg.Any<string>());
        string lastButtonStateText = onRefreshButtonStateEvent.ReceivedCalls().Last().GetArguments().GetValue(0) as string;
        Assert.AreEqual(">", lastButtonStateText);
    }

    [Test]
    public void update_timer_when_playing()
    {
        //Arrange 準備
        Action<string> onRefreshTimerTextEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshTimerText += onRefreshTimerTextEvent;
        //Act 執行
        timerModel.Init();
        timerModel.OnClickMainButton();
        timerModel.CheckUpdateTimer(1.5f);
        //Assert 驗證
        Assert.AreEqual(1.5f, timerModel.Timer);
        onRefreshTimerTextEvent.Received(1).Invoke("1.5");
    }

    [Test]
    public void update_timer_when_not_playing()
    {
        //Arrange 準備
        Action<string> onRefreshTimerTextEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshTimerText += onRefreshTimerTextEvent;
        //Act 執行
        timerModel.Init();
        timerModel.CheckUpdateTimer(1.5f);
        //Assert 驗證
        Assert.AreEqual(0, timerModel.Timer);
        onRefreshTimerTextEvent.DidNotReceive().Invoke(Arg.Any<string>());
    }

    [Test]
    public void click_button_when_playing()
    {
        //Arrange 準備
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshMainButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerModel.Init();
        timerModel.OnClickMainButton();
        timerModel.OnClickMainButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Pause, timerModel.CurrentTimerState);
        onRefreshButtonStateEvent.Received(3).Invoke(Arg.Any<string>());

        string lastButtonStateText = onRefreshButtonStateEvent.ReceivedCalls().Last().GetArguments().GetValue(0) as string;
        Assert.AreEqual("=", lastButtonStateText);
    }

    [Test]
    public void click_button_when_stop()
    {
        //Arrange 準備
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerModel.OnRefreshMainButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerModel.Init();
        timerModel.OnClickMainButton();
        timerModel.OnClickMainButton();
        timerModel.OnClickMainButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Play, timerModel.CurrentTimerState);
        onRefreshButtonStateEvent.Received(4).Invoke(Arg.Any<string>());

        string lastButtonStateText = onRefreshButtonStateEvent.ReceivedCalls().Last().GetArguments().GetValue(0) as string;
        Assert.AreEqual(">", lastButtonStateText);
    }

    [Test]
    public void hide_stop_button_at_start()
    {
        //Arrange
        Action<bool> onRefreshStopButtonActive = Substitute.For<Action<bool>>();
        timerModel.OnRefreshStopButtonActive += onRefreshStopButtonActive;

        //Act
        timerModel.Init();

        //Assert
        onRefreshStopButtonActive.Received(1).Invoke(false);
    }

    [Test]
    public void show_stop_button_when_playing()
    {
        //Arrange
        Action<bool> onRefreshStopButtonActive = Substitute.For<Action<bool>>();
        timerModel.OnRefreshStopButtonActive += onRefreshStopButtonActive;

        //Act
        timerModel.Init();
        timerModel.OnClickMainButton();

        //Assert
        bool isActive = (bool)onRefreshStopButtonActive.ReceivedCalls().Last().GetArguments().GetValue(0);
        Assert.IsTrue(isActive);
    }

    [Test]
    public void show_stop_button_when_pausing()
    {
        //Arrange
        Action<bool> onRefreshStopButtonActive = Substitute.For<Action<bool>>();
        timerModel.OnRefreshStopButtonActive += onRefreshStopButtonActive;

        //Act
        timerModel.Init();
        timerModel.OnClickMainButton();
        timerModel.OnClickMainButton();

        //Assert
        bool isActive = (bool)onRefreshStopButtonActive.ReceivedCalls().Last().GetArguments().GetValue(0);
        Assert.IsTrue(isActive);
    }
}