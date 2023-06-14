using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class PlayerTest
{
    private ITimerModel timerModelMock;
    private IInputController inputControllerMock;
    private PlayerModel playerModel;
    private Action onRefreshMovableColorEvent;
    private Action onRefreshUnmovableColorEvent;
    private Action<Vector3> onUpdateMovingEvent;

    [SetUp]
    public void Setup()
    {
        timerModelMock = Substitute.For<ITimerModel>();
        inputControllerMock = Substitute.For<IInputController>();
        playerModel = new PlayerModel(timerModelMock, inputControllerMock);

        onRefreshMovableColorEvent = Substitute.For<Action>();
        playerModel.OnRefreshMovableColor += onRefreshMovableColorEvent;

        onRefreshUnmovableColorEvent = Substitute.For<Action>();
        playerModel.OnRefreshUnmovableColor += onRefreshUnmovableColorEvent;

        onUpdateMovingEvent = Substitute.For<Action<Vector3>>();
        playerModel.OnUpdateMoving += onUpdateMovingEvent;
    }

    //Arrange
    //Act
    //Assert

    [Test]
    public void player_init()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Start);

        //Act
        playerModel.Init();

        //Assert
        onRefreshMovableColorEvent.DidNotReceive().Invoke();
        onRefreshUnmovableColorEvent.Received(1).Invoke();
    }

    [Test]
    public void input_move_key_but_timer_not_playing()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Start);
        inputControllerMock.GetAxis("Horizontal").Returns(0.6f);
        inputControllerMock.GetAxis("Vertical").Returns(0.6f);

        //Act
        playerModel.Init();
        playerModel.CheckUpdateMoving(1, 2);

        //Assert
        onUpdateMovingEvent.DidNotReceive().Invoke(Arg.Any<Vector3>());
    }

    [Test]
    public void move_right()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Play);
        inputControllerMock.GetAxis("Horizontal").Returns(0.6f);

        //Act
        playerModel.Init();
        playerModel.CheckUpdateMoving(1, 2);

        //Assert
        onUpdateMovingEvent.Received(1).Invoke(Arg.Is<Vector3>(vector3 => vector3.x > 0 && vector3.y == 0));
    }

    [Test]
    public void move_down()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Play);
        inputControllerMock.GetAxis("Vertical").Returns(-0.6f);

        //Act
        playerModel.Init();
        playerModel.CheckUpdateMoving(1, 2);

        //Assert
        onUpdateMovingEvent.Received(1).Invoke(Arg.Is<Vector3>(vector3 => vector3.x == 0 && vector3.y < 0));
    }

    [Test]
    public void move_left_and_up()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Play);
        inputControllerMock.GetAxis("Vertical").Returns(0.6f);
        inputControllerMock.GetAxis("Horizontal").Returns(-0.6f);

        //Act
        playerModel.Init();
        playerModel.CheckUpdateMoving(1, 2);

        //Assert
        onUpdateMovingEvent.Received(1).Invoke(Arg.Is<Vector3>(vector3 => vector3.x < 0 && vector3.y > 0));
    }

    [Test]
    public void no_move()
    {
        //Arrange
        timerModelMock.CurrentTimerState.Returns(TimerState.Play);
        inputControllerMock.GetAxis("Vertical").Returns(0);
        inputControllerMock.GetAxis("Horizontal").Returns(0);

        //Act
        playerModel.Init();
        playerModel.CheckUpdateMoving(1, 2);

        //Assert
        onUpdateMovingEvent.Received(1).Invoke(Arg.Is<Vector3>(vector3 => vector3.x == 0 && vector3.y == 0));
    }
}