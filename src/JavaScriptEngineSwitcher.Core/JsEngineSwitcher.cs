﻿using System;

using JavaScriptEngineSwitcher.Core.Resources;

namespace JavaScriptEngineSwitcher.Core
{
	/// <summary>
	/// JS engine switcher
	/// </summary>
	public sealed class JsEngineSwitcher : IJsEngineSwitcher
	{
		/// <summary>
		/// Default instance of JS engine switcher
		/// </summary>
		private static readonly Lazy<IJsEngineSwitcher> _default
			= new Lazy<IJsEngineSwitcher>(() => new JsEngineSwitcher());

		/// <summary>
		/// Current instance of JS engine switcher
		/// </summary>
		private static IJsEngineSwitcher _current;

		/// <summary>
		/// Gets or sets a instance of JS engine switcher
		/// </summary>
		public static IJsEngineSwitcher Current
		{
			get
			{
				return _current ?? _default.Value;
			}
			set
			{
				_current = value;
			}
		}

		/// <summary>
		/// Gets a instance of JS engine switcher
		/// </summary>
		[Obsolete("Use a `Current` property")]
		public static IJsEngineSwitcher Instance
		{
			get { return Current; }
		}


		/// <summary>
		/// Constructs an instance of JS engine switcher
		/// </summary>
		public JsEngineSwitcher()
			: this(new JsEngineFactoryCollection())
		{ }

		/// <summary>
		/// Constructs an instance of JS engine switcher
		/// </summary>
		public JsEngineSwitcher(JsEngineFactoryCollection engineFactories)
			: this(engineFactories, string.Empty)
		{ }

		/// <summary>
		/// Constructs an instance of JS engine switcher
		/// </summary>
		public JsEngineSwitcher(JsEngineFactoryCollection engineFactories, string defaultEngineName)
		{
			EngineFactories = engineFactories;
			DefaultEngineName = defaultEngineName;
		}


		#region IJsEngineSwitcher implementation

		/// <inheritdoc/>
		public string DefaultEngineName
		{
			get;
			set;
		}

		/// <inheritdoc/>
		public JsEngineFactoryCollection EngineFactories
		{
			get;
			private set;
		}


		/// <inheritdoc/>
		public IJsEngine CreateEngine(string name)
		{
			IJsEngine engine;
			IJsEngineFactory engineFactory = EngineFactories.Get(name);

			if (engineFactory != null)
			{
				engine = engineFactory.CreateEngine();
			}
			else
			{
				throw new JsEngineNotFoundException(
					string.Format(Strings.Configuration_JsEngineFactoryNotFound, name));
			}

			return engine;
		}

		/// <inheritdoc/>
		public IJsEngine CreateDefaultEngine()
		{
			string defaultJsEngineName = DefaultEngineName;
			if (string.IsNullOrWhiteSpace(defaultJsEngineName))
			{
				throw new InvalidOperationException(Strings.Configuration_DefaultJsEngineNameNotSpecified);
			}

			IJsEngine jsEngine = CreateEngine(defaultJsEngineName);

			return jsEngine;
		}

		#endregion
	}
}