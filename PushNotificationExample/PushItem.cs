namespace PushNotificationExample;

public class PushItem
{
    private string token;
    private string title;
    private string message;

    public PushItem(string token, string title, string message)
    {
        this.token = token;
        this.title = title;
        this.message = message;
    }

    public string Token
    {
        get => token;
        set => token = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Message
    {
        get => message;
        set => message = value ?? throw new ArgumentNullException(nameof(value));
    }
}