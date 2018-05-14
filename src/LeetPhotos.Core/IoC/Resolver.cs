using System;
namespace LeetPhotos.Core.IoC
{
	public static class Resolver
    {
		private static IResolver _resolver;

        public static void SetResolver(IResolver resolver)
        {
            _resolver = resolver;
        }
  
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }
    }
}
