using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HiLaarIsch
{
    public interface IMiddleWare
    {
        Task Invoke(IOwinContext context, Func<Task> next);
    }
}