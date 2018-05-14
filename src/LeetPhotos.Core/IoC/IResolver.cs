using System;
using System.Reflection;
namespace LeetPhotos.Core.IoC
{
	public interface IResolver
    {
		T Resolve<T>();
    }
}
