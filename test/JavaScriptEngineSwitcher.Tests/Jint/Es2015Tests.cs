﻿#if !NET452
namespace JavaScriptEngineSwitcher.Tests.Jint
{
	public class Es2015Tests : Es2015TestsBase
	{
		protected override string EngineName
		{
			get { return "JintJsEngine"; }
		}
	}
}
#endif