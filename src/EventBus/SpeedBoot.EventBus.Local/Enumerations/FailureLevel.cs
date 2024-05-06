namespace SpeedBoot.EventBus.Local;

public enum FailureLevel
{
    Throw = 1,

    ThrowAndCancel,

    Ignore
}
