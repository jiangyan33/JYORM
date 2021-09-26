using System;

namespace JYORMApi.Model
{
    public class CoreException : Exception
    {
        public int Code { get; set; }

        public CoreException(string msg = "", Exception innserException = null) : base(msg, innserException)
        {
        }
    }

    public class CoreCommonException : CoreException
    {
        public CoreCommonException(string msg, ResultCode code = ResultCode.UnknownError) : base(msg)
        {
            Code = (int)code;
        }
    }

    public class CoreRuntimeException : CoreException
    {
        public CoreRuntimeException(string msg, int code = 10004) : base(msg)
        {
            Code = code;
        }
    }

    public class CoreAuthException : CoreException
    {
        public CoreAuthException(string msg, int code = 10005) : base(msg)
        {
            Code = code;
        }
    }
}