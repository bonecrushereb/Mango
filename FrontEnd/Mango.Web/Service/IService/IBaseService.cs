using Mango.Web.Models;

namespace Mango.Web;

public interface IBaseService
{
    Task<ResponseDto> SendAsync(RequestDto requestDto)
}
