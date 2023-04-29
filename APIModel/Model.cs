namespace APIModel;

public class BaseResponse
{
    public string? Message { get; set; }
    public bool Error { get; set; }
}

public class LoginResponse : BaseResponse
{
    public string? Token { get; set; }
}

public class ContentResponse : BaseResponse
{
    public string? Content { get; set; }
    public string? Name { get; set; }
}

public class LockContentResponse : ContentResponse
{
    public bool CanModifyContent { get; set; }
}

// -----------------

public class BaseRequest
{
    public string? Token { get; set; }
}

public class LoginRequest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class ContentRequest : BaseRequest
{
    public string? Id { get; set; }
    public bool AsBinary { get; set; }
}

public class LockContentRequest : ContentRequest
{
    public bool RequireLock { get; set; }
}
