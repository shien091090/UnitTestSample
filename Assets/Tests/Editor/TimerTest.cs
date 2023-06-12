using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

public class TimerTest
{
    [Test]
    public void click_button_not_yet()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        //Assert 驗證
        Assert.AreEqual(TimerState.Start, timerManager.CurrentTimerState);
        onRefreshButtonStateEvent.DidNotReceive().Invoke(Arg.Any<string>());
    }

    [Test]
    public void first_click_button()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerManager.OnClickButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        onRefreshButtonStateEvent.Received(1).Invoke(Arg.Any<string>());
    }

    [Test]
    public void update_timer_when_playing()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshTimerTextEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshTimerText += onRefreshTimerTextEvent;
        //Act 執行
        timerManager.OnClickButton();
        timerManager.CheckUpdateTimer(1.5f);
        //Assert 驗證
        Assert.AreEqual(1.5f, timerManager.Timer);
        onRefreshTimerTextEvent.Received(1).Invoke("1.5");
    }

    [Test]
    public void update_timer_when_not_playing()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshTimerTextEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshTimerText += onRefreshTimerTextEvent;
        //Act 執行
        timerManager.CheckUpdateTimer(1.5f);
        //Assert 驗證
        Assert.AreEqual(0, timerManager.Timer);
        onRefreshTimerTextEvent.DidNotReceive().Invoke(Arg.Any<string>());
    }

    [Test]
    public void click_button_when_playing()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Stop, timerManager.CurrentTimerState);
        onRefreshButtonStateEvent.Received(2).Invoke(Arg.Any<string>());

        string lastButtonStateText = onRefreshButtonStateEvent.ReceivedCalls().Last().GetArguments().GetValue(0) as string;
        Assert.AreEqual("=", lastButtonStateText);
    }

    [Test]
    public void click_button_when_stop()
    {
        //Arrange 準備
        TimerManager timerManager = new TimerManager();
        Action<string> onRefreshButtonStateEvent = Substitute.For<Action<string>>();
        timerManager.OnRefreshButtonStateText += onRefreshButtonStateEvent;
        //Act 執行
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        timerManager.OnClickButton();
        //Assert 驗證
        Assert.AreEqual(TimerState.Play, timerManager.CurrentTimerState);
        onRefreshButtonStateEvent.Received(3).Invoke(Arg.Any<string>());

        string lastButtonStateText = onRefreshButtonStateEvent.ReceivedCalls().Last().GetArguments().GetValue(0) as string;
        Assert.AreEqual(">", lastButtonStateText);
    }
}