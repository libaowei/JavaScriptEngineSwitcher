﻿#if !NET452
namespace JavaScriptEngineSwitcher.Tests.Node
{
	public class EvalTests : EvalTestsBase
	{
		protected override string EngineName
		{
			get { return "NodeJsEngine"; }
		}
	}
}
#endif