using DotNetEnv;

namespace fosku_server.Helpers;

public static class AuthSettings
{
    public static string JWTPrivateKey { get; }
    static AuthSettings()
    {
        DotNetEnv.Env.Load();
        JWTPrivateKey = Env.GetString("JWT_SECRET_KEY");
    }
}