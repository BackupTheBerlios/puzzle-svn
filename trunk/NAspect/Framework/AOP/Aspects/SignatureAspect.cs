using System;
using System.Collections;
using System.Reflection.Emit;
using Puzzle.NAspect.Framework.Tools;

namespace Puzzle.NAspect.Framework.Aop
{
	/// <summary>
	/// Summary description for Aspect.
	/// </summary>
	public class SignatureAspect : AspectBase
	{
		public string TargetTypeSignature;

		#region Aspect

		public SignatureAspect(string Name, string TargetName, IList mixins, IList pointcuts)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetName;
			this.Mixins = mixins;
			this.Pointcuts = pointcuts;

		}

		#endregion

		#region Aspect

		public SignatureAspect(string Name, string TargetName, Type[] mixins, IPointcut[] pointcuts)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetName;
			this.Mixins = new ArrayList(mixins);
			this.Pointcuts = new ArrayList(pointcuts);
		}

		#endregion

		#region Aspect

		public SignatureAspect(string Name, string TargetName, string TargetMethodsignature, IInterceptor Interceptor)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetName;
			this.Pointcuts.Add(new SignaturePointcut(TargetMethodsignature, Interceptor));
		}

		#endregion

		#region Aspect

		public SignatureAspect(string Name, Type TargetType, IList mixins, IList pointcuts)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Mixins = mixins;
			this.Pointcuts = pointcuts;
		}

		#endregion

		#region Aspect

		public SignatureAspect(string Name, Type TargetType, Type[] mixins, IPointcut[] pointcuts)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Mixins = new ArrayList(mixins);
			this.Pointcuts = new ArrayList(pointcuts);
		}

		#endregion

		#region Aspect

		public SignatureAspect(string Name, Type TargetType, string TargetMethodsignature, IInterceptor Interceptor)
		{
			this.Name = Name;
			this.TargetTypeSignature = TargetType.FullName;
			this.Pointcuts.Add(new SignaturePointcut(TargetMethodsignature, Interceptor));
		}

		#endregion

		public override bool IsMatch(Type type)
		{
			Type tmp = type;
			//traverse back in inheritance hierarchy to first non runtime emitted type 
			while (tmp.Assembly is AssemblyBuilder)
				tmp = tmp.BaseType;


			if (Text.IsMatch(tmp.FullName, TargetTypeSignature))
				return true;
			else
				return false;
		}
	}
}