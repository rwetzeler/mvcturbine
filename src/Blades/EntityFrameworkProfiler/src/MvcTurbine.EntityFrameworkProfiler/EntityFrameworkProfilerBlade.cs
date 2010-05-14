namespace MvcTurbine.EntityFrameworkProfiler
{
	using Blades;

	public class EntityFrameworkProfilerBlade : Blade
	{
		#region Overrides of Blade

		/// <summary>
		/// Executes the current blade.
		/// </summary>
		public override void Spin(IRotorContext context)
		{
			HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
		}

		#endregion
	}
}
