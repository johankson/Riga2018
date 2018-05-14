using System;
using System.Threading.Tasks;

namespace LeetPhotos.Core.Net
{
	public interface IRestClient
    {
		Task<T> Get<T>(string url);
    }
}
