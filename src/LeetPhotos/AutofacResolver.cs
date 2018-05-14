using System;
using LeetPhotos.Core.IoC;
using Autofac.Core;
using Autofac;

namespace LeetPhotos
{
	public class AutofacResolver : IResolver
    {
		private IContainer container;

		public AutofacResolver(IContainer container)
        {
			this.container = container;
        }

		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}
  
	}
}
